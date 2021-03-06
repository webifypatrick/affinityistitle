using System;
using System.Web;
using System.Configuration;
using System.Collections;
using System.Threading;
using System.Collections.Specialized;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Xml;
using Com.VerySimple.Phreeze;

namespace Affinity
{
    /// <summary>
    /// Summary description for Global
    /// </summary>
    /// 
    public class Global : HttpApplication
    {
        public Global()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        void Application_Start(object sender, EventArgs e)
        {
            // configure the logger
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(Server.MapPath("log.config.xml")));
            log4net.LogManager.GetLogger("Application_Start").Info("Application Started");


            // load all the systems settings from the database
            Phreezer p = new Phreezer(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString);
            Affinity.SystemSetting ss = new Affinity.SystemSetting(p);
            ss.Load(Affinity.SystemSetting.DefaultCode);
            Application[Affinity.SystemSetting.DefaultCode] = ss.Settings;
            p.Close();

            Application["ACTIVE"] = new Hashtable();
            SetSchedule();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
            log4net.LogManager.GetLogger("Application_End").Info("Application Ended");
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

            System.Web.HttpContext context = HttpContext.Current;
            System.Exception ex = context.Server.GetLastError();

            string url = context.Request.Url.PathAndQuery;
            string logMsg = ex.InnerException.StackTrace.Replace("\r\n", "<br>");
            string msg = ex.InnerException.Message;

            log4net.LogManager.GetLogger("Application_Error").Error(url + " {" + logMsg.Replace("\n", " ") + "}");

            context.Server.ClearError();

            // compile errors in PageBase or MasterPage can cause an infinite loop
            if (!Request.Path.Contains("Error.aspx"))
            {
                Response.Redirect("Error.aspx?code=999&feedback=" + Server.UrlEncode("" + url + " " + msg + ""));
            }
            else
            {
                // TODO: this is ugly - but Error.aspx is unable to render due to a compilation error
                Response.Write("<html>\r\n");
                Response.Write("<h4>A Fatal Error Has Occured</h4>\r\n");
                Response.Write("<p>Technical Details:</p>\r\n");
                Response.Write("<textarea cols='80' rows='20'>");
                Response.Write(msg);
                Response.Write("</textarea>\r\n");
                Response.Write("</html>\r\n");
            }

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }



        // Sets a timer to call a method when the timer runs out
        public static void SetTimer(ScheduledEvents.TimerDelegate func, long repeatTime, bool skipRepeat, string id)
        {
            if (timers.Contains(id))
            {
                return;
            }
            TimerCallback timerDelegate = new TimerCallback(func);

            long repeat = (skipRepeat) ? Timeout.Infinite : repeatTime;
            Timer t = new Timer(timerDelegate, id, repeatTime, repeat);
            if (repeatTime != 0) return;
            // this lock was put in place to prevent two timers with the same key

            timers.Add(id, t);
        }


        public static void SetSchedule()
        {
            Com.VerySimple.Phreeze.Phreezer phreeze = new Com.VerySimple.Phreeze.Phreezer(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString);
            // run sql that checks for any search package where the first notification is null or second notification is null and no agent examination has been completed
            log4net.LogManager.GetLogger("SetSchedule").Info("Start");

            using (MySqlDataReader reader = phreeze.ExecuteReader("select s.s_id, s.a_id, s.o_id, s.r_id, s.att_id, s.ua_id, s.s_first_notification, s.s_second_notification, s.s_search_package_date, s.s_created, s.s_modified, orig.a_id, orig.a_username, orig.a_first_name, orig.a_last_name, orig.a_email, o.o_customer_id, o.o_client_name, o.o_id, o.o_internal_id, r.r_request_type_code from `schedule` s inner join `account` a on a.a_id = s.a_id and a.a_underwriter_codes not like '%dir-%' inner join `order` o on o.o_id = s.o_id inner join `request` r on r.r_id = s.r_id join account orig on orig.a_id = o.o_originator_id left join attachment att on att.att_request_id = r.r_id and (att.att_purpose_code = 'Committment' or att.att_purpose_code = 'ExamSheet') left join upload_log ul on ul.att_id = att.att_id and ul.ua_id <> o.o_originator_id where s.s_search_package_date is not null and att.att_request_id is null and ul.ul_id is null and (s.s_first_notification is null or s.s_second_notification is null) group by s.s_id, s.a_id, s.o_id, s.r_id, s.att_id, s.ua_id, s.s_first_notification, s.s_second_notification, s.s_search_package_date, s.s_created, s.s_modified, orig.a_id, orig.a_username, orig.a_first_name, orig.a_last_name, orig.a_email, o.o_customer_id, o.o_client_name, o.o_id, o.o_internal_id, r.r_request_type_code"))
            {
                while (reader.Read())
                {
                    string id = reader["s_id"].ToString();
                    ScheduledEvents.TimerDelegate p = Scheduler;
                    DateTime nw = DateTime.Now;
                    bool isFirstNotification = reader.IsDBNull(6);

                    // actual date set in database
                    DateTime dt = (DateTime)reader["s_search_package_date"];
                    //dt = dt.AddHours((isFirstNotification) ? 72 : 168);
                    dt = (isFirstNotification) ? AddWorkDays(dt, 3) : AddWorkDays(dt, 7);

                    TimeSpan ts = dt.Subtract(nw);
                    if (ts.TotalMilliseconds < 0)
                    {
                        log4net.LogManager.GetLogger("SetSchedule").Info("Schedule Now Id: " + id);
                        Scheduler(id);
                    }
                    else if (!timers.Contains(id))
                    {
                        log4net.LogManager.GetLogger("SetSchedule").Info("Schedule Later Id: " + id + " in " + ts.TotalMilliseconds.ToString() + " milliseconds");
                        SetTimer(p, ((long)ts.TotalMilliseconds), true, id);
                    }
                }
            }
            phreeze.Close();
            log4net.LogManager.GetLogger("SetSchedule").Info("End");
        }
        public static void Scheduler(object stateInfo)
        {
            Com.VerySimple.Phreeze.Phreezer phreeze1 = new Com.VerySimple.Phreeze.Phreezer(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString);
            Com.VerySimple.Phreeze.Phreezer phreeze2 = new Com.VerySimple.Phreeze.Phreezer(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString);
            string id = stateInfo.ToString();
            log4net.LogManager.GetLogger("Scheduler").Info("Schedule ID: " + id);

            XmlDocument settingsDoc = new XmlDocument();

            using (MySqlDataReader reader = phreeze1.ExecuteReader("SELECT ss_data FROM system_setting where ss_code = 'SYSTEM';"))
            {
                if (reader.Read())
                {
                    settingsDoc.LoadXml(reader["ss_data"].ToString());
                }
            }

            // run sql that checks if the first notification is null or second notification is null and no agent examination has been completed for the given id
            using (MySqlDataReader reader = phreeze1.ExecuteReader("select s.s_id, s.a_id, s.o_id, s.r_id, s.att_id, s.ua_id, s.s_first_notification, s.s_second_notification, s.s_search_package_date, s.s_created, s.s_modified, orig.a_id, orig.a_username, orig.a_first_name, orig.a_last_name, orig.a_email, o.o_customer_id, o.o_client_name, o.o_id, o.o_internal_id, r.r_request_type_code from `schedule` s inner join `account` a on a.a_id = s.a_id and a.a_underwriter_codes not like '%dir-%' inner join `order` o on o.o_id = s.o_id inner join `request` r on r.r_id = s.r_id join account orig on orig.a_id = o.o_originator_id left join attachment att on att.att_request_id = r.r_id and (att.att_purpose_code = 'Committment' or att.att_purpose_code = 'ExamSheet') left join upload_log ul on ul.att_id = att.att_id and ul.ua_id <> o.o_originator_id where s.s_id = " + id + " and s.s_search_package_date is not null and att.att_request_id is null and ul.ul_id is null and (s.s_first_notification is null or s.s_second_notification is null) group by s.s_id, s.a_id, s.o_id, s.r_id, s.att_id, s.ua_id, s.s_first_notification, s.s_second_notification, s.s_search_package_date, s.s_created, s.s_modified, orig.a_id, orig.a_username, orig.a_first_name, orig.a_last_name, orig.a_email, o.o_customer_id, o.o_client_name, o.o_id, o.o_internal_id, r.r_request_type_code"))
            {
                if (reader.Read())
                {
                    bool isFirstNotification = reader.IsDBNull(6);

                    // send email and update first notification or second notification dates
                    if (isFirstNotification)
                    {
                        log4net.LogManager.GetLogger("Scheduler").Info("First Notification: " + reader["s_id"].ToString());
                        phreeze2.ExecuteNonQuery("UPDATE `schedule` SET s_first_notification = NOW(), s_modified = NOW() WHERE s_id = " + reader["s_id"].ToString());

                        string to = reader["a_email"].ToString() + ", title@affinityistitle.com";
                        //to = "webify@att.net, guy@affinityistitle.com";

                        string workingID = (reader["o_internal_id"].ToString().Equals("")) ? "WEB-" + reader["o_id"].ToString() : reader["o_internal_id"].ToString();


                        string url = settingsDoc.SelectSingleNode("//field[@name='RootUrl']").InnerText + "MyOrder.aspx?id=" + reader["o_id"].ToString();
                        string subject = "First Notification for Agent Examination for Affinity Order '" + reader["o_client_name"].ToString().Replace("\r", "").Replace("\n", "") + "' #" + workingID;


                        Com.VerySimple.Email.Mailer mailer = new Com.VerySimple.Email.Mailer(settingsDoc.SelectSingleNode("//field[@name='SmtpHost']").InnerText);

                        string msg = "Dear " + reader["a_first_name"].ToString() + ",\r\n\r\n"
                            + "Your Affinity " + reader["r_request_type_code"].ToString() + " for '" + reader["o_client_name"].ToString() + "', ID #"
                            + workingID + " needs a completed Agent Examination form.\r\n\r\n"
                    + "Friendly: " + reader["o_client_name"].ToString() + "\r\n"
                    + "Tracking Code: " + reader["o_customer_id"].ToString() + "\r\n\r\n"
                            + "You may view the full details of your order anytime online at " + url + ".  "
                            + "If you would prefer to not receive this notification, you may also login "
                            + "to your Affinity account and customize your email notification preferences.\r\n\r\n"
                            + settingsDoc.SelectSingleNode("//field[@name='EmailFooter']").InnerText;
                        //+ "\r\n\r\nThis email would have gone to: " + reader["a_email"].ToString();

                        mailer.Send(
                            settingsDoc.SelectSingleNode("//field[@name='SendFromEmail']").InnerText
                            , to
                            , subject
                            , msg);


                    }
                    else
                    {
                        log4net.LogManager.GetLogger("Scheduler").Info("Second Notification: " + reader["s_id"].ToString());
                        phreeze2.ExecuteNonQuery("UPDATE `schedule` SET s_second_notification = NOW(), s_modified = NOW() WHERE s_id = " + reader["s_id"].ToString());

                        string to = reader["a_email"].ToString() + ", title@affinityistitle.com";
                        //to = "webify@att.net, guy@affinityistitle.com";

                        string workingID = (reader["o_internal_id"].ToString().Equals("")) ? "WEB-" + reader["o_id"].ToString() : reader["o_internal_id"].ToString();


                        string url = settingsDoc.SelectSingleNode("//field[@name='RootUrl']").InnerText + "MyOrder.aspx?id=" + reader["o_id"].ToString();
                        string subject = "Second Notification for Agent Examination for Affinity Order '" + reader["o_client_name"].ToString().Replace("\r", "").Replace("\n", "") + "' #" + workingID;


                        Com.VerySimple.Email.Mailer mailer = new Com.VerySimple.Email.Mailer(settingsDoc.SelectSingleNode("//field[@name='SmtpHost']").InnerText);

                        string msg = "Dear " + reader["a_first_name"].ToString() + ",\r\n\r\n"
                            + "Your Affinity " + reader["r_request_type_code"].ToString() + " for '" + reader["o_client_name"].ToString() + "', ID #"
                            + workingID + " needs a completed Agent Examination form.\r\n\r\n"
                    + "Friendly: " + reader["o_client_name"].ToString() + "\r\n"
                    + "Tracking Code: " + reader["o_customer_id"].ToString() + "\r\n\r\n"
                            + "You may view the full details of your order anytime online at " + url + ".  "
                            + "If you would prefer to not receive this notification, you may also login "
                            + "to your Affinity account and customize your email notification preferences.\r\n\r\n"
                            + settingsDoc.SelectSingleNode("//field[@name='EmailFooter']").InnerText
                            + "\r\n\r\nThis email would have gone to: " + reader["a_email"].ToString();

                        mailer.Send(
                            settingsDoc.SelectSingleNode("//field[@name='SendFromEmail']").InnerText
                            , to
                            , subject
                            , msg);
                    }
                }
            }
            phreeze1.Close();
            phreeze2.Close();
        }

        public static DateTime AddWorkDays(DateTime date, int workingDays)
        {
            int direction = workingDays < 0 ? -1 : 1;
            DateTime newDate = date;
            while (workingDays != 0)
            {
                newDate = newDate.AddDays(direction);

                if (newDate.DayOfWeek != DayOfWeek.Saturday &&
                    newDate.DayOfWeek != DayOfWeek.Sunday &&
                    !isHoliday(newDate))
                {
                    workingDays -= direction;
                }
            }
            return newDate;
        }

        public static bool isHoliday(DateTime date)
        {
            bool ret = false;

            // New Years Day
            if (date.Month == 1 && date.Day == 1)
            {
                ret = true;
            }

            // Memorial Day
            if (date.Month == 5 && date.Day > 24 && date.DayOfWeek == DayOfWeek.Monday)
            {
                ret = true;
            }

            // Independence Day
            if (date.Month == 7 && date.Day == 4)
            {
                ret = true;
            }

            // Labor Day
            if (date.Month == 9 && date.Day < 8 && date.DayOfWeek == DayOfWeek.Monday)
            {
                ret = true;
            }

            // Thanksgiving
            if (date.Month == 11 && date.Day > 21 && date.Day < 29 && date.DayOfWeek == DayOfWeek.Thursday)
            {
                ret = true;
            }

            // Black Friday Day
            if (date.Month == 11 && date.Day > 22 && date.Day < 30 && date.DayOfWeek == DayOfWeek.Friday)
            {
                ret = true;
            }

            // Christmas
            if (date.Month == 12 && date.Day == 25)
            {
                ret = true;
            }
            return ret;
        }


        private static readonly ListDictionary timers = new ListDictionary();
    }

    public static class ScheduledEvents
    {
        public delegate void TimerDelegate(object value);
    }
}
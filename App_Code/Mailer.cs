using System;
using System.Net.Mail;
using System.IO;

namespace Com.VerySimple.Email
{
    /// <summary>
    /// Email class wraps email sending functionality so that general pages do 
    /// not need to deal with the actual the process for sending the message.
    /// this.EmailFrom = "server@localhost";
    /// </summary>
    public class Mailer
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger("Com.VerySimple.Email.Mailer");

        public Mailer()
        {
            this.Host = "localhost";
        }

        public Mailer(string host)
        {
            this.Host = host;
        }

        public string Host;

        /// <summary>
        /// sends a MailMessage email object w/ error checking.
        /// </summary>
        /// <param name="email">MailMessage object</param>
        /// <returns>boolean indicating if message was sent sucessfully</returns>
        public bool Send(MailMessage email)
        {
            log.Debug("Send(email) called");
            // configure the SmtpMail object
            SmtpClient SmtpMail = new SmtpClient();
            SmtpMail.Host = this.Host;
            SmtpMail.Timeout = 3000000;
            bool result = false;

            // attempt to send the message
            try
            {
                SmtpMail.Send(email);
                log.Debug("Email Sent");
                result = true;
            }
            catch (Exception exc)
            {
                log.Warn(exc.ToString());
            }

            return result;
        }

        /// <summary>
        /// sends an email message with the specified parameters
        /// </summary>
        /// <param name="to">email to send message</param>
        /// <param name="subject">email subject</param>
        /// <param name="message">body of email message</param>
        /// <param name="attach">string to be attached</param>
        /// <param name="attachName">name of file attachment</param>
        /// <returns>boolean indicating if message was sent sucessfully</returns>
        public bool Send(string from, string to, string subject, string message, String attach, String attachTempPath)
        {
            log.Info("Send('" + from + "','" + to + "', '" + subject + "', '[not logged]')");
            // create and populate the mail message object
            MailMessage email = new MailMessage(from,to,subject,message);

            if (attach != null)
            {

                string attachName = System.IO.Path.GetFileName(attachTempPath);

                try
                {
                    FileStream fs = new FileStream(attachTempPath, FileMode.CreateNew, FileAccess.ReadWrite);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(attach);
                    sw.Close();
                    email.Attachments.Add(new Attachment(attachTempPath));
                }
                catch (Exception exc)
                {
                    email.Attachments.Clear();
                    log.Debug("File could not be attached: " + exc.Message);
                    email.Body += "\r\n\r\nERROR: File could not be attached: " + exc.Message;
                }
            }

            // send the message
            bool result = this.Send(email);

            if (attach != null)
            {
                try
                {
                    File.Delete(attachTempPath);
                }
                catch { }
            }

            return result;
        }

        /// <summary>
        /// sends an email message with the specified parameters
        /// </summary>
        /// <param name="to">email to send message</param>
        /// <param name="subject">email subject</param>
        /// <param name="message">body of email message</param>
        /// <returns>boolean indicating if message was sent sucessfully</returns>
        public bool Send(string from, string to, string subject, string message)
        {
            return this.Send(from, to, subject, message, null, null);
        }

    }
}

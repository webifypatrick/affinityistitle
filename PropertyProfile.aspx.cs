using System;
using System.Web;
using System.IO;

namespace Affinity
{
    public partial class PropertyProfile : PageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            // get stack trace
            System.Diagnostics.StackFrame fr = new System.Diagnostics.StackFrame(1, true);
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(fr);
            Response.Write( "method = " + fr.GetMethod().Name + " trace = " + st.ToString() );
            */
            // we have to call the base first so phreezer is instantiated
            base.PageBase_Init(sender, e);
            PageBase pb = (PageBase)this.Page;
            Affinity.Account acc = pb.GetAccount();
            //string contents = "";

            int id = NoNull.GetInt(Request["id"], 0);

            //Response.Write(Request["file"]);
            //Response.End();

            if (id > 0)
            {

                Affinity.Order order = new Affinity.Order(this.phreezer);
                order.Load(id);

                int filecount = 0;
                ((Affinity.MasterPage) this.Master).SetLayout("PropertyProfile", MasterPage.LayoutStyle.ContentOnly);
                if (Request["file"] != null)
                {
                    string[] file = Request["file"].Split(',');

                    for (int i = 0; i < file.Length; i++)
                    {
                        if ((file[i].Equals("WD") || file[i].Equals("WD_S") || file[i].Equals("WD_JT") || file[i].Equals("WD_TE")) && Request["warrantyfile"] == null) continue;
                        string path = HttpContext.Current.Server.MapPath(".") + "\\downloads\\" + file[i] + ".xml";
                        if (File.Exists(path))
                        {
                            filecount++;
                        }
                    }

                    //Response.Write(path);

                    /*
                    string fileNameXML = HttpContext.Current.Server.MapPath(".") + "\\downloads\\Disclosure_Agent.doc";
                      string FileName = HttpContext.Current.Server.MapPath(".") + "\\downloads\\tested.xml";

                           Document dc = new Document();
                              dc.LoadFromFile(fileNameXML, FileFormat.Doc);
                              dc.SaveToFile(FileName, FileFormat.Xml);
                              //dc.SaveToStream(HttpContext.Current.Response.OutputStream, FileFormat.Xml);
                       */

                    /*
                    if(false)
                    {
                        int len = contents.Length+50;
                        HttpContext.Current.Response.ContentType = "application/ms-excel";
                        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=PropertyProfile.xls");
                        HttpContext.Current.Response.AppendHeader("Content-Length", len.ToString());

                    HttpContext.Current.Response.Write(contents + "                                                  ");
                        HttpContext.Current.Response.Flush();
                    }
                    */
                }

            }
        }

    }
}
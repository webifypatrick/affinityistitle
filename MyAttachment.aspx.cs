using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;
using Com.VerySimple.Util;

public partial class MyAttachment : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
		// System.IO.Path.GetExtension();

		Affinity.Attachment att = new Affinity.Attachment(this.phreezer);
		att.Load(NoNull.GetInt(Request["id"],0));

		if (!att.Request.Order.CanRead(this.GetAccount()))
		{
			Crash(302,"You do not have permission to view this attachment");
		}

		string fileName = att.Filepath;
		string viewStyle = this.GetAccount().GetPreference("AttachmentBehavior", "attachment"); // inline || attachment
		string filePath = Server.MapPath("./") + "attachments/" + fileName.Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "").Replace("", "");
		
		FileInfo fi = new FileInfo(filePath);

		string contentType;

		// the viewstyle itself being inline/attachment would seem to be what we want
		// however it seems to be generally ignored by the browser.  So, in addition
		// to setting that header, we will force the content type as well
		if (viewStyle.Equals("attachment"))
		{
			contentType = "application/octet-stream";
		}
		else
		{
			switch (fi.Extension.Replace(".", "").ToLower())
			{
				case "pdf":
					contentType = "application/pdf"; 
					break;
				case "doc":
					contentType = "application/msword";
					break;
				case "xls":
					contentType = "application/vnd.ms-excel";
					break;
				case "tif":
				case "tiff":
					contentType = "image/tiff";
					break;
				default:
					contentType = "application/octet-stream";
					break;
			}
		}

		Response.Clear();
		Response.ContentType = contentType;
		Response.AddHeader("content-length", fi.Length.ToString());
		Response.AddHeader("Content-Disposition", "" + viewStyle + ";filename=\"" + fileName + "\"");

		Response.Buffer = true;
		Response.WriteFile(filePath);
		Response.End();

		/*
		// alternate way to write binary to the browser
		byte[] data = pdf.GetData();
		Response.Clear();
		Response.ContentType = "application/pdf";
		Response.AddHeader("content-disposition", "inline;filename=FileName.pdf");
		Response.AddHeader("content-length", data.Length.ToString());
		Response.Buffer = true;
		Response.BinaryWrite(data);
		Response.End();  // this is required or else the pdf will open in a new window
		*/
	}
}

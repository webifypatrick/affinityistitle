using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
//using FAXCOMEXLib;
//using Microsoft.Office.Interop.Word;
//using System.Diagnostics;
//[assembly: System.Runtime.InteropServices.ComVisible(false)]

/// <summary>
/// Summary description for FaxServer
/// </summary>
public class AffinityFaxServer
{
	public AffinityFaxServer()
	{
		//Form_Load();
	}

	public void Form_Load(string body, string FileName)
	{
		try
		{
			string path = System.Web.HttpContext.Current.Server.MapPath("") + "\\App_Data\\Faxes\\";

			//string FileName = "c:\\\\test.html";
			//string FileToSave = path + ".doc";

			using (StreamWriter sw = File.CreateText(path + FileName))
			{
				sw.Write(body);
				sw.Close();
			}

			/*
			 * In order for the fax to work, need to print HTML documents
			 * Printing HTML always requires a dialog box which is not good for automation
			 * Found a program called PrintHTML which is in the printhtml folder
			 * This program prints HTML without a dialog
			 * However, the way this program works requires a batch file to wrap around it
			 * I created a batch file called printit.bat which contains the following line:
			 * c:\printhtml\printhtml file=%1 printername=FAX nopreserve nogui
			 * I think set the Printto action for HTML to the following
			 * "c:\printit.bat" "%1"
			 * It used to be the following:
			 * "C:\WINDOWS\system32\rundll32.exe" "C:\WINDOWS\system32\mshtml.dll",PrintHTML "%1" "%2" "%3" "%4"
			 * 
			 * Since I was unable to run anything from the Web app because it just hangs,
			 * I created a program call FaxServerDos.exe which simply checks the c:\web\app_data\Faxes
			 * folder for files and then attempts to fax them and then deletes them every 5 minutes
			 * 
			*/
			/*
			using (new Impersonator("PJohnson", "WINDYLAKES", "Develop4ATS!"))
			{

			FaxDocument objFaxDocument = new FaxDocument();
			FaxServer objFaxServer = new FaxServer();
			FaxSender objSender = null;
			object JobID = null;

			//Connect to the fax server
			objFaxServer.Connect("");
						ApplicationClass objWord = new ApplicationClass();
						object fltDocFormat = WdSaveFormat.wdFormatDocument;
						object missing = System.Reflection.Missing.Value;
						//object readOnly = false; 
						object isVisible = false;

						Object filenam = FileName;
						Object confirmConversions = Type.Missing;
						Object readOnly = Type.Missing;
						Object addToRecentFiles = Type.Missing;
						Object passwordDocument = Type.Missing;
						Object passwordTemplate = Type.Missing;
						Object revert = Type.Missing;
						Object writePasswordDocument = Type.Missing;
						Object writePasswordTemplate = Type.Missing;
						Object format = Type.Missing;
						Object encoding = Type.Missing;
						Object visible = Type.Missing;
						Object openConflictDocument = Type.Missing;
						Object openAndRepair = Type.Missing;
						Object documentDirection = Type.Missing;
						Object noEncodingDialog = Type.Missing;

						objWord.Documents.Open(ref filenam,
							ref confirmConversions, ref readOnly, ref addToRecentFiles,
							ref passwordDocument, ref passwordTemplate, ref revert,
							ref writePasswordDocument, ref writePasswordTemplate,
							ref format, ref encoding, ref visible, ref openConflictDocument,
							ref openAndRepair, ref documentDirection, ref noEncodingDialog);
						//Do the background activity 
						objWord.Visible = false;
						//Document oDoc = objWord.ActiveDocument;
			*/
			/*
						oDoc.SaveAs(ref FileToSave, ref fltDocFormat, ref missing, ref missing, ref missing, ref missing,
						ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
						ref missing, ref missing);
			*/
			/*
						Document oDoc = objWord.ActiveDocument;
						Object fileFormat = WdSaveFormat.wdFormatDocument97;
						Object lockComments = Type.Missing;
						Object password = Type.Missing;
						Object writePassword = Type.Missing;
						Object readOnlyRecommended = Type.Missing;
						Object embedTrueTypeFonts = Type.Missing;
						Object saveNativePictureFormat = Type.Missing;
						Object saveFormsData = Type.Missing;
						Object saveAsAOCELetter = Type.Missing;
						Object insertLineBreaks = Type.Missing;
						Object allowSubstitutions = Type.Missing;
						Object lineEnding = Type.Missing;
						Object addBiDiMarks = Type.Missing;
						filenam = FileToSave;

						oDoc.SaveAs(ref filenam, ref fileFormat, ref lockComments,
						  ref password, ref addToRecentFiles, ref writePassword,
						  ref readOnlyRecommended, ref embedTrueTypeFonts,
						  ref saveNativePictureFormat, ref saveFormsData,
						  ref saveAsAOCELetter, ref encoding, ref insertLineBreaks,
						  ref allowSubstitutions, ref lineEnding, ref addBiDiMarks);

							//Close/quit word 
						objWord.Documents.Close(ref missing, ref missing, ref missing);
						objWord.Quit(ref missing, ref missing, ref missing);
						*/
			//System.Web.HttpContext.Current.Response.Write(path + "<Br>");
			//Set the fax body
			//objFaxDocument.Body = path;
			//objFaxDocument.Body = "C:\\Documents and Settings\\PJohnson\\My Documents\\Test.doc";
			//objFaxDocument.Body = FileToSave;
			//File.Move(FileToSave, FileToSave.Replace(".doc", "_fax.doc"));
			//objFaxDocument.Body = FileName;
			//objFaxDocument.Body = @"c:\test.html";
			//objFaxDocument.Body = "c:\\sample.pdf";

			//Name the document
			//objFaxDocument.DocumentName = "Request Fax";

			//Set the fax priority
			//objFaxDocument.Priority = FAX_PRIORITY_TYPE_ENUM.fptLOW;

			//Add the recipient with the fax number 15105450990
			//objFaxDocument.Recipients.Add("15105450990", "Guy");

			//Choose to attach the fax to the fax receipt
			//objFaxDocument.AttachFaxToReceipt = false;

			//Set the cover page type and the path to the cover page
			//objFaxDocument.CoverPageType = FAX_COVERPAGE_TYPE_ENUM.fcptSERVER;
			//objFaxDocument.CoverPage = "generic.cov";

			//Provide the cover page note
			//objFaxDocument.Note = "Here is the info you requested";

			//Provide the address for the fax receipt
			//objFaxDocument.ReceiptAddress = "webify@att.net";

			//Set the receipt type to email
			//objFaxDocument.ReceiptType = FAX_RECEIPT_TYPE_ENUM.frtNONE;

			//objFaxDocument.Subject = "Request";

			//Set the sender properties.
			/*
			objFaxDocument.Sender.Title = "Mr.";
			objFaxDocument.Sender.Name = "Bob";
			objFaxDocument.Sender.City = "Cleveland Heights";
			objFaxDocument.Sender.State = "Ohio";
			objFaxDocument.Sender.Company = "Microsoft";
			objFaxDocument.Sender.Country = "USA";
			objFaxDocument.Sender.Email = "someone@microsoft.com";
			objFaxDocument.Sender.FaxNumber = "12165555554";
			objFaxDocument.Sender.HomePhone = "12165555555";
			objFaxDocument.Sender.OfficeLocation = "Downtown";
			objFaxDocument.Sender.OfficePhone = "12165555553";
			objFaxDocument.Sender.StreetAddress = "123 Main Street";
			objFaxDocument.Sender.TSID = "Office fax machine";
			objFaxDocument.Sender.ZipCode = "44118";
			objFaxDocument.Sender.BillingCode = "23A54";
			objFaxDocument.Sender.Department = "Accts Payable";
			*/
			/*
						//Save sender information as default
						//objFaxDocument.Sender.SaveDefaultSender();

						//Submit the document to the connected fax server
						//and get back the job ID.

						//JobID = objFaxDocument.ConnectedSubmit(objFaxServer);
						//JobID = objFaxDocument.Submit(null);

						objFaxServer.Disconnect();

						// This snippet needs the "System.Diagnostics"
						// library

						// Application path and command line arguments
						string ApplicationPath = "c:\\FaxServerDOS.exe";
						string ApplicationArguments = "c:\\test.html";

						// Create a new process object
						Process ProcessObj = new Process();

						// StartInfo contains the startup information of
						// the new process
						ProcessObj.StartInfo.FileName = ApplicationPath;
						ProcessObj.StartInfo.Arguments = ApplicationArguments;
						ProcessObj.StartInfo.UserName = "PJohnson";
						System.Security.SecureString ss = new System.Security.SecureString();
						ss.AppendChar('D');
						ss.AppendChar('e');
						ss.AppendChar('v');
						ss.AppendChar('e');
						ss.AppendChar('l');
						ss.AppendChar('o');
						ss.AppendChar('p');
						ss.AppendChar('4');
						ss.AppendChar('A');
						ss.AppendChar('T');
						ss.AppendChar('S');
						ss.AppendChar('!');
						ProcessObj.StartInfo.Password = ss;

						// These two optional flags ensure that no DOS window
						// appears
						ProcessObj.StartInfo.UseShellExecute = false;
						ProcessObj.StartInfo.CreateNoWindow = true;

						// If this option is set the DOS window appears again :-/
						// ProcessObj.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

						// This ensures that you get the output from the DOS application
						ProcessObj.StartInfo.RedirectStandardOutput = true;

						// Start the process
						ProcessObj.Start();

						// Wait that the process exits
						ProcessObj.WaitForExit();

						// Now read the output of the DOS application
						string Result = ProcessObj.StandardOutput.ReadToEnd();			
			System.Web.HttpContext.Current.Response.Write(Result);
			System.Web.HttpContext.Current.Response.End();
						//File.Delete(path + ".doc");
						//File.Delete(path + ".html");
						*/
			//}
		}
		catch (Exception e)
		{
			System.Web.HttpContext.Current.Response.Write(e.Message + "<Br>");
			System.Web.HttpContext.Current.Response.Write(e.GetBaseException().Message + "<Br>");
			//System.Web.HttpContext.Current.Response.Write(e.InnerException.Message + "<Br>");
			System.Web.HttpContext.Current.Response.Write(e.Source + "<Br>");
			//System.Web.HttpContext.Current.Response.Write(e.StackTrace + "<Br>");
			System.Web.HttpContext.Current.Response.End();
		}
	}
}

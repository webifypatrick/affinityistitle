using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AffinityClient.AffinityServer;
using System.Xml;

namespace AffinityClient
{
	public partial class Form1 : Form
	{

		// our private variables to store the affinity web service objects
		private AffinityServer.WsProcessor webService;
		private AffinityServer.WsToken webToken;

		/// <summary>
		/// Constructor when form is created
		/// </summary>
		public Form1()
		{
			InitializeComponent();

			// create a new instance of the Affinity WsProcessor web service
			this.webService = new AffinityServer.WsProcessor();

			// authenticate with the service to receive a token that we will need for all other functions
			this.webToken = this.webService.Authenticate("admin", "pass123");

		}

		/// <summary>
		/// Click handler shows an example update
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_Click(object sender, EventArgs e)
		{

			// create the xml document object that we are going to use to pass info to the service
			XmlDocument doc = new XmlDocument();

			// add a root node, in our schema the root node name is "response"
			XmlElement root = doc.CreateElement("response");
			doc.AppendChild(root);

			/* add  fields to the document using their softpro (sp_id) field name
			 * this could be done one line at a time, or more likely in a loop
			 * the following sp_id *must* be included or the web service will return an error:
			 * AFF_ID
			 * WEB_ID
			 */
			root.AppendChild(NewFieldElement("AFF_ID", "AFF700001", doc));
			root.AppendChild(NewFieldElement("WEB_ID", "38", doc));
			//root.AppendChild(NewFieldElement("ABC1", "This is the value 1", doc));
			//root.AppendChild( NewFieldElement("ABC2", "This is the value 2", doc) );
			//root.AppendChild( NewFieldElement("ABC3", "This is the value 3", doc) );

			// call the web service SyncRequest method to send our changes
			AffinityServer.WsResponse resp = this.webService.SyncRequest(this.webToken, doc);

			// The response object has a few properties that we can use to determine success/fail
			if (resp.IsSuccess)
			{
				MessageBox.Show("SUCCESS: " + resp.Message);
			}
			else
			{
				MessageBox.Show("FAILED: " + resp.Message);
			}

		}

		/// <summary>
		/// Utility function that returns an XmlElement named "field" with provided values 
		/// for sp_id attribute and innerText
		/// </summary>
		/// <param name="sp_id">The SoftPro ID for the field</param>
		/// <param name="inner_text">The value of this SoftPro field</param>
		/// <param name="doc">A reference to the xml document being constructed</param>
		/// <returns>XmlElement</returns>
		private XmlElement NewFieldElement(string sp_id, string inner_text, XmlDocument doc)
		{
			XmlElement elem = doc.CreateElement("field");
			elem.SetAttribute("sp_id", sp_id);
			elem.InnerText = inner_text;
			return elem;
		}
	}
}
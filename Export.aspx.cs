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

using System.Xml;
using System.Collections;
using Affinity.ExportRenderer;

/// <summary>
/// This page exports a request as xml or plain text (for softpro)
/// </summary>
public partial class Export : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
		this.RequirePermission(Affinity.RolePermission.SubmitOrders);
		string id = NoNull.GetString(Request["id"]);
		string keyAttribute = NoNull.GetString(Request["key"],"sp_id");
		string format = NoNull.GetString(Request["format"],"hash");
		string exportformat = "PFT";

		if(!format.Equals("entire"))
		{
			Affinity.Request r = new Affinity.Request(this.phreezer);
			r.Load(id);

			// make sure this user has permission to make updates to this order
			if (!r.Order.CanRead(this.GetAccount()))
			{
				this.Crash(300, "Permission denied.");
			}

			IBaseRenderer renderer = null;

			// depending on the format requested, get the correct renderer object
			switch (format)
			{
				case "entire":
					break;
				case "xml":
					exportformat = "Generic XML";
					renderer = new XmlRenderer(r, this.GetSystemSettings());
					break;
				case "rei":
					exportformat = "REI XML";
					renderer = new XmlREIRenderer(r, this.GetSystemSettings());
					break;
                case "change":
                    exportformat = "PFT (Changes Only)";
                    renderer = new PFTChangeRenderer(r, this.GetSystemSettings());
                    break;
                case "TPS":
                    exportformat = "TPS";
                    renderer = new TPSServicePFTRenderer(r, this.GetSystemSettings());
                    break;
                //case "special":
				//	renderer = new XmlSpecialRenderer(r, this.GetSystemSettings());
				//	break;
				default:
					renderer = new PFTRenderer(r, this.GetSystemSettings());
					break;
			}

			Affinity.ExportLog exportlog = new Affinity.ExportLog(this.phreezer);
			Affinity.Account account = this.GetAccount();
			exportlog.AccountID = account.Id;
			exportlog.OrderID = r.OrderId;
			exportlog.RequestID = r.Id;
			exportlog.ExportFormat = exportformat;
			exportlog.Insert();

			// output the results of the renderer
			OutputRenderer(renderer, keyAttribute);
		}
		else
		{
			Affinity.RequestTypes rts = new Affinity.RequestTypes(this.phreezer);
			Affinity.RequestTypeCriteria rtc = new Affinity.RequestTypeCriteria();
			rtc.IsActive = 1;
			rts.Query(rtc);
			Object [] renderers = new Object[rts.Count];
			
			IEnumerator i = rts.GetEnumerator();
			int j = 0;
			bool isClerkingServices = false;
			Affinity.Account account = this.GetAccount();
			exportformat = "Entire Order";
			
			while(i.MoveNext())
			{
				Affinity.RequestType rt = (Affinity.RequestType) i.Current;
				Affinity.Order order = new Affinity.Order(this.phreezer);
				order.Load(id);
				
				Affinity.RequestCriteria rc = new Affinity.RequestCriteria();
				rc.RequestTypeCode = rt.Code;
				order.GetOrderRequests(rc);
				
				Affinity.Requests reqs = order.GetOrderRequests(rc);
				Affinity.Request r = null;
				
				if(reqs.Count > 0)
				{
					r = (Affinity.Request) reqs[0];
					
					if(rt.Code.Equals("ClerkingRequest")) isClerkingServices = true;
					IBaseRenderer renderer = new PFTRenderer(r, this.GetSystemSettings());
					
					renderers[j] = renderer;
					
					j++;
				}
				
				if(r != null)
				{
					Affinity.ExportLog exportlog = new Affinity.ExportLog(this.phreezer);
					exportlog.AccountID = account.Id;
					exportlog.OrderID = r.OrderId;
					exportlog.RequestID = r.Id;
					exportlog.ExportFormat = exportformat;
					exportlog.Insert();
				}
			}
			
			OutputMultiRenderer(renderers, keyAttribute, isClerkingServices);
		}
		
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="renderer"></param>
	/// <param name="keyAttribute"></param>
	protected void OutputRenderer(IBaseRenderer renderer, string keyAttribute)
	{
		Response.Clear();
		Response.AddHeader("Content-Disposition", "attachment;filename=\"" + renderer.GetFileName() + "\"");
		Response.ContentType = renderer.GetMimeType();
		Response.Write(renderer.Render(keyAttribute));
		Response.End(); // required or sometimes will show inline
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="renderer"></param>
	/// <param name="keyAttribute"></param>
	protected void OutputMultiRenderer(Object [] renderers, string keyAttribute, bool isClerkingServices)
	{
		Response.Clear();
		
		if(renderers.Length > 0)
		{
			bool contentTypeSet = false;
			IEnumerator i = renderers.GetEnumerator();
			
			while(i.MoveNext())
			{
				IBaseRenderer renderer = (IBaseRenderer) i.Current;
				if(renderer == null) continue;
				if(!contentTypeSet)
				{
					Response.AddHeader("Content-Disposition", "attachment;filename=\"" + renderer.GetFileName() + "\"");
					Response.ContentType = renderer.GetMimeType();
					contentTypeSet = true;
				}
				Response.Write(renderer.Render(keyAttribute));
			}
			
			if(isClerkingServices)
			{
				Response.Write("Clerking Services=Yes\n");
			}
		}
		Response.End(); // required or sometimes will show inline
	}
}

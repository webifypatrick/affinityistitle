using System;
using System.Data;
using System.Collections;

namespace Affinity.ExportRenderer
{

	/// <summary>
	/// Renders Export Output for SoftPro PFT
	/// </summary>
	public class PFTChangeRenderer : BaseRenderer
	{
		// use the base constructor
		public PFTChangeRenderer(Affinity.Request req, Hashtable sys) : base(req,sys) { }

		public override string GetFileName()
		{
			return this.request.Order.WorkingId + "_CHANGE_" + this.request.Id.ToString() + ".pft";
		}

		public override string GetMimeType()
		{
			return "text/plain";
		}

		public override string Render(string keyAttribute)
		{
			Hashtable ht = this.request.GetNewValues(keyAttribute);
			
			if(ht.ContainsKey("AG701ID") && ht["AG701ID"].ToString().Equals(""))
			{
				ht.Remove( "AG701ID" );
			}
			
			if(ht.ContainsKey("AG701CONTID") && ht["AG701CONTID"].ToString().Equals(""))
			{
				ht.Remove( "AG701CONTID" );
			}

			
			if(ht.ContainsKey("AG702ID") && ht["AG702ID"].ToString().Equals(""))
			{
				ht.Remove( "AG702ID" );
			}
			
			if(ht.ContainsKey("AG702CONTID") && ht["AG702CONTID"].ToString().Equals(""))
			{
				ht.Remove( "AG702CONTID" );
			}

			
			if(ht.ContainsKey("SLRATID") && ht["SLRATID"].ToString().Equals(""))
			{
				ht.Remove( "SLRATID" );
			}
			
			if(ht.ContainsKey("SLRATCONTID") && ht["SLRATCONTID"].ToString().Equals(""))
			{
				ht.Remove( "SLRATCONTID" );
			}

			
			if(ht.ContainsKey("LENID") && ht["LENID"].ToString().Equals(""))
			{
				ht.Remove( "LENID" );
			}
			
			if(ht.ContainsKey("LENCONTID") && ht["LENCONTID"].ToString().Equals(""))
			{
				ht.Remove( "LENCONTID" );
			}

			
			if(ht.ContainsKey("MTBID") && ht["MTBID"].ToString().Equals(""))
			{
				ht.Remove( "MTBID" );
			}
			
			if(ht.ContainsKey("MTBCONTID") && ht["MTBCONTID"].ToString().Equals(""))
			{
				ht.Remove( "MTBCONTID" );
			}

			
			if(ht.ContainsKey("BYRATID") && ht["BYRATID"].ToString().Equals(""))
			{
				ht.Remove( "BYRATID" );
			}
			
			if(ht.ContainsKey("BYRATCONTID") && ht["BYRATCONTID"].ToString().Equals(""))
			{
				ht.Remove( "BYRATCONTID" );
			}
			
			return this.HashTableToString(ht);
		}

	}
}
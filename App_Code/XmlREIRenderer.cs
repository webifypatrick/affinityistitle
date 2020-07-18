using System;
using System.Data;
using System.Collections;
using System.Text;

using System.Xml;
using Com.VerySimple.Util;

namespace Affinity.ExportRenderer
{

	/// <summary>
	/// Renders Export Output for REI
	/// </summary>
	public class XmlREIRenderer : BaseRenderer
	{
		// use the base constructor
		public XmlREIRenderer(Affinity.Request req, Hashtable sys) : base(req,sys) { }

		public override string GetFileName()
		{
			return this.request.Order.WorkingId + ".xml";
		}

		public override string GetMimeType()
		{
			return "text/xml";
		}

		/// <summary>
		/// Returns XML Rendered to REI specifications
		/// </summary>
		/// <param name="keyAttribute"></param>
		/// <returns>string (xml)</returns>
		public override string Render(string keyAttribute)
		{
			Hashtable ht = this.request.GetTranslatedHashTable(keyAttribute, true, false);
			StringBuilder sb = new StringBuilder();

            // get the fips codes
            // TODO: if affinity processes more than IL titles then this will need to be added to the database
            string stateFips = this.request.Order.PropertyState.ToUpper() == "IL" ? "17" : "";
            string countyFips = this.request.Order.GetZipCode().FipsCode;

			sb.Append("<Stewart.REI>\r\n");
			sb.Append("	<Stewart.REI.Header>\r\n");
			sb.Append("		<UserName>AffinityUser</UserName>\r\n");
			sb.Append("		<ClientId>6167</ClientId>\r\n");
			sb.Append("		<ClientTransactionReference>" + this.request.Order.WorkingId + "</ClientTransactionReference>\r\n");
			sb.Append("	</Stewart.REI.Header>\r\n");
			sb.Append("	<Stewart.REI.Request>\r\n");
			sb.Append("		<DASLRequest>\r\n");
            sb.Append("			<RequestCriteria CountyFIPS=\"" + countyFips + "\" StateFIPS=\"" + stateFips + "\">\r\n");
			sb.Append("				<ProductId>233</ProductId>\r\n");
            sb.Append("				<ProviderId>20</ProviderId>\r\n");
            sb.Append("				<ClientReference></ClientReference>\r\n");
            sb.Append("				<ClientReference>" + this.request.Order.WorkingId + "</ClientReference>\r\n");
			sb.Append("				<PropertySearch>\r\n");
			sb.Append("					<SearchType>I</SearchType>\r\n");
			sb.Append("					<APNs>\r\n");
			sb.Append("						<APN>" + this.request.Order.Pin + "</APN>\r\n");
			sb.Append("					</APNs>\r\n");
			sb.Append("				</PropertySearch>\r\n");
			sb.Append("				<PartySearch>\r\n");
			sb.Append("					<SearchType>I</SearchType>\r\n");
			sb.Append("					<Parties>\r\n");

			// add all the selling parties that have been entered
			for (int i = 1; i < 6; i++)
			{
				sb.Append(GetPartyInfo(ht, i));
			}

			sb.Append("					</Parties>\r\n");
			sb.Append("				</PartySearch>\r\n");
			sb.Append("			</RequestCriteria>\r\n");
			sb.Append("		</DASLRequest>\r\n");
			sb.Append("	</Stewart.REI.Request>\r\n");
			sb.Append("</Stewart.REI>\r\n");

			return sb.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ht"></param>
		/// <param name="num"></param>
		/// <returns></returns>
		private string GetPartyInfo(Hashtable ht, int num)
		{
			StringBuilder sb = new StringBuilder();

			string name1 = ifnull(ht, "Seller" + num + "Name1");
			string name2 = ifnull(ht, "Seller" + num + "Name2");

			if (!name1.Equals(""))
			{
				string[] pair = name1.Split(" ".ToCharArray(), 2);
				sb.Append("						<PartyInfo>\r\n");
				sb.Append("							<Party>\r\n");
				sb.Append("								<FirstName>" + pair[0] + "</FirstName>\r\n");
				sb.Append("								<LastName>" + (pair.Length > 1 ? pair[1] : "") + "</LastName>\r\n");
				sb.Append("							</Party>\r\n");
				sb.Append("						</PartyInfo>\r\n");
			}
			if (!name2.Equals(""))
			{
				string[] pair = name2.Split(" ".ToCharArray(), 2);
				sb.Append("						<PartyInfo>\r\n");
				sb.Append("							<Party>\r\n");
				sb.Append("								<FirstName>" + pair[0] + "</FirstName>\r\n");
				sb.Append("								<LastName>" + (pair.Length > 1 ? pair[1] : "") + "</LastName>\r\n");
				sb.Append("							</Party>\r\n");
				sb.Append("						</PartyInfo>\r\n");
			}

			return sb.ToString();
		}

		/// <summary>
		/// Returns string value of given key, checking for null
		/// </summary>
		/// <param name="ht"></param>
		/// <param name="key"></param>
		/// <returns>string</returns>
		private string ifnull(Hashtable ht, string key)
		{
			return (ht.ContainsKey(key) ? (string)ht[key] : "");
		}

	}
}
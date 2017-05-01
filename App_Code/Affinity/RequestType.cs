using System.IO;
using System.Xml;

namespace Affinity
{


	/// <summary>
	/// Business Logic For RequestType Class
	/// </summary>
	public partial class RequestType
	{
		public static string DefaultCode = "Order";
		public static string DefaultChangeCode = "PropertyChange";

		public static string ClosingRequestCode = "ClosingRequest";
		public static string ClerkingRequestCode = "ClerkingRequest";

		public static string UserPreferences = "UserPreferences";
		public static string SystemSettings = "SystemSettings";

		/// <summary>
		/// Returns definition as an XmlDocument
		/// </summary>
		/// <returns></returns>
		public XmlDocument GetDefDocument()
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(this.Definition);
			return doc;
		}

		/// <summary>
		/// This will reload the request type definitions from the
		/// xml files in the RequestTypes folder
		/// </summary>
		public void ReloadAllDefinitions(string path)
		{
			RequestTypes rts = new RequestTypes(this.phreezer);
			rts.Query(new RequestTypeCriteria());

			foreach (RequestType rt in rts)
			{
				rt.Definition = File.ReadAllText(path + rt.Code + ".xml");
				rt.Update();
			}
		}
	}
}
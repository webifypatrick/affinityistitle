using System;
using System.Data;
using System.Collections;

using System.Xml;
using System.Collections;
using Com.VerySimple.Util;

namespace Affinity.ExportRenderer
{
	/// <summary>
	/// Renders Export Output for Basic XML
	/// </summary>
	public class XmlRenderer : BaseRenderer
	{
		// use the base constructor
		public XmlRenderer(Affinity.Request req, Hashtable sys) : base(req, sys) {}

		public override string GetFileName()
		{
			return this.request.Order.WorkingId + ".xml";
		}

		public override string GetMimeType()
		{
			return "text/xml";
		}

		public override string Render(string keyAttribute)
		{
			XmlDocument doc = this.request.GetTranslatedResponse(keyAttribute, true, true);
			return XmlForm.XmlToString(doc).Replace("</field>", "</field>\r\n").Replace("<response>", "<response>\r\n");
		}


	}
}
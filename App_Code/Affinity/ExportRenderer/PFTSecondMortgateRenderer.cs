using System;
using System.Data;
using System.Collections;

namespace Affinity.ExportRenderer
{

	/// <summary>
	/// Renders Export Output for Second Mortgage
	/// </summary>
	public class PFTSecondMortgageRenderer : BaseRenderer
	{
		// use the base constructor
		public PFTSecondMortgageRenderer(Affinity.Request req, Hashtable sys) : base(req, sys) { }

		public override string GetFileName()
		{
			return this.request.Order.WorkingId + ".xml";
		}

		public override string GetMimeType()
		{
			return "text/xml";
		}



	}
}

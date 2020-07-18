using System;

namespace Affinity
{
    public partial class AdminReports : PageBase
    {

        private Affinity.RequestType rtype;
        private XmlForm xmlForm;

        /// <summary>
        /// The form controls are created at this point.  if we create them at page load
        /// then their viewstate will not be persisted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        override protected void PageBase_Init(object sender, System.EventArgs e)
        {
            // we have to call the base first so phreezer is instantiated
            base.PageBase_Init(sender, e);

            rtype = new Affinity.RequestType(this.phreezer);
            rtype.Load(Affinity.RequestType.SystemSettings);

            xmlForm = new XmlForm(this.GetAccount());

            LoadForm();
            //Affinity.SystemSetting ss = new Affinity.SystemSetting(this.phreezer);
            //ss.Load("SYSTEM");

            //pnlForm.Controls.Add(xmlForm.GetFormFieldControl(rtype.Definition, ss.Data));
        }

        private void LoadForm()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.RequirePermission(Affinity.RolePermission.AffinityManager);
            //this.RequirePermission(Affinity.RolePermission.AffinityStaff);
            //this.RequirePermission(Affinity.RolePermission.SubmitOrders);
            ((Affinity.MasterPage)this.Master).SetLayout("Reports", MasterPage.LayoutStyle.ContentOnly);
        }
    }
}
using System;
using System.Web.UI;

namespace Affinity
{
    public partial class AdminCompany : PageBase
    {
        private Affinity.Company company;
        private bool isUpdate;

        override protected void PageBase_Init(object sender, System.EventArgs e)
        {
            // we have to call the base first so phreezer is instantiated
            base.PageBase_Init(sender, e);

            int id = NoNull.GetInt(Request["id"], 0);
            this.company = new Affinity.Company(this.phreezer);

            this.isUpdate = (!id.Equals(0));

            if (this.isUpdate)
            {
                this.company.Load(id);
            }
        }


        /// <summary>
        /// Persist to DB
        /// </summary>
        protected void UpdateCompany()
        {
            //this.company.Id = int.Parse(txtId.Text);
            this.company.Name = txtName.Text;
            //this.company.Created = DateTime.Parse(txtCreated.Text);

            if (this.company.Id == 0)
            {
                this.company.Created = DateTime.Now;
                this.company.Insert();
            }
            else
            {
                this.company.Update();
            }

            this.Redirect("AdminCompanies.aspx?feedback=Company+Updated");
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            this.RequirePermission(Affinity.RolePermission.AdminSystem);
            this.RequirePermission(Affinity.RolePermission.AffinityManager);
            ((Affinity.MasterPage)this.Master).SetLayout("Company Details", MasterPage.LayoutStyle.ContentOnly);

            this.btnDelete.Attributes.Add("onclick", "return confirm('Delete this record?')");

            if (!Page.IsPostBack)
            {
                // populate the form
                txtId.Text = this.company.Id.ToString();
                txtName.Text = this.company.Name.ToString();
                txtCreated.Text = this.company.Created.ToShortDateString();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            UpdateCompany();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.Redirect("AdminCompanies.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.company.Delete();
                this.Redirect("AdminCompanies.aspx?feedback=Company+Deleted");
            }
            catch (Exception ex)
            {
                ((Affinity.MasterPage)this.Master).ShowFeedback("The company cannot be deleted.  Most likely because there are still accounts assigned to it.", MasterPage.FeedbackType.Error);
            }
        }
    }
}
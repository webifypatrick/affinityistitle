using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI.WebControls;

namespace Affinity
{
    public partial class AdminFees : PageBase
    {
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


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.RequirePermission(Affinity.RolePermission.AffinityManager);
            this.RequirePermission(Affinity.RolePermission.AffinityStaff);
            this.RequirePermission(Affinity.RolePermission.SubmitOrders);
            ((Affinity.MasterPage) this.Master).SetLayout("System Settings", MasterPage.LayoutStyle.ContentOnly);

            if (!IsPostBack)
            {
                refreshInsuranceRates("");
                loadFees();
            }
        }

        protected void loadFees()
        {
            ListDictionary fields = new ListDictionary();
            fields.Add("83", "AbstractFee");
            fields.Add("84", "MortgagePolicy");
            fields.Add("85", "ARMEndorsement");
            fields.Add("86", "BalloonEndorsement");
            fields.Add("87", "CondominiumEndorsement");
            fields.Add("88", "EPAEndorsement");
            fields.Add("89", "LocationEndorsement");
            fields.Add("90", "PUDEndorsement");
            fields.Add("91", "RevolvingCreditMortgageEndorsement");
            fields.Add("92", "CPLLenderEndorsement");
            fields.Add("93", "CPLBuyerEndorsement");
            fields.Add("94", "CPLSellerEndorsement");
            fields.Add("95", "Level100");
            fields.Add("96", "Level150");
            fields.Add("97", "Level200");
            fields.Add("98", "Level250");
            fields.Add("99", "Level300");
            fields.Add("100", "Level400");
            fields.Add("101", "Level500");
            fields.Add("102", "LenderClosingStatement");
            fields.Add("103", "RefinanceTitleInsuranceRate");
            fields.Add("104", "RefinanceTitleInsuranceMinimumFee");
            fields.Add("105", "InterimRiskFee");
            fields.Add("106", "InterimRiskOver100000Fee");
            fields.Add("107", "AfterHoursClosingFee");
            fields.Add("108", "ChainofTitle");
            fields.Add("109", "CommitmentLaterDate");
            fields.Add("110", "DryClosingFee");
            fields.Add("111", "EMailPackageHandlingFee");
            fields.Add("112", "JointOrderEscrows");
            fields.Add("113", "OvernightHandlingFee");
            fields.Add("114", "PLPDComplianceProcessingFee");
            fields.Add("115", "PolicyUpdateDate");
            fields.Add("116", "InHomeRemoteClosingFee");
            fields.Add("117", "TitleIndemnitiesProcessingFee");
            fields.Add("118", "TaxPaymentHandlingFee");
            fields.Add("119", "ChicagoWaterCertificationProcessingFee");
            fields.Add("120", "WireIncomingOutgoingTransferFee");
            fields.Add("121", "CertifiedChecksFee");
            fields.Add("122", "Upto100000");
            fields.Add("123", "Level110000");
            fields.Add("124", "Level120000");
            fields.Add("125", "Level130000");
            fields.Add("126", "Level140000");
            fields.Add("127", "Level150000");
            fields.Add("128", "Level160000");
            fields.Add("129", "Level170000");
            fields.Add("130", "Level180000");
            fields.Add("131", "Level190000");
            fields.Add("132", "Level200000");
            fields.Add("133", "Over200000");
            fields.Add("134", "AgencyClosingServices");
            fields.Add("135", "EndorsementsAll");
            fields.Add("136", "FirstMortgageClosingFeePurchase");
            fields.Add("137", "SecondMortgageClosingFeePurchase");
            fields.Add("138", "AbstractFeePurchase");
            fields.Add("139", "EmailPackageHandlingFeePurchase");
            fields.Add("140", "FirstLoanPolicyPurchase");
            fields.Add("141", "SecondLoanPolicyPurchase");
            fields.Add("142", "EndorsementsPurchase");
            fields.Add("143", "PLDPComplianceProcessingFeePurchase");
            fields.Add("144", "ChainofTitlePurchase");
            fields.Add("145", "PolicyUpdateFeePurchase");
            fields.Add("146", "OvernightHandlingFeeEachPurchase");
            fields.Add("147", "WireTransferFeeEachPurchase");
            fields.Add("148", "StatePolicyFeeEachPurchase");
            fields.Add("149", "CPLEndorsementsPurchase");
            fields.Add("150", "DeedMortgageReleaseRecordingEstimatePurchase");
            fields.Add("151", "DeedMortgageReleaseRecordingEstimateAgentRefinance");
            fields.Add("152", "DeedMortgageReleaseRecordingEstimateRefinance");
            fields.Add("153", "CPLEndorsementsAgentRefinance");
            fields.Add("154", "CPLEndorsementsRefinance");
            fields.Add("155", "StatePolicyFeeEachAgentRefinance");
            fields.Add("156", "StatePolicyFeeEachRefinance");
            fields.Add("157", "WireTransferFeeEachAgentRefinance");
            fields.Add("158", "WireTransferFeeEachRefinance");
            fields.Add("159", "OvernightHandlingFeeEachAgentRefinance");
            fields.Add("160", "OvernightHandlingFeeEachRefinance");
            fields.Add("161", "PolicyUpdateFeeAgentRefinance");
            fields.Add("162", "PolicyUpdateFeeRefinance");
            fields.Add("163", "ChainofTitleAgentRefinance");
            fields.Add("164", "ChainofTitleRefinance");
            fields.Add("165", "PLDPComplianceProcessingFeeAgentRefinance");
            fields.Add("166", "PLDPComplianceProcessingFeeRefinance");
            fields.Add("167", "EndorsementsAgentRefinance");
            fields.Add("168", "EndorsementsRefinance");
            fields.Add("170", "SecondLoanPolicyAgentRefinance");
            fields.Add("171", "SecondLoanPolicyRefinance");
            fields.Add("172", "FirstLoanPolicyAgentRefinance");
            fields.Add("173", "FirstLoanPolicyRefinance");
            fields.Add("174", "EmailPackageHandlingFeeAgentRefinance");
            fields.Add("175", "EmailPackageHandlingFeeRefinance");
            fields.Add("176", "AbstractFeeAgentRefinance");
            fields.Add("177", "AbstractFeeRefinance");
            fields.Add("178", "SecondMortgageClosingFeeAgentRefinance");
            fields.Add("179", "SecondMortgageClosingFeeRefinance");
            fields.Add("180", "FirstMortgageClosingFeeAgentRefinance");
            fields.Add("181", "FirstMortgageClosingFeeRefinance");
            fields.Add("182", "RefiRatesPerThousandOverConforming");

            Affinity.TitleFee tfitem = new Affinity.TitleFee(this.phreezer);

            //IEnumerator i = fields.GetEnumerator();

            foreach (DictionaryEntry i in fields)
            {
                string Id = i.Key.ToString();
                TextBox formfield = (TextBox)group_mortgage_fieldset.FindControl(i.Value.ToString());

                if (formfield != null)
                {
                    tfitem.Load(Id);
                    formfield.Text = tfitem.Fee.ToString();
                }
            }


        }

        protected void refreshInsuranceRates(string selectedValue)
        {
            Affinity.TitleFees tf = new Affinity.TitleFees(this.phreezer);

            Affinity.TitleFeesCriteria tfc = new Affinity.TitleFeesCriteria();
            tfc.AppendToOrderBy("Id");
            tfc.State = "IL";
            tfc.FeeType = "Insurance Rate";
            tf.Query(tfc);

            IEnumerator i = tf.GetEnumerator();
            InsuranceRate.Items.Clear();
            ListItem l = new ListItem();
            l.Value = "";
            l.Attributes.Add("rate", "");
            l.Text = "Select a rate level";
            InsuranceRate.Items.Add(l);

            // loop through the checkboxes and insert or delete as needed
            while (i.MoveNext())
            {
                Affinity.TitleFee tfitem = (Affinity.TitleFee)i.Current;

                l = new ListItem();
                l.Value = tfitem.Id;
                l.Attributes.Add("rate", tfitem.Fee.ToString());
                l.Text = tfitem.Name;
                if (tfitem.Id.Equals(selectedValue)) l.Selected = true;
                InsuranceRate.Items.Add(l);
            }
        }

        protected void btnChangeInsuranceRates_Click(object sender, EventArgs e)
        {
            string startingRate = StartingRate.Text;
            string rateIncrement = RateIncrement.Text;

            Affinity.TitleFees tf = new Affinity.TitleFees(this.phreezer);

            Affinity.TitleFeesCriteria tfc = new Affinity.TitleFeesCriteria();
            tfc.FeeType = "Insurance Rate";
            tfc.State = "IL";
            tfc.AppendToOrderBy("Id");
            tf.Query(tfc);

            decimal startingRateDecimal = 0;
            decimal.TryParse(startingRate, out startingRateDecimal);

            decimal rateIncrementDecimal = 0;
            decimal.TryParse(rateIncrement, out rateIncrementDecimal);

            IEnumerator i = tf.GetEnumerator();

            // loop through the checkboxes and insert or delete as needed
            while (i.MoveNext())
            {
                Affinity.TitleFee tfitem = (Affinity.TitleFee)i.Current;

                tfitem.Fee = startingRateDecimal;
                tfitem.Modified = DateTime.Now;
                tfitem.Update();
                startingRateDecimal += rateIncrementDecimal;
            }
            refreshInsuranceRates("");

            Message.InnerText = "All Insurance Rates Have Updated Successfully.";
            Message.Visible = true;

            // we have to re-load the form because the settings have changed and 
            // we need to show the new ones
        }
        protected void btnChangeInsuranceOneRate_Click(object sender, EventArgs e)
        {
            Affinity.TitleFee tfitem = new Affinity.TitleFee(this.phreezer);
            string Id = InsuranceRate.Value;

            tfitem.Load(Id);

            decimal rateChangeDecimal = 0;
            decimal.TryParse(RateChange.Text, out rateChangeDecimal);

            tfitem.Fee = rateChangeDecimal;
            tfitem.Modified = DateTime.Now;
            tfitem.Update();
            refreshInsuranceRates(Id);

            Message.InnerText = "Insurance Rate Updated Successfully.";
            Message.Visible = true;
        }
        protected void btnChangeFee_Click(object sender, EventArgs e)
        {
            Affinity.TitleFee tfitem = new Affinity.TitleFee(this.phreezer);
            string Id = "83";

            tfitem.Load(Id);

            decimal rateChangeDecimal = 0;
            decimal.TryParse(AbstractFee.Text, out rateChangeDecimal);

            tfitem.Fee = rateChangeDecimal;
            tfitem.Modified = DateTime.Now;
            tfitem.Update();

            Message.InnerText = "Abstract Fee/Search Fee Updated Successfully.";
            Message.Visible = true;
        }
        protected void btnChangeEndorsementFees_Click(object sender, EventArgs e)
        {
            ListDictionary fields = new ListDictionary();
            fields.Add("84", "MortgagePolicy");
            fields.Add("85", "ARMEndorsement");
            fields.Add("86", "BalloonEndorsement");
            fields.Add("87", "CondominiumEndorsement");
            fields.Add("88", "EPAEndorsement");
            fields.Add("89", "LocationEndorsement");
            fields.Add("90", "PUDEndorsement");
            fields.Add("91", "RevolvingCreditMortgageEndorsement");
            fields.Add("92", "CPLLenderEndorsement");
            fields.Add("93", "CPLBuyerEndorsement");
            fields.Add("94", "CPLSellerEndorsement");
            Affinity.TitleFee tfitem = new Affinity.TitleFee(this.phreezer);

            //IEnumerator i = fields.GetEnumerator();

            foreach (DictionaryEntry i in fields)
            {
                string Id = i.Key.ToString();
                TextBox formfield = (TextBox)group_mortgage_fieldset.FindControl(i.Value.ToString());

                if (formfield != null)
                {
                    tfitem.Load(Id);

                    decimal rateChangeDecimal = 0;
                    decimal.TryParse(formfield.Text, out rateChangeDecimal);

                    tfitem.Fee = rateChangeDecimal;
                    tfitem.Modified = DateTime.Now;
                    tfitem.Update();
                }

            }

            Message.InnerText = "Mortgage Policy/Endorsement Fees Updated Successfully.";
            Message.Visible = true;
        }
        protected void btnResidentialClosingFee_Click(object sender, EventArgs e)
        {
            ListDictionary fields = new ListDictionary();
            fields.Add("95", "Level100");
            fields.Add("96", "Level150");
            fields.Add("97", "Level200");
            fields.Add("98", "Level250");
            fields.Add("99", "Level300");
            fields.Add("100", "Level400");
            fields.Add("101", "Level500");
            fields.Add("102", "LenderClosingStatement");
            Affinity.TitleFee tfitem = new Affinity.TitleFee(this.phreezer);

            //IEnumerator i = fields.GetEnumerator();

            foreach (DictionaryEntry i in fields)
            {
                string Id = i.Key.ToString();
                TextBox formfield = (TextBox)group_escrow_fieldset.FindControl(i.Value.ToString());

                if (formfield != null)
                {
                    tfitem.Load(Id);

                    decimal rateChangeDecimal = 0;
                    decimal.TryParse(formfield.Text, out rateChangeDecimal);

                    tfitem.Fee = rateChangeDecimal;
                    tfitem.Modified = DateTime.Now;
                    tfitem.Update();
                }

            }

            Message.InnerText = "Escrow Services: Residential Closing Fees Updated Successfully.";
            Message.Visible = true;
        }

        protected void btnRefinance_Click(object sender, EventArgs e)
        {
            ListDictionary fields = new ListDictionary();
            fields.Add("103", "RefinanceTitleInsuranceRate");
            fields.Add("104", "RefinanceTitleInsuranceMinimumFee");
            Affinity.TitleFee tfitem = new Affinity.TitleFee(this.phreezer);

            //IEnumerator i = fields.GetEnumerator();

            foreach (DictionaryEntry i in fields)
            {
                string Id = i.Key.ToString();
                TextBox formfield = (TextBox)group_refinance_fieldset.FindControl(i.Value.ToString());

                if (formfield != null)
                {
                    tfitem.Load(Id);

                    decimal rateChangeDecimal = 0;
                    decimal.TryParse(formfield.Text, out rateChangeDecimal);

                    tfitem.Fee = rateChangeDecimal;
                    tfitem.Modified = DateTime.Now;
                    tfitem.Update();
                }

            }

            Message.InnerText = "Refinance Title Insurance Rates Updated Successfully.";
            Message.Visible = true;
        }

        protected void btnInterimRiskFee_Click(object sender, EventArgs e)
        {
            ListDictionary fields = new ListDictionary();
            fields.Add("105", "InterimRiskFee");
            fields.Add("106", "InterimRiskOver100000Fee");
            Affinity.TitleFee tfitem = new Affinity.TitleFee(this.phreezer);

            //IEnumerator i = fields.GetEnumerator();

            foreach (DictionaryEntry i in fields)
            {
                string Id = i.Key.ToString();
                TextBox formfield = (TextBox)group_interim_fieldset.FindControl(i.Value.ToString());

                if (formfield != null)
                {
                    tfitem.Load(Id);

                    decimal rateChangeDecimal = 0;
                    decimal.TryParse(formfield.Text, out rateChangeDecimal);

                    tfitem.Fee = rateChangeDecimal;
                    tfitem.Modified = DateTime.Now;
                    tfitem.Update();
                }

            }

            Message.InnerText = "Interim Risk Protection Updated Successfully.";
            Message.Visible = true;
        }

        protected void btnOtherSettlementServicesFees_Click(object sender, EventArgs e)
        {
            ListDictionary fields = new ListDictionary();
            fields.Add("107", "AfterHoursClosingFee");
            fields.Add("108", "ChainofTitle");
            fields.Add("109", "CommitmentLaterDate");
            fields.Add("110", "DryClosingFee");
            fields.Add("111", "EMailPackageHandlingFee");
            fields.Add("112", "JointOrderEscrows");
            fields.Add("113", "OvernightHandlingFee");
            fields.Add("114", "PLPDComplianceProcessingFee");
            fields.Add("115", "PolicyUpdateDate");
            fields.Add("116", "InHomeRemoteClosingFee");
            fields.Add("117", "TitleIndemnitiesProcessingFee");
            fields.Add("118", "TaxPaymentHandlingFee");
            fields.Add("119", "ChicagoWaterCertificationProcessingFee");
            fields.Add("120", "WireIncomingOutgoingTransferFee");
            fields.Add("121", "CertifiedChecksFee");
            Affinity.TitleFee tfitem = new Affinity.TitleFee(this.phreezer);

            //IEnumerator i = fields.GetEnumerator();

            foreach (DictionaryEntry i in fields)
            {
                string Id = i.Key.ToString();
                TextBox formfield = (TextBox)group_settlement_fieldset.FindControl(i.Value.ToString());

                if (formfield != null)
                {
                    tfitem.Load(Id);

                    decimal rateChangeDecimal = 0;
                    decimal.TryParse(formfield.Text, out rateChangeDecimal);

                    tfitem.Fee = rateChangeDecimal;
                    tfitem.Modified = DateTime.Now;
                    tfitem.Update();
                }

            }

            Message.InnerText = "Other Settlement Services Fees Updated Successfully.";
            Message.Visible = true;
        }

        protected void btnSecondEquityMortgageRates_Click(object sender, EventArgs e)
        {
            ListDictionary fields = new ListDictionary();
            fields.Add("122", "Upto100000");
            fields.Add("123", "Level110000");
            fields.Add("124", "Level120000");
            fields.Add("125", "Level130000");
            fields.Add("126", "Level140000");
            fields.Add("127", "Level150000");
            fields.Add("128", "Level160000");
            fields.Add("129", "Level170000");
            fields.Add("130", "Level180000");
            fields.Add("131", "Level190000");
            fields.Add("132", "Level200000");
            fields.Add("133", "Over200000");
            fields.Add("134", "AgencyClosingServices");
            fields.Add("135", "EndorsementsAll");
            Affinity.TitleFee tfitem = new Affinity.TitleFee(this.phreezer);

            //IEnumerator i = fields.GetEnumerator();

            foreach (DictionaryEntry i in fields)
            {
                string Id = i.Key.ToString();
                TextBox formfield = (TextBox)group_equity_fieldset.FindControl(i.Value.ToString());

                if (formfield != null)
                {
                    tfitem.Load(Id);

                    decimal rateChangeDecimal = 0;
                    decimal.TryParse(formfield.Text, out rateChangeDecimal);

                    tfitem.Fee = rateChangeDecimal;
                    tfitem.Modified = DateTime.Now;
                    tfitem.Update();
                }

            }

            Message.InnerText = "Second/Equity Mortgage Rates Updated Successfully.";
            Message.Visible = true;
        }

        protected void btnGFE_Click(object sender, EventArgs e)
        {
            ListDictionary fields = new ListDictionary();
            fields.Add("136", "FirstMortgageClosingFeePurchase");
            fields.Add("137", "SecondMortgageClosingFeePurchase");
            fields.Add("138", "AbstractFeePurchase");
            fields.Add("139", "EmailPackageHandlingFeePurchase");
            fields.Add("140", "FirstLoanPolicyPurchase");
            fields.Add("141", "SecondLoanPolicyPurchase");
            fields.Add("142", "EndorsementsPurchase");
            fields.Add("143", "PLDPComplianceProcessingFeePurchase");
            fields.Add("144", "ChainofTitlePurchase");
            fields.Add("145", "PolicyUpdateFeePurchase");
            fields.Add("146", "OvernightHandlingFeeEachPurchase");
            fields.Add("147", "WireTransferFeeEachPurchase");
            fields.Add("148", "StatePolicyFeeEachPurchase");
            fields.Add("149", "CPLEndorsementsPurchase");
            fields.Add("150", "DeedMortgageReleaseRecordingEstimatePurchase");
            fields.Add("151", "DeedMortgageReleaseRecordingEstimateAgentRefinance");
            fields.Add("152", "DeedMortgageReleaseRecordingEstimateRefinance");
            fields.Add("153", "CPLEndorsementsAgentRefinance");
            fields.Add("154", "CPLEndorsementsRefinance");
            fields.Add("155", "StatePolicyFeeEachAgentRefinance");
            fields.Add("156", "StatePolicyFeeEachRefinance");
            fields.Add("157", "WireTransferFeeEachAgentRefinance");
            fields.Add("158", "WireTransferFeeEachRefinance");
            fields.Add("159", "OvernightHandlingFeeEachAgentRefinance");
            fields.Add("160", "OvernightHandlingFeeEachRefinance");
            fields.Add("161", "PolicyUpdateFeeAgentRefinance");
            fields.Add("162", "PolicyUpdateFeeRefinance");
            fields.Add("163", "ChainofTitleAgentRefinance");
            fields.Add("164", "ChainofTitleRefinance");
            fields.Add("165", "PLDPComplianceProcessingFeeAgentRefinance");
            fields.Add("166", "PLDPComplianceProcessingFeeRefinance");
            fields.Add("167", "EndorsementsAgentRefinance");
            fields.Add("168", "EndorsementsRefinance");
            fields.Add("170", "SecondLoanPolicyAgentRefinance");
            fields.Add("171", "SecondLoanPolicyRefinance");
            fields.Add("172", "FirstLoanPolicyAgentRefinance");
            fields.Add("173", "FirstLoanPolicyRefinance");
            fields.Add("174", "EmailPackageHandlingFeeAgentRefinance");
            fields.Add("175", "EmailPackageHandlingFeeRefinance");
            fields.Add("176", "AbstractFeeAgentRefinance");
            fields.Add("177", "AbstractFeeRefinance");
            fields.Add("178", "SecondMortgageClosingFeeAgentRefinance");
            fields.Add("179", "SecondMortgageClosingFeeRefinance");
            fields.Add("180", "FirstMortgageClosingFeeAgentRefinance");
            fields.Add("181", "FirstMortgageClosingFeeRefinance");
            fields.Add("182", "RefiRatesPerThousandOverConforming");
            Affinity.TitleFee tfitem = new Affinity.TitleFee(this.phreezer);

            //IEnumerator i = fields.GetEnumerator();

            foreach (DictionaryEntry i in fields)
            {
                string Id = i.Key.ToString();
                TextBox formfield = (TextBox)group_gfe_fieldset.FindControl(i.Value.ToString());

                if (formfield != null)
                {
                    tfitem.Load(Id);

                    decimal rateChangeDecimal = 0;
                    decimal.TryParse(formfield.Text, out rateChangeDecimal);

                    tfitem.Fee = rateChangeDecimal;
                    tfitem.Modified = DateTime.Now;
                    tfitem.Update();
                }

            }

            Message.InnerText = "New RESPA Good Faith Estimate (GFE) and Standard Fees Updated Successfully.";
            Message.Visible = true;
        }
    }
}
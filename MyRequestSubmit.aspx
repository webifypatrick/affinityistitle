<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeBehind="MyRequestSubmit.aspx.cs" Inherits="Affinity.MyRequestSubmit" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pain_one_cph" runat="server">
        <style>
				#outer_CopyApplicationTo td
				{
					white-space: nowrap;
					font-size: 14px;
					padding: 0;
				}
      	</style>
      	<script>
              jQuery(function () {
                  var role = "<%=((Session["RoleCode"] != null) ? Session["RoleCode"].ToString() : this.GetAccount().RoleCode)%>";

                    if (role != "Realtor") {
                        jQuery("#ctl00_content_cph_field_CopyApplicationTo_3, #ctl00_content_cph_field_CopyApplicationTo_4").parents("td").hide();
                    }
                });

              function WebForm_DoPostBackWithOptions(options) {
                  var errors = "";
                  if (jQuery("#ctl00_content_cph_field_TransactionType_0").is(":checked") && "<%=propertyState%>" == "IN" && !WebForm_DoPostBackWithOptions.skip) {
                      if (jQuery.trim(jQuery("#ctl00_content_cph_field_Seller").val()) === "") {
                          // no seller
                          errors += "<li>No Seller Name was entered.</li>";
                      }
                      if (jQuery.trim(jQuery("#ctl00_content_cph_field_Buyer").val()) === "") {
                          // no buyer
                          errors += "<li>No Buyer Name was entered.</li>";
                      }

                      // Originator Contact Information check
                      if (jQuery.trim(jQuery("#ctl00_content_cph_field_ApplicantName").val()) === "") {
                          // no buyer
                          errors += "<li>No Firm Name was entered under Originator.</li>";
                      }
                      if (jQuery.trim(jQuery("#ctl00_content_cph_field_ApplicantAddress").val()) === "") {
                          // no buyer
                          errors += "<li>No Address Line 1 was entered under Originator.</li>";
                      }
                      if (jQuery.trim(jQuery("#ctl00_content_cph_field_ApplicantCity").val()) === "") {
                          // no buyer
                          errors += "<li>No City was entered under Originator.</li>";
                      }
                      if (jQuery.trim(jQuery("#ctl00_content_cph_field_ApplicantState").val()) === "") {
                          // no buyer
                          errors += "<li>No State was entered under Originator.</li>";
                      }
                      if (jQuery.trim(jQuery("#ctl00_content_cph_field_ApplicantZip").val()) === "") {
                          // no buyer
                          errors += "<li>No Zip was entered under Originator.</li>";
                      }

                      // Listing Agent Contact Information check
                      if (jQuery.trim(jQuery("#ctl00_content_cph_field_ListingRealtorName").val()) === "") {
                          // no buyer
                          errors += "<li>No Firm Name was entered under Listing Realtor.</li>";
                      }
                      if (jQuery.trim(jQuery("#ctl00_content_cph_field_ListingRealtorAddress").val()) === "") {
                          // no buyer
                          errors += "<li>No Address Line 1 was entered under Listing Realtor.</li>";
                      }
                      if (jQuery.trim(jQuery("#ctl00_content_cph_field_ListingRealtorCity").val()) === "") {
                          // no buyer
                          errors += "<li>No City was entered under Listing Realtor.</li>";
                      }
                      if (jQuery.trim(jQuery("#ctl00_content_cph_field_ListingRealtorZip").val()) === "") {
                          // no buyer
                          errors += "<li>No Zip was entered under Listing Realtor.</li>";
                      }
                      if (jQuery.trim(jQuery("#ctl00_content_cph_field_ListingRealtorPhone").val()) === "") {
                          // no buyer
                          errors += "<li>No Phone was entered under Listing Realtor.</li>";
                      }
                      if (jQuery.trim(jQuery("#ctl00_content_cph_field_ListingRealtorEmail").val()) === "") {
                          // no buyer
                          errors += "<li>No Email was entered under Listing Realtor.</li>";
                      }

                      // Selling Agent Contact Information check
                      if (jQuery.trim(jQuery("#ctl00_content_cph_field_SellersRealtorName").val()) === "") {
                          // no buyer
                          errors += "<li>No Firm Name was entered under Selling Realtor.</li>";
                      }
                      if (jQuery.trim(jQuery("#ctl00_content_cph_field_SellersRealtorAddress").val()) === "") {
                          // no buyer
                          errors += "<li>No Address Line 1 was entered under Selling Realtor.</li>";
                      }
                      if (jQuery.trim(jQuery("#ctl00_content_cph_field_SellersRealtorCity").val()) === "") {
                          // no buyer
                          errors += "<li>No City was entered under Selling Realtor.</li>";
                      }
                      if (jQuery.trim(jQuery("#ctl00_content_cph_field_SellersRealtorZip").val()) === "") {
                          // no buyer
                          errors += "<li>No Zip was entered under Selling Realtor.</li>";
                      }
                      if (jQuery.trim(jQuery("#ctl00_content_cph_field_SellersRealtorPhone").val()) === "") {
                          // no buyer
                          errors += "<li>No Phone was entered under Selling Realtor.</li>";
                      }
                      if (jQuery.trim(jQuery("#ctl00_content_cph_field_SellersRealtorEmail").val()) === "") {
                          // no buyer
                          errors += "<li>No Email was entered under Selling Realtor.</li>";
                      }
                      if (errors !== "") {
                          showPopup("Please address the following errors: <ul>" + errors + "</ul><br /><br />Click Continue Anyway to submit your form or Go Back to correct the information.<br /><br /><center><button onclick=\"WebForm_DoPostBackWithOptions.skip = true; $('#ctl00_content_cph_btnChange').click(); return false; \">Continue Anyway</button>&nbsp;&nbsp;&nbsp;<button onclick=\"hidePopup(); hidePopup(); return false; \">Go Back</button>");
                          event.cancelBubble = true;
                          event.stopImmediatePropagation();
                          event.preventDefault();
                          event.stopPropagation();
                          return false;
                      }
                  }
                  WebForm_DoPostBackWithOptions.skip = false;

                  var validationResult = true;
                  if (options.validation) {
                      if (typeof (Page_ClientValidate) == 'function') {
                          validationResult = Page_ClientValidate(options.validationGroup);
                      }
                  }
                  if (validationResult) {
                      if ((typeof (options.actionUrl) != "undefined") && (options.actionUrl != null) && (options.actionUrl.length > 0)) {
                          theForm.action = options.actionUrl;
                      }
                      if (options.trackFocus) {
                          var lastFocus = theForm.elements["__LASTFOCUS"];
                          if ((typeof (lastFocus) != "undefined") && (lastFocus != null)) {
                              if (typeof (document.activeElement) == "undefined") {
                                  lastFocus.value = options.eventTarget;
                              }
                              else {
                                  var active = document.activeElement;
                                  if ((typeof (active) != "undefined") && (active != null)) {
                                      if ((typeof (active.id) != "undefined") && (active.id != null) && (active.id.length > 0)) {
                                          lastFocus.value = active.id;
                                      }
                                      else if (typeof (active.name) != "undefined") {
                                          lastFocus.value = active.name;
                                      }
                                  }
                              }
                          }
                      }
                  }
                  if (options.clientSubmit) {
                      __doPostBack(options.eventTarget, options.eventArgument);
                  }
              }
              WebForm_DoPostBackWithOptions.skip = false;
      	</script>
    <h2 id="header" runat="server">Submit Request for Order</h2>
    <h3 id="ErrorH3" runat="server" visible="false" style="color:red;"></h3>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:Panel ID="pnlForm" runat="server" EnableViewState="false">
    </asp:Panel>
    
    <asp:Panel ID="pnlResults" runat="server" EnableViewState="false" Visible="false">
        <p class="information">Your request has been submitted.  We attempt to process all requests
        within three (3) business days.  You will receive an automatic notification
        when this request has been processed.</p>
        
        <h3>Where would you like to go next?</h3>
    </asp:Panel>

    <p id="action_buttons">
        <asp:Button ID="btnSubmit" runat="server" Text="Submit Order" OnClick="btnSubmit_Click" />
        <asp:Button ID="btnCancelSubmit" runat="server" Text="Cancel" OnClick="btnCancelSubmit_Click" />
        <asp:Button ID="btnChange" runat="server" Text="Submit Changes" Visible="false" OnClick="btnChange_Click" />
        <asp:Button ID="btnCancelChange" runat="server" Text="Cancel" Visible="false" OnClick="btnCancelChange_Click" />
    </p>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Please correct the following errors:" />
		<asp:Panel ID="pnlContentScript" runat="server"></asp:Panel>
</asp:Content>

<asp:Content ID="ContentFooter" ContentPlaceHolderID="lblcontent_footer" runat="server">
	<span id="ContentFooterSpan" runat="server">&copy; Copyright 2017, Affinity Title Services, LLC</span>
	<% if(Request["code"] != null && Request["code"].Equals("Order")) { %>
	<script>
        $("form").on("submit", function (event) {
            if ($.trim($("#ctl00_content_cph_field_Buyer").val()) == "" && $.trim($("#ctl00_content_cph_field_Buyer1Name2").val()) == "" && !confirm("You have not entered a Buyer name. Click OK to continue saving.  Click cancel to stop.")) {
                event.stopPropagation();
                return false;
            }
        });
	</script>
	<% } %>
</asp:Content>
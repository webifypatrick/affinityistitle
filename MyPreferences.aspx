<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeFile="MyPreferences.aspx.cs" Inherits="MyPreferences" Title="Untitled Page" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="pain_one" ContentPlaceHolderID="pain_one_cph" Runat="Server">
<h2>My Preferences</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>   
    
    <asp:Panel ID="pnlForm" runat="server">
        <div class="fields">

		    <div class="groupheader">Account Details</div>
		    <fieldset id="Account_fields">
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Username:</div>
				        <div class="input horizontal">
                            <asp:Label ID="txtUsername" runat="server"></asp:Label></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Name (First Last):</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtFirstName" runat="server" Width="90px"></asp:TextBox></div>
			        </div>
			        <div class="field horizontal">
				        <div class="label horizontal"></div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtLastName" runat="server" Width="125px"></asp:TextBox></div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Primary Email:</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtEmail" runat="server" Width="234px"></asp:TextBox></div>
			        </div>
		        </div>
		    </fieldset>
	    </div>
	    
        <div class="fields">
		    <div class="groupheader">New Password</div>
		    <fieldset id="Fieldset1">
		          <div class="line">
		          <em>If you would like to change your password, enter it below.  Leave these fields blank to keep your password unchanged.</em>
		          </div>
		          <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Password</div>
				        <div class="input horizontal">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="disableAutoComplete"></asp:TextBox>
                            Confirm: <asp:TextBox ID="txtPasswordConfirm" runat="server" TextMode="Password"></asp:TextBox></div>
			        </div>
		        </div>
		    </fieldset>
	    </div>

        <asp:Panel ID="pnlPreferences" runat="server">
        </asp:Panel>
		<div id="Email_Signature" class="groupshell" >
			<div id="Email_Signature_header" class="groupheader">Email Signature</div>
				<fieldset id="group_Email_Signature_fieldset">
				<div class="line">
					<div class="field vertical" id="outer_Email_Signature">
						<div class="label vertical">
							Upload Signature Image
						</div>
						<div class="input vertical">
						<asp:FileUpload ID="oFile" runat="server" />
						</div>
						<img src="" runat="server" id="signature" height="50" alt="" border="0" />
					</div>
				</div>
				</fieldset>
			</div>
		</div>
	    <p>
        <asp:Button ID="btnUpdate" runat="server" Text="Update Preferences" OnClick="btnUpdate_Click" /></p>
    </asp:Panel>
 
    <script language="JavaScript" type="text/javascript">
    // this brutally clears the password in firefox, which refuses to acknowledge autocomplete="off"
    function clearPwBox()
    {
        if (document.getElementById) {
            var pw = document.getElementById('ctl00_content_cph_txtPassword');
            if (pw != null)
            {
                pw.value = '';
            }
        }
    }
    window.setTimeout("clearPwBox()", 100);
    </script> 
       
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeBehind="UploadOrderSubmit.aspx.cs" Inherits="Affinity.UploadOrderSubmit" Title="Upload Order Submit" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2 id="header" runat="server">Import Tract Search / Equity Orders</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:Panel ID="pnlForm" runat="server" EnableViewState="true">
        <div class="fields">
		    <fieldset id="general">
		    	
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Originator</div>
				        <div class="input horizontal">
                  <asp:DropDownList ID="ddNewOriginator" runat="server">
                  </asp:DropDownList>
                </div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
                <div class="label horizontal width_125">County</div>
				        <div class="input horizontal">
                  <asp:TextBox ID="txtPropertyCounty" runat="server" Width="150" CssClass="textbox"></asp:TextBox>
    
                  <ajaxToolkit:AutoCompleteExtender 
                      runat="server" 
                      ID="txtPropertyCounty_ac" 
                      TargetControlID="txtPropertyCounty"
                      ServiceMethod="GetCounties"
                      ServicePath="AutoComplete.asmx" 
                      MinimumPrefixLength="1" 
                      CompletionInterval="500"
                      EnableCaching="true"
                      CompletionSetCount="12" />
			        	</div>
			        </div>
		    		</div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Transaction Type</div>
				        <div class="input horizontal">
                  <asp:RadioButtonList CssClass="radiobuttonlist" RepeatDirection="Horizontal" ID="ddTransactionType" runat="server">
                  	<asp:ListItem Value="Purchase" />
                  	<asp:ListItem Value="Refinance" />
                  	<asp:ListItem Selected="true" Value="Equity" />
                  </asp:RadioButtonList>
                </div>
                <div class="horizontal" style="padding-top:5px">
	                <div class="label horizontal">Tract Search</div>
					        <div class="input horizontal">
	                    <asp:CheckBox ID="TractSearch" runat="server" />
				        	</div>
				        </div>
			        </div>
		        </div>
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label  horizontal width_125">Upload File:<br />
				        <em>Select Excel file</em>
				        </div>
				        <div class="input horizontal">
                    <asp:FileUpload ID="fuAttachment" runat="server" CssClass="upload" multiple="multiple" />
			        </div>
		        </div>		        
		        
		    </fieldset>
	    </div>
    </asp:Panel>
    
    <asp:Panel ID="pnlResults" runat="server" EnableViewState="false" Visible="false">
	    <p ID="pResults" runat="server">
	    </p>
    </asp:Panel>

    <p>
        <asp:Button ID="btnSubmit" runat="server" Text="Import Orders" OnClick="btnSubmit_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
    </p>
    <p>&nbsp;</p>
    <p>&nbsp;</p>
    <p>
    	<asp:Panel ID="pnlImportForm" runat="server" EnableViewState="true">
    		<h2>Import Order Form Entries from AffinityIsTitle.com Web site</h2>
        <div class="fields">
		    <fieldset id="general">
		    	
		        <div class="line">
			        <div class="field horizontal">
				        <div class="label horizontal width_125">Originator</div>
				        <div class="input horizontal">
                  <asp:DropDownList ID="ddNewOriginatorImport" runat="server">
                  </asp:DropDownList>
                </div>
			        </div>
		        </div>
		    	
		        <div class="line">
			        <div class="field horizontal">
				        <asp:Button ID="btnSubmitImport" runat="server" Text="Import Orders From Web Site" OnClick="btnSubmitImport_Click" />
        				<asp:Button ID="btnCancelImport" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
			        </div>
		        </div>

		    </fieldset>
	    </div>
    </asp:Panel>
    
    <asp:Panel ID="pnlImportResults" runat="server" EnableViewState="false" Visible="false">
	    <p ID="pImportResults" runat="server">
	    </p>
    </asp:Panel>
    </p>
     

</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="Error.aspx.cs" Inherits="Affinity.Error" MasterPageFile="~/AffinityTemplate.master"%>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pain_one_cph" runat="server">
    <h2>An Error Has Occured</h2>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">

    <p>We apologize for the inconvenience but an error has occured.
    You may wish to return to the previous page and ensure that
    all form fields have been entered properly.  
    </p>
    
    <p><b>NOTE:</b> If your session has expired, you may
    <a href="Default.aspx">login again</a>.</p>
    
    <p>If you continue to experience problems, please contact your account representative
    and try to provide as much information as necessary to reproduce the error.
    This will allow our technical team to locate and fix the issue.</p>
    
    <p>Thank you for your assistance.</p>
    
</asp:Content>
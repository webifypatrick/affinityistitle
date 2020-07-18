<%@ Page Language="C#" MasterPageFile="~/AffinityTemplate.master" AutoEventWireup="true" CodeBehind="FeeFinder.aspx.cs" Inherits="Affinity.FeeFinder" Title="GFE Calculator" %>
<%@ MasterType virtualpath="~/AffinityTemplate.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pain_one_cph" runat="server">
	<link href="feefinder/style.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="feefinder/jquery-1.3.2.js"></script>
	<script type="text/javascript" src="feefinder/jquery.scrollTo.js"></script>
	<script type="text/javascript" src="feefinder/jquery-overrides.js"></script>
	<script type="text/javascript" src="FeeFinderJS.aspx"></script>
	<script type="text/javascript" src="feefinder/static.js"></script>
	<script type="text/javascript" src="feefinder/feefinder.js"></script>
</asp:Content>

<asp:Content ID="main_content" ContentPlaceHolderID="content_cph" runat="server">
	<iframe id="widgetFrame" width="780" height="830" src="http://www.smartgfecalculator.com/Widget/AutoLogin?clientKey=6586b213-06d9-4e4a-8c01-b18744c1d50f&officekey=46c989ae-2849-4f3d-80f5-679a23af1620" frameborder="0"></iframe> <div><a href="http://www.smartgfecalculator.com/learnmore" target="_blank"><img alt="" src="http://www.smartgfecalculator.com/Widget/LinkImage" style="border: none;" /></a></div>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Redirect.aspx.cs" Inherits="Affinity.Redirect" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
  "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title>One moment please...</title>
	
	<style type="text/css">
		#spacer {height: 130px;}
		#container {text-align: center;}
		#msgbox {padding: 35px; border: 1px solid #aaaaaa; background-color: #dddddd; width: 250px; margin: auto;}

		* {font-family: arial,helvetica; font-size: 9pt;}
		a {text-decoration: none; color: #000000;}
		a:hover {text-decoration: underline;}
	</style>

    <script type="text/javascript">
    function redirect(url)
    {
        self.location=url;
        setTimeout("redirect('" + url + "')", 10000);        
    }
    </script>
    	
</head>

<body id="body" runat="server">

<div id="spacer"></div>
<div id="container">
	<div id="msgbox">
		<a href="#">One moment please...</a>
	</div>
</div>

</body>

</html>

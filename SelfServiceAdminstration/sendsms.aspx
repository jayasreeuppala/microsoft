﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sendsms.aspx.cs" Inherits="SelfServiceAdminstration.sendsms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        mobile
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        message
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    
    </div>
    </form>
</body>
</html>
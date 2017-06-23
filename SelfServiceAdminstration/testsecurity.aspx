<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testsecurity.aspx.cs" Inherits="SelfServiceAdminstration.testsecurity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
    <div>
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="encrypty" />
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="decrypt" />
    
    </div>
    </form>
</body>
</html>

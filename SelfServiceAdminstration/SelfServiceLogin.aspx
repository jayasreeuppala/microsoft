<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelfServiceLogin.aspx.cs" Inherits="SelfServiceAdminstration.SelfServiceLogin"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<style type="text/css">
        .style3
        {
            color: #B3322D;
            border-left-color: #A0A0A0;
            border-right-color: #C0C0C0;
            border-top-color: #A0A0A0;
            border-bottom-color: #C0C0C0;
        }
        .style4
        {
        }
        .menudef {
            position: relative;
            left : 0px;
            padding: 1px;
            border-width: 0px;
            cursor: hand;
        }
        .menuOver {
            position: relative;
            left: -1px;
            padding: 1px;
            border: 1px #00f7ff solid;
            cursor: hand;
        }
    </style>
<head id="Head1" runat="server">
    <title>SSA Login</title>
</head>
<body>
<script src="/imagestyles/WebResource.axd" type="text/javascript"></script>
<script src="/imagestyles/WebResource(1).axd" type="text/javascript"></script>
<link href="/imagestyles/style.css" rel="stylesheet" type="text/css">
<link rel="stylesheet" type="text/css" href="/imagestyles/glowtabs.css">
<link rel="stylesheet" type="text/css" href="/imagestyles/slidingdoors.css">
<link rel="stylesheet" type="text/css" href="/imagestyles/bluetabs.css">
        <script type="text/javascript" src="/imagestyles/dropdowntabs.js">
</script>
<link rel="stylesheet" type="text/css" href="/imagestyles/ddcolortabs.css">
<link rel="stylesheet" type="text/css" href="/imagestyles/halfmoontabs.css">
<style type="text/css">
    .style1
    {
        width: 380px;
    }
    .topcorner{
   position:absolute;
   top:300;
   right:100;
  }

    #dialog {
    display: block;
      position:absolute;
   top:300;
   right:100;
}

.ui-dialog-title, .ui-dialog-content, .ui-widget-content {
    font-family: "Trebuchet MS", "Helvetica", "Arial",  "Verdana", "sans-serif";
    font-size: 62.5%;
}

.modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 400px;
        height: 200px;
    }
</style>
        <form runat="server" autocomplete="off">
         

                           

    <div>
   <table style="width:100%;  font-family:Verdana; font-size:small">
<tbody><tr>
<td align="right" valign="top">
    <table style="width:100%">
    <tbody><tr>
    <td><table align="left">
<tbody><tr>
<td>
<table>
<tbody><tr>
<td>
<div style="position:absolute; left:0; top:0px;">&nbsp<img src ="/imagestyles/Mahindralogo.png"> </div>
</td>
</tr>
</tbody></table>
</td>  
</tr>
</tbody></table>  
        </td>
    <td align="right">
        <img src="/imagestyles/mahindrarise.png" height=50 width=200>
		<br>
		<br>
		<br>
		<br>
		<br>
		<br>
    </td>
    </tr>   
    </tbody></table>
   <div style="background:silver" class="QuestionSectionHeader color_NPContentHeader">
    </div>
</td>
</tr>
</tbody></table>
    </div>
    <div>
    <div>
  <table width=100%>
<tr>
<td align=center>
    <div style="background:#eb2d39" class="QuestionSectionHeader color_NPContentHeader">
    <table style="width:100%">
     <tbody><tr align="left">
    <td align="left" style="color:#333333; font-weight:bold;font-size:large"><a href="SSAHome.aspx" style="color:white;">Home</a></td>
    <td align="center" style="color:white; font-weight:bold;font-size:x-large">Mahindra Self Service Portal</td>
    <td align="right" style="color:white; font-weight:bold;font-size:large">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</td>
</tr>
</table>
    </div>
 <div>   

<br>   

          <asp:Label ID="Errorlabel" runat="server" Text="" ForeColor=Red></asp:Label>
    <br>
    <br>
    <center>
        <table style="font-weight: bold">
        <tbody><tr align="left">
            <td align="right" colspan="2">
                
                &nbsp;</td>
            </tr>


        <tr align="left">

            <td style= "color:white" class="style3" bgcolor="#eb2d39" colspan="2">Please authenticate using your token id and password</td>

            </tr>
            <tr>
            <td align="left" class="style4">Login ID: </td>
            <td align="left">
                <asp:TextBox ID="userNameTxt" runat="server" CausesValidation="True" 
                    AutoCompleteType="Disabled"></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td align="left" class="style4">Password:</td>
            <td align="left">
                <asp:TextBox ID="passwordTxt" runat="server" TextMode=Password 
                    AutoCompleteType="Disabled"></asp:TextBox>
            </td>
            </tr>
           
            <tr>
            <td class="style4">
                &nbsp;</td>
            <td align="left">
               
                &nbsp;</td>
            </tr>

            <div runat=server id="captchadiv" visible=false>
         <tr>
         <td colspan=2>
           <asp:Label ID="lblmsg" runat="server" Font-Bold="True" 
	ForeColor="Red" Text=""></asp:Label>
    
         </td>
         </tr>
         <tr>
         <td>
         Please Enter Valid Captcha
         </td>
         <td >
         <asp:TextBox ID="txtimgcode" runat="server"></asp:TextBox>
    
         </td>
         </tr>
         <tr>

         <td>
         <asp:Image ID="Image1" runat="server" ImageUrl="~/CImage.aspx"/>
         </td>
         </tr>
         </div>
            <tr>
            <td class="style4">
                &nbsp;</td>
            <td > 
                <asp:Button ID="Loginbutton"  runat="server" Text="Login" onclick="Login_Click" /> 
               <asp:LinkButton ID="dummylink" runat="server" ></asp:LinkButton>
             
                &nbsp;
                <input type="reset" value="Clear">
            </td>
            </tr>
            <tr><td colspan=3></td></tr>
        </tbody></table>
        
        <br>
        &nbsp;</center>

     
    </div>

  <table width=100%>
<tr>
    <br>
    <br>
    <br>
    <br>
    
<td style="color:#333333;font-family:Verdana;font-size:large;font-weight:bold;">
<marquee style="color:#FFFFFF" bgcolor=#eb2d39 scrolldelay="110" DIRECTION="RIGHT" >  Powered by Corporate IT  </marquee>
</td>
</tr>
</table>
    </div>
    </form>
    </div>
    
</body>
</html>
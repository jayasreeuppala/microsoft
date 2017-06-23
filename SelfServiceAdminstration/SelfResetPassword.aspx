<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelfResetPassword.aspx.cs" Inherits="SelfServiceAdminstration.SelfResetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html >
<head>
<title>
	Reset your password
</title>
<link media="screen" href="/imagestyles/passwdcheck.css" type="text/css" rel="stylesheet"/>
    
    <style type="text/css">
        .style3
        {
        }
        .style4
        {
            width: 174px;
        }
        .style6
        {
            width: 317px;
        }
        .style7
        {
            width: 172px;
        }
        .style8
        {
            width: 190px;
        }
        </style>
    <script src="/imagestyles/passwdcheck.js" type="text/javascript"> </script>
    </head>
<body>
    <form id="form1" runat="server" autocomplete="off">



<script src="/imagestyles/WebResource.axd" type="text/javascript"></script>


<script src="/imagestyles/WebResource(1).axd" type="text/javascript"></script>


<div>

	<input type="hidden" name="__EVENTVALIDATION" id="__EVENTVALIDATION" value="/wEWBQLv4ojSCgL12efZBALW44bSBAKFqK2SBgLLss2aAQqG86M6GaY5bKuiioeFeYZLAATT">
</div>

  <link href="/imagestyles/style.css" rel="stylesheet" type="text/css">
<link rel="stylesheet" type="text/css" href="/imagestyles/glowtabs.css">
<link rel="stylesheet" type="text/css" href="/imagestyles/slidingdoors.css">
<link rel="stylesheet" type="text/css" href="/imagestyles/bluetabs.css">
        <script type="text/javascript" src="/imagestyles/dropdowntabs.js">


</script>


<link rel="stylesheet" type="text/css" href="/imagestyles/ddcolortabs.css">


<link rel="stylesheet" type="text/css" href="/imagestyles/halfmoontabs.css">
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
<td>    </td>
<td>  <img src="/imagestyles/techmlogo4.png"> </td>

</tr>
</tbody></table>
</td>  
</tr>
</tbody></table>
    
        </td>
    <td align="right">
      
    </td>
    </tr>   
    </tbody></table>
    <div style="background:silver" class="QuestionSectionHeader color_NPContentHeader">
    <table style="width:100%">
     <tbody><tr align="left">
    <td class="style8">
        <span id="mymenu_lblwelcome" style="font-family:Verdana;font-size:X-Small;">Welcome:</span>
        
        <span id="mymenu_lbluser" style="font-family:Verdana;font-size:X-Small;">
            <asp:Label ID="displayuser" runat="server" Text=""></asp:Label></span>
            <a href="SSAHome.aspx" style="color:#333333;">Home</a>
    </td>
    
  
    
   
    
     <td align="center" style="color:#333333; font-weight:bold;font-size:x-small">Mahindra Self Service Portal</td>
    <td align="left" style="color:#333333; font-weight:bold;font-size:x-small">
    
    <div id="mymenu_PanelMenu">
	
     
      
        
   
    
    
    <span id="mymenu_Label1">  |  </span>
      
    
    <a id="mymenu_lnklogout" href="SelfServiceLogin.aspx" style="color:#333333;font-family:Verdana;font-size:X-Small;font-weight:bold;">Logout</a>
            
</div>
     
   
	</td>
    </tr>
    </tbody></table>
    </div>
</td>
</tr>
</tbody></table>
</div>
       
<div>


<table align="center" width="700px">
<tbody><tr>
<td class="style6">
<div style="border-color:Gray;width: 681px; border-style:solid; left:20%">

 <div style="background-color:Gray;width: 681px; font-size:x-small; color:White; font-family:Verdana" align="center"><b>
     Reset your password</b>
 </div>

   <table style="width: 681px; font-size: x-small; ">
   <tbody><tr>
   <td class="style3" colspan="2">
       &nbsp;<img alt="" height="30" src="/imagestyles/ProfileLg.gif" width="30">&nbsp; <b>Answers 
       verified successfully</b>
       </td>
   <td>
   
   
   </td>
     
   </tr>
    <tr>
        <td colspan="2">
            <table  style="width:124%; font-family: Verdana; font-size: xx-small;">
                <tbody><tr>
                    <td colspan="2" style="font-weight: bold;">
                        Account Status</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style4">
                        Display name</td>
                    <td>
                        <span id="lblDisplayName">
                            <asp:Label ID="username" runat="server" Text="" Width='300px'></asp:Label></span>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style4">
                        Account status</td>
                    <td>
                        <span id="lblAccountStatus">
                            <asp:Label ID="activestatus" runat="server" Text="" Width='300px'></asp:Label></span>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style4">
                        Last Logon</td>
                    <td>
                        <span id="lblLastLogon">
                            <asp:Label ID="lastlogon" runat="server" Text="" Width='300px'></asp:Label></span>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style4">
                        Password status</td>
                    <td>
                        <span id="lblPasswordStatus">
                            <asp:Label ID="pwdstatus" runat="server" Text="" Width='300px'></asp:Label></span>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style4">
                        Password last changed</td>
                    <td>
                        <span id="lblPasswordLastChanged">
                            <asp:Label ID="pwdlastchange" runat="server" Text="" Width='300px'></asp:Label></span>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
             
                <tr>
                    <td class="style4">
                        Account created</td>
                    <td>
                        <span id="lblAccountCreated">
                            <asp:Label ID="accountcreated" runat="server" Text="" Width='300px'></asp:Label></span>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
              
               
                <tr>
                    <td colspan="2" style="font-weight: bold;">
                        Reset your password</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </tbody></table>
        </td>
        </tr>   
        <tr>
            <td>
            <table>
            <tbody>
              
            
                <tbody><tr>
                    <td align="left" class="style7" style="top: 0px">
                        New Password
                    </td>
                    <td align="left">
                        
                        
                        <asp:TextBox ID="TextBox1" runat="server"  onkeyup="EvalPwdStrength(document.forms[0],this.value);" TextMode=Password  ></asp:TextBox>
                        
                        
                        
                        
                    </td>
                </tr>
            <tr>
            <td align="left" class="style7">
                Confirm Password
            </td>
            <td>
                <asp:TextBox ID="reenterpwd" runat="server" TextMode=Password></asp:TextBox>
                
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
        <tr>
            <td class="style7">
                &nbsp;</td>
            <td>
                <asp:ImageButton ID="ImageButton1" runat="server" 
                    ImageUrl=  "/imagestyles/cmd_Reset.gif" style="border-width:0px;height: 23px" 
                    onclick="ImageButton1_Click" />
                
                <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
                
            </td>
            <td>
                &nbsp;</td>
        </tr>
            </tbody></table>
            </td>
            
            
            
        </tr>
        
     
        
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" class="pwdChkTbl1" xmlns="http://www.microsoft.com/MSCOM/MNP2/Schemas">
                    <tbody><tr>
                        <td align="right" class="pwdChkTd1">
                            &nbsp;</td>
                        <td class="pwdChkTd2" valign="top">
                            <br>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="pwdChkTd1" style="font-family: Verdana; font-size: x-small">
                            Strength:</td>
                        <td class="pwdChkTd2" valign="top">
                            <table cellpadding="0" cellspacing="0" class="pwdChkTbl2" style="font-family: Verdana; font-size: x-small">
                                <tbody><tr style="font-family: Verdana; font-size: x-small">
                                    <td class="pwdChkCon0" id="idSM1" align="middle" width="25%">
                                        <span style="FONT-SIZE: 1px">&nbsp;</span><span id="idSMT1" style="display: none;">Weak</span></td>
                                    <td class="pwdChkCon0" id="idSM2" style="BORDER-LEFT: #fff 1px solid" align="middle" width="25%">
                                        <span style="FONT-SIZE: 1px">&nbsp;</span><span id="idSMT0" style="display: inline; font-weight: normal; color: rgb(102, 102, 102);">Not 
                                rated</span><span id="idSMT2" style="display: none;">Medium</span></td>
                                    <td class="pwdChkCon0" id="idSM3" style="BORDER-LEFT: #fff 1px solid" align="middle" width="25%">
                                        <span style="FONT-SIZE: 1px">&nbsp;</span><span id="idSMT3" style="display: none;">Strong</span></td>
                                    <td class="pwdChkCon0" id="idSM4" style="BORDER-LEFT: #fff 1px solid" align="middle" width="25%">
                                        <span style="FONT-SIZE: 1px">&nbsp;</span><span id="idSMT4" style="DISPLAY: none">BEST</span></td>
                                </tr>
                            </tbody></table>
                        </td>
                    </tr>
                </tbody></table>
            
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3"><div align="left" style="font:font-family: Verdana; font-size: xx-small;">
                <b><font color="red">Note:</font></b> To better protect your account, make sure that your password is memorable for you but difficult for others to guess. Do not share your password with anyone, and never use the same password that you've used in the past. For security purposes, your new password must be a minimum of Eight characters long. A strong password contains a combination of uppercase and lowercase letters (remember that your password is case sensitive), numbers, and special characters which are configurable are @#$%^&amp;+=</div>
            </td>
        </tr>
        
        
        <tr>
            <td class="style6">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    
   </tbody></table>
   


</div>
</td></tr>
   
    </tbody></table>


    




</div>




    </form>
    <table width=100%>
<tr>
<td style="color:#333333;font-family:Verdana;font-size:small;">


   Powered by Corporate IT
</td></tr>
</table>

</body></html>

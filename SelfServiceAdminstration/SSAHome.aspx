<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master"  CodeBehind="SSAHome.aspx.cs" Inherits="SelfServiceAdminstration.SSAHome" %>


<asp:Content ContentPlaceHolderID="MainContent" runat="server" >
    <div align="left" style="background-color:#eb2d39; font-size:smaller; color:White; font-family:Verdana">
<table style="width:100%" style="background-color:#eb2d39; font-size:small; color:White; font-family:Verdana" border="0">
     <tbody><tr align="left">
    <td class="style1">
        <span id="menu1_lbluser" style="font-family:Verdana;font-size:Small;">
        <asp:Label ID="userLabel" runat="server" Text="" Font-Bold=true></asp:Label></span>
    </td>
    <td align="left" style="color:white; font-weight:bold;font-size:large">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
        Mahindra Self Service Portal</td>
    <td align="right" style="color:white; font-weight:bold;font-size:small">
        <asp:LinkButton ID="LinkButton1" runat="server" style="color:white;" OnClick="LinkButton1_Click1" >Logout</asp:LinkButton>
     
	</td>
    </tr>
    </tbody></table>
    </div>
    <div style="position:absolute; top:250px; bottom:120px; right:380px; left:400px; border-color:#eb2d39; border-style:solid">
    <table cellspacing="0" cellpadding="0" border="0" width=100%>
    <tbody>
    <tr>
    <td colspan="2">

    </td>
    </tr>
      <tr>
        <td colspan="2">
            <asp:Panel ID="Panel1" runat="server" Visible=false>
             <table  style="width:100%; font-family: Verdana; font-size: xx-small;" >
                <tbody>
                <!--
                <tr>
                    <td colspan="2" style="font-weight: bold;">
                        Account Status</td>
                    <td>
                        &nbsp;</td>
                </tr>
                -->
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
                <!--
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
                -->
                <!--
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
              -->
               <tr>
                    <td class="style4">
                        Mobile Number</td>
                    <td>
                        <span id="Span1">
                            <asp:Label ID="mobileno" runat="server" Text="" Width='300px'></asp:Label></span>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
<br>
                    <td colspan="2" style="font-weight: bold;font-size:x-small;" >
<br>
                        **Please create a ticket in SAP HR / Send a mail to MITC REQUEST team for changing 
                        mobile #</td>
                    <td>
                        &nbsp;</td>
                </tr>

            </tbody></table>
            </asp:Panel>
           
        </td>
        </tr>
    <tr>
    <td colspan="2">
        &nbsp;</td>
    </tr>
    <tr>
    <td style="PADDING-RIGHT: 5px">
    <div class="OptionButton" onmouseover="this.style.backgroundPosition=&#39;bottom&#39;;"  onmouseout="this.style.backgroundPosition=&#39;top&#39;;" style="background-position: 50% 0%;">
    <div id="menuItem2_1" class="menudef" onmouseover="this.className=&#39;menuOver&#39;" onmouseout="this.className=&#39;menudef&#39;">              
    <table style="cursor:pointer">
    <tbody>
   
    <tr>
    
    <td>
     <a id="ctl00_ContentPlaceHolder_hlMyProfile" href="SSARegisterProfile.aspx">
    <img style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" src="/imagestyles/ProfileLg.gif" id="IMG1" > 
    </a>
    </td>
    <td colspan=100>
    <span>
	<b>My Profile (Register/Edit)</b>
	<br>
	<a id="A2" href="SSARegisterProfile.aspx">Click here to setup or maintain the personal security profile</a>
    </span>
    </td>
    </tr>
    
    </tbody></table>    </div>         
                  
    </div>
   
    </td>
    </tr>
	</tbody>
    
    </table>
    <div id="Div1" class="menudef" onmouseover="this.className=&#39;menuOver&#39;" onmouseout="this.className=&#39;menudef&#39;">  
    <table cellspacing="0" cellpadding="0" border="0" width="500px" style="cursor:pointer" onmouseover="this.style.backgroundPosition=&#39;bottom&#39;;" onmouseout="this.style.backgroundPosition=&#39;top&#39;;">
    <tbody>
    <tr>
    <td style="PADDING-RIGHT: 5px">
    <a href="PasswordAuth.aspx"> 

    <img style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" src="/imagestyles/PasswordLg.gif"> 
    </a>
    </td>
<br>
    <td><spanclass >
    
    <b>Reset Password</b><span class="OptionButtonText" ><br>
<a href="PasswordAuth.aspx" >Click 
    here to reset the password by 
    correctly answering the personal security profile 
    questions.</a></span><br></spanclass>
    </td></tr>
   
    </tbody></table>
    <br>
    </div>
    
    </div>
<table width=100%>
<tr>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
<td style="color:#333333;font-family:Verdana;font-size:large;font-weight:bold;">
<marquee style="color:#FFFFFF" bgcolor=#eb2d39 scrolldelay="110" DIRECTION="RIGHT" >  Powered by Corporate IT  </marquee>
</td>
</tr>
</table>
</asp:Content>
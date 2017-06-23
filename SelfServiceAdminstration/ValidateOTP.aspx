<%@ Page Language="C#"   AutoEventWireup="true"  CodeBehind="ValidateOTP.aspx.cs" Inherits="SelfServiceAdminstration.ValidateOTP" MasterPageFile="~/Site.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server" >
    
    <style type="text/css">
        .style7
        {
            width: 172px;
        }
        </style>
    
<script>
    function ValidatePasswords() {

        var pwd1 = document.getElementById("TextBox1").value;

        var pwd2 = document.getElementById("reenterpwd").value;
        if (document.getElementById("TextBox1").value == "") {
            alert("Please enter password to reset");
            return false;
        }
        if (document.getElementById("reenterpwd").value == "") {
            alert("Please enter password to reset");
            return false;
        }
        if (pw1 != pwd2) {
            alert("Passwords Mismatch");
            return false;
        }
        return true;

    }
</script>

<div>
    <div style="background:#eb2d39" class="QuestionSectionHeader color_NPContentHeader">
        <table style="width:100%">
            <tr align="left">
                <td align="left" style="color:#333333; font-weight:bold;font-size:small">
                    <a href="SSAHome.aspx" style="color:white;">Home</a>
                </td>
                <td align="center" style="color:white; font-weight:bold;font-size:large">
                    Mahindra Self Service Portal</td>
                <td align="right" style="color:white; font-weight:bold;font-size:small">
                    <div id="mymenu_PanelMenu">
                        <a id="mymenu_lnklogout" href="SelfServiceLogin.aspx" 
                            style="color:white;font-family:Verdana;font-size:small;font-weight:bold;">
                        Logout</a>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</td>
</tr>
</tbody></table>


      <table>
            <tbody>
              
            
                <tbody>
                <tr>
            <td class="style7" colspan=2 nowrap>
                OTP 
                is sent to your registered Mobile
                <asp:Label ID="Label2" runat="server"></asp:Label>
            </td>
            </tr>
            
            <tr>
            <td align="left" class="style7">
                Enter OTP
            </td>
            <td>
                <asp:TextBox ID="otpval" runat="server" TextMode=Password 
                    AutoCompleteType="Disabled"></asp:TextBox>
                
            </td>
            <td>
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
         <td >
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
            <td class="style7">
                &nbsp;</td>
            <td>
                
                <asp:Button ID="Button1" runat="server" Text="Please Validate OTP" 
                    onclick="Button1_Click" />
            </td>
            <td>
&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp;&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp;&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp;&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp;
<img src="/imagestyles/OTPpass.jpg" height=380 width=290 align="center">
	    </td>
        </tr>
        <tr>
        <td>&nbsp;</td>
            <td class="style7" colspan=2 nowrap>
                Click here to generate new <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">OTP</asp:LinkButton>
            </td>
            </tr>
        <tr>
            <td class="style7">
                &nbsp;</td>
            <td>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
               
            </td>
            <td>
                &nbsp;</td>
        </tr>
            </tbody></table>


</div>
    
<table width=100%>
<tr>
<td style="color:#333333;font-family:Verdana;font-size:large;font-weight:bold;">
<marquee style="color:#FFFFFF" bgcolor=#eb2d39 scrolldelay="110" DIRECTION="RIGHT" >Powered by Corporate IT  </marquee>
</td>
</tr>
</table>
</asp:Content>
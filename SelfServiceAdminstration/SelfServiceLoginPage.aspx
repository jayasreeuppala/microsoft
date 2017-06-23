<%@ Page Title="" Language="C#" MasterPageFile="~/Site3.Master" AutoEventWireup="true" CodeBehind="SelfServiceLoginPage.aspx.cs" Inherits="SelfServiceAdminstration.SelfServiceLoginPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <div>
     <form runat="server" autocomplete="off">
  

  <table width=100%>
<tr>
<td align=center>
    <div style="background:#eb2d39" class="QuestionSectionHeader color_NPContentHeader">
    <table style="width:100%">
     <tbody><tr align="left">
    <td align="left" style="color:#333333; font-weight:bold;font-size:small"><a href="SSAHome.aspx" style="color:white;">Home</a></td>
    <td align="center" style="color:white; font-weight:bold;font-size:large">Mahindra Self Service Portal</td>
    <td align="right" style="color:white; font-weight:bold;font-size:small">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</td>
</tr>
</table>
    </div>



      <div>   
    <div style="color:White; background-image:url(Images/menu_bg.jpg); background-repeat:repeat-x">
    &nbsp;
    Sign in
    </div>   
    &nbsp;
    Authenticate yourself
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
            <td style="color: #FF0000" class="style4" bgcolor="#E0E0E0" colspan="2">
                <span class="style3">Sign in</span>
            </td>
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
            
                &nbsp;
                <input type="reset" value="Clear">
            </td>
            </tr>
            <tr><td colspan=3></td></tr>
        </tbody></table>
        
        <br>
        &nbsp;</center>
    <br>
    <br>
    <br>
     
    </div>
    

    

   



  <table width=100%>
<tr>
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
    <form>
    </form>
</asp:Content>

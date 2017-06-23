<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordAuth.aspx.cs" Inherits="SelfServiceAdminstration.PasswordAuth"  MasterPageFile="~/Site1.Master"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server" >
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
            <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="dummylink"
    CancelControlID="Button3" BackgroundCssClass="modalBackground" >
</cc1:ModalPopupExtender>
            

                
                    
                    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" style = "display:none">
    <span style="font-size:large;font-style:italic;font-weight:bold;color:red">Alert - User Id already in use</span>  <br />
    

    
        <table border="0">
            <tr>
            <td colspan="2">

                If you click on continue,previous user's session will be terminated.
            </td>
                </tr>
                    <tr>
            <td colspan="2">

                <hr />
            </td>
                </tr>
                        <tr>
            <td colspan="2">

                Do you wish to continue?
            </td>
                </tr>
            <tr>
                        <td colspan="2">

                <hr />
            </td>
                </tr>
            <tr>

                <td>
                    
                    <asp:Button ID="continue"  runat="server" Text="Continue.." OnClick="continue_Click" />
                </td>
                <td>

                    <asp:Button ID="Button3" runat="server" Text="Cancel" />
                </td>
            </tr>
        </table>

    
</asp:Panel>            
      
              
    
    
   
    
     
    <table width="100%" border="0">
    <tr style="background:#eb2d39" class="QuestionSectionHeader color_NPContentHeader">
    <td colspan="3" align="center" style="color:white; font-size:large;font-weight:bold">Mahindra Self Service Portal</td>
    </tr>
    

    <tr>

        <td><img src ="/imagestyles/Winlogo.png" /> </td>
        <td>  
    
    <div style="border-color:aqua; border-style:solid; width:500px; left:20%;align-content:center" >
 
 
 
 
    

  <div style="background-color:#eb2d39; font-size:Large; color:White; font-family:Verdana;" >
    <b>Search for your user profile</b>
 </div>
 <div style="background-color:Gray; color:White; font-family:Verdana">
     <asp:Label ID="Errorlbl" runat="server" Text=""></asp:Label>
 </div>
   <table>
   <tr>
   <td style="font-family: Verdana; font-size: xx-small" >
       &nbsp;</td>
   <td>
        &nbsp;</td>
   </tr>
   <tr >
           <td style="font-family: Verdana; font-size: large" colspan=2>
               Please enter Token number only</td>
               </tr>
      <tr >
          <td style="font-family: Verdana; font-size: large" nowrap>
<br>
               Login ID:</td>
           <td >
<br>
               <asp:TextBox ID="txtloginid" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
               <asp:Label ID="lblMessage" runat="server" Text=" "></asp:Label>
           </td>
       </tr>
       <div runat=server id="captchadiv" >
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
           <td align="center" colspan="2">
               <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Submit" />
               <asp:LinkButton ID="dummylink" runat="server" UseSubmitBehavior="false"></asp:LinkButton>
           </td>
       </tr>
   </table>
   


</td> 
        <td><img src ="/imagestyles/MahindraSSS.jpg"></td>
    </tr>

  


<tr>
<td style="color:#333333;font-family:Verdana;font-size:small;" colspan="3">


 
    
    
 <a id="A2" href="SSARegisterProfile.aspx">Click 
    here </a> to setup or maintain the personal security profile.
 

</td></tr>
</table>


 <table width=100%>
<tr>
<td style="color:#333333;font-family:Verdana;font-size:large;font-weight:bold;">
<marquee style="color:#FFFFFF" bgcolor=#eb2d39 scrolldelay="110" DIRECTION="RIGHT" >Powered by Corporate IT  </marquee>
</td>
</tr>
</table>
    </asp:Content>
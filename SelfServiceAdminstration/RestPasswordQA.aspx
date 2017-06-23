<%@ Page Language="C#"   EnableViewState="false"  AutoEventWireup="true" CodeBehind="RestPasswordQA.aspx.cs" Inherits="SelfServiceAdminstration.RestPasswordQA" MasterPageFile="~/Site.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server" >
    <script type="text/javascript">
        function Cancel() {
            var ok = confirm('Do you want to cancel your changes and goto main page!');
            if (ok == true)
            { return true; }
            else
            { return false; }
        }
        function noofquestions() {

            for (i = 1; i <= 6; i++) {
                var txt = 'answer' + i;
                var a1 = document.getElementById(txt);
                if(i >= 3)
                return true;
                else
                return false;
            }

        }
        function FinalValidation()
        {
            if(noofquestions())
                validate1()
        }
        function validate1() {
            
            var go;
            go = 'true';
            var firstcount=1;
            var secCount =1;
            for (i = 1; i <= 6; i++) {
                var txt = 'answer' + i;
                var a1 = document.getElementById(txt);

                var s = new String(a1.value);
                if (s.length < 3 || s.length > 15) {
                    //a1.style.backgroundColor = 'red';

                    go = 'false';
                    
                    
                }
                else { 
                
                a1.style.backgroundColor = 'white'; 
                firstcount++;
                }
            }
        if (go == 'false' && firstcount<3) {
                alert('Answer length must be between 3 to 15 character.');
                return false;
            }
            for (i = 5; i <= 6; i++) {
                var txt = 'ques' + i;
                var a1 = document.getElementById(txt);

                var s = new String(a1.value);
                if (s.length < 10) {
                    //alert('Question length must be greater than 10 characters');
                    //a1.style.backgroundColor = 'red';
                    //a1.focus();
                    
                    
                    
                        go = 'false';    
                    
                    //return false;
                }
                else {
                    a1.style.backgroundColor = 'white';
                    secCount++;
                }
            }
            if (go == 'true' && secCount>=3)
            { return true; }
            else {
                alert('Question length must be greater than 10 characters');
                return false;
            }
        }

</script>

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
</style>

    <div>
    <div style="background:#eb2d39" class="QuestionSectionHeader color_NPContentHeader">
    <table style="width:100%">
     <tbody><tr align="left">
   <td class="style1">
        <span id="mymenu_lblwelcome" style="font-family:Verdana;font-size:small; color:white; font-weight:bold"> 
        <asp:Label ID="displayuser" runat="server" Text=""></asp:Label>
        </span>
    </td>
    <td align="left" style="color:white; font-weight:bold;font-size:large">Mahindra Self Service Portal</td>
    <td align="right" style="color:white; font-weight:bold;font-size:small">
	<a href="SelfServiceLogin.aspx" style="color:white;">Logout</a>
	</td>
    </tr>
    </tbody></table>
    </div>
</div>



        <table border=0 width=50% align=center><tr><td> 
        
       <div style="border-color:#eb2d39; border-style:solid; width: 500px; height: 230px; left:100;border-style:solid;">

        <div style="position:absolute; left:0; top:250px;">&nbsp<img src ="/imagestyles/SecurityQn.jpg"> </div>
<div style="position:absolute; right:0; top:250px;"><img src ="/imagestyles/SecurityKey.jpg"> </div>
                    <table border=0  align=center >
                    <tr >
                    <td colspan=2 nowrap><div style="background:silver;"  class="QuestionSectionHeader color_NPContentHeader"><span>Please 
                        answer these questions</span> (Mandatory) 
                            <asp:Label ID="resultlable" runat="server" Text="" ForeColor="Red"></asp:Label></div></td>
                    </tr>
                       
                    <tr>
                    <td>Q1.</td>
                    <td><asp:Label ID="Label1" runat="server" answer1=""></asp:Label></td>
                    </tr>
                    <tr>
                    <td>&nbsp;</td>
                    <td colspan=2> 
                        <asp:TextBox ID="answer1" runat="server" MaxLength='250' 
                            style="width:330px;"  AutoCompleteType = "Disabled" TextMode="Password"></asp:TextBox></td>
                    </tr>

                    <tr>
                    <td>Q2.</td>
                    <td><asp:Label ID="Label2" runat="server" answer1=""></asp:Label></td>
                    </tr>

                    <tr>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="answer2" runat="server" MaxLength='250' style="width:330px;" 
                            AutoCompleteType="Disabled" TextMode="Password" ></asp:TextBox></td>
                    </tr>
                    <tr>
                    <td>Q3.</td>
                    <td></asp:TextBox><asp:Label ID="Label3" runat="server" answer1=""></asp:Label></td>
                    </tr>
                    <tr>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="answer3" runat="server" MaxLength=250 style="width:330px;" 
                            AutoCompleteType='Disabled' TextMode="Password" ></asp:TextBox></td>
                    </tr>
                    
                    <tr>
                    <td colspan=2 align=justify>
                     <asp:Button ID="save" runat="server" Text="Validate and Reset" 
                            OnClientClick=" return validate1();" onclick="save_Click" 
                            UseSubmitBehavior="true"  />
&nbsp;
                    <asp:Button ID="Cancel" runat="server" Text="Close" onclick="Cancel_Click" />
                    </td>
                    </tr>

                    </table>
                    </div></td></tr></table>
        
                   
                        
                   
                

   

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
<td style="color:#333333;font-family:Verdana;font-size:large;font-weight:bold;">
<marquee style="color:#FFFFFF" bgcolor=#eb2d39 scrolldelay="110" DIRECTION="RIGHT" >Powered by Corporate IT  </marquee>
</td>
</tr>
</table>

</asp:Content>
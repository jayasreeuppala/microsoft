<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SSARegisterProfile.aspx.cs" Inherits="SelfServiceAdminstration.SSARegisterProfile" MasterPageFile="~/Site.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server" >
   

    <script type="text/javascript">
        function Cancel() {
            var ok = confirm('Do you want to cancel your changes and goto main page!');
            if (ok == true)
            { return true; }
            else
            { return false; }
        }
        function checkduplicates() {
            var validate;
            validate = 'true';
            //alert("in duplicates " + document.getElementById("questionSet1").value)
            
            if (document.getElementById("questionSet1").value == document.getElementById("questionSet2").value) {
                alert("Question 3 and Question 4 can't be same, Please choose different questions")
                return false;
            }
            else {
                return true;
            }
            //questionSet1
            //questionSet2

        }
        function validate() {
            var go;
            go = 'true';
            if (checkduplicates()) {

                for (i = 1; i <= 6; i++) {
                    var txt = 'answer' + i;
                    var a1 = document.getElementById(txt);

                    var s = new String(a1.value);
                    if (s.length < 3 || s.length > 15) {
                        a1.style.backgroundColor = 'red';

                        go = 'false';

                    }
                    else { a1.style.backgroundColor = 'white'; }
                }
                if (go == 'false') {
                    alert('Answer length must be between 3 to 15 character.');
                    return false;
                }
                for (i = 5; i <= 6; i++) {
                    var txt = 'question' + i;
                    var a1 = document.getElementById(txt);

                    var s = new String(a1.value);
                    if (s.length < 10) {
                        //alert('Question length must be greater than 10 characters');
                        a1.style.backgroundColor = 'red';
                        //a1.focus();
                        go = 'false';
                        //return false;
                    }
                    else { a1.style.backgroundColor = 'white'; }
                }
                if (go == 'true')
                { return true; }
                else {
                    alert('Question length must be greater than 10 characters');
                    return false;
                }
            }
            else {
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
        <span id="mymenu_lblwelcome" style="font-family:Verdana;font-size:Small;color:white;font-weight:bold;">Welcome:&nbsp;
        <asp:Label ID="usernamelbl" runat="server" Text="" Font-Bold=true></asp:Label> </td>
	<td align="center" style="color:white; font-weight:bold;font-size:large">Mahindra Self Service Portal</td>
	<td align="right" style="color:white; font-weight:bold;font-size:small">
         <asp:LinkButton ID="LinkButton1" runat="server" style="color:white;" OnClick="LinkButton1_Click1" >Logout</asp:LinkButton>
	<a href="SelfServiceLogin.aspx" style="color:white;">Logout</a> </td>
</span>
    </tr>
    </tbody></table>
    </div>
</td>
</tr>
</tbody></table>
</div>



  <div class="vcontent color_NPContentBackground">
            <table cellspacing="0" cellpadding="1" width="100%" border=1>
             
              <tbody><tr>
                <td class="QuestionHeader color_NPBlue" style="PADDING-LEFT: 5px; HEIGHT: 20px" colspan=2>
                    System Settings</td>
                 </tr>
              <tr>
                <td style="HEIGHT: 109px">
                  <table cellspacing="0" cellpadding="2" width="100%">
                    
                    <tbody><tr>
                      <td valign="top" width="50%">
                        
                        <table width="100%">
                          
                          <tbody><tr>
                            <td class="style2" style="PADDING-LEFT: 5px"><span id="ctl00_ContentPlaceHolder_lblQuestionSettings">Question Settings:</span></td>
                              </tr>
                          <tr>
                            <td width="100%"><b class="vb1"></b><b class="vb2 color_NPWhite"></b><b class="vb3 color_NPWhite"></b><b class="vb4 color_NPWhite"></b>
                              <div class="vcontent color_NPWhite">
                              <table height="70" width="100%">
                                <tbody>
                                <tr>
                                <td class="SettingLabel"><span id="ctl00_ContentPlaceHolder_lblMinLength">Minimum 
                                Question Length:</span> </td>
                                <td width="10"></td>
                                <td class="SettingLabel" align="left"><span id="ctl00_ContentPlaceHolder_lblMinQuestionLength">15</span> 
                                </td></tr>
                                <tr>
                                <td class="SettingLabel"><span id="ctl00_ContentPlaceHolder_lblMaxLength">Maximum 
                                Question Length:</span> </td>
                                <td width="10"></td>
                                <td class="SettingLabel" align="left"><span id="ctl00_ContentPlaceHolder_lblMaxQuestionLength">250</span></td></tr>
                                <tr>
                                <td class="SettingLabel" colspan="3">&nbsp;</td></tr>
                                </tbody>
                                </table>
                              </div>
                              </td>
                            </tr>
                  </tbody></table>
                  </td>
                  </tr>
                  </tbody></table>
                              </td>
                      <td width="50%"><b class="vb1"></b><b class="vb2 color_NPRed"></b><b class="vb3 color_NPRed"></b><b class="vb4 color_NPRed"></b>
                        <div class="vcontent color_NPRed">
                        <table width="100%">
                          <tbody>
                          <tr>
                            <td class="SettingHeader" style="PADDING-LEFT: 5px">
                                <span id="ctl00_ContentPlaceHolder_lblAnswerSettings" class="style2">Answer 
                              Settings:</span> </td></tr>
                          <tr>
                            <td width="100%"><b class="vb1"></b><b class="vb2 color_NPWhite"></b><b class="vb3 color_NPWhite"></b><b class="vb4 color_NPWhite"></b>
                              <div class="vcontent color_NPWhite">
                              <table height="70" width="100%">
                                <tbody>
                                <tr>
                                <td class="SettingLabel"><span id="ctl00_ContentPlaceHolder_lblMinAnsLength">Minimum 
                                Answer Length:</span> </td>
                                <td width="10"></td>
                                <td class="SettingLabel" align="left">3 
                                </td></tr>
                                <tr>
                                <td class="SettingLabel"><span id="ctl00_ContentPlaceHolder_lblMaxAnsLength">Maximum 
                                Answer Length:</span> </td>
                                <td width="10"></td>
                                <td class="SettingLabel" align="left"><span id="ctl00_ContentPlaceHolder_lblMaxAnswerLength">250</span> 
                                </td></tr>
                                <tr>
                                <td class="SettingLabel" colspan="3">
                                    
                                &nbsp;
                                    </td></tr></tbody></table>
                               </div><b class="vb4 color_NPWhite"></b><b class="vb3 color_NPWhite"></b><b class="vb2 color_NPWhite"></b><b class="vb1"></b></td></tr></tbody></table>
                          
                          </div><b class="vb4 color_NPRed"></b><b class="vb3 color_NPRed"></b><b class="vb2 color_NPRed"></b><b class="vb1"></b>
                    </td>
                    </tr>
                    
                    </tbody></table>
                    
                   
                    
                    <table height="100%" cellspacing="2" cellpadding="0" width="100%">
              <tbody>
              <tr>
                <td></td></tr>
              <tr>
                <td style="MARGIN-RIGHT: 5px" valign="top" colspan="2">
                  <div>
                  <table id="ctl00_ContentPlaceHolder_tableQuestions" style="WIDTH: 100%; BORDER-COLLAPSE: collapse" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                    <tr>
                      <td>
                        <div style="background:silver" class="QuestionSectionHeader color_NPContentHeader"><span style="FONT-WEIGHT: bold">Step 1: </span><span>Please 
                        answer these questions</span> (Mandatory) </div></td></tr>
                    <tr>
                      <td colspan="2"></td></tr>
                    <tr>
                      <td colspan="2">
                        <table style="BACKGROUND: url(images/Question_Background.png) repeat-x; MARGIN: 7px 0px 7px 20px" cellspacing="0" cellpadding="3" width="97%" border="0">
                          <tbody>
                          

                          <tr>
                            <td nowrap="" width="100%"><span class="SettingLabel"><b>Question 1:</b> 
                            </span></td></tr>
                            <tr>
                            <td nowrap="" width="100%"><span class="SettingLabel">
                                <asp:Label ID="Label1" runat="server" Text="What is your Mother Maiden Name?"></asp:Label>
                            </span></td></tr>

                            <tr>
                            <td nowrap="" width="100%"><span class="SettingLabel">
                                <asp:TextBox ID="answer1" runat="server" MaxLength='250' style="width:330px;" 
                                    TextMode=Password AutoCompleteType="Disabled" ></asp:TextBox>
                            </span></td></tr>
                          

                          <tr>
                            <td nowrap="" width="100%"><span class="SettingLabel"><b>Question 2:</b> 
                            </span></td></tr>
                            <tr>
                            <td nowrap="" width="100%"><span class="SettingLabel">
                                <asp:Label ID="Label2" runat="server" Text="In what town were you born?"></asp:Label>
                            </span></td></tr>

                            <tr>
                            <td nowrap="" width="100%"><span class="SettingLabel">
                                <asp:TextBox ID="answer2" runat="server" MaxLength='250' style="width:330px;" 
                                    TextMode=Password AutoCompleteType="Disabled"></asp:TextBox>
                            </span></td></tr>


                        </tbody></table></td></tr>
                    
                    <tr>
                      <td>
                        <div style="background:silver" class="QuestionSectionHeader color_NPContentHeader"><span style="FONT-WEIGHT: bold">Step 2: </span><span>Select a 
                        question from the list and provide an answer</span> (Mandatory)</div></td></tr>
                    <tr>
                      <td colspan="2"></td></tr>
                    <tr>
                      <td colspan="2">
                        <table style="BACKGROUND: url(images/Question_Background.png) repeat-x; MARGIN: 7px 0px 7px 20px" cellspacing="0" cellpadding="3" width="97%" border="0">
                          <tbody>
                          <tr>
                            <td nowrap="" width="100%"><span class="SettingLabel"><b>Question 3:</b> 
                            </span></td></tr>
                          <tr>
                            <td>
                                <asp:DropDownList ID="questionSet1" runat="server">
                                </asp:DropDownList>
                              </td></tr>
                          <tr>
                            <td nowrap="" width="100%"><span class="SettingLabel">Answer 3: </span></td></tr>
                          <tr>
                            <td>
                                
                                <asp:TextBox ID="answer3" runat="server" MaxLength='250' style="width:330px;" 
                                    TextMode=Password AutoCompleteType="Disabled"></asp:TextBox>
                              </td></tr>
                           </tbody>
                           </table></td></tr>
                    <tr>
                      <td colspan="2"></td></tr>
                    <tr>
                      <td colspan="2">
                        <table style="BACKGROUND: url(images/Question_Background.png) repeat-x; MARGIN: 7px 0px 7px 20px" cellspacing="0" cellpadding="3" width="97%" border="0">
                          <tbody>
                          <tr>
                            <td nowrap="" width="100%"><span class="SettingLabel"><b>Question 4:</b> 
                            </span></td></tr>
                          <tr>
                            <td>
                                <asp:DropDownList ID="questionSet2" runat="server">
                                </asp:DropDownList>
                              </td></tr>
                          <tr>
                            <td nowrap="" width="100%"><span class="SettingLabel">Answer 4: </span></td></tr>
                          <tr>
                            <td>
                                
                                <asp:TextBox ID="answer4" runat="server" maxlength="250" style="width:330px;" 
                                    TextMode=Password AutoCompleteType="Disabled"></asp:TextBox>

                              </td></tr></tbody></table></td></tr>
                    <tr>
                      <td>
                        <div style="background:silver" class="QuestionSectionHeader color_NPContentHeader"><span style="FONT-WEIGHT: bold">Step 3: </span><span>Define 
                        your own questions and answers</span> (Mandatory)</div></td></tr>
                    <tr>
                      <td colspan="2"></td></tr>
                    <tr>
                      <td colspan="2">
                        <table style="BACKGROUND: url(images/Question_Background.png) repeat-x; MARGIN: 7px 0px 7px 20px" cellspacing="0" cellpadding="3" width="97%" border="0">
                          <tbody>
                          <tr>
                            <td nowrap="" width="100%"><span class="SettingLabel"><b>Question 5:</b> 
                            </span></td></tr>
                          <tr>
                            <td>
                                &nbsp;<asp:TextBox ID="question5" 
                                    runat="server" style="width:330px;" MaxLength=250 AutoCompleteType="Disabled" ></asp:TextBox>
                              </td></tr>
                          <tr>
                            <td nowrap="" width="100%"><span class="SettingLabel">Answer 5: </span></td></tr>
                          <tr>
                            <td>
                                
                                <asp:TextBox ID="answer5" runat="server" style="width:330px;" MaxLength=250 
                                    TextMode=Password AutoCompleteType="Disabled"></asp:TextBox>
                                
                              </td></tr></tbody></table></td></tr>
                    <tr>
                      <td colspan="2"></td></tr>
                    <tr>
                      <td colspan="2">
                        <table style="BACKGROUND: url(images/Question_Background.png) repeat-x; MARGIN: 7px 0px 7px 20px" cellspacing="0" cellpadding="3" width="97%" border="0">
                          <tbody>
                          <tr>
                            <td nowrap="" width="100%"><span class="SettingLabel"><b>Question 6:</b> 
                                </span></td></tr>
                          <tr>
                            <td>
                                &nbsp;<asp:TextBox ID="question6" 
                                    runat="server" MaxLength='250' style="width:330px;" 
                                    AutoCompleteType="Disabled"  ></asp:TextBox>
                              </td></tr>
                          <tr>
                            <td nowrap="" width="100%"><span class="SettingLabel">Answer 6: </span></td></tr>
                          <tr>
                            <td>
                                &nbsp;<asp:TextBox ID="answer6" 
                                    runat="server" style="width:330px;" MaxLength=250 TextMode=Password 
                                    AutoCompleteType="Disabled"></asp:TextBox>
                              </td></tr>
                          <tr>
                            <td>&nbsp;</td></tr></tbody></table>
                      </td></tr></tbody>
                    </table>
                    </div>
                    </td>
                    </tr>
              <tr>
                <td></td>
                <td style="WIDTH: 100%">
                    
                    <asp:ImageButton ID="ImageButton1" runat="server"  OnClientClick="return validate();" onclick="save_Click" ImageUrl="/imagestyles/cmd_save.gif" />
                    <asp:ImageButton ID="ImageButton2" runat="server" 
                        ImageUrl="/imagestyles/cmd_cancel.gif" onclick="ImageButton2_Click" />
                  </td>
                  </tr>
                  </tbody>
             </table>
             </div>   

    
     <table width=100%>
<tr>
<td style="color:#333333;font-family:Verdana;font-size:large;font-weight:bold;">
<marquee style="color:#FFFFFF" bgcolor=#eb2d39 scrolldelay="110" DIRECTION="RIGHT" >Powered by Corporate IT  </marquee>
</td>
</tr>
</table>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="loginScreen" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/login.css" rel="stylesheet" type="text/css" />
    
    <script src='https://www.google.com/recaptcha/api.js'></script>
    <style type="text/css">
        .auto-style1 {
            border-radius: 5px;
            width: 24%;
            position: relative;
            transition: all 5s ease-in-out;
            left: 0px;
            top: 0px;
            height: 57px;
            margin: auto;
            padding: 20px;
            background: #fff;
        }
        .auto-style2 {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            background-color: transparent;
            position: absolute;
            top: 2px;
            right: 0px;
            font-size: 36px;
            margin-left: 50px;
            padding: 16px;
        }
        .auto-style3 {
            position: absolute;
            top: 47%;
            left: 50%;
            transform: translate(-50%, -50%);
            width: 360px;
            height: 361px;
            padding: 80px 40px;
            box-sizing: border-box;
    /*background: rgba(0,0,0,.5);*/
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" margin-left="auto" margin-right="auto">
        <div class="auto-style3">
            <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate" DestinationPageUrl="CEOPostWall.aspx">
                <LayoutTemplate>
                    <table cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                        <tr>
                            <td>
                                <table cellpadding="0">
                                    <tr>
                                        <td align="center" class="td">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/image/Picture2.png" class="user" /></td>
                                       
                                    </tr>
                                    <tr>
                                       <%-- <td align="center">--%>
                                            <asp:TextBox ID="UserName" runat="server" CssClass="textbox" placeholder="Email Address"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ForeColor="Red" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        <%--</td>--%><br />
                                    </tr>
                                    <tr>
                                        <%--<td align="center">--%>
                                            <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="textbox" placeholder="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ForeColor="Red" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                       <%-- </td>--%><br />
                                    </tr>
                                    <tr>
                                      <%--  <td align="center">--%>
                                            <asp:CheckBox ID="RememberMe" runat="server" CssClass="td" Text="Remember Me" />
                                       <%-- </td>--%><br />
                                    </tr>
                                    <tr>
                                        <td <%--align="center"--%> style="color: Red;">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </td><br />
                                    </tr>
                                    <tr>
                                           <div class="g-recaptcha" data-sitekey="6Lc7q1IUAAAAAFarqQDYs1ZgXcB6TKlh_kcHzR0i"></div>

                                        <%--<td align="center">--%>
                                         
                                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" CssClass="btn-login" Text="Log In" ValidationGroup="Login1" />
                                        <asp:LinkButton  ID="btnForgotPassword" runat="server" CssClass="btn-forgot" Text="Forgot Password"></asp:LinkButton>
                                         <ajaxToolkit:ModalPopupExtender ID="popResendPassword" runat="server" TargetControlID="btnForgotPassword" PopupControlID="divResendPass" CancelControlID="cancelPass" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                        <%--</td>--%><br />
                                    </tr>
                                    <tr>
                                        <td align="center">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="center"></td>
                                        <td align="center"></td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:Login>
           
        </div>
        
        <br />
      
        <div>
             
    <%--        <asp:Button ID="btnForgotPassword" CssClass="btn-forgot" runat="server" Text="Forgot Password"/>--%>
        </div>


        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
        <%--<ajaxToolkit:ModalPopupExtender ID="popResendUserName" runat="server" TargetControlID="btnForgotUsername" PopupControlID="divResendUserName" CancelControlID="cancelEmail" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>--%>
        <%--<ajaxToolkit:ModalPopupExtender ID="popResendPassword" runat="server" TargetControlID="btnForgotPassword" PopupControlID="divResendPass" CancelControlID="cancelPass" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>--%>
        <div id="divResendPass" class="popup">
            <asp:Table ID="tblResetUserName" runat="server" HorizontalAlign="Center">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox ID="txtResetEmail" runat="server" CssClass="popuptextbox" placeholder="Enter Email to receive default Password"></asp:TextBox>
                    </asp:TableCell><asp:TableCell>
                        <asp:RequiredFieldValidator ValidationGroup="Email" ID="RequiredFieldValidator1" runat="server" class="auto-style8" ForeColor="Red" ControlToValidate="txtResetEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ValidationGroup="UserName" ID="RegularExpressionValidator1" runat="server"
                            ControlToValidate="txtResetEmail" ErrorMessage="Please enter correct email" ForeColor="Red"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Button ID="btnResetPass" ValidationGroup="Pass" runat="server" Text="Send Your default Password to Email" CssClass="button" OnClick="btnResetPass_Click" />
            <asp:Button ID="cancelPass" runat="server" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
        </div>

    </form>

</body>
</html>



<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="loginScreen" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/login.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }

        .auto-style2 {
            width: 5px;
        }

        .auto-style3 {
            color: #fff;
            background-color: #28a745;
            margin-left: auto;
            margin-right: auto;
            display: block;
            width: 25%;
            height: 15%;
            margin-bottom: 5px;
            font-weight: normal;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            border: 1px solid transparent;
            font-size: 1rem;
            line-height: 1.25;
            border-radius: 0.25rem;
            transition: all 0.15s ease-in-out;
            position: Relative;
            bottom: -32px;
            left: -97px;
        }

        .auto-style4 {
            color: #fff;
            background-color: #28a745;
            margin-left: auto;
            margin-right: auto;
            display: block;
            width: 25%;
            height: 15%;
            margin-bottom: 5px;
            font-weight: normal;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            border: 1px solid transparent;
            font-size: 1rem;
            line-height: 1.25;
            border-radius: 0.25rem;
            transition: all 0.15s ease-in-out;
            position: Relative;
            bottom: -4px;
            left: 115px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" margin-left="auto" margin-right="auto">
        <div class="auto-style1">
            <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate" Style="align-content: center" Width="428px" DestinationPageUrl="CEOPostWall.aspx">
                <LayoutTemplate>
                    <table cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                        <tr>
                            <td>
                                <table cellpadding="0">
                                    <tr>
                                        <td align="center" class="td">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/image/ExcelLogo.png" class="rewardIcon" /></td>
                                    </tr>
                                    <tr>
                                        <%--<td align="right" class="auto-style2">&nbsp;</td>--%>
                                        <td align="center">
                                            <asp:TextBox ID="UserName" runat="server" CssClass="textbox" placeholder="Email Address"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ForeColor="Red" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                       <%-- <td align="right" class="auto-style2">&nbsp;</td>--%>
                                        <td align="center">
                                            <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="textbox" placeholder="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ForeColor="Red" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="RememberMe" runat="server" CssClass="td" Text="Remember me next time." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="color: Red;">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" CssClass="btn-login" Text="Log In" ValidationGroup="Login1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right"></td>
                                        <td align="right"></td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:Login>
        </div>
        <div>
            <%--<asp:Button ID="btnForgotUsername" CssClass="btn-forgot" runat="server" Text="Forgot Username" Width="130px" Height="30px" />--%>
            <asp:Button ID="btnForgotPassword" CssClass="btn-forgot" runat="server" Text="Forgot Password" Width="130px" Height="30px" />
        </div>


        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
        <%--<ajaxToolkit:ModalPopupExtender ID="popResendUserName" runat="server" TargetControlID="btnForgotUsername" PopupControlID="divResendUserName" CancelControlID="cancelEmail" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>--%>
        <ajaxToolkit:ModalPopupExtender ID="popResendPassword" runat="server" TargetControlID="btnForgotPassword" PopupControlID="divResendPass" CancelControlID="cancelPass" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
        <%--        <div id="divResendUserName" class="popup" style="width: 280px">
            <asp:Table ID="tblResetEmail" runat="server" HorizontalAlign="Left" CssClass="table">

                <asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Width="230px" placeholder="Enter Email to receive UserName"></asp:TextBox>
                    </asp:TableCell><asp:TableCell>
                        <asp:RequiredFieldValidator ValidationGroup="UserName" ID="RequiredFieldValidator2" runat="server" class="auto-style8" ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ValidationGroup="UserName" ID="RegularExpressionValidator2" runat="server"
                            ControlToValidate="txtEmail" ErrorMessage="Please enter correct email"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Button ID="resendEmail" ValidationGroup="UserName" runat="server" Text="Send Your UserName to Your Email" CssClass="button" OnClick="resendEmail_Click" />
            <asp:Button ID="cancelEmail" runat="server" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
        </div>--%>
        <div id="divResendPass" class="popup" style="width: 300px">
            <asp:Table ID="tblResetUserName" runat="server" HorizontalAlign="Left" CssClass="table">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox ID="txtResetEmail" runat="server" CssClass="textbox" Width="250" placeholder="Enter Email to receive default Password"></asp:TextBox>
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



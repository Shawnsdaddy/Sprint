<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="loginScreen" %>

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
            width: 100%;
        }
        .newStyle1 {
            font-family: tahoma;
        }
        .auto-style3 {
            height: 75px;
            text-align: center;
        }
        .auto-style7 {
            height: 62px;
            text-align: center;
            font-family: tahoma;
            color: #FFFFFF;
            font-size: medium;
        }
        .auto-style10 {
            height: 50px;
            text-align: center;
            font-family: tahoma;
            color: #FFFFFF;
            font-size: medium;
        }
        .auto-style11 {
            height: 57px;
            text-align: center;
            font-family: tahoma;
            color: #FFFFFF;
            font-size: medium;
        }
        .auto-style12 {
            color: #fff;
            background-color: #4C8DCA;
            margin-left: auto;
            margin-right: auto;
            display: block;
            margin-bottom: 30px;
            font-weight: normal;
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
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" margin-left="auto" margin-right="auto">
        <div class="auto-style1">
        </div>
        <div>
            <%--<asp:Button ID="btnForgotUsername" CssClass="btn-forgot" runat="server" Text="Forgot Username" Width="130px" Height="30px" />--%>
            <table class="auto-style2">
                <tr>
                    <td class="auto-style7">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/image/ExcelLogo.png" class="rewardIcon" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style11">
                    </td>
                </tr>
                <tr>
                    <td class="auto-style7"><strong>You Have Successfully Logged Out!</strong></td>
                </tr>
                <tr>
                    <td class="auto-style10"></td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Button ID="Button1" runat="server" Text="Log Back In" CssClass ="auto-style12" OnClick ="backToLoginButton_Click" Height="44px" Width="180px"/>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>


        <%--<ajaxToolkit:ModalPopupExtender ID="popResendUserName" runat="server" TargetControlID="btnForgotUsername" PopupControlID="divResendUserName" CancelControlID="cancelEmail" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>--%>
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

    </form>

</body>
</html>



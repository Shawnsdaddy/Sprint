<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true" CodeFile="CashOut.aspx.cs" Inherits="CashOut" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .pager {
            background-color: #B3C100;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: white;
            height: 30px;
            text-align: center;
        }

        .grid {
            margin: auto;
        }

        .button,.buttonLeft, .buttonRight {
            color: #fff;
            background-color: #17468A;
            display: inline;
            height: 30px;
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
            position: relative;
            margin: auto;
        }
        .buttonLeft{
            float:right;
            margin-right:3%;
        }
        .buttonRight{
            float:left;
             margin-left:3%;
        }
    </style>

    <br />
    <h2>
        <asp:Label runat="server" Text="Redeem Reward" ID="lblName" CssClass="reward-points" Font-Size="X-Large" ForeColor="#17468A"></asp:Label></h2>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="btn-aligncenter">
        </div>
    <div style="text-align: center; align-content: center">
        <asp:Label ID="redeemlabel" runat="server" Text="Label" ForeColor="#105c96" Style="font-size: large; text-align: center; margin: auto"></asp:Label>
        <br />
        <asp:Label ID="RedeemedGiftLabel" runat="server" Text="Label" ForeColor="#105c96" Style="font-size: large"></asp:Label>
        <br />
        <asp:Label ID="Quantitylbl" runat="server" Style="font-size: medium; text-decoration: none; text-align: center; margin-left: 2%" ForeColor="#105c96"></asp:Label>

        <asp:TextBox ID="Quantitytxt" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
        <div class="grid">
            <asp:GridView ID="gvImages" runat="server" AutoGenerateColumns="False" CssClass="grid" DataKeyNames="GiftCardID" OnRowDataBound="OnRowDataBound2" RowStyle-CssClass="rows" OnSelectedIndexChanged="gvImages_onSelectIndexChanged" AllowPaging="True" OnPageIndexChanging="RewardsGrid_ChangingPages" PageSize="10" CellPadding="5" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="GiftCard">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" />
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:BoundField DataField="ProviderName" HeaderText="Provider Name" />
                    <asp:BoundField DataField="GiftCardAmount" HeaderText="Amount" />

                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

                <HeaderStyle CssClass="header" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

                <PagerStyle CssClass="pager" BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>

            </asp:GridView>
            <asp:Button ID="RedeemButton" runat="server" Text="Get Reward" OnClick="btnCompany_Click" CssClass="button" />
            </div>
        </div>
            <%--<asp:Button ID="withdraw" runat="server" Text="Withdraw Points" CssClass="button" />
        </div>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
    <ajaxToolkit:ModalPopupExtender ID="popRedeem" runat="server" TargetControlID="RedeemButton" PopupControlID="divRedeem" CancelControlID="btnRedeemCancel" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popWithdraw" runat="server" TargetControlID="withdraw" PopupControlID="divWithdraw" CancelControlID="btnWithdrawCancel" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>

    <div id="divRedeem" class="popup" style="width: 30%">

        <div style="width:50%;float:left">
            <asp:Button ID="btnCompany" runat="server" Text="Use Points" OnClick="btnCompany_Click" CssClass="buttonLeft" />

        </div>
                <div style="width:50%;float:right">
            <asp:Button ID="btnPerson" runat="server" Text="Use Paypal" OnClick="Button1_Click" CssClass="buttonRight" />

        </div>

        <asp:Button ID="btnRedeemCancel" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
    </div>

    <div id="divWithdraw" class="popup" style="width: 30%">
        <asp:Table ID="tblwithdraw" runat="server" HorizontalAlign="Center">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Label ID="lblWithDraw" runat="server" Text="Withdraw Amount:" ></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="txtWithDraw" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell runat="server">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="withdraw" runat="server" ForeColor="Red" ControlToValidate="txtWithDraw" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="withdraw" ID="withdrawMessage" runat="server" ForeColor="Red" ControlToValidate="txtWithDraw" ValidationExpression="^[1-9]\d*$" Text="Only interger allowed" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell runat="server">
                    <asp:Button ID="withdrawCommit" runat="server" Text="Commit" ValidationGroup="withdraw" OnClick="withdrawCommit_Click" CssClass="button" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Button ID="btnWithdrawCancel" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
    </div>--%>

</asp:Content>

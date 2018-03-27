<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true" CodeFile="CashOut.aspx.cs" Inherits="CashOut" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        body {
        }

        .mydatagrid {
            border: 1px solid #fff
        }

        .rounded-corners {
            border: 1px solid #fff;
            -webkit-border-radius: 8px;
            -moz-border-radius: 8px;
            border-radius: 8px;
            overflow: hidden;
        }

        .header {
            background-color: #B3C100;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: white;
            border: none 0px transparent;
            height: 25px;
            text-align: center;
            font-size: 20px;
        }

        .rows {
            background-color: #fff;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-size: 15px;
            color: #000;
            min-height: 25px;
            text-align: center;
            border: none 0px transparent;
        }

            /*.rows:hover {
                background-color: #EAEDED;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                color: #666262;
                text-align: center;
            }*/

        .selectedrow {
            background-color: #EAEDED;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-weight: bold;
            text-align: center;
        }

        .mydatagrid {
            background-color: transparent;
            padding: 5px 5px 5px 5px;
            color: #fff;
            text-decoration: none;
            font-weight: bold;
        }

        .mydatafrid a:hover {
            background-color: #000;
            color: #566573;
        }

        .mydatagrid span {
            background-color: #bece02;
            color: #000;
            padding: 5px 5px 5px 5px;
        }

        .pager {
            background-color: #B3C100;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: white;
            height: 30px;
            text-align: center;
        }

        .mydatagrid td {
            padding: 5px;
        }

        .mydatagrid th {
            padding: 5px;
        }

        .auto-style12 {
            width: 1069px;
        }

        .auto-style13 {
            text-align: center;
        }

        .auto-style14 {
            width: 120%;
            margin-left: 8px;
        }
        .auto-style16 {
            margin-left: 163px;
        }

        .auto-style17 {
            font-family: 'Raleway', sans-serif; /*height: 10%;*/
            padding: 9px 9px 9px 9px;
            margin-left: auto;
            margin-right: auto;
            border-radius: 25px;
            border: 2px solid #17468A;
            margin: 2%;
        }

        .auto-style18 {
            text-align: center;
            height: 22px;
        }

        .auto-style19 {
            width: 658px;
        }

        .auto-style20 {
            text-align: right;
            width: 658px;
        }

    </style>

    <br />
    <h2>
        <asp:Label runat="server" Text="Redeem Reward" ID="lblName" CssClass="reward-points" Width="25%" Font-Size="X-Large" ForeColor="#17468A"></asp:Label></h2>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div class="btn-aligncenter" style="width: 90%">
    </div>
    <br />
    <asp:Label ID="redeemlabel" runat="server" Text="Label" ForeColor="#105c96" Style="font-size: large; text-align: center; margin-left: 30%"></asp:Label>
    <br />



    <br />
    <div style="margin-left:5%; text-align:center;align-content:center">
        <table class="auto-style16">
                <tr>
                    <td class="auto-style12">
                        <div class="auto-style13">
                            <table class="auto-style14">
                                <tr>
                                    <td class="auto-style18" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style19">

                                        <asp:Label ID="RedeemedGiftLabel" runat="server" Text="Label" CssClass="auto-style14" ForeColor="#105c96" Style="font-size: large"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="Quantitylbl" runat="server" Style="font-size: medium; text-decoration: none; text-align:center; margin-left:2%" ForeColor="#105c96"></asp:Label>
                                        <br />

                                        <asp:TextBox ID="Quantitytxt" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>

                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style20">
                                        <div class="auto-style13">
                                            &nbsp;&nbsp;&nbsp;
                        <asp:GridView ID="gvImages" runat="server" AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound2"  RowStyle-CssClass= "rows" OnSelectedIndexChanged="gvImages_onSelectIndexChanged" AllowPaging="True" OnPageIndexChanging="RewardsGrid_ChangingPages" PageSize="5" Width="80%" Height="70%" CellPadding="5" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="GiftCard">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" />
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:BoundField DataField="JobTitle" HeaderText="Provider Name" SortExpression="JobTitle" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />

                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

                            <HeaderStyle CssClass="header" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

                            <PagerStyle CssClass="pager" BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>

<%--                            <RowStyle CssClass="rows" BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />--%>
                        </asp:GridView>
                                            <br />
                                            <br />
                                        <br />
                                        <asp:Button ID="RedeemButton" runat="server" OnClick="Button1_Click" Text="Redeem" CssClass="btn-profile" Width="20%" Height="30%" />
                                        </div>  
                                    </td>
                            </table>
                        </div>
            </table>
    </div>


</asp:Content>

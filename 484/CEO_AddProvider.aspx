<%@ Page Title="" Language="C#" MasterPageFile="~/CEOMasterPage.master" AutoEventWireup="true" CodeFile="CEO_AddProvider.aspx.cs" Inherits="CEO_AddProvider" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .btn {
            color: #fff;
            background-color: #17468A;
            width: auto;
            height: 30px;
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
            position: relative;
            float: left;
            top: 0px;
            left: 1px;
        }

        #label {
            display: inline-block;
        }

        table {
            margin: auto;
        }

        /*.mybutton {
            border-width: 0;
            border-style: none;
            background-color: #B3C100;
            font-weight: bold;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-size: medium;
            color: white;
        }*/

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
            height: 50px;
            text-align: center;
            border: none 0px transparent;
        }

        .pager {
            background-color: #B3C100;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: white;
            height: 30px;
            text-align: center;
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

            .rows:hover {
                background-color: #EAEDED;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                color: #666262;
                text-align: center;
            }

        .selectedrow {
            background-color: #EAEDED;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-weight: bold;
            text-align: center;
        }

        .mydatagrid {
            background-color: white;
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

        #pending {
            float: left;
            width: 50%;
            margin-left: 3%;
        }

        #approve {
            float: right;
            width: 46%;
        }

        @media screen and (min-width:760px) and (max-width: 1023px) {
            .rows {
                font-size: 10px;
                height: auto;
            }

            .header {
                border: none 0px transparent;
                height: 15px;
                text-align: center;
                font-size: 14px;
            }

            #main {
                padding: 10px;
            }

            .btn {
                font-size: 12px;
            }
        }

        @media screen and (max-width: 759px) {
            #main {
                padding: 0;
            }

            .rows {
                font-size: 10px;
                height: auto;
            }

            .btn {
                font-size: 12px;
                display: inline;
                margin: auto;
            }

            #pending {
                width: 100%;
                margin: auto;
                display: inline;
            }

            #approve {
                position: relative;
                float: none;
                display: inline;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <h2>
        <asp:Label runat="server" Text="Provider Information" ID="lblTitle" CssClass="reward-points" Font-Size="X-Large" ForeColor="#17468A"></asp:Label></h2>
    <div class="btn-aligncenter" style="width: 90%;"></div>
    <br />
    <div id="pending">
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Size="Larger" Text="Pending Gift Card" CssClass="lable" ForeColor="SlateGray"></asp:Label>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gdvPending" ForeColor="#333333" runat="server" CellPadding="3" GridLines="None" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="GiftCardID" OnRowDeleting="gdvPending_RowDeleting" OnPageIndexChanging="gdvPending_PageIndexChanging" CellSpacing="1">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="rows" />
                        <Columns>
                            <asp:TemplateField HeaderText="GiftCardID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblGiftCardID" runat="server" Text='<%# Eval("GiftCardID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ProviderName">
                                <ItemTemplate>
                                    <asp:Label ID="lblProviderName" runat="server" Text='<%# Eval("ProviderName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblTypeOfBusiness" runat="server" Text='<%# Eval("TypeOfBusiness") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblGiftCardAmount" runat="server" Text='<%# Eval("GiftCardAmount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:CommandField DeleteText="Decline" ShowDeleteButton="True" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                        <EmptyDataRowStyle ForeColor="#999999" HorizontalAlign="Center" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="rows" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" Text="Approve" CssClass="btn" />

                </td>
            </tr>
        </table>
    </div>
    <div id="approve">
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Size="Larger" Text="Approved Gift Card" ForeColor="SlateGray"></asp:Label>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gdvApproved" ForeColor="#333333" runat="server" CellPadding="3" GridLines="None" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gdvApproved_PageIndexChanging" OnRowDeleting="gdvApproved_RowDeleting" CellSpacing="1">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="rows" />
                        <Columns>
                            <asp:TemplateField HeaderText="GiftCardID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblGiftCardID" runat="server" Text='<%# Eval("GiftCardID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ProviderName">
                                <ItemTemplate>
                                    <asp:Label ID="lblProviderName" runat="server" Text='<%# Eval("ProviderName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblTypeOfBusiness" runat="server" Text='<%# Eval("TypeOfBusiness") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblGiftCardAmount" runat="server" Text='<%# Eval("GiftCardAmount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField DeleteText="Decline" ShowDeleteButton="True" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                        <EmptyDataRowStyle ForeColor="#999999" HorizontalAlign="Center" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="rows" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Add New Provider" CssClass="btn" />
                    <asp:Button ID="btnCurrent" runat="server" Text="Current Providers" CssClass="btn" OnClick="btnCurrent_Click" />

                </td>
            </tr>
        </table>

        <br />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnCurrent" PopupControlID="divCurrent" CancelControlID="btnClose" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>

    </div>







    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
    <ajaxToolkit:ModalPopupExtender ID="popAdd" runat="server" TargetControlID="btnAdd" PopupControlID="divAdd" CancelControlID="btnCancel" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <div id="divAdd" class="popup">
        <h1>ADD NEW PROVIDER</h1>
        <asp:Table ID="EditProfile" runat="server" HorizontalAlign="Center">

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblName" runat="server" class="auto-style1" Text="Provider Name:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtProviderName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="NameRequire" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtProviderName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="FirstNameMessage" runat="server" ForeColor="Red" ControlToValidate="txtProviderName" ValidationExpression="^[a-zA-Z\s']{1,30}$" Text="No more than 30 alphabetic characters" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblEmail" runat="server" class="auto-style1" Text="Email:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtProviderEmail" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="Required" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtProviderEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="RegularExpressionValidator1" runat="server" ForeColor="Red"
                        ControlToValidate="txtProviderEmail" ErrorMessage="Please enter corect email. For example john@example.com"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblType" runat="server" class="auto-style1" Text="Type of Bussiness:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:DropDownList ID="TypeOfBusiness" runat="server" Width="200px" CssClass="textbox">
                        <asp:ListItem Value="0">Choose a type</asp:ListItem>
                        <asp:ListItem Value="1">Restaurant</asp:ListItem>
                        <asp:ListItem Value="2">Lodging</asp:ListItem>
                        <asp:ListItem Value="3">Clothing</asp:ListItem>
                        <asp:ListItem Value="4">Shopping</asp:ListItem>
                        <asp:ListItem Value="5">Other</asp:ListItem>
                    </asp:DropDownList>
                    <asp:CompareValidator ID="reqType" runat="server" ControlToValidate="TypeOfBusiness" ErrorMessage="*" ForeColor="Red"
                        ValueToCompare="0" Operator="NotEqual" ValidationGroup="profile"></asp:CompareValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                </asp:TableCell><asp:TableCell>
                    <asp:Button ID="btnAddNew" runat="server" ValidationGroup="profile" Text="Add" CssClass="button" OnClick="btnAddNew_Click" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Button ID="btnCancel" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
    </div>
    <div id="divCurrent" class="popup">
        <asp:GridView ID="CurrentProvider" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <asp:Button ID="btnClose" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />

    </div>
</asp:Content>




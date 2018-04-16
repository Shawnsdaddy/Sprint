<%@ Page Title="" Language="C#" MasterPageFile="~/RewardProviderMasterPage.master" AutoEventWireup="true" CodeFile="GiftCardInfo.aspx.cs" Inherits="GiftCardInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .btn {
            color: #fff;
            background-color: #17468A;
            border-color: #17468A;
            /*width: 20%;*/
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
        }
        .popup{
            width:30%;
        }

        .mybutton {
            border-width: 0;
            border-style: none;
            background-color: #B3C100;
            font-weight: bold;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-size: medium;
            color: white;
        }
        table{
            margin:auto;
            font-size:25px;
        }

        .rows {
            background-color: #fff;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-size: 25px;
            color: #000;
            height: 50px;
            text-align: center;
            border: none 0px transparent;
        }

        @media screen and (max-width:788px) {
            table {
                font-size: 10px;
            }
         .popup{
            width:50%;
            font-size:small;
        }
                    #button{
                        width:auto;
                    }
           
        }
        .rows{
            font-size:12px;
            height:auto;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <h2>
        <asp:Label runat="server" Text="Company/GiftCard Information" ID="lblTitle" CssClass="reward-points" Font-Size="X-Large" ForeColor="#17468A"></asp:Label></h2>
    <div class="btn-aligncenter" style="width: 100%;"></div>
    <div style="margin-right: 40px; margin-left: 40px; text-align: center;">
        <asp:Table ID="Table1" runat="server" CellPadding="3" CellSpacing="5" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblName" runat="server" Text="Search by Name"></asp:Label>
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblemail" runat="server" Text="Search by E-mail"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="txtName" runat="server" ></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label1" runat="server" Text="or" ForeColor="LightGray"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtemail" runat="server" ></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>                   
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" OnClick="btnSearch_Click" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <table>
        <tr>
            <td>    <div runat="server" id="divCompany" style="width: 100%">
        <asp:GridView ID="gdvShow" HorizontalAlign="Center" ForeColor="#333333" runat="server" CellPadding="3" GridLines="None" AutoGenerateColumns="False" DataKeyNames="BusinessEntityID" AllowPaging="true" OnPageIndexChanging="gdvShow_PageIndexChanging" PageSize="10" EmptyDataText="No Record Found" CellSpacing="1" OnSelectedIndexChanged="gdvShow_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="rows" />
            <Columns>
                <asp:TemplateField HeaderText="BusinessEntityID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblGID" runat="server" Text='<%# Eval("BusinessEntityID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--                <asp:BoundField DataField="BusinessEntityID" HeaderText="BusinessEntityID" InsertVisible="False" ReadOnly="True" SortExpression="BusinessEntityID" />--%>
                <asp:BoundField DataField="BusinessEntityName" HeaderText="Company Name" SortExpression="BusinessEntityName" />
                <asp:BoundField DataField="PhoneNumber" HeaderText="PhoneNumber" SortExpression="PhoneNumber" />
                <asp:BoundField DataField="BusinessEntityEmail" HeaderText="Email" SortExpression="BusinessEntityEmail" />
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
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
        <%--        <asp:Table ID="Table2" runat="server" CellPadding="3" CellSpacing="5" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="btnSelect" runat="server" Text="Edit GiftCard Information" CssClass="btn" Width="130px" OnClick="btnSelect_Click" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>--%>
    </div></td>
        </tr>
        <tr>
            <td>    <div runat="server" id="divCard" style="float: left; width: 90%">
        <asp:GridView ID="gdvCard" HorizontalAlign="Center" ForeColor="#333333" runat="server" OnRowDeleting="gdvCard_RowDeleting" OnRowEditing="gdvCard_RowEditing" OnRowCancelingEdit="gdvCard_RowCancelingEdit" OnRowUpdating="gdvCard_RowUpdating" OnRowDataBound="OnRowDataBound2" CellPadding="3" GridLines="None" AutoGenerateColumns="False" DataKeyNames="GiftCardID" AllowPaging="true" OnPageIndexChanging="gdvCard_PageIndexChanging" PageSize="10" EmptyDataText="No Record Found" CellSpacing="1" OnSelectedIndexChanged="gdvCard_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="rows" />
            <Columns>
                <asp:TemplateField HeaderText="GiftCard">
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GiftCardID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblGID" runat="server" Text='<%# Eval("GiftCardID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Giftcard Amount">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCAmount" runat="server" Text='<%# Eval("GiftCardAmount") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCAmount" runat="server" Text='<%# Eval("GiftCardAmount") %>'></asp:Label>
                    </ItemTemplate>

                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- <asp:BoundField DataField="GiftCardID" HeaderText="GiftCardID" InsertVisible="False" ReadOnly="True" SortExpression="GiftCardID" />
                <asp:BoundField DataField="GiftCardAmount" HeaderText="GiftCardAmount" SortExpression="GiftCardAmount" />--%>
                <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                <asp:CommandField HeaderText="Remove" ShowDeleteButton="True" />
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
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
        <asp:Table ID="ButtonTable" runat="server" CellPadding="3" CellSpacing="5" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="btnAddCard" runat="server" Text="Add New GiftCard" CssClass="btn" />
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label2" runat="server" Text="or" ForeColor="LightGray"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnUpdateCard" runat="server" Text="Update GiftCard Picture" CssClass="btn" OnClick="btnUpdateCard_Click" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div></td>
        </tr>
    </table>


    <div>
        <asp:Table ID="Table2" runat="server" CaptionAlign="Left" CellPadding="10" CellSpacing="5">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/image/Excel.jpg" Height="66px" Width="77px" />
                </asp:TableCell>
                <asp:TableCell>
                    <asp:LinkButton ID="BusinessEntity" runat="server" OnClick="BusinessEntities">Download Users</asp:LinkButton>
                </asp:TableCell>



            </asp:TableRow>
        </asp:Table>


    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
    <ajaxToolkit:ModalPopupExtender ID="popAdd" runat="server" TargetControlID="btnAddCard" PopupControlID="divAdd" CancelControlID="btnCancel" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popUpdate" runat="server" TargetControlID="btnUpdateCard" PopupControlID="divChange" CancelControlID="btnClosed" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>

    <div id="divAdd" class="popup">
        <h1>ADD NEW GiftCard</h1>
        <asp:Table ID="AddGiftCard" runat="server" CssClass="table" Width="30%">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblAmount" runat="server" class="auto-style1" Text="Amount:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Add" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtAmount" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revNumber" runat="server" ControlToValidate="txtAmount" ErrorMessage="Please enter valid numbers like 10 or 10.00" ForeColor="Red" ValidationExpression="^\d+(\.\d\d)?$"></asp:RegularExpressionValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell>
                    <asp:FileUpload ID="PictureAdd" runat="server" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnAddNew" runat="server" class="auto-style6" Text="Add" ValidationGroup="Add" OnClick="btnAddNew_Click" CssClass="button"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Button ID="btnCancel" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
    </div>

    <div id="divChange" class="popup">
        <h1>Edit a GiftCard</h1>
        <br />
        <asp:FileUpload ID="PictureUpload" runat="server" />
        <asp:RequiredFieldValidator ID="upload" runat="server" ControlToValidate="PictureUpload" ErrorMessage="*" ForeColor="red" ValidationGroup="upload"></asp:RequiredFieldValidator>
        <br />
        <asp:Image ID="updatePicture" runat="server" Height="30%" />
        <br />
        <asp:Button ID="btnUpdateGift" runat="server" class="auto-style6" Text="Update" OnClick="btnUpdateGift_Click" CssClass="button" ValidationGroup="upload"></asp:Button>
        <asp:Button ID="btnClosed" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />

    </div>
</asp:Content>



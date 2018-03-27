<%@ Page Title="" Language="C#" MasterPageFile="~/CEOMasterPage.master" AutoEventWireup="true" CodeFile="CEO_AddProvider.aspx.cs" Inherits="CEO_AddProvider" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    &nbsp;
    <style type="text/css">
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .btn {
            color: #fff;
            background-color: #17468A;
            border-color: #17468A;
            width: 20%;
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

        .mybutton {
            border-width: 0;
            border-style: none;
            background-color: #B3C100;
            font-weight: bold;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-size: medium;
            color: white;
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

        .auto-style11 {
            color: #fff;
            text-decoration: none;
            font-weight: bold;
            border: 1px solid #fff;
            padding: 3px;
            background-color: transparent;
        }
        .mydatagrid
        {
           border:1px solid #fff
        }
         .rounded-corners
        {
            border: 1px solid #fff;
            -webkit-border-radius: 8px;
            -moz-border-radius: 8px;
            border-radius: 8px;
            overflow: hidden;
        }
        .header{
            background-color: #B3C100;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: white;
            border: none 0px transparent;
            height: 25px;
            text-align: center;
            font-size: 20px;           
        }
        .rows{
            background-color: #fff;
            font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-size: 15px;
            color: #000;
            min-height: 25px;
            text-align: center;
            border: none 0px transparent;
        }
        .rows:hover{
            background-color: #EAEDED;
            font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: #666262;
            text-align: center;
        }
        .selectedrow{
            background-color: #EAEDED;
            font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-weight: bold;
            text-align: center;
        }
        .mydatagrid{
            background-color: white;
            padding: 5px 5px 5px 5px;
            color: #fff;
            text-decoration: none;
            font-weight: bold;
        }
        .mydatafrid a:hover{
            background-color: #000;
            color: #566573;
        }
        .mydatagrid span{
            background-color: #bece02;
            color: #000;
            padding: 5px 5px 5px 5px;

        }
        .pager{
            background-color: #B3C100;
            font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: white;
            height: 30px;
            text-align: center;

        }
        .mydatagrid td{
            padding: 5px;
        }
        .mydatagrid th{
            padding: 5px;
        }
    .auto-style13 {
        border: 1px solid #fff;
        -webkit-border-radius: 8px;
        -moz-border-radius: 8px;
        border-radius: 8px;
        overflow: hidden;
        text-align: center;
    }
    </style>
    <script type="text/javascript">
        function myselection(rbtnid) {
            var rbtn = document.getElementById(rbtnid);
            var rbtnlist = document.getElementsByTagName("input");
            for (i = 0; i < rbtnlist.length; i++) {
                if (rbtnlist[i].text == "radio" && rbtnlist[i].id != rbtn.id) {
                    rbtnlist[i].checked = false;

                }
            }
        }
    </script>

    <br />
    <h2>
        <asp:Label runat="server" Text="Provider Information" ID="lblTitle" CssClass="reward-points" Width="30%" Font-Size="X-Large" ForeColor="#17468A"></asp:Label></h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="btn-aligncenter" style="width: 90%;"></div>
    <div style="margin-right: 15%; margin-left: 15%; text-align: center;">
        <asp:Table ID="Table1" runat="server" CellPadding="3" CellSpacing="5" HorizontalAlign ="Center">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblCompany" runat="server" Text="Search by Name"></asp:Label>
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblemail" runat="server" Text="Search by E-mail"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="txtCompany" runat="server" Width="180px"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label1" runat="server" Text="or" ForeColor="LightGray"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtemail" runat="server" Width="180px" ></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>

                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="150px" CssClass="btn" OnClick="btnSearch_Click" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <br />
        <div style="float: left; width: 90%;">
            <asp:GridView ID="gdvShow" HorizontalAlign="Center" ForeColor="#333333" runat="server" CellPadding="3" GridLines="None" AutoGenerateColumns="False" AllowPaging ="True" OnPageIndexChanging ="gdvShow_PageIndexChanging" DataKeyNames="ProviderID" OnSelectedIndexChanged ="gdvShow_SelectedIndexChanged1" CellSpacing="1">
               <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="rows" />
                <Columns>
                    <asp:BoundField DataField="ProviderID" HeaderText="ProviderID" SortExpression="ProviderID" ReadOnly="True" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" ReadOnly="True" />
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
                 <EditRowStyle BackColor="#999999" />
                 <EmptyDataTemplate>No Record Found</EmptyDataTemplate>  
                <EmptyDataRowStyle ForeColor ="#999999" HorizontalAlign="Center" />
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
            <br />
            <br />
        </div>
        <br />
        <br />
        <asp:Table ID="Table2" runat="server" CellPadding="3" CellSpacing="5" HorizontalAlign ="Center">

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn" Width="130px" OnClick ="btnUpdate_Click" />


                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label4" runat="server" Text="or" ForeColor="LightGray"></asp:Label>


                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="130px" CssClass="btn" OnClick="btnRemove_Click" />


                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label3" runat="server" Text="or" ForeColor="LightGray"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnAdd" runat="server" Text="Add New Provider" Width="180px" CssClass="btn" OnClick="btnAddProvider_Click" />


                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>


    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
    <ajaxToolkit:ModalPopupExtender ID="popAdd" runat="server" TargetControlID="btnAdd" PopupControlID="divAdd" CancelControlID="btnCancel" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popUpdate" runat="server" TargetControlID="btnUpdate" PopupControlID="divUpdate" CancelControlID="btnClose" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>


    <div id="divAdd" class="popup" style="width: 800px;">
        <h1>ADD NEW PROVIDER</h1>
        <asp:Table ID="AddProvider" runat="server" CssClass="table" Width="800px">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblProviderName" runat="server" class="auto-style1" Text="Provider Name:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtProviderName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Provider" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtProviderName" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="Provider" ForeColor="Red" ID="ProviderNameMessage" runat="server" ControlToValidate="txtProviderName" ValidationExpression="^[a-zA-Z\s']{1,20}$" Text="No more than 30 alphabetic characters " />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblProviderEmail" runat="server" class="auto-style1" Text="Provider Email:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtProviderEmail" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ID="RequireddValidator1" ValidationGroup="Provider" ForeColor="Red" runat="server" class="auto-style1" ControlToValidate="txtProviderEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="Provider" ID="RegularExpressionValidator2" runat="server" ForeColor="Red"
                        ControlToValidate="txtProviderEmail" ErrorMessage="Please enter corect email, eg. john@example.com"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
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
                </asp:TableCell><asp:TableCell>
                    <asp:CompareValidator ID="reqType" runat="server" ControlToValidate="TypeOfBusiness" ErrorMessage="*" ForeColor="Red"
                        ValueToCompare="0" Operator="NotEqual" ValidationGroup="Provider"></asp:CompareValidator>
                </asp:TableCell></asp:TableRow><%--<asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblAmount" runat="server" class="auto-style1" Text="Amount Provided:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Provider" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtAmount" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="comAmount" Text="Enter valid integer" ControlToValidate="txtAmount" Type="Integer" Operator="DataTypeCheck" runat="server" />
                </asp:TableCell></asp:TableRow>--%><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblpic" runat="server" class="auto-style1" Text="Provider Picture:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:FileUpload ID="Picture" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Provider" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="Picture" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>          
                </asp:TableCell><asp:TableCell>              
                </asp:TableCell><asp:TableCell>            
                </asp:TableCell></asp:TableRow><asp:TableFooterRow>
                <asp:TableCell>
                    <asp:Button ID="btnAddProvider" runat="server" class="auto-style6" Text="Add" Width="150px" ValidationGroup="Provider" OnClick="btnAddProvider_Click" CssClass="button"></asp:Button>
                </asp:TableCell></asp:TableFooterRow></asp:Table><asp:Button ID="btnCancel" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />

    </div>
    <div id="divUpdate" class="popup" style="width: 800px; height: 700px">
        <h1>UPDATE PROVIDER</h1><asp:Table ID="updateprovider" runat="server" CssClass="table" Width="800px">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblprovider" runat="server" class="auto-style1" Text="Provider Name:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtprovider" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="provider" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtprovider" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="provider" ID="Message" runat="server" ForeColor="Red" ControlToValidate="txtprovider" ValidationExpression="^[a-zA-Z\s']{1,20}$" Text="No more than 30 alphabetic characters " />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblPEmail" runat="server" class="auto-style1" Text="Provider Email:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtPEmail" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ID="RequireddValidator4" ValidationGroup="provider" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtPEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="provider" ID="RegularExpressionValidator3" runat="server" ForeColor="Red"
                        ControlToValidate="txtPEmail" ErrorMessage="Please enter corect email, eg. john@example.com"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Type" runat="server" class="auto-style1" Text="Type of Bussiness:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:DropDownList ID="Bussiness" runat="server" Width="200px" CssClass="textbox">
                        <asp:ListItem Value="0">Choose a type</asp:ListItem>
                        <asp:ListItem Value="1">Restaurant</asp:ListItem>
                        <asp:ListItem Value="2">Lodging</asp:ListItem>
                        <asp:ListItem Value="3">Clothing</asp:ListItem>
                         <asp:ListItem Value="4">Shopping</asp:ListItem>
                        <asp:ListItem Value="5">Other</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell><asp:TableCell>
                    <asp:CompareValidator ID="reqTB" runat="server" ControlToValidate="Bussiness" ErrorMessage="*" ForeColor="Red"
                        ValueToCompare="0" Operator="NotEqual" ValidationGroup="provider"></asp:CompareValidator>
                </asp:TableCell></asp:TableRow><%--<asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Amount" runat="server" class="auto-style1" Text="Amount Provided:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtAmountProvide" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="provider" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtAmountProvide" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="comamountprovide" Text="Enter valid integer" ControlToValidate="txtAmountProvide" Type="Integer" Operator="DataTypeCheck" runat="server" />
                </asp:TableCell></asp:TableRow>--%><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblPicture" runat="server" class="auto-style1" Text="Provider Picture:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:FileUpload ID="Photo" runat="server" />
                </asp:TableCell></asp:TableRow></asp:Table><br />
        <asp:Image ID="ProfilePicture" runat="server" Height="231px" Style="margin-left: 72px" Width="260px" /><br />
        <asp:Button ID="btnUpdateProvider" runat="server" class="auto-style6" Text="Update" Width="20%" ValidationGroup="provider" OnClick="btnUpdateProvider_Click" CssClass="button"></asp:Button>
        <asp:Button ID="btnDelete" runat="server" CausesValidation="false" Text="Delete" OnClick="btnRemove_Click" CssClass="button" Width="20%"></asp:Button>
        <asp:Button ID="btnClose" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />

    </div>
</asp:Content>




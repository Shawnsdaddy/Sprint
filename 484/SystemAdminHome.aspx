<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdminMasterPage.master" AutoEventWireup="true" CodeFile="SystemAdminHome.aspx.cs" Inherits="SystemAdminHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <style type="text/css">
        .modalPopup {
        }

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
            background-color: transparent;
            padding: 5px 5px 5px 5px;
            color: #fff;
            text-decoration: none;
            font-size: x-small;
            text-align: center;
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
        .calendar{
            font-family: 'Raleway', sans-serif;
    margin-top: auto;
    margin-bottom: auto;
    margin-left: 20%;
    margin-right: auto;
        }
         .Businesstable {
             margin-left: 302px;
         }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div class="btn-aligncenter">
        <br />
        <asp:Label ID="lblPoints" runat="server" CssClass="reward-points" Font-Size="X-Large"></asp:Label>
        </div>
    <div style="align-content: center;width:90%;margin-left:25%; font-size:large">
        <br />
       <asp:Table ID="EditProfile" runat="server" CssClass="table" Width="80%" Height="60%">
            <asp:TableHeaderRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="Label1" runat="server" Text="Business Information" Font-Underline="true" Font-Size="Large" Font-Bold="true"></asp:Label>
                </asp:TableCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblName" runat="server" class="auto-style1" Text="Business Name:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="NameRequire" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtName" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </asp:TableCell></asp:TableRow><asp:TableRow>
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell ColumnSpan="2">
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="FirstNameMessage" runat="server" ForeColor="Red" ControlToValidate="txtName" ValidationExpression="^[a-zA-Z\s']{1,30}$" Text="No more than 30 alphabetic characters" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblBusinessEmail" runat="server" class="auto-style1" Text="Email:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtBusinessEmail" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="Required" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtBusinessEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow><asp:TableCell></asp:TableCell><asp:TableCell ColumnSpan="2">
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="RegularExpressionValidator1" runat="server" ForeColor="Red"
                        ControlToValidate="txtBusinessEmail" ErrorMessage="Please enter corect email. For example john@example.com"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>                   
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblPhoneNumber" runat="server" class="auto-style1" Text="Contact Number:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtPhoneNumber" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow><asp:TableCell></asp:TableCell><asp:TableCell ColumnSpan="2">
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="RegularExpressionValidator2" runat="server" ForeColor="Red" ControlToValidate="txtPhoneNumber" ValidationExpression="[0-9]{10}" Text="Enter correct phone number, eg. xxxxxxxxxx" />
                </asp:TableCell></asp:TableRow><asp:TableHeaderRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="Label2" runat="server" Text="CEO Information" Font-Underline="true" Font-Size="Large" Font-Bold="true"></asp:Label>
                </asp:TableCell></asp:TableHeaderRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblFirstName" runat="server" class="auto-style1" Text="First Name:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtFirstName" ErrorMessage="*"></asp:RequiredFieldValidator>
               </asp:TableCell></asp:TableRow><asp:TableRow><asp:TableCell></asp:TableCell><asp:TableCell ColumnSpan="2">
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="RegularExpressionValidator3" runat="server" ForeColor="Red" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z\s']{1,20}$" Text="No more than 20 alphabetic characters" />
                </asp:TableCell></asp:TableRow><asp:TableRow>

                <asp:TableCell>
                    <asp:Label ID="lblMI" runat="server" Text="Middle Initial:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtMI" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                </asp:TableCell></asp:TableRow><asp:TableRow><asp:TableCell></asp:TableCell><asp:TableCell ColumnSpan="2">
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="MIMessage" ForeColor="Red" runat="server" ControlToValidate="txtMI" ValidationExpression="^[a-zA-Z\s']{0,1}$" Text="One alphabetic characters" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblLastName" runat="server" class="auto-style1" Text="Last Name:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequireddValidator1" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtLastName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow><asp:TableCell></asp:TableCell><asp:TableCell ColumnSpan="2">
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="LastNameMessage" runat="server" ForeColor="Red" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z\s']{1,20}$" Text="No more than 20 alphabetic characters" />
                </asp:TableCell></asp:TableRow><asp:TableFooterRow>
                    <asp:TableCell></asp:TableCell><asp:TableCell ColumnSpan="2">
                    <asp:Button ID="btnCommit" runat="server" class="button" Text="Commit" Width="20%" ValidationGroup="profile" OnClick="btnCommit_Click"></asp:Button>
                </asp:TableCell></asp:TableFooterRow></asp:Table><br />       
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdminMasterPage.master" AutoEventWireup="true" CodeFile="SystemAdmin.aspx.cs" Inherits="SystemAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .Table {
                font-family: 'Raleway', sans-serif;
    margin-top: 3%;
    margin-bottom: auto;
    margin-left: 34%;
    margin-right: 10%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="btn-aligncenter">
        <br/>
        <asp:Label ID="lblPoints" runat="server" Text="" CssClass="reward-points" Font-Size="X-Large" ></asp:Label>

  </div>

    <div style="text-align:center;font-size:medium; width:90%; height:90%; margin-left:60px; float:left;">
        <br />

        <asp:Table ID="EditProfile" runat="server" CssClass="Table" Width="90%" Height="50%">
            <asp:TableHeaderRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="Label1" runat="server" class="auto-style1" Text="Business Information" Font-Underline="true" Font-Size="Large" Font-Bold="true"></asp:Label>
                </asp:TableCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblName" runat="server" class="auto-style1" Text="Business Name:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="NameRequire" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell><asp:TableCell>
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="FirstNameMessage" runat="server" ForeColor="Red" ControlToValidate="txtName" ValidationExpression="^[a-zA-Z\s']{1,30}$" Text="No more than 30 alphabetic characters" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblBusinessEmail" runat="server" class="auto-style1" Text="Email:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtBusinessEmail" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="Required" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtBusinessEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell><asp:TableCell>
                    
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="RegularExpressionValidator1" runat="server" ForeColor="Red"
                        ControlToValidate="txtBusinessEmail" ErrorMessage="Please enter corect email. For example john@example.com"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>                   
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblPhoneNumber" runat="server" class="auto-style1" Text="Contact Number:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtPhoneNumber" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell><asp:TableCell>
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="RegularExpressionValidator2" runat="server" ForeColor="Red" ControlToValidate="txtPhoneNumber" ValidationExpression="[0-9]{10}" Text="Enter correct phone number, eg. xxxxxxxxxx" />
                </asp:TableCell></asp:TableRow><asp:TableHeaderRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="Label2" runat="server" class="auto-style1" Text="CEO Information" Font-Underline="true" Font-Size="Large" Font-Bold="true"></asp:Label>
                </asp:TableCell></asp:TableHeaderRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblFirstName" runat="server" class="auto-style1" Text="First Name:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtFirstName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell><asp:TableCell>
                    
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="RegularExpressionValidator3" runat="server" ForeColor="Red" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z\s']{1,20}$" Text="No more than 20 alphabetic characters" />
                </asp:TableCell></asp:TableRow><asp:TableRow>

                <asp:TableCell>
                    <asp:Label ID="lblMI" runat="server" Text="Middle Initial:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtMI" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="MIMessage" ForeColor="Red" runat="server" ControlToValidate="txtMI" ValidationExpression="^[a-zA-Z\s']{0,1}$" Text="One alphabetic characters" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblLastName" runat="server" class="auto-style1" Text="Last Name:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequireddValidator1" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtLastName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell><asp:TableCell>
                    
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="LastNameMessage" runat="server" ForeColor="Red" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z\s']{1,20}$" Text="No more than 20 alphabetic characters" />
                </asp:TableCell></asp:TableRow><%--<asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblTitle" runat="server" class="auto-style1" Text="Job Title:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                     <asp:DropDownList ID="ddlJob" runat="server" Width="200px" CssClass="textbox">
                    <asp:ListItem Value="0">Choose a Title</asp:ListItem>
                    <asp:ListItem Value="1">CEO</asp:ListItem>
                </asp:DropDownList>
                    <asp:CompareValidator ID="reqJob" runat="server" ControlToValidate="ddlJob" ErrorMessage="*" ForeColor="Red"
                                ValueToCompare="0" Operator="NotEqual" ValidationGroup="profile"></asp:CompareValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblPosition" runat="server" class="auto-style1" Text="Position:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                     <asp:DropDownList ID="ddlPosition" runat="server" Width="200px" CssClass="textbox">
                    <asp:ListItem Value="0">Choose a Position</asp:ListItem>
                    <asp:ListItem Value="1">Administrative</asp:ListItem>
                </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlPosition" ErrorMessage="*" ForeColor="Red"
                                ValueToCompare="0" Operator="NotEqual" ValidationGroup="profile"></asp:CompareValidator>
                </asp:TableCell></asp:TableRow>--%><asp:TableFooterRow>
                    <asp:TableCell></asp:TableCell><asp:TableCell>
                    <asp:Button ID="btnCommit" runat="server" class="button" Text="Commit" Width="50%" ValidationGroup="profile" OnClick="btnCommit_Click"></asp:Button>
                </asp:TableCell></asp:TableFooterRow></asp:Table></div>
    
</asp:Content>


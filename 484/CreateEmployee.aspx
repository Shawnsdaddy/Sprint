﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CEOMasterPage.master" AutoEventWireup="true" CodeFile="CreateEmployee.aspx.cs" Inherits="CreateEmployee" %>

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

                .rows {
            background-color: #fff;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-size: 15px;
            color: #000;
            height: 50px;
            text-align: center;
            border: none 0px transparent;
        }
    </style>

    <br />
    <h2>
        <asp:Label runat="server" Text="Employee Information" ID="lblTitle" CssClass="reward-points" Width="30%" Font-Size="X-Large" ForeColor="#17468A"></asp:Label></h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="btn-aligncenter" style="width: 90%;"></div>
    <div style="margin-right: 40px; margin-left: 40px; text-align: center;">
        <asp:Table ID="Table1" runat="server" CellPadding="3" CellSpacing="5" HorizontalAlign ="Center">
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
                    <asp:TextBox ID="txtName" runat="server" Width="180px"></asp:TextBox>


                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label1" runat="server" Text="or" ForeColor="LightGray"></asp:Label>


                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtemail" runat="server" Width="180px"></asp:TextBox>


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
        <div style="float: left;  width: 90%">
            <asp:GridView ID="gdvShow" HorizontalAlign="Center" ForeColor="#333333" runat="server" CellPadding="3" GridLines="None" AutoGenerateColumns="False" DataKeyNames="PersonID" AllowPaging ="true" OnPageIndexChanging ="gdvShow_PageIndexChanging" PageSize="10" EmptyDataText="No Record Found" CellSpacing="1" OnSelectedIndexChanged="gdvShow_SelectedIndexChanged" >
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="rows" />
            <Columns>
                <asp:BoundField DataField="PersonID" HeaderText="PersonID" InsertVisible="False" ReadOnly="True" SortExpression="PersonID" />
                <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                <asp:BoundField DataField="MI" HeaderText="MI" SortExpression="MI" />
                <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                <asp:BoundField DataField="PersonEmail" HeaderText="PersonEmail" SortExpression="PersonEmail" />
                <asp:BoundField DataField="JobTitle" HeaderText="JobTitle" SortExpression="JobTitle" />
                <asp:BoundField DataField="Privilege" HeaderText="Privilege" SortExpression="Privilege" />
                 <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
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
        <asp:Table ID="Table2" runat="server" CellPadding="3" CellSpacing="5"  HorizontalAlign ="Center">

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
                    <asp:Label ID="Label2" runat="server" Text="or" ForeColor="LightGray"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnAdd" runat="server" Text="Add New Employee" Width="180px" CssClass="btn" />


                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
    <ajaxToolkit:ModalPopupExtender ID="popAdd" runat="server" TargetControlID="btnAdd" PopupControlID="divAdd" CancelControlID="btnCancel" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popUpdate" runat="server" TargetControlID="btnUpdate" PopupControlID="divChange" CancelControlID="btnClose" BackgroundCssClass="modalBackground" ></ajaxToolkit:ModalPopupExtender>

    <div id="divAdd" class="popup" style="width: 800px;">
        <h1>ADD NEW Employee</h1>
        <asp:Table ID="AddEmployee" runat="server" CssClass="table" Width="800px">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblFirstName" runat="server" class="auto-style1" Text="First Name:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Provider" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtFirstName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell><asp:TableCell>
                    
                    <asp:RegularExpressionValidator ValidationGroup="Provider" ID="ProviderNameMessage" runat="server" ForeColor="Red" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z\s']{1,20}$" Text="No more than 20 characters " />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblMI" runat="server" class="auto-style1"  Text="Middle Initial:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtMI" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    
                    <asp:RegularExpressionValidator ValidationGroup="Provider" ForeColor="Red" ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMI" ValidationExpression="^[a-zA-Z\s]{0,1}$" Text="One character" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblLastName" runat="server" class="auto-style1" Text="Last Name:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Provider" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtLastName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell><asp:TableCell>
                    
                    <asp:RegularExpressionValidator ValidationGroup="Provider" ID="RegularExpressionValidator3" runat="server" ForeColor="Red" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z\s']{1,20}$" Text="No more than 20 characters " />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblEmployeeEmail" runat="server" class="auto-style1" Text="Employee Email:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtEmployeeEmail" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequireddValidator1" ValidationGroup="Provider" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtEmployeeEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell><asp:TableCell>
                    
                    <asp:RegularExpressionValidator ValidationGroup="Provider" ID="RegularExpressionValidator2" runat="server" ForeColor="Red"
                        ControlToValidate="txtEmployeeEmail" ErrorMessage="Please enter corect email, eg. john@example.com"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblJobTitle" runat="server" class="auto-style1" Text="Job Title:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                     <asp:DropDownList ID="ddlJob" runat="server" Width="200px" CssClass="textbox">
                    <asp:ListItem Value="0">Choose a Title</asp:ListItem>
                    <asp:ListItem Value="1">Trucking</asp:ListItem>
                    <asp:ListItem Value="2">Logistics</asp:ListItem>
                </asp:DropDownList>
                    <asp:CompareValidator ID="reqJob" runat="server" ControlToValidate="ddlJob" ErrorMessage="*" ForeColor="Red"
                                ValueToCompare="0" Operator="NotEqual" ValidationGroup="Provider"></asp:CompareValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblPosition" runat="server" Text="Type:"></asp:Label>
                </asp:TableCell><asp:TableCell Width="20%">
                    <asp:RadioButtonList ID="type" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="Administrative" Text="Administrative"></asp:ListItem>
                        <asp:ListItem Value="Employee" Text="Employee"></asp:ListItem>
                    </asp:RadioButtonList>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="Provider" ID="typeRequired" ControlToValidate="type" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>          
                </asp:TableCell><asp:TableCell>              
                </asp:TableCell><asp:TableCell>            
                </asp:TableCell></asp:TableRow><asp:TableFooterRow>
                <asp:TableCell>
                    <asp:Button ID="btnAddNew" runat="server" class="auto-style6" Text="Add" Width="130px" ValidationGroup="Provider" OnClick="btnAddNew_Click" CssClass="button"></asp:Button>
                </asp:TableCell></asp:TableFooterRow></asp:Table><asp:Button ID="btnCancel" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
    </div>

    <div id="divChange" class="popup" style="width: 800px;">
        <h1>Update Employee</h1><asp:Table ID="Table3" runat="server" CssClass="table" Width="800px">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblFirst" runat="server" class="auto-style1" Text="First Name:"></asp:Label>
                </asp:TableCell><asp:TableCell> 
                    <asp:TextBox ID="txtFirst" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="update" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtFirst" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell><asp:TableCell>
                    
                    <asp:RegularExpressionValidator ValidationGroup="update" ForeColor="Red" ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtFirst" ValidationExpression="^[a-zA-Z\s']{1,20}$" Text="No more than 20 characters " />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblMiddle" runat="server" class="auto-style1" Text="Middle Initial:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtMiddle" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    
                    <asp:RegularExpressionValidator ValidationGroup="update" ForeColor="Red" ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtMiddle" ValidationExpression="^[a-zA-Z\s]{0,1}$" Text="One character" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblLast" runat="server" class="auto-style1" Text="Last Name:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtLast" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="update" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtLast" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell><asp:TableCell>
                    
                    <asp:RegularExpressionValidator ValidationGroup="update" ID="RegularExpressionValidator6" runat="server" ForeColor="Red" ControlToValidate="txtLast" ValidationExpression="^[a-zA-Z\s']{1,20}$" Text="No more than 20 characters " />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblEmployee" runat="server" class="auto-style1" Text="Employee Email:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtEmployee" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="update" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtEmployee" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell><asp:TableCell>
                    
                    <asp:RegularExpressionValidator ValidationGroup="update" ID="RegularExpressionValidator7" runat="server"
                        ControlToValidate="txtEmployee" ErrorMessage="Please enter corect email, eg. john@example.com" ForeColor="Red"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblJob" runat="server" class="auto-style1" Text="Job Title:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                     <asp:DropDownList ID="ddlTitle" runat="server" Width="200px" CssClass="textbox">
                    <asp:ListItem Value="0">Choose a Title</asp:ListItem>
                    <asp:ListItem Value="1">Trucking</asp:ListItem>
                    <asp:ListItem Value="2">Logistics</asp:ListItem>
                </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlTitle" ErrorMessage="*" ForeColor="Red"
                                ValueToCompare="0" Operator="NotEqual" ValidationGroup="update"></asp:CompareValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblType" runat="server" Text="Type:"></asp:Label>
                </asp:TableCell><asp:TableCell Width="20%">
                    <asp:RadioButtonList ID="rdtnType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="Administrative" Text="Administrative"></asp:ListItem>
                        <asp:ListItem Value="Employee" Text="Employee"></asp:ListItem>
                    </asp:RadioButtonList>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="update" ID="RequiredFieldValidator6" ControlToValidate="rdtnType" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>          
                </asp:TableCell><asp:TableCell>              
                </asp:TableCell><asp:TableCell>            
                </asp:TableCell></asp:TableRow><asp:TableFooterRow>
                <asp:TableCell>
                    <asp:Button ID="btnUpdateEmployee" runat="server" class="auto-style6" Text="Update" Width="130px" ValidationGroup="update" OnClick="btnUpdateEmployee_Click" CssClass="button"></asp:Button>
                </asp:TableCell></asp:TableFooterRow></asp:Table><asp:Button ID="btnClose" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdminMasterPage.master" AutoEventWireup="true" CodeFile="SystemAdmin.aspx.cs" Inherits="SystemAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
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
        .auto-style2 {
            margin-left: 116px;
        }
    </style>
    <br />
    <h2><asp:Label runat="server" Text="Terminate Users" ID="lblTitle" CssClass="reward-points" Width="30%" Font-Size="X-Large" ForeColor="#17468A"></asp:Label></h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="btn-aligncenter">
  </div>

    <div style="text-align:center;font-size:medium; width:77%; height:100%;margin-left:10%">
        <br />
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
        <div style="float: left; width: 80%;margin-left:5%">
            <asp:GridView ID="gdvShow" HorizontalAlign="Center" ForeColor="#333333" runat="server" CellPadding="3" GridLines="None" AutoGenerateColumns="False" AllowPaging ="True" OnPageIndexChanging ="gdvShow_PageIndexChanging" DataKeyNames="BusinessEntityID" CellSpacing="1" Width="64%" OnRowDeleting="gdvShow_RowDeleting" style="margin-left: 12%" >
               <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="rows" />
                <Columns>
                <asp:TemplateField HeaderText="BusinessEntityID">
                    <%--<EditItemTemplate>
                        <asp:Label ID="lbleditPID" runat="server" Text='<%# Eval("ProjectID") %>'></asp:Label>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                        <asp:Label ID="lblBusinessEntityID" runat="server" Text='<%# Eval("BusinessEntityID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BusinessEntityName">
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="txteditPName" runat="server" Text='<%# Eval("ProjectName") %>'></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                        <asp:Label ID="lblBusinessEntityName" runat="server" Text='<%# Eval("BusinessEntityName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BusinessEntityEmail">
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="txtEditPDes" runat="server" Text='<%# Eval("ProjectDescription") %>'></asp:TextBox>
                    </EditItemTemplate>--%>
                    <ItemTemplate>
                        <asp:Label ID="lblBusinessEntityEmail" runat="server" Text='<%# Eval("BusinessEntityEmail") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                     <asp:CommandField DeleteText="Terminate" ShowDeleteButton="True" />
                </Columns>
                <%--<Columns>
                    <asp:BoundField DataField="BusinessEntityID" HeaderText="BusinessEntityID" SortExpression="BusinessEntityID" InsertVisible="False" ReadOnly="True" />
                    <asp:BoundField DataField="BusinessEntityName" HeaderText="BusinessEntityName" SortExpression="BusinessEntityName" />                    
                    <asp:BoundField DataField="BusinessEntityEmail" HeaderText="BusinessEntityEmail" SortExpression="BusinessEntityEmail" />
                    <asp:CommandField DeleteText="Terminate" ShowDeleteButton="True" />
                </Columns>--%>
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
        </div>
            <div >
            <asp:Table ID="Table2" runat="server" CaptionAlign="Left" CellPadding="10" CellSpacing="5" CssClass="auto-style2" Width="423px">
                <asp:TableRow>
                    <asp:TableCell>
                     <asp:Image ID="Image2" runat="server" ImageUrl="~/image/Excel.jpg" Height="66px" Width="77px" />
                         </asp:TableCell>
                    <asp:TableCell>
                    <asp:LinkButton ID="User" runat="server" OnClick="Download">Download Users</asp:LinkButton>
                    </asp:TableCell>
                   

                </asp:TableRow>
            </asp:Table>
             
        
        </div>
    </div>
    </asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true" CodeFile="UserDashboard.aspx.cs" Inherits="UserDashboard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type ="text/css">
        body {
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
        .auto-style14 {
            width: 278px;
        }
        .auto-style15 {
            width: 386px;
        }
        .auto-style16 {
            font-family: 'Raleway', sans-serif; /*height: 10%;*/;
            padding: 5px 5px 5px 5px;
            margin-left: auto;
            margin-right: auto;
            border-radius: 5px;
            border: 2px solid #17468A;
            margin: 2%;
        }
    </style>
    <br />
    <h2>
        <asp:Label runat="server" Text="Redemptions & Activities Dashboard" ID="lblName" CssClass="reward-points" Width="40%" Font-Size="X-Large" ForeColor="#17468A"></asp:Label></h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div class="btn-aligncenter" style="width: 90%">
    </div>
    <div style="float:left;width:40%">
        <table>
        <tr>
            <td class="auto-style15">
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Large" Font-Underline="true" ForeColor="#0B3171" Text="Your Redemptions"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style15">
                <strong>
                <asp:Label ID="Label5" runat="server" Text="Your Current Points:" style="color: #8C8C8C; margin-left:5%" Width="217px"></asp:Label>
                
                <a href="CashOut.aspx"><asp:Label ID="balance" CssClass="auto-style16" runat="server" ForeColor="Black" Text="Label" style="font-size: large; text-decoration: none" Width="85px"></asp:Label></a>
            
                </strong>
                </td>    
        </tr>
        <tr style="width:70%">
            <td class="auto-style15">
                <div class ="auto-style13">
                    <div class ="rounded-corners"></div>
                <asp:GridView ID="RedeemedGrid" runat="server" AllowPaging ="True" OnPageIndexChanging ="grdData_PageIndexChanging" Width="79%" CellPadding="4" ForeColor="#333333"  RowStyle-CssClass= "rows" GridLines="None" PageSize="10">
        
               
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

                            <HeaderStyle CssClass="header" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

                            <PagerStyle CssClass="pager" BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>

                          <%--  <RowStyle CssClass="rows" BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />--%>
        
               
                </asp:GridView>
                      <div class ="rounded-corners"></div>
                    </div>
            </td>
            
        </tr></table>
    </div>
    <div style="float:left;width:40%">
        <table>
        <tr>
            <td>
                <strong>
                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="Large" Font-Underline="true" ForeColor="#0B3171" Text="Your Activities"></asp:Label>
                </strong>
                
                </td>
        </tr>
        <tr>
            <td>
                <strong>
                    <asp:Label ID="Label7" runat="server" CssClass="auto-style14" Text="Number of Activites Completed:" style="color: #8C8C8C; margin-left:5%"></asp:Label>
                
                <asp:Label ID="actquantity" runat="server" CssClass="auto-style14" style="font-size: x-large; color: #B3C100;" Text="Label"></asp:Label>
            
                </strong>
            </td>
        </tr>
        <tr style="width:70%">
            <td>
                <div class ="auto-style13">
                      <div class ="rounded-corners"></div>
                <asp:GridView ID="ActivitiesGrid" runat="server" AllowPaging ="true" OnPageIndexChanging ="ActivitiesGrid_PageIndexChanging" DataField="RedeemedDate"  RowStyle-CssClass= "rows" GridLines="None" SortExpression="RedeemedDate"
                    DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" Width="97%" CellPadding="4">
<%--                     <Columns>
            <asp:BoundField DataField="Value.ValueName" HeaderText="Company Value" />
            <asp:BoundField DataField="Category.CategoryName" HeaderText="Company Category" />
           <asp:BoundField DataField="PeerTransaction.PointsAmount" HeaderText="Points" />
        </Columns>--%>
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

                            <HeaderStyle CssClass="header" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

                            <PagerStyle CssClass="pager" BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>

                          <%--  <RowStyle CssClass="rows" BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />--%>
                </asp:GridView>
                      <div class ="rounded-corners"></div>
                    </div>
            </td>
            
        </tr>
    </table>
    </div>
</asp:Content>





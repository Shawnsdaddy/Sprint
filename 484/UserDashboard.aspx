<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true" CodeFile="UserDashboard.aspx.cs" Inherits="UserDashboard" %>


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
            font-family: 'Raleway', sans-serif; /*height: 10%;*/
            
            padding: 5px 5px 5px 5px;
            margin-left: auto;
            margin-right: auto;
            border-radius: 5px;
            border: 2px solid #17468A;
            margin: 2%;
        }

        #redem {
            float: left;
            width: 40%;
        }
        #activity{
            float: right; 
            width: 50%;
        }
        @media screen and (min-width:733px) and (max-width: 1023px) {
            .rows {
                font-size: 10px;
                min-height: 10px;
            }

            .header {
                border: none 0px transparent;
                height: 15px;
                text-align: center;
                font-size: 14px;
            }
        }
        @media screen and (max-width: 732px) {
            #redem {
            float:none;     
            display:inline;
            width:100%
        }
            #activity{
            float: none; 
            width: 100%;
        }
            main{
                padding:0;
            }
        }
    </style>
    <br />
    <h2>
        <asp:Label runat="server" Text="Redemptions & Activities Dashboard" ID="lblName" CssClass="reward-points" Font-Size="X-Large" ForeColor="#17468A"></asp:Label></h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div id="redem">
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Large" Font-Underline="true" ForeColor="#0B3171" Text="Your Redemptions"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Label ID="Label5" runat="server" Text="Your Current Points:" Style="color: #8C8C8C; margin-left: 5%"></asp:Label>

                        <a href="CashOut.aspx">
                            <asp:Label ID="balance" CssClass="auto-style16" runat="server" ForeColor="Black" Text="Label" Style="font-size: large; text-decoration: none"></asp:Label></a>

                    </strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="auto-style13">
                        <div class="rounded-corners"></div>
                        <asp:GridView ID="RedeemedGrid" runat="server" AllowPaging="True" OnPageIndexChanging="grdData_PageIndexChanging" CellPadding="4" ForeColor="#333333" RowStyle-CssClass="rows" GridLines="None" PageSize="10">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle CssClass="header" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                            <PagerStyle CssClass="pager" BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>


                        </asp:GridView>
                        <div class="rounded-corners"></div>
                    </div>
                </td>

            </tr>
        </table>
    </div>
    <div id="activity">
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
                        <asp:Label ID="Label7" runat="server" CssClass="auto-style14" Text="Number of Activites Completed:" Style="color: #8C8C8C; margin-left: 5%"></asp:Label>

                        <asp:Label ID="actquantity" runat="server" CssClass="auto-style14" Style="font-size: x-large; color: #B3C100;" Text=""></asp:Label>

                    </strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="auto-style13">
                        <div class="rounded-corners"></div>
                        <asp:GridView ID="ActivitiesGrid" runat="server" AllowPaging="true" OnPageIndexChanging="ActivitiesGrid_PageIndexChanging" DataField="RedeemedDate" RowStyle-CssClass="rows" GridLines="None" SortExpression="RedeemedDate"
                            DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" Width="97%" CellPadding="4">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

                            <HeaderStyle CssClass="header" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

                            <PagerStyle CssClass="pager" BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>

                        </asp:GridView>
                        <div class="rounded-corners"></div>
                    </div>
                </td>

            </tr>
        </table>
    </div>
</asp:Content>





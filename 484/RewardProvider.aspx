<%@ Page Title="" Language="C#" MasterPageFile="~/RewardProviderMasterPage.master" AutoEventWireup="true" CodeFile="RewardProvider.aspx.cs" Inherits="RewardProvider" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div class="btn-aligncenter">
        <br />
        <asp:Label ID="lblPoints" runat="server" CssClass="reward-points" Font-Size="X-Large"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnEvent" runat="server" Text="Add Event" CssClass="button" TabIndex="3" OnClick="btnEvent_Click" />


        <br />


        <br />
    </div>
    <div style="align-content: center">
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Table ID="EventDetails" runat="server" CssClass="calendar" Width="80%" HorizontalAlign="Center">
             <asp:TableRow>
                 <asp:TableCell>
                     <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChange" CssClass="btn-aligncenter" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="70%">
                         <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                         <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                         <OtherMonthDayStyle ForeColor="#999999" />
                         <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                         <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                         <TodayDayStyle BackColor="#CCCCCC" />
                     </asp:Calendar>
                 </asp:TableCell>
                 
             </asp:TableRow>

         </asp:Table>
        <br />
        <br />

        <asp:GridView ID="gvEvents" runat="server" AllowPaging="True" OnPageIndexChanging="gvEvents_PageIndexChanging" CellPadding="4"  GridLines="None" PageSize="5" ForeColor="#333333" HorizontalAlign ="Center">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />




        </asp:GridView>

        <br />

    </div>
    <ajaxToolkit:ModalPopupExtender ID="popUpdate" runat="server" TargetControlID="btnContinuetoAdd" PopupControlID="divAddEvent" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popSelectCompany" runat="server" TargetControlID="btnEvent" PopupControlID="divSelectCompany" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
    <div id="divSelectCompany" class="popup" style="width: 700px">
      <asp:Button ID="Button1" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
        <h1> SELECT COMPANY</h1>
        <%--<asp:Table ID="CompanyTable" runat="server" CssClass="table" Width="700px"  HorizontalAlign="Center">--%>
        <asp:GridView ID="CompanyGrid" runat="server" AllowPaging="True" RowStyle-CssClass="rows" OnSelectedIndexChanged="CompanyGrid_SelectedIndexChanged" OnPageIndexChanging="CompanyGrid_PageIndexChanging" Width="90%" CellPadding="4" ForeColor="#333333" GridLines="None" PageSize="5" EmptyDataText="No Record Found" CellSpacing="1" AutoGenerateColumns="False">

            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="BusinessEntityID" Visible ="false">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("BusinessEntityID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("BusinessEntityID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BusinessEntityName">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("BusinessEntityName") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("BusinessEntityName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BusinessEntityEmail">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("BusinessEntityEmail") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("BusinessEntityEmail") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
<RowStyle CssClass="rows" BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />


        </asp:GridView>
           <%-- <asp:TableRow>
                <asp:TableCell>--%>
                    <asp:Button ID="BtnContinuetoAdd" runat="server" class="auto-style6" Text="Add" Width="130px" ValidationGroup="Provider" OnClick="BtnContinuetoAdd_Click" CssClass="button"></asp:Button>
              <%--  </asp:TableCell>
            </asp:TableRow>--%>
        <%--</asp:Table>--%>
    </div>
    <div id="divAddEvent" class="popup" style="width: 700px">
        <h1>ADD NEW EVENTS</h1>
        <asp:Table ID="Table1" runat="server" CssClass="table" Width="700px" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblEventName" runat="server" class="auto-style1" Text="Event Name:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtEventName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Provider" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtEventName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>                   
                </asp:TableCell>
                <asp:TableCell>
                    <asp:RegularExpressionValidator ValidationGroup="Provider" ID="EventName" ForeColor="Red" runat="server" ControlToValidate="txtEventName" ValidationExpression="^[a-zA-Z\s']{1,30}$" Text="No more than 30 characters " />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblTime" runat="server" class="auto-style1" Text="Event Time:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtTime" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Provider" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtTime" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell></asp:TableCell><asp:TableCell>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="Red" ErrorMessage="Enter correct time format,eg. 13:59" ValidationGroup="Provider" Display="Dynamic" ControlToValidate="txtTime" ValidationExpression="^(?:[01]\d|2[0-3]):[0-5]\d$"></asp:RegularExpressionValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblDate" runat="server" class="auto-style1" Text="Event Date:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Provider" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                    

                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell></asp:TableCell><asp:TableCell>
                     <asp:CompareValidator id="cmpEventDate" ControlToValidate ="txtDate" ForeColor="Red" Text ="MM/DD/YYYY" Operator="DataTypeCheck" Type="Date" Runat ="server" ValidationGroup="Provider" />
               &nbsp;&nbsp;&nbsp;&nbsp;     <asp:CompareValidator ID="laterthantoday" runat="server"  ValidationGroup="Provider" ControlToValidate="txtDate" ForeColor="Red" Type="Date" Operator="GreaterThanEqual" ErrorMessage="You cannot add an event in the past time! "></asp:CompareValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                 <asp:TableCell>
                    <asp:Label ID="lblStreet" runat="server" class="auto-style1" Text="Street:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtStreet" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Provider" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtStreet" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblCity" runat="server" class="auto-style1" Text="City:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtCity" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequireddValidator1" ValidationGroup="Provider" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtCity" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblState" runat="server" class="auto-style1" Text="State:"></asp:Label>
                </asp:TableCell><asp:TableCell>

                    <asp:DropDownList ID="drpState" runat="server" CssClass="textbox" Width="200px">
                        <asp:ListItem Value="0" Text="Choose State"></asp:ListItem>
                        <asp:ListItem>AK</asp:ListItem>
                        <asp:ListItem>AL</asp:ListItem>
                        <asp:ListItem>AZ</asp:ListItem>
                        <asp:ListItem>AR</asp:ListItem>
                        <asp:ListItem>CA</asp:ListItem>
                        <asp:ListItem>CO</asp:ListItem>
                        <asp:ListItem>CT</asp:ListItem>
                        <asp:ListItem>DE</asp:ListItem>
                        <asp:ListItem>FL</asp:ListItem>
                        <asp:ListItem>GA</asp:ListItem>
                        <asp:ListItem>HI</asp:ListItem>
                        <asp:ListItem>ID</asp:ListItem>
                        <asp:ListItem>IL</asp:ListItem>
                        <asp:ListItem>IN</asp:ListItem>
                        <asp:ListItem>IA</asp:ListItem>
                        <asp:ListItem>KS</asp:ListItem>
                        <asp:ListItem>KY</asp:ListItem>
                        <asp:ListItem>LA</asp:ListItem>
                        <asp:ListItem>ME</asp:ListItem>
                        <asp:ListItem>MD</asp:ListItem>
                        <asp:ListItem>MA</asp:ListItem>
                        <asp:ListItem>MI</asp:ListItem>
                        <asp:ListItem>MN</asp:ListItem>
                        <asp:ListItem>MS</asp:ListItem>
                        <asp:ListItem>MO</asp:ListItem>
                        <asp:ListItem>MT</asp:ListItem>
                        <asp:ListItem>NE</asp:ListItem>
                        <asp:ListItem>NV</asp:ListItem>
                        <asp:ListItem>NH</asp:ListItem>
                        <asp:ListItem>NJ</asp:ListItem>
                        <asp:ListItem>NM</asp:ListItem>
                        <asp:ListItem>NY</asp:ListItem>
                        <asp:ListItem>NC</asp:ListItem>
                        <asp:ListItem>ND</asp:ListItem>
                        <asp:ListItem>OH</asp:ListItem>
                        <asp:ListItem>OK</asp:ListItem>
                        <asp:ListItem>OR</asp:ListItem>
                        <asp:ListItem>PA</asp:ListItem>
                        <asp:ListItem>RI</asp:ListItem>
                        <asp:ListItem>SC</asp:ListItem>
                        <asp:ListItem>SD</asp:ListItem>
                        <asp:ListItem>TN</asp:ListItem>
                        <asp:ListItem>TX</asp:ListItem>
                        <asp:ListItem>UT</asp:ListItem>
                        <asp:ListItem>VT</asp:ListItem>
                        <asp:ListItem>VA</asp:ListItem>
                        <asp:ListItem>WA</asp:ListItem>
                        <asp:ListItem>WV</asp:ListItem>
                        <asp:ListItem>WI</asp:ListItem>
                        <asp:ListItem>WY</asp:ListItem>
                    </asp:DropDownList>
                    <asp:CompareValidator ID="reqTB" runat="server" ControlToValidate="drpState" ErrorMessage="*" ForeColor="Red"
                        ValueToCompare="0" Operator="NotEqual" ValidationGroup="Provider"></asp:CompareValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblZip" runat="server" Text="Zip:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtZip" runat="server" CssClass="textbox" Width="200px" MaxLength="5"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="Provider" ID="typeRequired" ControlToValidate="txtZip" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                     <asp:CompareValidator id="CompareValidator1" ControlToValidate ="txtZip" ForeColor="Red" Text ="Enter Integer" Operator="DataTypeCheck" Type="Integer" Runat ="server" ValidationGroup="Provider" />
                    
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell></asp:TableCell><asp:TableCell>
                    </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>          
                </asp:TableCell><asp:TableCell>                      
                </asp:TableCell></asp:TableRow><asp:TableFooterRow>
                <asp:TableCell>
                    <asp:Button ID="btnAddNew" runat="server" class="auto-style6" Text="Add" Width="130px" ValidationGroup="Provider" OnClick="btnAddNew_Click" CssClass="button"></asp:Button>
                    <asp:Button ID="btnautofill" runat="server" class="auto-style6" Text="Auto-fill" Width="130px" CausesValidation="false" OnClick="btnautofill_Click" CssClass="button"></asp:Button>

                </asp:TableCell></asp:TableFooterRow></asp:Table><asp:Button ID="btnCancel" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
        </div></asp:Content>
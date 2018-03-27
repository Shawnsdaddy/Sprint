<%@ Page Title="" Language="C#" MasterPageFile="~/RewardProviderMasterPage.master" AutoEventWireup="true" CodeFile="Providerprofile.aspx.cs" Inherits="Providerprofile" %>

<%@ MasterType VirtualPath="~/RewardProviderMasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type ="text/css">
        
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
        .pager{
            background-color: #B3C100;
            font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: white;
            height: 30px;
            text-align: center;

        }

        }
        .auto-style11 {
            color: #fff;
            text-decoration: none;
            font-weight: bold;
            border: 1px solid #fff;
            padding: 3px;
            background-color: transparent;
        }
        .auto-style12 {
            width: 1313px;
        }
        .auto-style13 {
            text-align: center;
        }
        .auto-style14 {
            width: 120%;
            margin-left: 8px;
        }
        .auto-style15 {
            text-align: right;
        }
        .auto-style16 {
            margin-left: 163px;
        }
        .auto-style17 {
            font-family: 'Raleway', sans-serif; /*height: 10%;*/
            padding: 3px;
            margin-left: auto;
            margin-right: auto;
            border-radius: 25px;
            border: 2px solid #17468A;
            margin: 2%;
        }
    </style>

    <br />
    <h2>
        <asp:Label runat="server" Text="" ID="lblName" CssClass="reward-points" Width="30%" Font-Size="X-Large" ForeColor="#17468A"></asp:Label></h2>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="btn-aligncenter" style="width: 90%">

        <div style="float: left; margin-left: 40px;">
            <br />
            <asp:Image ID="picture" runat="server" Height="200px" />
        </div>
        <div style="float: left; margin-left: 20px; text-align: left;">
            <h2>
                <asp:Label ID="lblFullName" runat="server" Text="" CssClass="reward-points"></asp:Label></h2>
            <br />
            <h3>
                <asp:Label ID="Label3" runat="server" Text="Contact" CssClass="reward-points"></asp:Label></h3>
            <asp:Label ID="lblContact" runat="server" Text="" CssClass="reward-points"></asp:Label>
        </div>
        <div style="float: right; margin-right: 40px; text-align: left;">
            <asp:Table ID="Table1" runat="server">
                <%--<asp:TableRow>
                    <asp:TableCell>
                        <asp:Button ID="btnProfile" runat="server" Text="Edit Profile" CssClass="btn-profile" Width="150px" />
                    </asp:TableCell>
                </asp:TableRow>--%>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Button ID="btnPass" runat="server" Text="Change Password" CssClass="btn-profile" Width="150px" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Button ID="btnPic" runat="server" Text="Change Picture" CssClass="btn-profile" Width="150px" />
                    </asp:TableCell>
                </asp:TableRow>
                 <asp:TableRow>
                    <asp:TableCell>
                        <asp:Button ID="btnSetDefault" runat="server" Text="Set Default Page" CssClass="btn-profile" Width="150px" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </div>
    <br />

    <div style="float: left; margin-left: 20px; text-align: center; overflow: auto; height: 60%; width: 90%; margin-top:10%">
        <asp:GridView ID="gdvShow" runat="server" AutoGenerateColumns="False"
            RowStyle-CssClass= "rows" CssClass="auto-style11" PagerStyle-CssClass="pager"
            PageSize="5" Width="70%" DataKeyNames="ProviderID,TypeOfBusiness,Amount" OnRowCancelingEdit="gdvShow_RowCancelingEdit" OnRowEditing="gdvShow_RowEditing" OnRowUpdating="gdvShow_RowUpdating" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging ="true" OnPageIndexChanging ="gdvShow_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="ProviderID" SortExpression="ProviderID">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("ProviderID") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProviderID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TypeOfBusiness" SortExpression="TypeOfBusiness">
                    <EditItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("TypeOfBusiness") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("TypeOfBusiness") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount" SortExpression="Amount">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:TextBox>
                        <%--<asp:Label ID="Label3" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>--%>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
<HeaderStyle CssClass="header" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

<PagerStyle CssClass="pager" BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>

<RowStyle CssClass="rows" BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" DeleteCommand="DELETE FROM [ProviderAmount] WHERE [ProviderID] = @ProviderID AND [TypeOfBusiness] = @TypeOfBusiness AND [Amount] = @Amount" InsertCommand="INSERT INTO [ProviderAmount] ([ProviderID], [TypeOfBusiness], [Amount]) VALUES (@ProviderID, @TypeOfBusiness, @Amount)" SelectCommand="SELECT [ProviderID], [TypeOfBusiness], [Amount] FROM [ProviderAmount]">
            <DeleteParameters>
                <asp:Parameter Name="ProviderID" Type="Int32" />
                <asp:Parameter Name="TypeOfBusiness" Type="String" />
                <asp:Parameter Name="Amount" Type="Decimal" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="ProviderID" Type="Int32" />
                <asp:Parameter Name="TypeOfBusiness" Type="String" />
                <asp:Parameter Name="Amount" Type="Decimal" />
            </InsertParameters>
        </asp:SqlDataSource>
    </div>

    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
    <%--<ajaxToolkit:ModalPopupExtender ID="popProfile" runat="server" TargetControlID="btnProfile" PopupControlID="divInfomation" CancelControlID="btnCancel" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>--%>
    <ajaxToolkit:ModalPopupExtender ID="popPass" runat="server" TargetControlID="btnPass" PopupControlID="divPassword" CancelControlID="btnClose" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popPic" runat="server" TargetControlID="btnPic" PopupControlID="divPicture" CancelControlID="btnClosed" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popDefault" runat="server" TargetControlID="btnSetDefault" PopupControlID="divDefault" CancelControlID="btnCLOSE1" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>

    <%--<div id="divInfomation" class="popup" style="width: 700px;">
        <h1>UPDATE PROFILE INFORMATION</h1>

        <asp:Table ID="EditProfile" runat="server" CssClass="table" Width="700px">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblFirstName" runat="server" class="auto-style1" Text="First Name:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtFirstName" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="FirstNameMessage" runat="server" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z\s']{1,20}$" Text="No more than 20 alphabetic characters in length " />
                </asp:TableCell></asp:TableRow><asp:TableRow>

                <asp:TableCell>
                    <asp:Label ID="lblMI" runat="server" Text="Middle Initial:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtMI" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="MIMessage" runat="server" ControlToValidate="txtMI" ValidationExpression="^[a-zA-Z\s']{0,1}$" Text="1 alphabetic characters in length" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblLastName" runat="server" class="auto-style1" Text="Last Name:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ID="RequireddValidator1" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtLastName" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="LastNameMessage" runat="server" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z\s']{1,30}$" Text="No more than 30 alphabetic characters in length" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblEmail" runat="server" class="auto-style1" Text="Email Address:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox"></asp:TextBox>
                </asp:TableCell><asp:TableCell>
                    <asp:RequiredFieldValidator ID="Required" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="RegularExpressionValidator1" runat="server"
                        ControlToValidate="txtEmail" ErrorMessage="Please enter corect email. For example john@example.com"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
    <%--            DONE email validation requries                
                <asp:RequiredFieldValidator ID="RequiredEmail" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="EmailMessage" runat="server" ControlToValidate="txtEmail" ValidationExpression="^[a-zA-Z\s']{0,30}$" Text="invalid input" />--%>
    <%--</asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>          
                </asp:TableCell><asp:TableCell>              
                </asp:TableCell><asp:TableCell>            
                </asp:TableCell></asp:TableRow><asp:TableFooterRow>
                <asp:TableCell>
                    <asp:Button ID="btnChangeProfile" runat="server" class="auto-style6" Text="Change Profile" Width="150px" ValidationGroup="profile" OnClick="btnChangeProfile_Click" CssClass="button"></asp:Button>
                </asp:TableCell></asp:TableFooterRow></asp:Table><asp:Button ID="btnCancel" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
    </div>--%>
    <div id="divPassword" class="popup" style="width: 700px;">
        <h1>UPDATE PASSWORD</h1><asp:Table ID="TablePassword" runat="server" CssClass="table" Width="700px" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblOldPass" runat="server" Text="Old PassWord:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtOldPass" TextMode="Password" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="password" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtOldPass" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblNewPass1" runat="server" Text="New PassWord:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtNew1" TextMode="Password" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="password" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtNew1" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell>
                    </asp:TableRow><asp:TableRow>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell>
                    
                    <asp:RegularExpressionValidator ValidationGroup="password" ID="RegularNew1" runat="server" ForeColor="Red" ControlToValidate="txtNew1" ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[\W]).{6,20})" Text="Must be 6-20 characters,at least one uppercase&lowercase&digital&special letter" CssClass="table" Display="Dynamic" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblNewPass2" runat="server" Text="Re-enter New Password:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtNew2" runat="server" TextMode="Password" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="password" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtNew2" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell>
                    </asp:TableRow><asp:TableRow>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell>
                    
                    <asp:CompareValidator ValidationGroup="password" ID="RegularNew2" runat="server" ForeColor="Red" ControlToValidate="txtNew2" ControlToCompare="txtNew1" Type="String" Operator="Equal" Text="Password must be same as new password" />
                </asp:TableCell></asp:TableRow>
            <asp:TableRow>
                    <asp:TableCell></asp:TableCell></asp:TableRow>
            <asp:TableFooterRow>
                <asp:TableCell>
                    <asp:Button ID="btnChangePassWord" ValidationGroup="password" runat="server" class="auto-style6" Text="Change Password" Width="150px" OnClick="btnChangePassword_Click" CssClass="button"></asp:Button>
                </asp:TableCell></asp:TableFooterRow></asp:Table><asp:Button ID="btnClose" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
    </div>
    <div id="divPicture" style="width: 500px; text-align: center; height: 500px;" class="popup">
        <h1>UPDATE PROFILE PICTURE</h1><asp:FileUpload ID="PictureUpload" runat="server" />
        <asp:Button ID="Upload" runat="server" Text="Upload" OnClick="Upload_Click" CssClass="button" />
        <asp:Image ID="ProfilePicture" runat="server" Height="270px" />
        <asp:Button ID="btnClosed" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
    </div>
     <div id="divDefault" style="width:500px" class="popup">
        <h1>SET YOUR DEFAULT PAGE</h1><asp:Button ID="btnCLOSE1" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
        <asp:Table ID="Table2" runat="server" Width="211px" HorizontalAlign="Center"><asp:TableRow>
                <asp:TableCell>
                     <asp:Label ID="lblSetpage" runat="server" Text="Pages:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:DropDownList ID="dropPages" runat="server" Enabled="True" Width="200px" CssClass="textbox" AppendDataBoundItems="True">
                      <asp:ListItem Text="Choose Default Page" Value="0">
                       </asp:ListItem>
                        <asp:ListItem Text="Home Page" Value="Homepage">
                        </asp:ListItem>
                       
                           <asp:ListItem Text="Setting" Value="Setting">
                       </asp:ListItem>

                    </asp:DropDownList>
                    <asp:CompareValidator ID="reqTB" runat="server" ControlToValidate="dropPages" ErrorMessage="*" ForeColor="Red"
                        ValueToCompare="0" Operator="NotEqual" ValidationGroup="provider"></asp:CompareValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="btnSet" runat="server" Text="Set" Width="100px" OnClick="btnSet_Click" CssClass="button" ValidationGroup="provider" />
                </asp:TableCell></asp:TableRow></asp:Table></div></asp:Content>
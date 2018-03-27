<%@ Page Title="" Language="C#" MasterPageFile="~/CEOMasterPage.master" AutoEventWireup="true" CodeFile="CEOprofile.aspx.cs" Inherits="CEOprofile" %>

<%@ MasterType VirtualPath="~/CEOMasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                <asp:Label ID="Label1" runat="server" Text="Position" CssClass="reward-points"></asp:Label></h3>
            <asp:Label ID="lblPosition" runat="server" Text="" CssClass="reward-points"></asp:Label>
            <br />
            <h3>
                <asp:Label ID="Label3" runat="server" Text="Contact" CssClass="reward-points"></asp:Label></h3>
            <asp:Label ID="lblContact" runat="server" Text="" CssClass="reward-points"></asp:Label>
        </div>
        <div style="float: right; margin-right: 40px; text-align: left;">
            <asp:Table ID="Table1" runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Button ID="btnProfile" runat="server" Text="Edit Profile" CssClass="btn-profile" Width="150px" />
                    </asp:TableCell>
                </asp:TableRow>
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

    <%--<div class="btn-aligncenter">

        <asp:Button ID="btnProfile" runat="server" Text="Edit Profile" CssClass="btn-profile" />

        <asp:Button ID="btnPass" runat="server" Text="Change Password" CssClass="btn-profile" />
        <asp:Button ID="btnPic" runat="server" Text="Change Profile Picture" CssClass="btn-profile" />
    </div>--%>
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
    <ajaxToolkit:ModalPopupExtender ID="popProfile" runat="server" TargetControlID="btnProfile" PopupControlID="divInfomation" CancelControlID="btnCancel" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popPass" runat="server" TargetControlID="btnPass" PopupControlID="divPassword" CancelControlID="btnClose" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popPic" runat="server" TargetControlID="btnPic" PopupControlID="divPicture" CancelControlID="btnClosed" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popDefault" runat="server" TargetControlID="btnSetDefault" PopupControlID="divDefault" CancelControlID="btnCLOSE1" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>


    <div id="divInfomation" class="popup" style="width: 700px;">
        <h1>UPDATE PROFILE INFORMATION</h1>

        <asp:Table ID="EditProfile" runat="server" CssClass="table" Width="700px" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblFirstName" runat="server" class="auto-style1" Text="First Name:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtFirstName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell>
                </asp:TableRow><asp:TableRow>
                    <asp:TableCell></asp:TableCell>
                <asp:TableCell>
                    
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="FirstNameMessage" runat="server" ForeColor="Red" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z\s']{1,20}$" Text="No more than 20 characters" />
                </asp:TableCell></asp:TableRow><asp:TableRow>

                <asp:TableCell>
                    <asp:Label ID="lblMI" runat="server" Text="Middle Initial:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtMI" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                </asp:TableCell>
                    </asp:TableRow><asp:TableRow>
                        <asp:TableCell></asp:TableCell>
                    <asp:TableCell>
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="MIMessage" runat="server" ControlToValidate="txtMI" ValidationExpression="^[a-zA-Z\s']{0,1}$" Text="1 alphabetic characters" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblLastName" runat="server" class="auto-style1" Text="Last Name:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequireddValidator1" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtLastName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell>
                    
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="LastNameMessage" runat="server" ForeColor="Red" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z\s']{1,30}$" Text="No more than 30 characters" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblEmail" runat="server" class="auto-style1" Text="Email Address:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="Required" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell>
                    </asp:TableRow><asp:TableRow>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell>
                    
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="RegularExpressionValidator1" runat="server" ForeColor="Red"
                        ControlToValidate="txtEmail" ErrorMessage="Enter corect email,eg john@example.com"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    <%--            DONE email validation requries                
                <asp:RequiredFieldValidator ID="RequiredEmail" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="EmailMessage" runat="server" ControlToValidate="txtEmail" ValidationExpression="^[a-zA-Z\s']{0,30}$" Text="invalid input" />--%>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>          
                </asp:TableCell><asp:TableCell>                         
                </asp:TableCell></asp:TableRow><asp:TableFooterRow>
                <asp:TableCell>
                    <asp:Button ID="btnChangeProfile" runat="server" class="auto-style6" Text="Change Profile" Width="150px" ValidationGroup="profile" OnClick="btnChangeProfile_Click" CssClass="button"></asp:Button>
                </asp:TableCell></asp:TableFooterRow></asp:Table><asp:Button ID="btnCancel" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
    </div>
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
                    
                    <asp:RegularExpressionValidator ValidationGroup="password" ID="RegularNew1" ForeColor="Red" runat="server" ControlToValidate="txtNew1" ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[\W]).{6,20})" Text="Must be 6-20 characters,at least one uppercase&lowercase&digital&special letter" CssClass="table" Display="Dynamic" />
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
                       <asp:ListItem Text="Provider Infor" Value="ProviderInfor">
                       </asp:ListItem>
                        <asp:ListItem Text="Employee Infor" Value="EmployeeInfor">
                       </asp:ListItem>
                        <asp:ListItem Text="View Report" Value="ViewReport">
                       </asp:ListItem>
                           <asp:ListItem Text="Setting" Value="Setting">
                       </asp:ListItem>

                    </asp:DropDownList>
                    <asp:CompareValidator ID="reqTB" runat="server" ControlToValidate="dropPages" ErrorMessage="*" ForeColor="Red"
                        ValueToCompare="0" Operator="NotEqual" ValidationGroup="provider"></asp:CompareValidator>
                </asp:TableCell></asp:TableRow>
            <asp:TableRow>
        
                <asp:TableCell>
                    <asp:Button ID="btnSet" runat="server" Text="Set" Width="100px" OnClick="btnSet_Click" CssClass="button" ValidationGroup="provider" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table></div></asp:Content>
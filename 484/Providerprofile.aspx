<%@ Page Title="" Language="C#" MasterPageFile="~/RewardProviderMasterPage.master" AutoEventWireup="true" CodeFile="Providerprofile.aspx.cs" Inherits="Providerprofile" %>

<%@ MasterType VirtualPath="~/RewardProviderMasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .btn-profile {
            width: auto;
        }

        #profilepic {
            float: left;
            margin-left: 40px;
            margin-top: 15px;
            visibility: visible;
            position: relative;
            width: 15%;
        }

        #profilecontent {
            float: left;
            margin-left: 20px;
            text-align: left;
        }

        .popup {
            width: 500px;
        }

        @media screen and (min-width:810px) and (max-width: 1023px) {
            #profilepic {
                width: 20%;
                margin-left: 20px;
            }

            .reward-points {
                font-size: large;
            }
        }

        @media screen and (max-width:809px) {
            #profilepic {
                visibility: hidden;
                position: absolute;
            }

            .btn-profile {
                font-size: 12px;
                height: 30px;
                margin: auto;
                width: 100%;
                margin-top: 20px;
            }

            .popup {
                width: 50%;
                height: auto;
                font-size: 14px;
            }

            table {
                font-size: 10px;
            }

            .textbox {
                width: 150px;
            }
        }
    </style>

    <br />
    <h2>
        <asp:Label runat="server" Text="" ID="lblName" CssClass="reward-points" Width="30%" Font-Size="X-Large" ForeColor="#17468A"></asp:Label></h2>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="btn-aligncenter">

        <div id="profilepic">
            <br />
            <asp:Image ID="picture" runat="server" Width="100%" />
        </div>
        <div id="profilecontent">
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
                        <asp:Button ID="btnPass" runat="server" Text="Change Password" CssClass="btn-profile" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Button ID="btnPic" runat="server" Text="Change Picture" CssClass="btn-profile" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Button ID="btnSetDefault" runat="server" Text="Set Default Page" CssClass="btn-profile" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </div>
    <br />



    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
    <ajaxToolkit:ModalPopupExtender ID="popPass" runat="server" TargetControlID="btnPass" PopupControlID="divPassword" CancelControlID="btnClose" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popPic" runat="server" TargetControlID="btnPic" PopupControlID="divPicture" CancelControlID="btnClosed" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popDefault" runat="server" TargetControlID="btnSetDefault" PopupControlID="divDefault" CancelControlID="btnCLOSE1" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>


    <div id="divPassword" class="popup">
        <h1>UPDATE PASSWORD</h1>
        <asp:Table ID="TablePassword" runat="server" CssClass="table" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblOldPass" runat="server" Text="Old Password:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtOldPass" TextMode="Password" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="password" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtOldPass" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblNewPass1" runat="server" Text="New Password:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtNew1" TextMode="Password" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="password" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtNew1" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell>

                    <asp:RegularExpressionValidator ValidationGroup="password" ID="RegularNew1" runat="server" ForeColor="Red" ControlToValidate="txtNew1" ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[\W]).{6,20})" Text="Must be 6-20 characters,at least one uppercase&lowercase&digital&special letter" CssClass="table" Display="Dynamic" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblNewPass2" runat="server" Text="Re-enter New Password:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtNew2" runat="server" TextMode="Password" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="password" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtNew2" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell><asp:TableCell>

                    <asp:CompareValidator ValidationGroup="password" ID="RegularNew2" runat="server" ForeColor="Red" ControlToValidate="txtNew2" ControlToCompare="txtNew1" Type="String" Operator="Equal" Text="Password must be same as new password" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableFooterRow>
                <asp:TableCell>
                    <asp:Button ID="btnChangePassWord" ValidationGroup="password" runat="server" class="auto-style6" Text="Change Password" Width="150px" OnClick="btnChangePassword_Click" CssClass="button"></asp:Button>
                </asp:TableCell>
            </asp:TableFooterRow>
        </asp:Table>
        <asp:Button ID="btnClose" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
    </div>
    <div id="divPicture" class="popup">
        <h1>UPDATE PROFILE PICTURE</h1>
        
        <div style="margin: auto">
            <asp:FileUpload ID="PictureUpload" runat="server" />
            <asp:Button ID="Upload" runat="server" Text="Upload" OnClick="Upload_Click" CssClass="button" />
            <asp:Image ID="ProfilePicture" runat="server" Height="270px"/>
            <asp:Button ID="btnClosed" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />

        </div>
    </div>
    <div id="divDefault" style="width: 500px" class="popup">
        <h1>SET YOUR DEFAULT PAGE</h1>
        <asp:Button ID="btnCLOSE1" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
        <asp:Table ID="Table2" runat="server" Width="211px" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblSetpage" runat="server" Text="Pages:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:DropDownList ID="dropPages" runat="server" Enabled="True" Width="200px" CssClass="textbox" AppendDataBoundItems="True">
                        <asp:ListItem Text="Choose Default Page" Value="0">
                        </asp:ListItem>
                        <asp:ListItem Text="Home Page" Value="Homepage">
                        </asp:ListItem>
                        <asp:ListItem Text="GiftCard Info" Value="GiftCardInfo">
                        </asp:ListItem>
                        <asp:ListItem Text="Setting" Value="Setting">
                        </asp:ListItem>

                    </asp:DropDownList>
                    <asp:CompareValidator ID="reqTB" runat="server" ControlToValidate="dropPages" ErrorMessage="*" ForeColor="Red"
                        ValueToCompare="0" Operator="NotEqual" ValidationGroup="provider"></asp:CompareValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="btnSet" runat="server" Text="Set" Width="100px" OnClick="btnSet_Click" CssClass="button" ValidationGroup="provider" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>

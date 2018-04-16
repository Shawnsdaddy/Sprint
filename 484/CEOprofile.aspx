<%@ Page Title="" Language="C#" MasterPageFile="~/CEOMasterPage.master" AutoEventWireup="true" CodeFile="CEOprofile.aspx.cs" Inherits="CEOprofile" %>

<%@ MasterType VirtualPath="~/CEOMasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <style type="text/css">
        #profilepic{
            float: left; 
            margin-left: 40px;
            margin-top:15px;
            visibility:visible;
            position:relative;
            width:15%;
        }
        #profilecontent{
            float: left; 
            margin-left: 20px; 
            text-align: left;
        }
        .btn-profile{
            width:auto;
        }
        #function{
            float:right;
        }
        @media screen and (min-width:900px) and (max-width: 1023px) {
            #profilepic{
            width:15%;
            margin-left: 20px;
            
        }
                    #profilecontent{
            float: left; 
            margin-left: 10px; 
            font-size:10px;
        }
       .btn-profile{
            font-size:12px;
            height:30px;
            margin:auto;
            width:100%;
            margin-top:20px;

        }
           
                    .reward-points{
                        font-size:large;
                    }
        }
        @media screen and (max-width:899px){
              #profilepic{
            visibility:hidden;
            position:absolute;
        }
                                  #profilecontent{
            float: left; 
            margin-left: 10px; 
            font-size:10px;
        }
                     .btn-profile{
            font-size:12px;
            height:30px;
            margin:auto;
            width:100%;
            margin-top:20px;

        }
        }

        </style>
    
    <br />
    <h2>
        <asp:Label runat="server" Text="" ID="lblName" CssClass="reward-points" Font-Size="X-Large" ForeColor="#17468A"></asp:Label></h2>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <div class="btn-aligncenter" style="width: 100%">

        <div id="profilepic">
            <br />
            <asp:Image ID="picture" runat="server" Width="100%" />
        </div>
        <div id="profilecontent">
            <h2>
                <asp:Label ID="lblFullName" runat="server" Text="" CssClass="reward-points"  Font-Bold="true"></asp:Label></h2>
            <br />
            <h3>
                <asp:Label ID="Label2" runat="server" Text="Nick Name" CssClass="reward-points" Font-Bold="true"></asp:Label></h3>
            <asp:Label ID="lblNick" runat="server" Text="" CssClass="reward-points"></asp:Label>
            <br />
            <h3>
                <asp:Label ID="Label1" runat="server" Text="Position" CssClass="reward-points"  Font-Bold="true"></asp:Label></h3>
            <asp:Label ID="lblPosition" runat="server" Text="" CssClass="reward-points"></asp:Label>
            <br />
            <h3>
                <asp:Label ID="Label3" runat="server" Text="Contact" CssClass="reward-points"  Font-Bold="true"></asp:Label></h3>
            <asp:Label ID="lblContact" runat="server" Text="" CssClass="reward-points"></asp:Label>
        </div>
        <div id="function">
            <asp:Table ID="Table1" runat="server" Width="342px">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Button ID="btnProfile" runat="server" Text="Edit Profile" CssClass="btn-profile"/>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Button ID="btnLimitation" runat="server" Text="Set Limitations" CssClass="btn-profile" />
                    </asp:TableCell>

                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Button ID="btnPass" runat="server" Text="Change Password" CssClass="btn-profile" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Button ID="btnValues" runat="server" Text="Set Company Values" CssClass="btn-profile"  />
                    </asp:TableCell>

                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Button ID="btnPic" runat="server" Text="Change Picture" CssClass="btn-profile"/>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Button ID="btnCategories" runat="server" Text="Set Company Categories" CssClass="btn-profile" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label runat="server"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Button ID="btnSetDefault" runat="server" Text="Set Default Page" CssClass="btn-profile" />
                    </asp:TableCell>
                </asp:TableRow>
            
            </asp:Table>
        </div>
    </div>
   
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
    <ajaxToolkit:ModalPopupExtender ID="popProfile" runat="server" TargetControlID="btnProfile" PopupControlID="divInfomation" CancelControlID="btnCancel" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popPass" runat="server" TargetControlID="btnPass" PopupControlID="divPassword" CancelControlID="btnClose" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popPic" runat="server" TargetControlID="btnPic" PopupControlID="divPicture" CancelControlID="btnClosed" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popDefault" runat="server" TargetControlID="btnSetDefault" PopupControlID="divDefault" CancelControlID="btnCLOSE1" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popLimit" runat="server" TargetControlID="btnLimitation" PopupControlID="divLimit" CancelControlID="btnCLOSE2" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popValue" runat="server" TargetControlID="btnValues" PopupControlID="divValue" CancelControlID="btnClose3" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:ModalPopupExtender ID="popCategory" runat="server" TargetControlID="btnCategories" PopupControlID="divCategory" CancelControlID="btnClose4" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
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
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell>

                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="FirstNameMessage" runat="server" ForeColor="Red" ControlToValidate="txtFirstName" ValidationExpression="^[a-zA-Z\s']{1,20}$" Text="No more than 20 characters" />
                </asp:TableCell></asp:TableRow><asp:TableRow>

                <asp:TableCell>
                    <asp:Label ID="lblMI" runat="server" Text="Middle Initial:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtMI" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell></asp:TableCell><asp:TableCell>
                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="MIMessage" runat="server" ControlToValidate="txtMI" ValidationExpression="^[a-zA-Z\s']{0,1}$" Text="1 alphabetic characters" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblLastName" runat="server" class="auto-style1" Text="Last Name:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequireddValidator1" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtLastName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell></asp:TableCell><asp:TableCell>

                    <asp:RegularExpressionValidator ValidationGroup="profile" ID="LastNameMessage" runat="server" ForeColor="Red" ControlToValidate="txtLastName" ValidationExpression="^[a-zA-Z\s']{1,30}$" Text="No more than 30 characters" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblNickName" runat="server" class="auto-style1" Text="Nick Name:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtNickName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>

                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblEmail" runat="server" class="auto-style1" Text="Email Address:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="Required" ValidationGroup="profile" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell></asp:TableCell><asp:TableCell>

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
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell></asp:TableCell><asp:TableCell>

                    <asp:RegularExpressionValidator ValidationGroup="password" ID="RegularNew1" ForeColor="Red" runat="server" ControlToValidate="txtNew1" ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[\W]).{6,20})" Text="Must be 6-20 characters,at least one uppercase&lowercase&digital&special letter" CssClass="table" Display="Dynamic" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblNewPass2" runat="server" Text="Re-enter New Password:"></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtNew2" runat="server" TextMode="Password" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="password" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtNew2" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell></asp:TableCell><asp:TableCell>

                    <asp:CompareValidator ValidationGroup="password" ID="RegularNew2" runat="server" ForeColor="Red" ControlToValidate="txtNew2" ControlToCompare="txtNew1" Type="String" Operator="Equal" Text="Password must be same as new password" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell></asp:TableCell></asp:TableRow><asp:TableFooterRow>
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
    <div id="divDefault" style="width: 500px" class="popup">
        <h1>SET YOUR DEFAULT PAGE</h1><asp:Button ID="btnCLOSE1" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
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
                </asp:TableCell></asp:TableRow><asp:TableRow>

                <asp:TableCell>
                    <asp:Button ID="btnSet" runat="server" Text="Set" Width="100px" OnClick="btnSet_Click" CssClass="button" ValidationGroup="provider" />
                </asp:TableCell></asp:TableRow></asp:Table></div><div id="divLimit" runat="server" style="width: 600px" class="popup">
        <h1>SET BUSINESS LIMITS</h1><asp:Button ID="btnCLOSE2" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />


        <asp:GridView ID="GridLimit" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="504px" AutoGenerateColumns="False" HorizontalAlign="Center" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating1">
            <AlternatingRowStyle BackColor="White" />

            <Columns>
                <asp:TemplateField HeaderText="Calendar Day">
                    <EditItemTemplate>
                        <asp:Label ID="lblLimitPeriod" runat="server" Text='<%# Eval("LimitPeriod") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPeriod" runat="server" Text='<%# Eval("LimitPeriod") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Give Limitation">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtlimit" runat="server" Text='<%# Eval("GiveLimitation") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbllimit" runat="server" Text='<%# Eval("GiveLimitation") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Alert Balance">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAlert" runat="server" Text='<%# Eval("AlertBalance") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblAlert" runat="server" Text='<%# Eval("AlertBalance") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField HeaderText="Edition" ShowEditButton="True" />
               
            </Columns>

            <EditRowStyle BackColor="#999999" />
            <EmptyDataTemplate>No Record Found</EmptyDataTemplate>  
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
    <div id="divValue" class="popup" style="width: 900px;">
        <h1>SET COMPANY VALUES</h1><asp:Button ID="btnClose3" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
        <br />
        <div style="text-align:center;margin-left:10%;">
            <asp:GridView ID="ValueGrid" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnPageIndexChanging="ValueGrid_PageIndexChanging" OnRowCancelingEdit="ValueGrid_RowCancelingEdit" OnRowEditing="ValueGrid_RowEditing" OnRowUpdating="ValueGrid_RowUpdating" OnRowDeleting="ValueGrid_RowDeleting" Width="85%" CellPadding="4" ForeColor="#333333" RowStyle-CssClass="rows" GridLines="None" PageSize="5">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                            <asp:TemplateField HeaderText="ValueID">                  
                    <ItemTemplate>
                        <asp:Label ID="lblValueID" runat="server" Text='<%# Eval("ValueID") %>'></asp:Label>
                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="ValueName">
                    <EditItemTemplate>
                        <asp:TextBox ID="textValueName" runat="server" Text='<%# Eval("ValueName") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblValueName" runat="server" Text='<%# Eval("ValueName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                            <asp:TemplateField HeaderText="ValueDescription">
                    <EditItemTemplate>
                        <asp:TextBox ID="textValueDescription" runat="server" Text='<%# Eval("ValueDescription") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblValueDescription" runat="server" Text='<%# Eval("ValueDescription") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" />
                            <asp:CommandField ShowDeleteButton="true" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                <EmptyDataTemplate>No Record Found</EmptyDataTemplate>  
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle CssClass="header" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

                        <PagerStyle CssClass="pager" BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                        
                    </asp:GridView>
        </div>
        <br />
        <asp:Table ID="Table4" runat="server" Width="60%" HorizontalAlign="Center"><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblValueName" runat="server" Text="Value Name: "></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtValueName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Value" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtValueName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblValueDesc" runat="server" Text="Value Description: "></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtValueDesc" runat="server" TextMode="MultiLine" CssClass="textbox" Width="200px" Height="100px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Value" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtValueDesc" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="btnAddNewValue" runat="server" Text="Add New Value" Width="60%" OnClick="btnAddNewValue_Click" CssClass="button" ValidationGroup="Value" />
                </asp:TableCell></asp:TableRow></asp:Table></div>
    
    
    <div id="divCategory" class="popup" style="width: 700px;">
        <h1>SET COMPANY CATEGORIES</h1><asp:Button ID="btnClose4" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
        
        <br />
        <div style="text-align:center;margin-left:15%;">
            <asp:GridView ID="CategoryGrid" runat="server" AllowPaging="True" OnRowEditing="CategoryGrid_RowEditing" OnRowCancelingEdit="CategoryGrid_RowCancelingEdit" OnRowUpdating="CategoryGrid_RowUpdating" OnRowDeleting="CategoryGrid_RowDeleting" AutoGenerateColumns="false" OnPageIndexChanging="grdData_PageIndexChanging" Width="79%" CellPadding="4" ForeColor="#333333" RowStyle-CssClass="rows" GridLines="None" PageSize="5">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                <EmptyDataTemplate>No Record Found</EmptyDataTemplate>  
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle CssClass="header" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle CssClass="pager" BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="CategoryID">                  
                    <ItemTemplate>
                        <asp:Label ID="lblCategoryID" runat="server" Text='<%# Eval("CategoryID") %>'></asp:Label>
                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="CategoryName">
                    <EditItemTemplate>
                        <asp:TextBox ID="textCategoryName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>                            
                            <asp:CommandField ShowEditButton="True" />
                            <asp:CommandField ShowDeleteButton="true" />
                        </Columns>
                    </asp:GridView>
        </div>
        <br />
        <asp:Table ID="CategoryTable" runat="server" Width="60%" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblCategoryName" runat="server" Text="Category Name: "></asp:Label>
                </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtCatName" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="Category" runat="server" class="auto-style1" ForeColor="Red" ControlToValidate="txtCatName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="btnAddNewCategory" runat="server" Text="Add New Category" Width="85%" OnClick="btnAddNewCategory_Click" CssClass="button" ValidationGroup="Category" />
                </asp:TableCell></asp:TableRow></asp:Table></div></asp:Content>
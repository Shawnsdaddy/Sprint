<%@ Page Title="" Language="C#" MasterPageFile="~/CEOMasterPage.master" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <script>
      function CheckAll(oCheckbox)
 {
     var GridView2 = document.getElementById("<%=GridView1.ClientID %>");
     for(i = 1;i < GridView2.rows.length; i++)
     {
        GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
      }
        }
        </script>
      <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
      <Columns>
   <asp:TemplateField>
    <HeaderTemplate>
     <input id="Checkbox2" type="checkbox" onclick="CheckAll(this)" runat="server" />
    </HeaderTemplate>
    <ItemTemplate>
     <asp:CheckBox ID="ItemCheckBox" runat="server" />
    </ItemTemplate>
   </asp:TemplateField>
          <asp:BoundField DataField="FirstName" HeaderText="FirstName" />
          <asp:BoundField DataField="LastName" HeaderText="LastName" />
          <asp:BoundField DataField="MI" HeaderText="MI" />
          <asp:BoundField DataField="PersonEmail" HeaderText="PersonEmail" />
          <asp:BoundField DataField="JobTitle" HeaderText="JobTitle" />
          <asp:BoundField DataField="Privilege" HeaderText="Privilege" />
  </Columns>
    </asp:GridView>
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/CEOMasterPage.master" AutoEventWireup="true" CodeFile="CEOPostWall.aspx.cs" Inherits="CEOLogin" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .modalPopup {
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="btn-aligncenter">
        <br/>
        <asp:Label ID="lblPoints" runat="server" Text="" CssClass="reward-points"></asp:Label>
        <br/>
        <asp:Button ID="btnReward" runat="server" Text="Reload Points" CssClass="button" Width="15%" TabIndex="3" />
        <br />
  </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
    <ajaxToolkit:ModalPopupExtender ID="popReward" runat="server" TargetControlID="btnReward" PopupControlID="pnlReward" CancelControlID="btnCancel" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <%--<ajaxToolkit:PopupControlExtender ID="popReward" runat="server" TargetControlID ="btnReward" PopupControlID ="pnlReward" Position="Center"></ajaxToolkit:PopupControlExtender>--%>

    <div id="SlideIn">
        <asp:Panel ID="pnlReward" runat="server" CssClass="popup">


            <p>&nbsp;&nbsp; &nbsp;</p>
            <p>Enter the amount of points to reload</p>
            <p>
                <asp:TextBox ID="txtFrontLoad" runat="server" CssClass="textbox"></asp:TextBox>

                <asp:Button ID="btnCommit" runat="server" Text="Reload" OnClick="btnCommit_Click" CssClass="button" />
                <asp:Button ID="btnCancel" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />

            </p>


        </asp:Panel>
    </div>
        <div>
    
            <asp:DataList ID="dlPosts" runat="server"  HorizontalAlign="Center">
            <ItemTemplate>
                
                        <section class="card-content">

                          <%--  <div class="card-content">
                            <a href="#" class="card-image" style="background-image: url(https://image.flaticon.com/icons/png/128/201/201651.png);"></a>
                            </div>--%>
                                    <h2 class="card-title"><%# DataBinder.Eval(Container.DataItem, "RewarderName")%> rewards <%# DataBinder.Eval(Container.DataItem, "ReceiverName")%> for <%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "PointsAmount"))%> Points
                                    for the contribution to: <%# DataBinder.Eval(Container.DataItem, "CategoryName")%> <%# DataBinder.Eval(Container.DataItem, "ValueName")%></h2>

                                    <p class="card-body">Comments: <%# DataBinder.Eval(Container.DataItem, "EventDescription")%></p>

                                    
                                    <asp:Label runat="server" ID="lblPostedOn" ForeColor="Black" Font-Italic="True" Font-Size="Small" cssclass="card-time">
                                            Posted On: <%# DataBinder.Eval(Container.DataItem, "LastUpdated","{0:d/M/yyyy}")%>
                                    </asp:Label>
                            
                        </section>
                </article>

            </ItemTemplate>
        </asp:DataList>
       </div>

    <div>
        <asp:Table runat="server" HorizontalAlign ="Right">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:LinkButton ID="lnkbtnPrevious" runat="server" OnClick="lnkbtnPrevious_Click"><<</asp:LinkButton>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DataList ID="dlPaging" runat="server" OnItemCommand="dlPaging_ItemCommand" OnItemDataBound="dlPaging_ItemDataBound" ItemStyle-VerticalAlign="Middle" RepeatDirection="Horizontal">

                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbtnPaging" runat="server" CommandArgument='<%# Eval("PageIndex") %>' CommandName="lnkbtnPaging" Text='<%# Eval("PageText") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                </asp:TableCell>
                <asp:TableCell>

                    <asp:LinkButton ID="lnkbtnNext" runat="server" OnClick="lnkbtnNext_Click">>></asp:LinkButton>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>



    </div>
    
</asp:Content>


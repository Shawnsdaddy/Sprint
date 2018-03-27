<%@ Page Title="" Language="C#" MasterPageFile="~/EmployeeMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeReward.aspx.cs" Inherits="EmployeeReward" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
     <style type="text/css">
/*WHEEL*/
    text{
       font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        font-size:11px;
        pointer-events:none;
    }
         
    #chart{
        position:absolute;
        width:60px;
        height:60px;
     
        align-content:center;
    }
    #question{
        /*position: absolute;*/
        width:600px;
        height:100px;
        bottom: 400px;
        right:60px;
        
    }
         #question h1 {
             font-size: 15px;
             font-weight: bold;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
             /*position: absolute;*/
             padding: 0;
             margin: 0;
             top: 10%;
             right: 10%;
         }

/*WHEEL*/
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
            background-color: #17468A;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: white;
            border: none 0px transparent;
            height: 25px;
            text-align: center;
            font-size: 12px;
            font-weight:bold;
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
            background-color: #17468A;
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
             font-weight: 700;
             color: #000099;
             font-size: large;
         }

        .auto-style19 {
            width: 658px;
        }

        .auto-style20 {
            text-align: right;
            width: 658px;
        }

    </style>

    <script type="text/javascript">
        function ItemSelected(sender, e) {
            $get("<%=hfEmployeeId.ClientID %>").value = e.get_value("PersonID");
          
           
        }
    </script>

    <div class="btn-aligncenter">
        <br />
        <asp:Label ID="lblPoints" runat="server" Text="Points" CssClass="auto-style18"></asp:Label>
        <br />
        <asp:Button ID="btnReward" runat="server" Text="Reward Peers" class="button" TabIndex="3" />
        <asp:Button ID="btnCalendar" runat="server" Text="View Events Calendar" class="button"  TabIndex="3" CausesValidation="False" />
        <asp:Button ID="btnSpin" runat="server" Text="Free Time On Your Hands?" class="button" TabIndex="3" CausesValidation="False" />

                <%--<ajaxToolkit:ModalPopupExtender ID="btnSpin_PopUp" runat="server" TargetControlID="btnSpin" PopupControlID="spin" CancelControlID="btnCancel" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>--%>
        <br />
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
    <ajaxToolkit:ModalPopupExtender ID="popReward" runat="server" TargetControlID="btnReward" PopupControlID="pnlReward" CancelControlID="btnCancel" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlReward" runat="server" CssClass="popup">

        <h1>Reward Peer</h1>
               
        <asp:Table ID="Table1" runat="server" CssClass="table" CellPadding="8" CellSpacing="8">
            <asp:TableRow runat="server" CssClass="table-content" HorizontalAlign="Left">
                <asp:TableCell runat="server" HorizontalAlign="Left">
                    <asp:Label ID="lblRName" runat="server" Text="Name:"></asp:Label>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" EnableCaching="false" TargetControlID="txtName" MinimumPrefixLength="1" ServiceMethod="SearchName" CompletionInterval="10" CompletionListCssClass="autoCompleteList" OnClientItemSelected="ItemSelected"></ajaxToolkit:AutoCompleteExtender>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtName" CssClass="textbox" runat="server" Width="250px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" class="auto-style8" ControlToValidate="txtName" ForeColor="Red" ErrorMessage="*" ValidationGroup="pop"></asp:RequiredFieldValidator>

                    <asp:HiddenField ID="hfEmployeeId" runat="server" OnLoad="getCustomerID" />
                    <%--<ajaxToolkit:ComboBox ID="cbName" OnSelectedIndexChanged="cbName_SelectedIndexChanged" AutoCompleteMode="Suggest" runat="server"  DropDownStyle="DropDownList" AutoPostBack="false" ValidateRequestMode="Enabled"></ajaxToolkit:ComboBox>--%>
                    <%--                    <asp:CompareValidator ID="reqName" runat="server" ControlToValidate="cbName" ErrorMessage="*" ForeColor="Red"></asp:CompareValidator>--%>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" HorizontalAlign="Left">
                    <asp:Label ID="lblRValue" runat="server" Text="Company Value:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>

                    <asp:DropDownList DataSourceID="srcRValue" OnSelectedIndexChanged="ddlRValue_SelectedIndexChanged" CssClass="textbox" DataTextField="ValueName" DataValueField="ValueID" ID="ddlRValue" AppendDataBoundItems="true" runat="server" Width="200px">
                        <asp:ListItem Value="-1" Text="--Select Value--"></asp:ListItem>

                    </asp:DropDownList>
                    <%--                    <asp:CompareValidator ID="reqValue" runat="server" ControlToValidate="ddlRValue" ErrorMessage="*" ForeColor="Red"></asp:CompareValidator>--%>

                    <asp:SqlDataSource ID="srcRValue" SelectCommand="SELECT [ValueID],[ValueName]FROM [dbo].[Value]" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" runat="server" />

                </asp:TableCell>

            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell runat="server" HorizontalAlign="Left">
                    <asp:Label ID="lblRCategory" runat="server" Text="Reward Category:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="ddlRCategory" CssClass="textbox"  OnSelectedIndexChanged="ddlRCategory_SelectedIndexChanged" DataSourceID="srcRCategory" DataTextField="CategoryName" DataValueField="CategoryID" AppendDataBoundItems="true" runat="server" Width="200px">
                        <asp:ListItem Value="-1" Text="--Select Category--"></asp:ListItem>

                    </asp:DropDownList>
                    <%--                    <asp:CompareValidator ID="reqCategory" runat="server" ControlToValidate="ddlRCategory" ErrorMessage="*" ForeColor="Red"></asp:CompareValidator>--%>
                    <asp:SqlDataSource ID="srcRCategory" SelectCommand="SELECT [CategoryID],[CategoryName]FROM [dbo].[Category]" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" runat="server" />

                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow CssClass="table-content">
                <asp:TableCell runat="server" HorizontalAlign="Left" VerticalAlign="Top">
                    <asp:Label ID="lblRDescription" runat="server" Text="Description:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtRDescription" CssClass="textbox" runat="server" TextMode="MultiLine" Style="overflow: hidden" Height="50px" Width="250px"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" class="auto-style8" ControlToValidate="txtRDescription" ForeColor="Red" ErrorMessage="*" ValidationGroup="pop"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblValue" runat="server" Text="Reward Value:"></asp:Label>
                </asp:TableCell>

                <asp:TableCell>
                    <asp:RadioButtonList ID="rblRewardPoints" OnSelectedIndexChanged="rblRewardPoints_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal" Width="330px" BorderColor="#3366CC" CssClass="textbox">
                        <asp:ListItem Value="10">10 Points</asp:ListItem>


                        <asp:ListItem Value="25">25 Points</asp:ListItem>


                        <asp:ListItem Value="50">50 Points</asp:ListItem>


                    </asp:RadioButtonList>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnCommit" runat="server" Text="Reward" CssClass="button" Width="125px" OnClick="btnCommit_Click" OnClientClick="if (!confirm('Please double check your entered data')) return false" ValidationGroup="pop" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <asp:Button ID="btnCancel" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />


    </asp:Panel>


    <div id="container">
        <asp:DataList ID="dlPosts" runat="server" HorizontalAlign="Center">
            <ItemTemplate>

                <section class="card-content">

                    <%--  <div class="card-content">
                            <a href="#" class="card-image" style="background-image: url(https://image.flaticon.com/icons/png/128/201/201651.png);"></a>
                            </div>--%>
                    <h2 class="card-title"><%# Eval("RewarderName")%> rewards <%# DataBinder.Eval(Container.DataItem, "ReceiverName")%> for <%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "PointsAmount"))%> Points
                                    for the contribution to: <%# DataBinder.Eval(Container.DataItem, "CategoryName")%> <%# DataBinder.Eval(Container.DataItem, "ValueName")%></h2>

                    <p class="card-body">Comments: <%# DataBinder.Eval(Container.DataItem, "EventDescription")%></p>


                    <asp:Label runat="server" ID="lblPostedOn" ForeColor="Black" Font-Italic="True" Font-Size="Small" CssClass="card-time">
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
    <%-- TESTING FOR THE WHEEL --%>
    
   <ajaxToolkit:ModalPopupExtender ID="btnSpin_PopUp" runat="server" TargetControlID="btnSpin" PopupControlID="spin" CancelControlID="btnCancel2" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    
    <div id="spin" class="popup" style="height: 550px; width:500px;">
          <h1> Spin The Wheel!</h1>
   <%--   <td class="auto-style50"> --%>
             
          <div id="chart"></div>
    <div id="question"><h1></h1></div>

                
                          <asp:Button ID="btnCancel2" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
                            
<%--                              <asp:HyperLink ID="hlDemo" runat="server" onclick="openNewWindows()" style="cursor:pointer; text-decoration:underline;">HyperLink Demo </asp:HyperLink>--%>

                        <a href="https://docs.google.com/forms/d/e/1FAIpQLScjwETJ0wezco7DCB4Uj-KjkAEOu9uOf97X_cxrpe5spDwDhA/viewform?usp=pp_url&entry.877086558=Yes,++I'll+be+there&entry.1498135098=Kristina+Truong&entry.2606285=Yes" target="_blank" class="button" style ="position:relative; bottom:100px;" >Sign Up Today!</a>


    <script src="https://d3js.org/d3.v3.min.js" charset="utf-8"></script>
    <script type="text/javascript" charset="utf-8">
        var padding = { top: 20, right: 20, bottom: 20, left: 0 },
            w = 500 - padding.left - padding.right,
            h = 500 - padding.top - padding.bottom,
            r = Math.min(w, h) / 2,
            rotation = 0,
            oldrotation = 0,
            picked = 100000,
            oldpick = [],
            color = d3.scale.category20();//category20c()

        var data = [
            { "label": "Rockingham/Harrisonburg SPCA", "question": "Need Volunteers March 28 @ 10:00 AM" }, // padding
            { "label": "Harrisonburg Fire Department", "question": "Need Volunteers April 5 @ 11:00 AM" }, //font-family
            { "label": "Habitat For Humanity", "question": "Need Volunteers April 5 @ 3:00 PM" }, //color
            { "label": "Harrisonburg Soup Kitchen", "question": "Need Volunteers May 28 @ 12:00 PM" }, //font-weight
            { "label": "Rockingham Children's Hospital", "question": "Need Volunteers March 26 @ 3:00 PM" }, //color
            { "label": "Cat's Craddle", "question": "Need Volunteers April 4 @ 12:00 PM" }, //font-weight
            { "label": "Rockingham Retirement Home", "question": "Need Volunteers March 28 @ 1:00 PM" } //font-weight
            
        ];
        var svg = d3.select('#chart')
            .append("svg")
            .data([data])
            .attr("width", w + padding.left + padding.right)
            .attr("height", h + padding.top + padding.bottom);
        var container = svg.append("g")
            .attr("class", "chartholder")
            .attr("transform", "translate(" + (w / 2 + padding.left) + "," + (h / 2 + padding.bottom) + ")");
        var vis = container
            .append("g");

        var pie = d3.layout.pie().sort(null).value(function (d) { return 1; });
        // declare an arc generator function
        var arc = d3.svg.arc().outerRadius(r);
        // select paths, use arc generator to draw
        var arcs = vis.selectAll("g.slice")
            .data(pie)
            .enter()
            .append("g")
            .attr("class", "slice");

        arcs.append("path")
            .attr("fill", function (d, i) { return color(i); })
            .attr("d", function (d) { return arc(d); });
        // add the text
        arcs.append("text").attr("transform", function (d) {
            d.innerRadius = 0;
            d.outerRadius = r;
            d.angle = (d.startAngle + d.endAngle) / 2;
            return "rotate(" + (d.angle * 180 / Math.PI - 90) + ")translate(" + (d.outerRadius - 10) + ")";
        })
            .attr("text-anchor", "end")
            .text(function (d, i) {
                return data[i].label;
            });
        container.on("click", spin);
        function spin(d) {

            container.on("click", null);
            //all slices have been seen, all done
            console.log("OldPick: " + oldpick.length, "Data length: " + data.length);
            if (oldpick.length == data.length) {
                console.log("done");
                container.on("click", null);
                return;
            }
            var ps = 360 / data.length,
                pieslice = Math.round(1440 / data.length),
                rng = Math.floor((Math.random() * 1440) + 360);

            rotation = (Math.round(rng / ps) * ps);

            picked = Math.round(data.length - (rotation % 360) / ps);
            picked = picked >= data.length ? (picked % data.length) : picked;
            if (oldpick.indexOf(picked) !== -1) {
                d3.select(this).call(spin);
                return;
            } else {
                oldpick.push(picked);
            }
            rotation += 90 - Math.round(ps / 2);
            vis.transition()
                .duration(3000)
                .attrTween("transform", rotTween)
                .each("end", function () {
                    //mark question as seen
                    d3.select(".slice:nth-child(" + (picked + 1) + ") path");

                    //populate question
                    d3.select("#question h1")
                        .text(data[picked].question);
                    oldrotation = rotation;

                    container.on("click", spin);
                });
        }
        //make arrow
        svg.append("g")
            .attr("transform", "translate(" + (w + padding.left + padding.right) + "," + ((h / 2) + padding.top) + ")")
            .append("path")
            .attr("d", "M-" + (r * .15) + ",0L0," + (r * .05) + "L0,-" + (r * .05) + "Z")
            .style({ "fill": "Tomato" });
        //draw spin circle
        container.append("circle")
            .attr("cx", 0)
            .attr("cy", 0)
            .attr("r", 60)
            .style({ "fill": "Snow", "cursor": "pointer" });
        //spin text
        container.append("text")
            .attr("x", 0)
            .attr("y", 15)
            .attr("text-anchor", "middle")
            .text("Elk Events")
            .style({ "font-weight": "bold", "font-size": "13px" });


        function rotTween(to) {
            var i = d3.interpolate(oldrotation % 360, rotation);
            return function (t) {
                return "rotate(" + i(t) + ")";
            };
        }


        function getRandomNumbers() {
            var array = new Uint16Array(1000);
            var scale = d3.scale.linear().range([360, 1440]).domain([0, 100000]);
            if (window.hasOwnProperty("crypto") && typeof window.crypto.getRandomValues === "function") {
                window.crypto.getRandomValues(array);
                console.log("works");
            } else {

                for (var i = 0; i < 1000; i++) {
                    array[i] = Math.floor(Math.random() * 100000) + 1;
                }
            }
            return array;
        } 
    </script>
   
     
                  </div>
     <%--   <tr>--%>
           
    <%-- TESTING FOR THE WHEEL --%>
        <div class="btn-aligncenter">
        <br />

            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnCalendar" PopupControlID="CalendarPanel" CancelControlID="btnCancel" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
             <asp:Panel ID="CalendarPanel" CssClass="popup" runat="server">
                 <h1> Find Events Happening Around You!</h1>
                 
                 <asp:Table ID="EventDetails" runat="server" CssClass="table" Width="434px" HorizontalAlign="Center">
             <asp:TableRow>
                <asp:TableCell>

         
            <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender"  OnSelectionChanged="Calendar1_SelectionChange"  CssClass="btn-aligncenter" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px">
                

            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                

            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                
            <OtherMonthDayStyle ForeColor="#999999" />
                

            <SelectedDayStyle BackColor="#333399" ForeColor="White" />

            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />

            <TodayDayStyle BackColor="#CCCCCC" />

        </asp:Calendar>        


                </asp:TableCell>
                <asp:TableCell>

                    
                </asp:TableCell><asp:TableCell>


       
                    
                </asp:TableCell></asp:TableRow>
             
         </asp:Table>

                     <div class ="rounded-corners"></div>
    
        <asp:GridView ID="gvEvents" runat="server" AllowPaging ="True"  RowStyle-CssClass= "rows" OnPageIndexChanging ="gvEvents_PageIndexChanging" Width="90%" CellPadding="4" ForeColor="#333333" GridLines="None" PageSize="5">
          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

                            <HeaderStyle CssClass="header" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

                            <PagerStyle CssClass="pager" BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                   <%-- <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

                            <HeaderStyle CssClass="header" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

                            <PagerStyle CssClass="pager" BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>

                            <RowStyle CssClass="rows" BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />--%>
</asp:GridView>
                      <div class ="rounded-corners"></div>
        
        <br />
                 <asp:Button ID="CancelCalendar" runat="server" Text="" CssClass="btn-close" Style="background-image: url('http://icons.iconarchive.com/icons/iconsmind/outline/24/Close-icon.png'); background-repeat: no-repeat" />
             </asp:Panel>
         

    </div>


        </asp:Content>



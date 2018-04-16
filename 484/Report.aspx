
<%@ Page Title="" Language="C#" MasterPageFile="~/CEOMasterPage.master" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Test" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style type="text/css">
        .auto-style2 {
            width: 87%;
            height: 676px;
            margin-left: 0px;
        }

        .auto-style3 {
            width: 91%;
            height: 2331px;
        }

        </style>
    <script type='text/javascript' src='https://us-east-1.online.tableau.com/javascripts/api/viz_v1.js'></script>
    
    <br />
    <h2>
        <asp:Label runat="server" Text="Dashboard" ID="lblName" CssClass="reward-points" Width="30%" Font-Size="X-Large" ForeColor="#17468A"></asp:Label></h2>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="btn-aligncenter" style="width: 90%"></div>
    <br />
        <iframe id="Tableau" runat="server" style="height:850px;width:90%;"></iframe>
        <br />
    
        <div >
            <asp:Table ID="Table1" runat="server" CaptionAlign="Left" CellPadding="10" CellSpacing="5">
                <asp:TableRow>
                    <asp:TableCell>
                     <asp:Image ID="Image2" runat="server" ImageUrl="~/image/Excel.jpg" Height="66px" Width="77px" />
                         </asp:TableCell>
                    <asp:TableCell>
<asp:LinkButton ID="Employee" runat="server" OnClick="EmployeeSheet">Employee Sheet</asp:LinkButton>
                    </asp:TableCell>
                    <asp:TableCell>
   <asp:LinkButton ID="RewardProvider" runat="server" OnClick="RewardProviderSheet">Reward Provider Sheet</asp:LinkButton>
                    </asp:TableCell>
                    <asp:TableCell>
 <asp:LinkButton ID="PeerRewardHistory" runat="server" OnClick="PeerRewardHistorySheet">Peer Reward History Sheet</asp:LinkButton>
                    </asp:TableCell>
                    <asp:TableCell>
   <asp:LinkButton ID="MoneyTransaction" runat="server" OnClick="MoneyTransactionSheet">Money Transaction Sheet</asp:LinkButton>
            
                    </asp:TableCell>

                </asp:TableRow>
            </asp:Table>
             
        
        </div>



<%--        <div hidden="hidden">
             <asp:Chart ID="ValueGraph" runat="server" DataSourceID="value" Height="380px" Width="815px">
                <Series>
                    <asp:Series Name="Series1" XValueMember="ValueName" YValueMembers="RewardTimes" ChartType="Radar" IsValueShownAsLabel="True">
                        <SmartLabelStyle IsMarkerOverlappingAllowed="True" />
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ValueGraph" BackColor="White" BackImageAlignment="Center">
                        <AxisY MapAreaAttributes="Count(ValueName)" Title="No. of Rewarded Times by Each Value">
                        </AxisY>
                        <AxisX MapAreaAttributes="ValueName" Title="Value Name">
                        </AxisX>
                    </asp:ChartArea>
                </ChartAreas>
                
            
                 <Titles>
                     <asp:Title Name="ValuePerformance" Text="Business Value Performance">
                     </asp:Title>
                 </Titles>
            </asp:Chart>
         
            
            <asp:SqlDataSource ID="value" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" SelectCommand="SELECT Value.ValueName, COUNT(PeerTransaction.ValueID) AS RewardTimes FROM Value INNER JOIN PeerTransaction ON Value.ValueID = PeerTransaction.ValueID GROUP BY Value.ValueName"></asp:SqlDataSource>
             <asp:Chart ID="EmployeePerformance"   runat="server" DataSourceID="EmployeeReceiveReward" Height="315px" Width="752px">
                 <Series>
                     <asp:Series Name="Series1" IsValueShownAsLabel="true" XValueMember="JobTitle" YValueMembers="ReceiveTimes">
                     </asp:Series>
                 </Series>
                 <ChartAreas>
                     <asp:ChartArea Name="EmployeePerformance">
                         <AxisY MapAreaAttributes="COUNT(PeerTransaction.ValueID)" Title="No. of Reward Received">
                        </AxisY>
                     </asp:ChartArea>
                 </ChartAreas>
                 <Titles>
                     <asp:Title Name="Title1" Text="Trucking/Logistics Performance">
                     </asp:Title>
                 </Titles>
             </asp:Chart>
             <asp:SqlDataSource ID="EmployeeReceiveReward" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" SelectCommand="SELECT        COUNT(PeerTransaction.PointsTransactionID) AS ReceiveTimes, Person.JobTitle
FROM            PeerTransaction INNER JOIN
                         Person ON PeerTransaction.ReceiverID = Person.PersonID
GROUP BY Person.JobTitle"></asp:SqlDataSource>
             <asp:Chart ID="MoneyTrendChart"   runat="server" DataSourceID="MoneyTrend" Width="720px" Height="297px">
                 <Series>
                     <asp:Series ChartType="Line" Name="Series1" IsValueShownAsLabel="true" XValueMember="Date" YValueMembers="Dollars">
                     </asp:Series>
                 </Series>
                 <ChartAreas>
                     <asp:ChartArea Name="MoneyTrebd">
                         <AxisY MapAreaAttributes="min(TotalAmount) " Title="$ Left in the pool">
                        </AxisY>
                     </asp:ChartArea>
                 </ChartAreas>
                 <Titles>
                     <asp:Title Name="Title1" Text="Money Trend in the Pool">
                     </asp:Title>
                 </Titles>
             </asp:Chart>
             <asp:SqlDataSource ID="MoneyTrend" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" SelectCommand="SELECT  min(TotalAmount) As Dollars, Convert(Date,LastUpdated) as [Date]
FROM            MoneyTransaction
GROUP BY  Convert(Date,LastUpdated)

"></asp:SqlDataSource>
             <asp:Chart ID="ProviderPerformance"  runat="server" DataSourceID="ProviderCash" Height="353px" Width="689px">
                 <Series>
                     <asp:Series ChartType="Pie" Name="Series1" IsValueShownAsLabel="true" XValueMember="JobTitle" YValueMembers="Percentage" LabelFormat="{P}" Legend="Legend1">
                     </asp:Series>
                 </Series>
                 <ChartAreas>
                     <asp:ChartArea Name="ChartArea1">
                     </asp:ChartArea>
                 </ChartAreas>
                 <Legends>
                     <asp:Legend Name="Legend1">
                     </asp:Legend>
                 </Legends>
                 <Titles>
                     <asp:Title Name="Title1" Text="Percentage of Redeemption Made by Each Reward Provider">
                     </asp:Title>
                 </Titles>
             </asp:Chart>
            <%-- <asp:SqlDataSource ID="ProviderCash" runat="server" ConnectionString="<%$ ConnectionStrings:GroupProjectConnectionString %>" SelectCommand="SELECT        SUM(RedeemTransaction.RedeemAmount * RedeemTransaction.RedeemQuantity)/
(SELECT        SUM(MoneyTransaction.TransactionAmount) AS Expr1
FROM            MoneyTransaction
where 
[TransactionType]='Redeemption') as Percentage, Person.JobTitle
FROM            RedeemTransaction INNER JOIN
                         Person ON RedeemTransaction.ProviderID = Person.PersonID
GROUP BY Person.JobTitle
"></asp:SqlDataSource>
        </div>--%>
  
    



</asp:Content>




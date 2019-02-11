<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="
    SummaryReport.aspx.cs" Inherits="LKReportingSystem.Forms.MailPromo.MailPromoSummaryReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .canvasjs-chart-credit {
           display: none !important;
        }
    </style>

    <link href="<%= Page.ResolveClientUrl("~/Component/footable/css/footable.min.css") %>" rel="stylesheet" type="text/css">
    <script src="<%= Page.ResolveClientUrl("~/Component/footable/js/footable.min.js") %>"></script>
    
    <script src="<%= Page.ResolveClientUrl("~/Scripts/canvasjs.min.js") %>"></script>
    <script>
        function pageLoad() {
            $(".footable").footable();

            $(".datepicker").datetimepicker({
                format: 'M-YYYY',
                viewMode: "months"
            });
        }

        function CreateStackedChartClickAndSent(arr, arr2) {
            var arrContent = new Array(2);
            var arrChartClick = new Array(arr.length);
            var arrChartSent = new Array(arr2.length);

            for (i = 0 ; i < arr.length ; i++) {
                for (var j = 0; j < arr[i].length; j++) {
                    arrChartClick[j] = new Array(2);

                    arrChartClick[j]["y"] = parseFloat(arr[i][j]["totalclick"]);
                    arrChartClick[j]["label"] = arr[i][j]["monthperiod"] + " / " + arr[i][j]["yearperiod"];
                }
            }

            for (i = 0 ; i < arr2.length ; i++) {

                for (var j = 0; j < arr2[i].length; j++) {
                    arrChartSent[j] = new Array(2);

                    arrChartSent[j]["y"] = parseFloat(arr2[i][j]["totalsend"]);
                    arrChartSent[j]["label"] = arr2[i][j]["monthperiod"] + " / " + arr2[i][j]["yearperiod"];
                }
            }

            arrContent[0] = new Array(4);
            arrContent[0]["type"] = "column";
            arrContent[0]["showInLegend"] = true;
            arrContent[0]["legendText"] = "Clicked";
            arrContent[0]["dataPoints"] = arrChartClick;

            arrContent[1] = new Array(4);
            arrContent[1]["type"] = "column";
            arrContent[1]["showInLegend"] = true;
            arrContent[1]["legendText"] = "Sent";
            arrContent[1]["dataPoints"] = arrChartSent;

            var chart = new CanvasJS.Chart("chartClickAndSentPerMonth", {
                title: {
                    text: "Advertisement Sent and Clicked"
                },
                animationEnabled: true,
                axisY: {
                    title: "Value"
                },
                data: arrContent
            });

            chart.render();
        }

        function CreateChartSentPerAds(title, divID, arr) {

            var arrContent = new Array(1);
            var arrChart = new Array(arr.length);

            for (i = 0 ; i < arr.length ; i++) {
                var AdsName = arr[i]["promotitle"];

                arrChart[i] = new Array(5);

                arrChart[i]["y"] = parseFloat(arr[i]["totalsend"]);
                arrChart[i]["indexLabel"] = arr[i]["promotitle"];

            }

            arrContent[0] = new Array(6);
            arrContent[0]["type"] = "pie";
            arrContent[0]["showInLegend"] = true;
            arrContent[0]["toolTipContent"] = "{y} - #percent %";
            arrContent[0]["legendText"] = "{indexLabel}";
            arrContent[0]["dataPoints"] = arrChart;

            var chart = new CanvasJS.Chart(divID, {
                theme: "theme2",
                title: {
                    text: title
                },
                data: arrContent
            });

            chart.render();
        }

        function CreateChartClickPerAds(title, divID, arr) {

            var arrContent = new Array(1);
            var arrChart = new Array(arr.length);

            for (i = 0 ; i < arr.length ; i++) {
                var AdsName = arr[i]["promotitle"];

                arrChart[i] = new Array(5);

                arrChart[i]["y"] = parseFloat(arr[i]["totalclick"]);
                arrChart[i]["indexLabel"] = arr[i]["promotitle"];

            }

            arrContent[0] = new Array(6);
            arrContent[0]["type"] = "pie";
            arrContent[0]["showInLegend"] = true;
            arrContent[0]["toolTipContent"] = "{y} - #percent %";
            arrContent[0]["legendText"] = "{indexLabel}";
            arrContent[0]["dataPoints"] = arrChart;

            var chart = new CanvasJS.Chart(divID, {
                theme: "theme2",
                title: {
                    text: title
                },
                data: arrContent
            });

            chart.render();
        }

        function CreateChartClickPerMonth(arr) {

            var arrContent = new Array(arr.length);

            for (i = 0 ; i < arr.length ; i++) {
                var AdsName = arr[i][0]["promotitle"];

                var arrChart = new Array(arr[i].length);
                for (var j = 0; j < arr[i].length; j++) {
                    arrChart[j] = new Array(5);

                    arrChart[j]["x"] = new Date(parseInt(arr[i][j]["yearperiod"]), parseInt(arr[i][j]["monthperiod"]) - 1, 1);
                    arrChart[j]["y"] = parseFloat(arr[i][j]["totalclick"]);
                    arrChart[j]["indexLabel"] = "";
                    arrChart[j]["markerColor"] = "";
                    arrChart[j]["markerType"] = "";
                }

                arrContent[i] = new Array(4);
                arrContent[i]["type"] = "bar";
                arrContent[i]["showInLegend"] = true;
                arrContent[i]["name"] = AdsName;
                arrContent[i]["dataPoints"] = arrChart;
            }

            var chart = new CanvasJS.Chart("chartClickPerAds", {
                theme: "theme3",
                title: {
                    text: "Advertisement Sent"
                },
                animationEnabled: true,
                axisX: {
                    valueFormatString: "MMM",
                    interval: 1,
                    intervalType: "month"

                },
                axisY: {
                    includeZero: false

                },
                data: arrContent
            });

            chart.render();
        }

        function CreateChartSendPerMonth(arr) {

            var arrContent = new Array(arr.length);

            for (i = 0 ; i < arr.length ; i++) {
                var AdsName = arr[i][0]["promotitle"];

                var arrChart = new Array(arr[i].length);
                for (var j = 0; j < arr[i].length; j++) {
                    arrChart[j] = new Array(5);

                    arrChart[j]["x"] = new Date(parseInt(arr[i][j]["yearperiod"]), parseInt(arr[i][j]["monthperiod"]) - 1, 1);
                    arrChart[j]["y"] = parseFloat(arr[i][j]["totalsend"]);
                    arrChart[j]["indexLabel"] = "";
                    arrChart[j]["markerColor"] = "";
                    arrChart[j]["markerType"] = "";
                }

                arrContent[i] = new Array(4);
                arrContent[i]["type"] = "line";
                arrContent[i]["showInLegend"] = true;
                arrContent[i]["name"] = AdsName;
                arrContent[i]["dataPoints"] = arrChart;
            }

            var chart = new CanvasJS.Chart("chartSendPerAds", {
                theme: "theme3",
                title: {
                    text: "Advertisement Sent and Clicked"
                },
                animationEnabled: true,
                axisX: {
                    valueFormatString: "MMM",
                    interval: 1,
                    intervalType: "month"

                },
                axisY: {
                    includeZero: false

                },
                data: arrContent
            });

            chart.render();
        }
    </script>

    <div id="page-wrapper">
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Report <small>Mail Promo</small>
                    </h1>

                    <ol class="breadcrumb">
                        <li class="active">
                            <i class="fa fa-pencil"></i>&nbsp; Summary Report
                        </li>
                    </ol>
                </div>
            </div>
            
            
            <asp:UpdatePanel ID="updatePanelHtmlNotificationHistory" UpdateMode="Conditional" runat="server" class="col-md-6">
                <ContentTemplate>
                    <div><br /></div>

                    <div class="col-md-6" id="htmlNotificationHistory" runat="server"></div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div id="divContent" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="fa fa-newspaper-o fa-fw"></i>&nbsp;Filter</h3>
                            </div>
                            <div class="panel-body">
                                <div class="row martop5">
                                    <div class="col-lg-3 col-sm-3 col-md-2">Start Date</div>
                                    <div class="col-lg-9 col-sm-9 col-md-10">
                                        <asp:TextBox ID="txtStartDate1" runat="server" CssClass="form-control datepicker" MaxLength="11"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row martop5">
                                    <div class="col-lg-3 col-sm-3 col-md-2">End Date</div>
                                    <div class="col-lg-9 col-sm-9 col-md-10">
                                        <asp:TextBox ID="txtEndDate1" runat="server" CssClass="form-control datepicker" MaxLength="11"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row martop5">
                                    <div class="col-lg-3 col-sm-3 col-md-2"></div>
                                    <div class="col-lg-9 col-sm-9 col-md-10">
                                        <asp:Button ID="btnGenerate" runat="server" OnClick="btnGenerate_Click" Text="Generate" Width="100" CssClass="btn btn-primary rightAlign"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            

                <div class="row" style="display: none">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="fa fa-bar-chart-o fa-fw"></i> Ads Send Per Month</h3>
                            </div>
                            <div class="panel-body">
                                <p class="text-center">
                                    <strong>Email : <asp:Label ID="lblStartPeriod" runat="server"></asp:Label> - <asp:Label ID="lblEndPeriod" runat="server"></asp:Label> </strong>
                                </p>
                            
	                            <div id="chartSendPerAds" style="width: 100%; height: 300px;display: inline-block;"></div>
                            </div>
                        </div>
                    </div>
                </div>
            

                <div class="row" style="display: none">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="fa fa-bar-chart-o fa-fw"></i> Ads Clicked Per Month</h3>
                            </div>
                            <div class="panel-body">
                                <p class="text-center">
                                    <strong>Email : <asp:Label ID="lblStartPeriod2" runat="server"></asp:Label> - <asp:Label ID="lblEndPeriod2" runat="server"></asp:Label> </strong>
                                </p>
                            
	                            <div id="chartClickPerAds" style="width: 100%; height: 300px;display: inline-block;"></div>
                            </div>
                        </div>
                    </div>
                </div>

            
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="fa fa-bar-chart-o fa-fw"></i> Sent and Clicked Per Month</h3>
                            </div>
                            <div class="panel-body">
                                <p class="text-center">
                                    <strong>Periode : <asp:Label ID="lblStartPeriod3" runat="server"></asp:Label> - <asp:Label ID="lblEndPeriod3" runat="server"></asp:Label> </strong>
                                </p>
                            
	                            <div id="chartClickAndSentPerMonth" style="width: 100%; height: 300px;display: inline-block;"></div>
                            </div>
                        </div>
                    </div>
                </div>
            
            
                <div class="row" style="display: none">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="fa fa-bar-chart-o fa-fw"></i> Sent Per Ads</h3>
                            </div>
                            <div class="panel-body">
                            
                                <asp:Literal ID="ltDetailAds" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="fa fa-bar-chart-o fa-fw"></i> Clicked Per Ads</h3>
                            </div>
                            <div class="panel-body">
                            
                                <asp:Literal ID="ltDetailClickedAds" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
                
            
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="fa fa-bar-chart-o fa-fw"></i> Detail Ads</h3>
                            </div>
                            <div class="panel-body">
                                <asp:GridView ID="gvMailPromo" EmptyDataText="No Data Available" CssClass="footable" runat="server" EnableViewState="true"
                                    AutoGenerateColumns="false" Style="max-width: 100%">
                                    <Columns>
                                        <asp:BoundField DataField="monthPeriod" HeaderText="Month" />
                                        <asp:BoundField DataField="yearPeriod" HeaderText="Year" />
                                        <asp:BoundField DataField="promoTitle" HeaderText="Title" />
                                        <asp:BoundField DataField="totalsend" HeaderText="Total Sent" />
                                        <asp:BoundField DataField="totalClick" HeaderText="Total Clicked" />
                                       
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:Image id="imgAds" runat="server" ImageUrl='<%#  Bind("promoArchiveImage") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
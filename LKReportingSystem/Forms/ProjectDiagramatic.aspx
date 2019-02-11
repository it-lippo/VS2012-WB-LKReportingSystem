<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectDiagramatic.aspx.cs" Inherits="LKReportingSystem.Forms.ProjectDiagramatic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <link href="<%= Page.ResolveClientUrl("~/Content/scrollable-table/scrollable-table.css") %>" rel="stylesheet" type="text/css">
    <link href="<%= Page.ResolveClientUrl("~/Content/scrollable-table/component-table.css") %>" rel="stylesheet" type="text/css">

    <script src="<%= Page.ResolveClientUrl("~/Content/scrollable-table/jquery.ba-throttle-debounce.min.js") %>"></script>
    <script src="<%= Page.ResolveClientUrl("~/Content/scrollable-table/jquery.stickyheader.js") %>"></script>

    <link href="<%= Page.ResolveClientUrl("~/Content/footable/css/footable.min.css") %>" rel="stylesheet" type="text/css">
    <script src="<%= Page.ResolveClientUrl("~/Content/footable/js/footable.min.js") %>"></script>

    <script>
        window.onresize = SetScreenSize;

        function pageLoad() {
            $(".select2").select2();
            $(".footable").footable();
            SetScreenSize();
        }

        $(function () {
            //InitialStickyHeader();
        });

        function SetScreenSize() {
            var w = window.innerWidth;
            var h = window.innerHeight;

            $("[id$='hfScreenWidth']").val(w);
            $("[id$='hfScreenHeight']").val(h);
        }

        function selectOnlyThis(id) {
            var myCheckbox = document.getElementsByName("cbDiagramatic");
            Array.prototype.forEach.call(myCheckbox, function (el) {
                el.checked = false;
            });
            id.checked = true;

            $("[id$='btnReserveDiagram']").click();
        }


        function GetSelectedUnitDiagram() {
            debugger;
            var result = false;
            var currObj = new Array();

            $(".cbUnit").each(function (index) {
                if ($(this).is(':checked')) {
                    //if ($(this).next().html() != null && $(this).next().html()) {
                    flag = 1;
                    //textUnitCode = textUnitCode + $(this).next().html() + ",";

                    var tempString = $(this).val().split('#');
                    var unitcode = tempString[0];
                    var unitno = tempString[1];
                    var unitname = tempString[2];
                    var clustercode = tempString[3];

                    var obj = new Object();
                    obj.unitcode = unitcode;
                    obj.unitno = unitno;
                    obj.unitname = unitname;
                    obj.clustercode = clustercode;

                    currObj.push(obj);

                }
            });


            if (currObj.length == 0) {
                bootbox.alert('No Unit Selected. Please choose your preferred Units.');
            }
            else {
                result = true;

                var jsonString = JSON.stringify(currObj);

                $("[id$='hfSelectUnitDiagramatic']").val(jsonString);
            }

            return result;
        }

        function ChangeColor(GridViewId, SelectedRowId) {
            var GridViewControl = document.getElementById(GridViewId);
            if (GridViewControl != null) {
                var GridViewRows = GridViewControl.rows;
                if (GridViewRows != null) {
                    var SelectedRow = GridViewRows[SelectedRowId];
                    //Remove Selected Row color if any
                    for (var i = 1; i < GridViewRows.length; i++) {
                        var row = GridViewRows[i];
                        if (row == SelectedRow) {
                            //Apply Yellow color to selected Row
                            row.style.backgroundColor = "#A1DCF2";
                        }
                        else {
                            //Apply White color to rest of rows
                            row.style.backgroundColor = "#ffffff";
                        }
                    }

                }
            }
        }

        function HideElement(id) {
            $("[id$='" + id + "']").addClass("hiddenComp").removeClass("showComp");
        }

        function ShowElement(id) {
            $("[id$='" + id + "']").addClass("showComp").removeClass("hiddenComp");
        }

        //start ifan : Change size from diagramatic, change parameter size be landsize and buildingsize
        function ShowDetailUnitHover(event, bookCode, bookDate, bookingAge, renov, stpDate, stfDate, pppuNo, pppuDate)
        {
            var x = event.clientX;
            var y = event.clientY;

            var w = window.innerWidth;

            if (x + 550 >= w) x = x - 550;

            $("[id$='lblBookCode']").text(bookCode);
            $("[id$='lblBookDate']").text(bookDate);
            $("[id$='lblBookingAge']").text(bookingAge);
            $("[id$='lblRenov']").text(renov);
            $("[id$='lblSTPDate']").text(stpDate);
            $("[id$='lblSTFDate']").text(stfDate);
            $("[id$='lblPPPUNo']").text(pppuNo);
            $("[id$='lblPPPUDate']").text(pppuDate);

            $("#divDetailUnit").css("left", x);
            $("#divDetailUnit").css("top", y);
            $("#divDetailUnit").removeClass('hiddenElm').addClass('showElm');
        }

        function ShowPopUpDetailUnitHover(event, bookCode, bookDate, bookingAge, renov, stpDate, stfDate, pppuNo, pppuDate)
        {
            $("[id$='lblPopUpBookCode']").text(bookCode);
            $("[id$='lblPopUpBookingAge']").text(bookingAge);
            $("[id$='lblPopUpSTPDate']").text(stpDate);
            $("[id$='lblPopUpPPPUNo']").text(pppuNo);
            $("[id$='lblPopUpBookDate']").text(bookDate);
            $("[id$='lblPopUpRenov']").text(renov);
            $("[id$='lblPopUpSTFDate']").text(stfDate);
            $("[id$='lblPopUpPPPUDate']").text(pppuDate);

            $("#divPopUpDetailUnit").modal('hide');
            $("#divPopUpDetailUnit").modal('show');
        }

        function HideDetailUnitHover() {
            $("[id$='lblBookCode']").val("");
            $("[id$='lblBookDate']").val("");
            $("[id$='lblBookingAge']").text("");
            $("[id$='lblRenov']").text("");
            $("[id$='lblSTPDate']").text("");
            $("[id$='lblSTFDate']").text("");
            $("[id$='lblPPPUNo']").text("");
            $("[id$='lblPPPUDate']").text("");
            
            $("#divDetailUnit").removeClass('showElm').addClass('hiddenElm');
        }

        //end ifan : Change Size in Diagramatic, Change diplay Size be Building and land
    </script>

    <style>
        #MainContent_rblDisplayType td {
            background-color: white;
        }

        .modal {
            overflow: auto !important;
        }


        .divDetail {
            width: 550px;
            height: auto;
            position: fixed;
            background-color: #EEE;
            border: 1px solid #636060;
            border-radius: 4px;
            z-index: 99999;
        }

        .divDetailSold {
            width: 550px;
            height: auto;
            position: fixed;
            background-color: #f45d96;
            border: 1px solid #636060;
            border-radius: 4px;
            z-index: 99999;
        }


        .borderBottom {
            border-top: none;
            border-right: none;
            border-left: none;
            text-align: center;
            font-size: larger;
        }

        .borderBottomUp {
            border-top: none;
            border-right: none;
            border-left: none;
            text-align: center;
        }

        .borderLessBtn {
            border-top: none;
            border-right: none;
            border-left: none;
            border-bottom: none;
            color: blue;
            background-color: transparent;
            text-align: center;
        }

            .borderLessBtn:hover {
                color: blue;
                background-color: transparent;
            }

        .rbl input[type="radio"] {
            margin-left: 10px;
            margin-right: 1px;
        }

        #MainContent_rblPayment td {
            background-color: white;
            border-color: white;
            padding: 0px;
        }

        .button-like-link {
            background: none !important;
            color: inherit;
            border: none;
            padding: 0 !important;
            font: inherit;
            /*border is optional*/
            border-bottom: 1px solid #444;
            cursor: pointer;
        }

        .label-diagram {
            height: 30px;
            width: 100%;
            cursor: pointer;
            text-align: center;
            line-height: 30px;
            margin: 0px !important;
        }

        #diagram-container input[type=checkbox] {
            display: none;
        }

        #diagram-container input[type=checkbox]:checked + .label-diagram {
            background-color: #F7F71C !important;
        }
    </style>

    <style>
        .diag_h:hover .img_lay {
            visibility: visible;
            width: auto;
            height: auto;
        }

        .img_lay {
            visibility: hidden;
            width: 1px;
            height: 1px;
            z-index: 9999999;
            position: absolute;
            top: 105px;
            left: 100px;
        }
    </style>
    
    <asp:HiddenField ID="hfScreenWidth" runat="server" />
    <asp:HiddenField ID="hfScreenHeight" runat="server" />

    <asp:UpdatePanel ID="upHtmlNotifMain" UpdateMode="Conditional" runat="server" class="col-md-6">
	    <ContentTemplate>
		    <div><br /></div>

		    <div class="col-md-6" id="htmlNotifMain" runat="server"></div>
	    </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divContent" runat="server">
        <section class="content">

            <asp:UpdatePanel ID="upHF" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hfSelectUnitDiagramatic" runat="server" />
                    <asp:HiddenField ID="hfClusterCode1" runat="server" />
                    <asp:HiddenField ID="hfClusterCode2" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:Panel ID="pnlContent" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-warning" >
                            <div class="box-header with-border">
                                <h3 class="box-title">Filter</h3>

                                <div class="box-body" style="display: block;">

                                    <asp:UpdateProgress ID="uProgFilter" runat="server" AssociatedUpdatePanelID="upFilter">
                                        <ProgressTemplate>
                                            <div class="modalLoader">
                                                <div class="centerLoader">
                                                    <img alt="" src="<%= Page.ResolveClientUrl("~/Images/loadingOB.gif") %>" />
                                                </div>
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <asp:UpdatePanel ID="upFilter" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Project </label>
                                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control select2 select2-hidden-accessible" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" Width="100%" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Cluster / Tower </label>
                                                        <asp:DropDownList ID="ddlCluster" runat="server" CssClass="form-control select2 select2-hidden-accessible" Width="100%" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label></label>
                                                        <asp:Button ID="btnShowDiagramatic" runat="server" CssClass="btn btn-info form-control" OnClick="btnShowDiagramatic_Click" Text="Show Diagramatic" Width="100%" />
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="modalLoader">
                            <div class="centerLoader">
                                <img alt="" src="<%= Page.ResolveClientUrl("~/Images/loading.gif") %>" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

                <asp:UpdatePanel ID="upDiagramatic" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlDiagramatic" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="box box-info" style="margin-top: 40px">
                                        <div class="box-header with-border">
                                            <div class="box-body" style="display: block;">

                                                <div class="box-body" style="display: block; overflow: auto !important;">
                                                    <div id="diagram-container">
                                                        <div class="row">
                                                            <div class="col-lg-12" style="text-align: center">
                                                                <asp:Literal ID="ltTitleDiagramatic" runat="server"></asp:Literal>
                                                            </div>
                                                        </div>

                                                        <div class="row">

                                                            <div class="col-lg-12">
                                                                <asp:Literal ID="ltDiagramatic1" runat="server"></asp:Literal>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-lg-12" style="text-align: center">
                                                                <h3 id="titleDiagramatic2" runat="server"></h3>
                                                            </div>
                                                        </div>

                                                        <div class="row">

                                                            <div class="col-lg-12">
                                                                <asp:Literal ID="ltDiagramatic2" runat="server"></asp:Literal>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="upSummary" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlSummary" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="box box-success" >
                                        <div class="box-header with-border">
                                            <h3 class="box-title">Summary</h3>

                                            <div class="box-body" style="display: block; margin-bottom: 3em;">
                                                <div class="row">
                                                    <div class="col-md-1" style="margin-left: 10px; background-color: #f45d96;">&nbsp;</div>
                                                    <div class="col-md-10">
                                                        <asp:Label id="lblSumRed" runat="server" Text="0"></asp:Label> unit (sudah lewat dari tanggal PPPU dan belum serah terima fisik)
                                                    </div>
                                                </div>

                                                <div class="row" style="padding-top: 10px;">
                                                    <div class="col-md-1" style="margin-left: 10px; background-color: #F7F71C;">&nbsp;</div>
                                                    <div class="col-md-10">
                                                        <asp:Label id="lblSumYellow" runat="server" Text="0"></asp:Label> unit (belum lewat dari tanggal PPPU dan akan diserah terimakan)
                                                    </div>
                                                </div>

                                                <div class="row" style="padding-top: 10px;">
                                                    <div class="col-md-1" style="margin-left: 10px; background-color: #98eb92;">&nbsp;</div>
                                                    <div class="col-md-10">
                                                        <asp:Label id="lblSumGreen" runat="server" Text="0"></asp:Label> unit (sudah serah terima fisik)
                                                    </div>
                                                </div>

                                                <div class="row" style="padding-top: 10px;">
                                                    <div class="col-md-1" style="margin-left: 10px; background-color: #2b78d1;">&nbsp;</div>
                                                    <div class="col-md-10">
                                                        <asp:Label id="lblSumBlue" runat="server" Text="0"></asp:Label> unit (tanggal serah terima PPPU tidak diketahui (hubungi PSAS))
                                                    </div>
                                                </div>
                                            </div>

                                            <h3 class="box-title">Akan jatuh tempo serah terima fisik (sejak tanggal PPPU)</h3>
                            
                                            <div class="box-body" style="display: block;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <table>
                                                          <thead>
                                                            <tr>
                                                              <th scope="col" style="background-color: #F7F71C; color: #000000;">1 bulan</th>
                                                              <th scope="col" style="background-color: #F7F71C; color: #000000;">3 bulan</th>
                                                              <th scope="col" style="background-color: #F7F71C; color: #000000;">6 bulan</th>
                                                              <th scope="col" style="background-color: #F7F71C; color: #000000;">9 bulan</th>
                                                              <th scope="col" style="background-color: #F7F71C; color: #000000;">12 bulan</th>
                                                              <th scope="col" style="background-color: #F7F71C; color: #000000;">> 12 bulan</th>
                                                            </tr>
                                                          </thead>
                                                          <tbody>
                                                            <tr>
                                                              <td><asp:Label ID="lblJatuhTempo1Bln" runat="server"></asp:Label></td>
                                                              <td><asp:Label ID="lblJatuhTempo3Bln" runat="server"></asp:Label></td>
                                                              <td><asp:Label ID="lblJatuhTempo6Bln" runat="server"></asp:Label></td>
                                                              <td><asp:Label ID="lblJatuhTempo9Bln" runat="server"></asp:Label></td>
                                                              <td><asp:Label ID="lblJatuhTempo12Bln" runat="server"></asp:Label></td>
                                                              <td><asp:Label ID="lblJatuhTempo12BlnPlus" runat="server"></asp:Label></td>
                                                            </tr>
                                                          </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>

                                            <h3 class="box-title">Sudah lewat jatuh tempo serah terima (sejak tanggal PPPU)</h3>
                            
                                            <div class="box-body" style="display: block;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <table>
                                                          <thead>
                                                            <tr>
                                                              <th scope="col" style="background-color: #f45d96; color: #000000;">1 bulan</th>
                                                              <th scope="col" style="background-color: #f45d96; color: #000000;">3 bulan</th>
                                                              <th scope="col" style="background-color: #f45d96; color: #000000;">6 bulan</th>
                                                              <th scope="col" style="background-color: #f45d96; color: #000000;">9 bulan</th>
                                                              <th scope="col" style="background-color: #f45d96; color: #000000;">12 bulan</th>
                                                              <th scope="col" style="background-color: #f45d96; color: #000000;">> 12 bulan</th>
                                                            </tr>
                                                          </thead>
                                                          <tbody>
                                                            <tr>
                                                              <td><asp:Label ID="lblLewatJatuhTempo1Bln" runat="server"></asp:Label></td>
                                                              <td><asp:Label ID="lblLewatJatuhTempo3Bln" runat="server"></asp:Label></td>
                                                              <td><asp:Label ID="lblLewatJatuhTempo6Bln" runat="server"></asp:Label></td>
                                                              <td><asp:Label ID="lblLewatJatuhTempo9Bln" runat="server"></asp:Label></td>
                                                              <td><asp:Label ID="lblLewatJatuhTempo12Bln" runat="server"></asp:Label></td>
                                                              <td><asp:Label ID="lblLewatJatuhTempo12BlnPlus" runat="server"></asp:Label></td>
                                                            </tr>
                                                          </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </asp:Panel>
        </section>
    </div>

    <div class="hiddenElm divDetail" id="divDetailUnit">
        <table style="width: 100%">
            <tr>
                <td style="width: 20%"><b>Booking Code</b></td>
                <td> <asp:Label ID="lblBookCode" runat="server"></asp:Label></td>
                <td style="width: 20%"><b>Booking Date</b></td>
                <td> <asp:Label ID="lblBookDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 20%"><b>Booking Age</b></td>
                <td> <asp:Label ID="lblBookingAge" runat="server"></asp:Label></td>
                <td style="width: 20%"><b>Renovasi</b></td>
                <td> <asp:Label ID="lblRenov" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 20%"><b>STP Date</b></td>
                <td> <asp:Label ID="lblSTPDate" runat="server"></asp:Label></td>
                <td style="width: 20%"><b>STF Date</b></td>
                <td> <asp:Label ID="lblSTFDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 20%"><b>PPPU No</b></td>
                <td> <asp:Label ID="lblPPPUNo" runat="server"></asp:Label></td>
                <td style="width: 20%"><b>PPPU Date</b></td>
                <td> <asp:Label ID="lblPPPUDate" runat="server"></asp:Label></td>
            </tr>
        </table>
    </div>

    <div class="modal inmodal fade" id="divPopUpDetailUnit" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">&times;</span>
                        <span class="sr-only">Close</span>
                    </button>
                </div>

                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="box box-info">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <b>Booking Code</b>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblPopUpBookCode" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                    <br />

                                    <div class="row">
                                        <div class="col-md-6">
                                            <b>Booking Age</b>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblPopUpBookingAge" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                    <br />

                                    <div class="row">
                                        <div class="col-md-6">
                                            <b>STP Date</b>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblPopUpSTPDate" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                    <br />

                                    <div class="row">
                                        <div class="col-md-6">
                                            <b>PPPU No</b>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblPopUpPPPUNo" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="box box-info">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <b>Booking Date</b>
                                        </div>
                                        <div class="col-md-6" style="text-align: right !important">
											<asp:Label ID="lblPopUpBookDate" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <b>Renovasi</b>
                                        </div>
                                        <div class="col-md-6" style="text-align: right !important">
											<asp:Label ID="lblPopUpRenov" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <b>STF Date</b>
                                        </div>
                                        <div class="col-md-6" style="text-align: right !important">
											<asp:Label ID="lblPopUpSTFDate" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <b>PPPU Date</b>
                                        </div>
                                        <div class="col-md-6" style="text-align: right !important">
											<asp:Label ID="lblPopUpPPPUDate" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>

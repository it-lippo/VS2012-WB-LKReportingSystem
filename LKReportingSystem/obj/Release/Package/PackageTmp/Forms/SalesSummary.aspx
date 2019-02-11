<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalesSummary.aspx.cs" Inherits="LKReportingSystem.Forms.SalesSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <link href="<%= Page.ResolveClientUrl("~/Component/footable/css/footable.min.css") %>" rel="stylesheet" type="text/css">
    <script src="<%= Page.ResolveClientUrl("~/Component/footable/js/footable.min.js") %>"></script>

    <link href="<%= Page.ResolveClientUrl("~/Component/bootstrap-select/css/bootstrap-multiselect.css") %>" rel="stylesheet" type="text/css">
    <script src="<%= Page.ResolveClientUrl("~/Component/bootstrap-select/js/bootstrap-multiselect.js") %>"></script>

    <style>
        .btnWidth150 {
            width: 150px;
        }
    </style>
    <script>

        function pageLoad() {
            $(".datepicker").datetimepicker({
                format: 'DD/MM/YYYY',
                locale: 'en',
            });

            $(".selectpicker").selectpicker();


            $(".footable").footable();

            $(".multiselect").multiselect({
                includeSelectAllOption: true,
                buttonClass: 'form-control'
            });
        }

        function ValidateForm() {
            if ($("[id$='ddlProject']").val() == "" || $("[id$='ddlProject']").val() == null) {
                BootboxAlert("Project is Required. Please choose Project.");
                return false;
            }
            if ($("[id$='lbxCluster']").val() == "" || $("[id$='lbxCluster']").val() == null) {
                BootboxAlert("Cluster is Required. Please choose Cluster.");
                return false;
            }

            return true;
        }
    </script>



    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upTransactionUnit">
        <ProgressTemplate>
            <div class="modalLoading">
                <div class="centerLoading">
                    <img alt="" src="<%= Page.ResolveClientUrl("~/Images/loading.gif") %>" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="upTransactionUnit" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="gvDataSalesSummary" />
        </Triggers>
        <ContentTemplate>
            <div id="page-wrapper">
                <div class="container-fluid">
                    <!-- Page Heading -->
                    <div class="row">
                        <div class="col-lg-12">
                            <h1 class="page-header">Summary <small>Sales Overview</small>
                            </h1>
                            <ol class="breadcrumb">
                                <li class="active">
                                    <i class="fa fa-table"></i>Sales Summary
                                </li>
                            </ol>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><i class="fa fa-navicon fa-fw"></i>Parameters</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-lg-3 col-sm-3 col-md-2">Project</div>
                                        <div class="col-lg-9 col-sm-9 col-md-10">
                                            <asp:DropDownList ID="ddlProject" CssClass="selectpicker form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlProject_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="row martop5">
                                        <div class="col-lg-3 col-sm-3 col-md-2">Cluster</div>
                                        <div class="col-lg-9 col-sm-9 col-md-10">
                                            <asp:ListBox ID="lbxCluster" CssClass="multiselect form-control" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>

                                    <div class="row martop5">
                                        <div class="col-lg-3 col-sm-3 col-md-2">As Of Date</div>
                                        <div class="col-lg-9 col-sm-9 col-md-10">
                                            <asp:TextBox ID="txtAsOfDate" runat="server" CssClass="form-control datepicker" MaxLength="11"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row martop5">
                                        <div class="col-lg-3 col-sm-3 col-md-2"></div>
                                        <div class="col-lg-9 col-sm-9 col-md-10">
                                            <asp:Button ID="btnGenerate" OnClientClick="return ValidateForm();" OnClick="btnGenerate_Click" Text="Generate" CssClass="btn btn-primary" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><i class="fa fa fa-gears fa-fw"></i>Master Data</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row btn-group">
                                        <div class="col-md-3">
                                            <asp:Button ID="BtnInitialBudget" OnClick="btnInitialBudget_Click" Text="Initial Budget" CssClass="btn btn-primary btnWidth150" runat="server" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="BtnConstructionCost" OnClick="btnConstructionCost_Click" Text="Construction Cost" CssClass="btn btn-primary btnWidth150" runat="server" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="BtnSalarySetup" OnClick="btnSalarySetup_Click" Text="Salary Expense" CssClass="btn btn-primary btnWidth150" runat="server" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="BtnCashflowSetup" OnClick="btnCashflowSetup_Click" Text="Cashflow" CssClass="btn btn-primary btnWidth150" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><i class="fa fa fa-bar-chart fa-fw"></i>History</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="gvDataSalesSummary" EmptyDataText="No Data Available" CssClass="footable" runat="server" EnableViewState="true"
                                        AutoGenerateColumns="false" Style="max-width: 100%" OnRowDataBound="gvDataSalesSummary_RowDataBound" OnRowCommand="gvDataSalesSummary_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="batchID" HeaderText="ID" />
                                            <asp:BoundField DataField="batchDate" HeaderText="Generate Date" />
                                            <asp:BoundField DataField="projectcode" HeaderText="Project Code" />
                                            <asp:BoundField DataField="projectname" HeaderText="Project" />
                                            <asp:BoundField DataField="reportasofdate" HeaderText="Report As Of Date" />
                                            <%--<asp:BoundField DataField="reportasofdate" HeaderText="Report As Of Date" dataformatstring="{0:dd MMMM yyyy}" htmlencode="false"/> --%>

                                            <%--  <asp:TemplateField HeaderText="Update">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbUpdate" runat="server" CommandName="UpdateData" CommandArgument='<%#Eval("batchid")%>'><i class="fa fa-pencil-square-o"></i> Update</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="View">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbView" runat="server" CommandName="ViewData" CommandArgument='<%#Eval("batchid")%>'><i class="fa fa-eye"></i> View</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Download">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbDownload" runat="server" CommandName="DownloadData" CommandArgument='<%#Eval("batchid")%>'><i class="fa fa-cloud-download"></i> Download</asp:LinkButton>
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


            <%--            <div class="modal fade" id="modalView" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Data Sales Summary</h4>
                        </div>
                        <div class="modal-body">
                            <asp:Literal ID="ltView" runat="server"></asp:Literal>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>

                </div>
            </div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

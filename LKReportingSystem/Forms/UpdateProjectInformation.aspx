<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateProjectInformation.aspx.cs" Inherits="LKReportingSystem.Forms.UpdateProjectInformation" %>

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
        .rightAlign {
            text-align: right;
        }
    </style>

    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            return !(charCode > 31 && (charCode < 48 || charCode > 57));
        }
    </script>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upProjectInformation">
        <ProgressTemplate>
            <div class="modalLoading">
                <div class="centerLoading">
                    <img alt="" src="<%= Page.ResolveClientUrl("~/Images/loading.gif") %>" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="upProjectInformation" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="gvDataProjectInformation" />
        </Triggers>
        <ContentTemplate>
            <div id="page-wrapper">
                <div class="container-fluid">
                    <!-- Page Heading -->
                    <div class="row">
                        <div class="col-lg-12">
                            <h1 class="page-header">Project Information <small>Setup</small>
                            </h1>
                            <%-- <ol class="breadcrumb">
                                <li class="active">
                                    <i class="fa fa-table"></i>Sales Summary
                                </li>
                            </ol>--%>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                            <%--<div class="panel panel-default">
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
                            </div>--%>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><i class="fa fa fa-bar-chart fa-fw"></i>Initial Budget</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="gvDataProjectInformation" EmptyDataText="No Data Available" CssClass="footable" runat="server" EnableViewState="true"
                                        AutoGenerateColumns="false" OnRowDataBound="gvDataProjectInformation_RowDataBound" Style="max-width: 100%" ViewStateMode="Enabled">
                                        <Columns>
                                            <asp:BoundField DataField="ClusterCode" HeaderText="ClusterCode" />
                                            <asp:BoundField DataField="ClusterName" HeaderText="Cluster" />
                                            <asp:TemplateField HeaderText="No Of Unit">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitBudgetNoOfUnit" runat="server" Text='<%# Bind("InitBudgetNoOfUnit") %>' onkeypress="return isNumberKey(this);" CssClass="rightAlign" Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="M2">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitBudgetMSquare" runat="server" Text='<%# Bind("InitBudgetMSquare", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" CssClass="rightAlign" Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rp/M2">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitBudgetValuePerMSquare" runat="server" Text='<%# Bind("InitBudgetValuePerMSquare", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" CssClass="rightAlign" Width="120px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rp/Unit">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitBudgetValuePerUnit" runat="server" Text='<%# Bind("InitBudgetValuePerUnit", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" CssClass="rightAlign" Width="120px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rp">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitBudgetValueTotal" runat="server" Text='<%# Bind("InitBudgetValueTotal", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" CssClass="rightAlign" Width="120px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="% Sales MKT<br />Expense">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitPctSalesMktExpense" runat="server" Text='<%# Bind("InitPctSalesMktExpense", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" CssClass="rightAlign" Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="help-block"></div>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><i class="fa fa fa-bar-chart fa-fw"></i>Construction Cost</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="gvDataConstCost" EmptyDataText="No Data Available" CssClass="footable" runat="server" EnableViewState="true"
                                        AutoGenerateColumns="false" OnRowDataBound="gvDataConstCost_RowDataBound" Style="max-width: 100%" ViewStateMode="Enabled">
                                        <Columns>
                                            <asp:BoundField DataField="ClusterCode" HeaderText="ClusterCode" />
                                            <asp:BoundField DataField="ClusterName" HeaderText="Cluster" />
                                            <asp:TemplateField HeaderText="Const. Cost Original<br \>Rp">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbConstCostOriginalValueTotal" runat="server" Text='<%# Bind("ConstCostOriginalValueTotal", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" CssClass="rightAlign"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Const. Cost Original<br \>Rp/M2">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbConstCostPerMSquareArea" runat="server" Text='<%# Bind("ConstCostPerMSquareArea", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" CssClass="rightAlign"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Projected Till Completion<br \>Rp">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbProjectedValueTotalTillCompletion" runat="server" Text='<%# Bind("ProjectedValueTotalTillCompletion", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" CssClass="rightAlign"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Projected Till Completion<br \>Rp/M2">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbProjectedValuePerMSquareAreaTillCompletion" runat="server" Text='<%# Bind("ProjectedValuePerMSquareAreaTillCompletion", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" CssClass="rightAlign"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-1">
                                    <%--<asp:Button ID="btnSave" OnClientClick="return ValidateForm();" OnClick="btnSave_Click" Text="Update" CssClass="btn btn-primary" runat="server" />--%>
                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" Text="Update" CssClass="btn btn-primary" runat="server" />
                                </div>
                                <div class="col-md-11">
                                    <asp:Button ID="btnCancel" OnClick="btnCancel_Click" Text="Back" CssClass="btn btn-primary" runat="server" />
                                </div>
                            </div>
                            <div class="help-block"></div>
                            <hr />
                        </div>
                    </div>
                </div>
            </div>


            <%--<div class="modal fade" id="modalView" role="dialog">
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

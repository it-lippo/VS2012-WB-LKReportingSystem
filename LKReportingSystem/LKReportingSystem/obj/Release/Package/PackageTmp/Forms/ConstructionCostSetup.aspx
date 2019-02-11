<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConstructionCostSetup.aspx.cs" Inherits="LKReportingSystem.Forms.ConstructionCostSetup" %>

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

        .btnWidth120 {
            width: 120px;
        }
    </style>

    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            return !(charCode > 31 && (charCode < 48 || charCode > 57));
        }
    </script>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upConstructionCost">
        <ProgressTemplate>
            <div class="modalLoading">
                <div class="centerLoading">
                    <img alt="" src="<%= Page.ResolveClientUrl("~/Images/loading.gif") %>" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="upConstructionCost" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="gvConstructionCost" />
        </Triggers>
        <ContentTemplate>
            <div id="page-wrapper">
                <div class="container-fluid">
                    <!-- Page Heading -->
                    <div class="row">
                        <div class="col-lg-12">
                            <h1 class="page-header">Construction Cost</h1>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><i class="fa fa fa-building fa-fw"></i>Construction Cost</h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView ID="gvConstructionCost" EmptyDataText="No Data Available" CssClass="footable" runat="server" EnableViewState="true"
                                        AutoGenerateColumns="false" OnRowDataBound="gvConstructionCost_RowDataBound" Style="max-width: 100%" ViewStateMode="Enabled">
                                        <Columns>
                                            <asp:BoundField DataField="ClusterCode" HeaderText="ClusterCode" />
                                            <asp:BoundField DataField="ClusterName" HeaderText="Cluster" />
                                            <asp:TemplateField HeaderText="Const. Cost Original<br \>Rp">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitBudgetValueTotal" runat="server" Text='<%# Bind("InitBudgetValueTotal", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" BackColor="#F5F5F5" CssClass="form-control rightAlign"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Const. Cost Original<br \>Rp/M2">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitBudgetValueConstCostPerMSquare" runat="server" Text='<%# Bind("InitBudgetValueConstCostPerMSquare", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" BackColor="#F5F5F5" CssClass="form-control rightAlign"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Projected Till Completion<br \>Rp">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbProjectedValueTotalTillCompletion" runat="server" Text='<%# Bind("ProjectedValueTotalTillCompletion", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" CssClass="form-control rightAlign"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Projected Till Completion<br \>Rp/M2">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbProjectedValuePerMSquareAreaTillCompletion" runat="server" Text='<%# Bind("ProjectedValuePerMSquareAreaTillCompletion", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" CssClass="form-control rightAlign"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-2">
                                    <%--<asp:Button ID="btnSave" OnClientClick="return ValidateForm();" OnClick="btnSave_Click" Text="Update" CssClass="btn btn-primary" runat="server" />--%>
                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" Text="Update" CssClass="btn btn-primary btnWidth120" runat="server" />
                                </div>
                                <div class="col-md-10">
                                    <asp:Button ID="btnCancel" OnClick="btnCancel_Click" Text="Back" CssClass="btn btn-primary btnWidth120" runat="server" />
                                </div>

                                <asp:UpdatePanel ID="updatePanelHtmlNotificationConstruction" UpdateMode="Conditional" runat="server" class="col-md-6">
                                            <ContentTemplate>
                                                <div><br /></div>
                                                <div class="col-md-6" id="htmlNotificationConstruction" runat="server"></div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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

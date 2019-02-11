<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InitialBudgetSetup.aspx.cs" Inherits="LKReportingSystem.Forms.InitialBudgetSetup" %>

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
            text-align: right !important;
        }

        .centerAlign {
            text-align: center !important;
        }

        .fontSize10 {
            font-size: 10px !important;
        }

        .fontSize12 {
            font-size: 12px !important;
        }

        .fontSize14 {
            font-size: 14px !important;
        }

        .fontSize16 {
            font-size: 16px !important;
        }

        .tbNarrowPadding {
            padding-left: 4px !important;
            padding-right: 4px !important;
        }

        .btnWidth120 {
            width: 120px;
        }
    </style>


    <script type="text/javascript">
        function CalculateAmount(InitBudgetNoOfUnitCtrlID, InitBudgetMSquareCtrlID, InitBudgetValuePerMSquareCtrlID, InitBudgetValueTotalCtrlID, InitBudgetValuePerUnitCtrlID, InitBudgetValueLandCostPerMSquareCtrlID, InitBudgetValueConstCostPerMSquareCtrlID, InitBudgetValueCOGSPerMSquareCtrlID, InitBudgetValueGrossMarginPerMSquareCtrlID) {
            debugger;
            var InitBudgetNoOfUnit = document.getElementById(InitBudgetNoOfUnitCtrlID).value.split(",").join("");
            var InitBudgetMSquare = document.getElementById(InitBudgetMSquareCtrlID).value.split(",").join("");
            var InitBudgetValuePerMSquare = document.getElementById(InitBudgetValuePerMSquareCtrlID).value.split(",").join("");
            var InitBudgetValueTotal = document.getElementById(InitBudgetValueTotalCtrlID);
            var InitBudgetValuePerUnit = document.getElementById(InitBudgetValuePerUnitCtrlID);

            var ValInitBudgetNoOfUnit = 0;
            var ValInitBudgetMSquare = 0;
            var ValInitBudgetValuePerMSquare = 0;

            var InitBudgetValueLandCostPerMSquare = document.getElementById(InitBudgetValueLandCostPerMSquareCtrlID).value.split(",").join("");
            var InitBudgetValueConstCostPerMSquare = document.getElementById(InitBudgetValueConstCostPerMSquareCtrlID).value.split(",").join("");
            var InitBudgetValueCOGSPerMSquare = document.getElementById(InitBudgetValueCOGSPerMSquareCtrlID);
            var InitBudgetValueGrossMarginPerMSquare = document.getElementById(InitBudgetValueGrossMarginPerMSquareCtrlID);


            var ValBudgetValueLandCostPerMSquare = 0;
            var ValBudgetValueConstCostPerMSquare = 0;


            try {
                ValInitBudgetNoOfUnit = parseFloat(InitBudgetNoOfUnit);
                ValInitBudgetMSquare = parseFloat(InitBudgetMSquare);
                ValInitBudgetValuePerMSquare = parseFloat(InitBudgetValuePerMSquare);

                ValBudgetValueLandCostPerMSquare = parseFloat(InitBudgetValueLandCostPerMSquare);
                ValBudgetValueConstCostPerMSquare = parseFloat(InitBudgetValueConstCostPerMSquare);

            }
            catch (err) {
                ValInitBudgetNoOfUnit = 1;
                ValInitBudgetMSquare = 0;
                ValInitBudgetValuePerMSquare = 0;

                ValBudgetValueLandCostPerMSquare = 0;
                ValBudgetValueConstCostPerMSquare = 0;

            }

            var totalRpRevenue = Math.round(parseFloat(ValInitBudgetMSquare * ValInitBudgetValuePerMSquare));
            var totalRpPerUnitRevenue = Math.round(parseFloat(totalRpRevenue / ValInitBudgetNoOfUnit));

            InitBudgetValueTotal.value = FormatCurrency(totalRpRevenue);
            InitBudgetValuePerUnit.value = FormatCurrency(totalRpPerUnitRevenue);
            document.getElementById(InitBudgetMSquareCtrlID).value = FormatCurrency(ValInitBudgetMSquare);
            document.getElementById(InitBudgetValuePerMSquareCtrlID).value = FormatCurrency(ValInitBudgetValuePerMSquare);

            var totalRpCOGSPerMSquare = ValBudgetValueLandCostPerMSquare + ValBudgetValueConstCostPerMSquare;
            var totalRpGrossMarginPerMSquare = ValInitBudgetValuePerMSquare - totalRpCOGSPerMSquare;
            //var totalRpGrossMarginPerMSquare = totalRpRevenue - totalRpCOGSPerMSquare;

            InitBudgetValueCOGSPerMSquare.value = FormatCurrency(totalRpCOGSPerMSquare);
            InitBudgetValueGrossMarginPerMSquare.value = FormatCurrency(totalRpGrossMarginPerMSquare);
            document.getElementById(InitBudgetValueLandCostPerMSquareCtrlID).value = FormatCurrency(ValBudgetValueLandCostPerMSquare);
            document.getElementById(InitBudgetValueConstCostPerMSquareCtrlID).value = FormatCurrency(ValBudgetValueConstCostPerMSquare);

        }

    </script>


    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upInitialBudget">
        <ProgressTemplate>
            <div class="modalLoading">
                <div class="centerLoading">
                    <img alt="" src="<%= Page.ResolveClientUrl("~/Images/loading.gif") %>" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="upInitialBudget" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="gvInitialBudget" />
        </Triggers>
        <ContentTemplate>
            <div id="page-wrapper">
                <div class="container-fluid">
                    <!-- Page Heading -->
                    <div class="row">
                        <div class="col-lg-12">
                            <h1 class="page-header">Initial Budget
                            </h1>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><i class="fa fa fa-dollar fa-fw"></i>Initial Budget</h3>
                                </div>
                                <div class="panel-body container-fluid" style="overflow: auto">
                                    <asp:GridView ID="gvInitialBudget" EmptyDataText="No Data Available" CssClass="footable" runat="server" EnableViewState="true"
                                        AutoGenerateColumns="false" OnRowDataBound="gvInitialBudget_RowDataBound" ViewStateMode="Enabled">
                                        <Columns>
                                            <asp:BoundField DataField="ClusterCode" HeaderText="ClusterCode" HeaderStyle-CssClass="centerAlign fontSize12" />
                                            <asp:BoundField DataField="ClusterName" HeaderText="Cluster" ItemStyle-CssClass="fontSize12" HeaderStyle-CssClass="centerAlign fontSize12" />
                                            <asp:TemplateField HeaderText="No Of Unit" HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitBudgetNoOfUnit" runat="server" Text='<%# Bind("InitBudgetNoOfUnit") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="M2 (SGA)" HeaderStyle-CssClass="centerAlign fontSize12 tbNarrowPadding">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitBudgetMSquare" runat="server" Text='<%# Bind("InitBudgetMSquare", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onchange="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" CssClass="form-control rightAlign fontSize12 tbNarrowPadding" Width="100%"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rp/M2 (SGA)<br />(Revenue)" HeaderStyle-CssClass="centerAlign fontSize12">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitBudgetValuePerMSquare" runat="server" Text='<%# Bind("InitBudgetValuePerMSquare", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onchange="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" CssClass="form-control rightAlign fontSize12 tbNarrowPadding" Width="100%"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rp/Unit<br />(Revenue)" HeaderStyle-CssClass="centerAlign fontSize12">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitBudgetValuePerUnit" runat="server" Text='<%# Bind("InitBudgetValuePerUnit", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onchange="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" BackColor="#F5F5F5" CssClass="form-control rightAlign fontSize12 tbNarrowPadding"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rp<br />(Revenue)" HeaderStyle-CssClass="centerAlign fontSize12">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitBudgetValueTotal" runat="server" Text='<%# Bind("InitBudgetValueTotal", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onchange="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" BackColor="#F5F5F5" CssClass="form-control rightAlign fontSize12 tbNarrowPadding" Width="100%"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Land Cost/M2" HeaderStyle-CssClass="centerAlign fontSize12">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitBudgetValueLandCostPerMSquare" runat="server" Text='<%# Bind("InitBudgetValueLandCostPerMSquare", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onchange="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" CssClass="form-control rightAlign fontSize12 tbNarrowPadding" Width="100%"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Const Cost/M2" HeaderStyle-CssClass="centerAlign fontSize12">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitBudgetValueConstCostPerMSquare" runat="server" Text='<%# Bind("InitBudgetValueConstCostPerMSquare", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onchange="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" CssClass="form-control rightAlign fontSize12 tbNarrowPadding" Width="100%"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="COGS/M2" HeaderStyle-CssClass="centerAlign fontSize12">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitBudgetValueCOGSPerMSquare" runat="server" Text='<%# Bind("InitBudgetValueCOGSPerMSquare", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onchange="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" BackColor="#F5F5F5" CssClass="form-control rightAlign fontSize12 tbNarrowPadding" Width="100%"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gross Margin/M2" HeaderStyle-CssClass="centerAlign fontSize12">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitBudgetValueGrossMarginPerMSquare" runat="server" Text='<%# Bind("InitBudgetValueGrossMarginPerMSquare", "{0:###,###}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onchange="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" BackColor="#F5F5F5" CssClass="form-control rightAlign fontSize12 tbNarrowPadding" Width="100%"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="% MKT<br />Expense" HeaderStyle-CssClass="centerAlign fontSize12">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitPctMKTExpense" runat="server" Text='<%# Bind("InitPctMKTExpense", "{0:n2}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onchange="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" CssClass="form-control rightAlign fontSize12 tbNarrowPadding" Width="100%"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="% Sales<br />Expense" HeaderStyle-CssClass="centerAlign fontSize12">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitPctSalesExpense" runat="server" Text='<%# Bind("InitPctSalesExpense", "{0:n2}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onchange="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" CssClass="form-control rightAlign fontSize12 tbNarrowPadding" Width="100%"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="% Capex & Subsidy<br />Expense" HeaderStyle-CssClass="centerAlign fontSize12">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitPctCapexSubsidyExpense" runat="server" Text='<%# Bind("InitPctCapexSubsidyExpense", "{0:n2}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onchange="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" CssClass="form-control rightAlign fontSize12 tbNarrowPadding" Width="100%"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="% Sales MKT<br />Expense" HeaderStyle-CssClass="centerAlign fontSize12">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TbInitPctSalesMktExpense" runat="server" Text='<%# Bind("InitPctSalesMktExpense", "{0:n2}") %>' onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onchange="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" CssClass="form-control rightAlign fontSize12 tbNarrowPadding" Width="100%"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>


                                </div>
                            </div>
                            <div class="help-block"></div>
                            <hr />
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" Text="Update" CssClass="btn btn-primary btnWidth120" runat="server" />
                                </div>
                                <div class="col-md-10">
                                    <asp:Button ID="btnCancel" OnClick="btnCancel_Click" Text="Back" CssClass="btn btn-primary btnWidth120" runat="server" />
                                </div>

                                <asp:UpdatePanel ID="updatePanelHtmlNotificationBudget" UpdateMode="Conditional" runat="server" class="col-md-6">
                                            <ContentTemplate>
                                                <div><br /></div>
                                                <div class="col-md-6" id="htmlNotificationBudget" runat="server"></div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                            </div>
                            <div class="help-block"></div>
                            <hr />
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

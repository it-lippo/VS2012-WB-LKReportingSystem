<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CashFlowSetup.aspx.cs" Inherits="LKReportingSystem.Forms.CashFlowSetup" %>

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
    </style>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatePanel">
        <ProgressTemplate>
            <div class="modalLoading">
                <div class="centerLoading">
                    <img alt="" src="<%= Page.ResolveClientUrl("~/Images/loading.gif") %>" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
        <ContentTemplate>
            <div id="page-wrapper">
                <div class="container-fluid">
                    <!-- Page Heading -->
                    <div class="row">
                        <div class="col-lg-12">
                            <h1 class="page-header">Cashflow</h1>
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
                                        <div class="col-lg-2 col-sm-2 col-md-2">Select a file to upload:</div>
                                        <div class="col-lg-10 col-sm-10 col-md-10">
                                            <asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload>
                                        </div>
                                    </div>

                                    <div class="row martop5">
                                        <div class="col-lg-2 col-sm-2 col-md-2"></div>
                                        <div class="col-lg-10 col-sm-10 col-md-10">
                                            <div class="form-group">
                                                <asp:Button ID="btnUpload" OnClientClick="return ValidateForm();" OnClick="UploadButton_Click" Text="Upload" CssClass="btn btn-primary" runat="server" />
                                                <asp:Button ID="btnTemplate" OnClick="TemplateButton_Click" Text="Download Template" CssClass="btn btn-primary" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h3 class="panel-title"><i class="fa fa fa-building fa-fw"></i>Cashflow</h3>
                                        </div>
                                        <div class="panel-body">
                                            <asp:GridView ID="DGV" EmptyDataText="No Data Available" CssClass="footable" runat="server" EnableViewState="true"
                                                AutoGenerateColumns="false" OnRowDataBound="DGV_RowDataBound" ViewStateMode="Enabled">
                                                <Columns>
                                                    <asp:BoundField DataField="CashflowItem" HeaderText="ITEM" DataFormatString="{0:###,###}" HtmlEncode="false" HeaderStyle-CssClass="centerAlign" />
                                                    <asp:BoundField DataField="Cashflowvalue" HeaderText="VALUE" DataFormatString="{0:###,###}" HtmlEncode="false" HeaderStyle-CssClass="centerAlign" ItemStyle-CssClass="rightAlign" />
                                                </Columns>
                                            </asp:GridView>
                                            <%--<asp:GridView ID="DGV" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="DGV_RowDataBound">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333"  />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>--%>
                                        </div>
                                    </div>
                                    
                                    <hr />
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-1">
                                                 <asp:Button ID="ButtonUpdate" OnClick="btnSave_Click" Text="Update" CssClass="btn btn-primary btnWidth120" runat="server" />
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Button ID="btnCancel" OnClick="btnCancel_Click" Text="Back" CssClass="btn btn-primary btnWidth120" runat="server" />
                                            </div>

                                            <asp:UpdatePanel ID="updatePanelHtmlNotificationCashFlow" UpdateMode="Conditional" runat="server" class="col-md-6">
                                            <ContentTemplate>
                                                <div><br /></div>
                                                <div class="col-md-6" id="htmlNotificationCashFlow" runat="server"></div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        </div>

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

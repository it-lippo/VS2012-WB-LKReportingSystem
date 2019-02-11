<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectSalarySetup.aspx.cs" Inherits="LKReportingSystem.Forms.ProjectSalarySetup" %>

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

        .verCenterAlign {
            vertical-align: bottom;
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

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upDGV">
        <ProgressTemplate>
            <div class="modalLoading">
                <div class="centerLoading">
                    <img alt="" src="<%= Page.ResolveClientUrl("~/Images/loading.gif") %>" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="upDGV" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="gvSalary" />
        </Triggers>
        <ContentTemplate>
            <div id="page-wrapper">
                <div class="container-fluid">
                    <!-- Page Heading -->
                    <div class="row">
                        <div class="col-lg-12">
                            <h1 class="page-header">Project Salary</h1>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><i class="fa fa fa-money fa-fw"></i>Project Salary</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="col-lg-3 col-sm-3 col-md-3">
                                                <asp:Label ID="LblCurrentMonth" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="col-lg-9 col-sm-9 col-md-9">
                                                <asp:TextBox ID="txtSalaryValue" runat="server" CssClass="form-control" onkeypress="return isNumberKeyWithDecimal(event);" onkeyup="this.value=FormatCurrency(this.value);" onkeydown="return AvoidSpace();" MaxLength="12"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row martop5">
                                        <div class="col-lg-12">
                                            <asp:GridView ID="gvSalary" CssClass="footable" runat="server" EnableViewState="true"
                                                AutoGenerateColumns="false" Style="max-width: 100%" ViewStateMode="Enabled">
                                                <Columns>
                                                    <asp:BoundField DataField="SalaryYear" HeaderText="Year" />
                                                    <asp:BoundField DataField="SalaryMonth" HeaderText="Month" />
                                                    <asp:BoundField DataField="SalaryValue" DataFormatString="{0:###,###}" HeaderText="Amount" ItemStyle-CssClass="rightAlign" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="help-block"></div>
                            <hr />
                            <div class="row">
                                <div class="col-md-2">
                                    <%--<asp:Button ID="btnSave" OnClientClick="return ValidateForm();" OnClick="btnSave_Click" Text="Update" CssClass="btn btn-primary" runat="server" />--%>
                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" Text="Update" CssClass="btn btn-primary btnWidth120" runat="server" />
                                </div>
                                <div class="col-md-10">
                                    <asp:Button ID="btnCancel" OnClick="btnCancel_Click" Text="Back" CssClass="btn btn-primary btnWidth120" runat="server" />
                                </div>

                                <asp:UpdatePanel ID="updatePanelHtmlNotificationSalary" UpdateMode="Conditional" runat="server" class="col-md-6">
                                    <ContentTemplate>
                                        <div><br /></div>
                                        <div class="col-md-6" id="htmlNotificationSalary" runat="server"></div>
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


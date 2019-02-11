<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SMSUndelivered.aspx.cs" Inherits="LKReportingSystem.Forms.Notification.SMSUndelivered" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <link href="<%= Page.ResolveClientUrl("~/Component/footable/css/footable.min.css") %>" rel="stylesheet" type="text/css">
    <script src="<%= Page.ResolveClientUrl("~/Component/footable/js/footable.min.js") %>"></script>

    <link href="<%= Page.ResolveClientUrl("~/Component/bootstrap-select/css/bootstrap-multiselect.css") %>" rel="stylesheet" type="text/css">
    <script src="<%= Page.ResolveClientUrl("~/Component/bootstrap-select/js/bootstrap-multiselect.js") %>"></script>

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

        function openModalEmail() {
            $("[id$='btnDisplayModal']").click();
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
            <ContentTemplate>
                <div id="page-wrapper">
                <div class="container-fluid">
                    <!-- Page Heading -->
                    <div class="row">
                        <div class="col-lg-12">
                            <h1 class="page-header">Report <small>Undelivered SMS</small>
                            </h1>

                            <ol class="breadcrumb">
                                <li class="active">
                                    <i class="fa fa-pencil"></i>&nbsp; Summary Report
                                </li>
                            </ol>
                        </div>
                    </div>
            
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><i class="fa fa-newspaper-o fa-fw"></i>&nbsp;Filter</h3>
                                </div>
                                <div class="panel-body" id="divFilter" runat="server">
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
                                        <div class="col-lg-3 col-sm-3 col-md-2">Type of Notification</div>
                                        <div class="col-lg-9 col-sm-9 col-md-10">
                                            <asp:ListBox ID="lbNotifType" CssClass="multiselect form-control" SelectionMode="Multiple" runat="server"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="row martop5">
                                        <div class="col-lg-3 col-sm-3 col-md-2"></div>
                                        <div class="col-lg-9 col-sm-9 col-md-10">
                                            <asp:Button ID="btnGenerate" OnClick="btnGenerate_Click" runat="server" Text="Generate" Width="100" CssClass="btn btn-primary rightAlign"/>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><i class="fa fa fa-bar-chart fa-fw"></i>Report</h3>
                                </div>
                                <div class="panel-body">
                                    <button type="button" id="btnDisplayModal" style="visibility:hidden" class="form-control btn btn-info col-sm-5 col-xs-12" data-toggle="modal" data-target="#viewSMSModal">View</button>

                                    <asp:GridView ID="gvReportUndeliv" EmptyDataText="No Data Available" CssClass="footable" runat="server" EnableViewState="true"
                                        AutoGenerateColumns="false" Style="max-width: 100%" OnRowCommand="gvReportUndeliv_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Unit" ItemStyle-Font-Size="11px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnitCode" runat="server" Text='<%# Bind("unitcode")  %>' /><br />
                                                    <asp:Label ID="lblUnitNo" runat="server" Text='<%# Bind("unitno") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Customer" ItemStyle-Font-Size="11px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPSCode" runat="server" Text='<%# Bind("pscode")  %>' /><br />
                                                    <asp:Label ID="lblPSName" runat="server" Text='<%# Bind("psname") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="notiftypedesc" HeaderText="Notification Type" ItemStyle-Font-Size="11px"/>
                                            <asp:BoundField DataField="smssubject" HeaderText="Subject" ItemStyle-Font-Size="11px" />
                                            <asp:BoundField DataField="sentTime" HeaderText="Sent Time"  ItemStyle-Font-Size="11px" />
                                            <asp:BoundField DataField="phoneno" HeaderText="Email" ItemStyle-Font-Size="11px"/>

                                            <asp:TemplateField HeaderText="View">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbViewContentEmail" runat="server" CssClass="form-control btn btn-info col-sm-5 col-xs-12" CommandName="ContentSMS" CommandArgument='<%#Eval("notifcode")%>'><i class="fa fa-eye" ></i>&nbsp;Content</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Log">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbViewResultEmail" runat="server" CssClass="form-control btn btn-info col-sm-5 col-xs-12" CommandName="ResultEmail" CommandArgument='<%#Eval("notifcode")%>'><i class="fa fa-eye" ></i>&nbsp;Result</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    <asp:UpdatePanel ID="updatePanelHtmlNotificationHistory" UpdateMode="Conditional" runat="server" class="col-md-6">
                                        <ContentTemplate>
                                            <div><br /></div>

                                            <div class="col-md-6" id="htmlNotificationHistory" runat="server"></div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

                <div class="modal" id="viewSMSModal" role="dialog">
                    <div class="modal-dialog" style="width: 90%">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Email Content</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <div style="width: 100%; overflow:auto">
                                        <asp:Literal ID="ltContentSMS" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Close</button>
                            </div>
                        </div>
      
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    
</asp:Content>

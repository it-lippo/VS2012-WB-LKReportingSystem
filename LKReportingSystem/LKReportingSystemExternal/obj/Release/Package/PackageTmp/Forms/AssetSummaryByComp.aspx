<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssetSummaryByComp.aspx.cs" Inherits="LKReportingSystemExternal.Forms.AssetSummaryByComp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <link href="<%= Page.ResolveClientUrl("~/Component/footable/css/footable.min.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%= Page.ResolveClientUrl("~/Component/footable/js/footable.min.js") %>"></script>

    <link href="<%= Page.ResolveClientUrl("~/Component/bootstrap-select/css/bootstrap-multiselect.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%= Page.ResolveClientUrl("~/Component/bootstrap-select/js/bootstrap-multiselect.js") %>"></script>

    <script>

        function pageLoad() {
            $(".multiselect").multiselect({
                includeSelectAllOption: true,
                buttonClass: 'form-control',
                maxHeight:250
            });
        }

        function ValidateForm() {
            var lbCompany = document.getElementById('<%=lbxCompany.ClientID %>');
            var lbProvince = document.getElementById('<%=lbxProvince.ClientID %>');
            var length = lbCompany.length;
            var i = 0;
            var SelectedItemCount = 0;

            for (i = 0; i < length; i++) {
                if (lbCompany.options[i].selected) {
                    SelectedItemCount = SelectedItemCount + 1;
                }
            }

            if (SelectedItemCount == 0) {
                BootboxAlert("Company is Required. Please choose Company.");
                return false;
            }

            length = lbProvince.length;
            i = 0;
            SelectedItemCount = 0;

            for (i = 0; i < length; i++) {
                if (lbProvince.options[i].selected) {
                    SelectedItemCount = SelectedItemCount + 1;
                }
            }

            if (SelectedItemCount == 0) {
                BootboxAlert("Province is Required. Please choose Province.");
                return false;
            }

            return true;
        }
    </script>

    <div id="page-wrapper">
        <div class="container-fluid">

            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Asset Summary By Company</h1>
                </div>
            </div>
                        
            <asp:UpdatePanel ID="upHtmlNotifMain" UpdateMode="Conditional" runat="server" class="col-md-6">
	            <ContentTemplate>
		            <div><br /></div>

		            <div class="col-md-6" id="htmlNotifMain" runat="server"></div>
	            </ContentTemplate>
            </asp:UpdatePanel>

            <div id="divContent" runat="server"> 
                <div class="row"> 
                    <div class="col-lg-12">
                        <div class="panel panel-default"> 
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="fa fa-navicon fa-fw"></i>Parameters</h3>
                            </div>
                            <div class="panel-body">
                                <div class="row"> 
                                    <div class="col-lg-3 col-sm-3 col-md-2" style="padding-top:8px">Perusahaan</div>    
                                    <div class="col-lg-9 col-sm-9 col-md-10" style="max-height:100px;">
                                        <asp:ListBox ID="lbxCompany" CssClass="multiselect form-control" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                </div>
                                <div class="row" style="padding-top:10px"> 
                                    <div class="col-lg-3 col-sm-3 col-md-2" style="padding-top:8px">Provinsi</div>    
                                    <div class="col-lg-9 col-sm-9 col-md-10" style="max-height:100px;">
                                        <asp:ListBox ID="lbxProvince" CssClass="multiselect form-control" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                </div>
                            </div> 
                        </div> 
                                          
                        <div>  
                            <asp:LinkButton ID="btnDownload" runat="server" OnClientClick="return ValidateForm();" CssClass="btn btn-info btn-md" style="float:right; margin-left:5px;" OnClick="btnDownload_Click"><i class="glyphicon glyphicon-download-alt"></i> Download as PDF</asp:LinkButton>

                            <asp:LinkButton ID="btnView" runat="server" OnClientClick="return ValidateForm();" CssClass="btn btn-info btn-md" style="float:right; margin-left:5px;" OnClick="btnView_Click"><i class="glyphicon glyphicon-eye-open"></i> View</asp:LinkButton>
                        </div> 
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
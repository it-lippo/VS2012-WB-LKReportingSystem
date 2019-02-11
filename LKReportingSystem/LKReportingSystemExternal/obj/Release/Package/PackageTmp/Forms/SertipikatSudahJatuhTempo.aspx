<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SertipikatSudahJatuhTempo.aspx.cs" Inherits="LKReportingSystemExternal.Forms.SertipikatSudahJatuhTempo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <link href="<%= Page.ResolveClientUrl("~/Component/footable/css/footable.min.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%= Page.ResolveClientUrl("~/Component/footable/js/footable.min.js") %>"></script>

    <link href="<%= Page.ResolveClientUrl("~/Component/bootstrap-select/css/bootstrap-multiselect.css") %>" rel="stylesheet" type="text/css" />
    <script src="<%= Page.ResolveClientUrl("~/Component/bootstrap-select/js/bootstrap-multiselect.js") %>"></script>

    <div id="page-wrapper">
        <div class="container-fluid">

            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Sertipikat Sudah Jatuh Tempo</h1>
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
                                <h3 class="panel-title"><i class="fa fa-navicon fa-fw"></i>Actions</h3>
                            </div>
                            <div class="panel-body">
                                <div>  
                                    <asp:LinkButton ID="btnView" runat="server" CssClass="btn btn-info btn-md" style="margin-left:5px;" OnClick="btnView_Click"><i class="glyphicon glyphicon-eye-open"></i> View</asp:LinkButton>
                                    
                                    <asp:LinkButton ID="btnDownload" runat="server" CssClass="btn btn-info btn-md" style="margin-left:5px;" OnClick="btnDownload_Click"><i class="glyphicon glyphicon-download-alt"></i> Download as PDF</asp:LinkButton>
                                </div> 
                            </div> 
                        </div> 
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
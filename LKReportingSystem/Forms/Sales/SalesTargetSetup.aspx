<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalesTargetSetup.aspx.cs" Inherits="LKReportingSystem.Forms.Sales.SalesTargetSetup" %>
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

        .rbl input[type="radio"]
        {
           margin-left: 10px;
           margin-right: 1px;
        }
    </style>

    <script>

        function pageLoad() {
            $(".datepicker").datetimepicker({
                format: 'YYYY',
                locale: 'en',
                viewMode: "years"
            });

            $(".footable").footable();
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
        <ContentTemplate>
            
            <div id="page-wrapper">
                <div class="container-fluid">
                    <!-- Page Heading -->
                    <div class="row">
                        <div class="col-lg-12">
                            <h1 class="page-header">Sales Target Setup
                            </h1>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><i class="fa fa fa-dollar fa-fw"></i>Setup Target</h3>
                                </div>
                                <div class="panel-body container-fluid" style="overflow: auto">
                                    
                                    <div class="row ">
                                        <div class="col-lg-3 col-sm-3 col-md-2">Year Periode</div>
                                        <div class="col-lg-5 col-sm-9 col-md-10">
                                            <asp:TextBox ID="txtyearperiod" runat="server" CssClass="form-control datepicker" MaxLength="11"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row martop5" >
                                        <div class="col-lg-3 col-sm-3 col-md-2">Input Type</div>
                                        <div class="col-lg-5 col-sm-9 col-md-10">
                                            <asp:RadioButtonList ID="rblInputType" runat="server" RepeatDirection="Horizontal" CssClass="rbl" >
                                                <asp:ListItem Value="monthly" Text="Monthly" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="yearly" Text="Yearly"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row martop5" >
                                        <div class="col-lg-3 col-sm-3 col-md-2">Input Style</div>
                                        <div class="col-lg-5 col-sm-9 col-md-10">
                                            <asp:RadioButtonList ID="rbl" runat="server" RepeatDirection="Horizontal" CssClass="rbl" >
                                                <asp:ListItem Value="cluster" Text="Per Cluster" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="project" Text="Per Project"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    
                                    
                                    <div class="row" style="margin: 30px 2px">
                                        <asp:GridView ID="gvSetupTarget" EmptyDataText="No Data Available" CssClass="footable" runat="server" EnableViewState="true"
                                            AutoGenerateColumns="false" OnDataBound="gvSetupTarget_OnDataBound" Width="3500px" AllowPaging="true" PageSize="20">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Project" HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtFilterProject" runat="server" CssClass="form-control" OnTextChanged="txtFilterProject_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </HeaderTemplate>
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProjectCode" runat="server" Text='<%# Bind("ProjectCode") %>'></asp:Label> - 
                                                        <asp:Label ID="lblProject" runat="server" Font-Bold="true" Text='<%# Bind("ProjectName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cluster" HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtFilterCluster" runat="server" CssClass="form-control" OnTextChanged="txtFilterCluster_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClusterCode" runat="server" Text='<%# Bind("clusterCode") %>'></asp:Label> - 
                                                        <asp:Label ID="lblCluster" runat="server" Font-Bold="true" Text='<%# Bind("clusterName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>

                                                <%--Start January--%>
                                                <asp:TemplateField HeaderText="MKT" HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetJanMKT" runat="server" Text='<%# Bind("JanTargetAmtMKT") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Corp." HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetJanCorp" runat="server" Text='<%# Bind("JanTargetAmtCorp") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <%--End January--%>
                                            
                                                <%--Start February--%>
                                                <asp:TemplateField HeaderText="MKT" HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetFebMKT" runat="server" Text='<%# Bind("FebTargetAmtMKT") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Corp." HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetFebCorp" runat="server" Text='<%# Bind("FebTargetAmtCorp") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <%--End February--%>
                                            
                                                <%--Start March--%>
                                                <asp:TemplateField HeaderText="MKT" HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetMarMKT" runat="server" Text='<%# Bind("MarTargetAmtMKT") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Corp." HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetMarCorp" runat="server" Text='<%# Bind("MarTargetAmtCorp") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <%--End March--%>
                                            
                                                <%--Start April--%>
                                                <asp:TemplateField HeaderText="MKT" HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetAprMKT" runat="server" Text='<%# Bind("AprTargetAmtMKT") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Corp." HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetAprCorp" runat="server" Text='<%# Bind("AprTargetAmtCorp") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <%--End April--%>
                                            
                                                <%--Start May--%>
                                                <asp:TemplateField HeaderText="MKT" HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetMayMKT" runat="server" Text='<%# Bind("MayTargetAmtMKT") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Corp." HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetMayCorp" runat="server" Text='<%# Bind("MayTargetAmtCorp") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <%--End May--%>
                                            
                                                <%--Start June--%>
                                                <asp:TemplateField HeaderText="MKT" HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetJunMKT" runat="server" Text='<%# Bind("JunTargetAmtMKT") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Corp." HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetJunCorp" runat="server" Text='<%# Bind("JunTargetAmtCorp") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <%--End June--%>

                                                <%--Start July--%>
                                                <asp:TemplateField HeaderText="MKT" HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetJulMKT" runat="server" Text='<%# Bind("JulTargetAmtMKT") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Corp." HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetJulCorp" runat="server" Text='<%# Bind("JulTargetAmtCorp") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <%--End July--%>
                                            
                                                <%--Start August--%>
                                                <asp:TemplateField HeaderText="MKT" HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetAugMKT" runat="server" Text='<%# Bind("AugTargetAmtMKT") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Corp." HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetAugCorp" runat="server" Text='<%# Bind("AugTargetAmtCorp") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <%--End August--%>
                                            
                                                <%--Start September--%>
                                                <asp:TemplateField HeaderText="MKT" HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetSepMKT" runat="server" Text='<%# Bind("SepTargetAmtMKT") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Corp." HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetSepCorp" runat="server" Text='<%# Bind("SepTargetAmtCorp") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <%--End September--%>
                                            
                                                <%--Start October--%>
                                                <asp:TemplateField HeaderText="MKT" HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetOctMKT" runat="server" Text='<%# Bind("OctTargetAmtMKT") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Corp." HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetOctCorp" runat="server" Text='<%# Bind("OctTargetAmtCorp") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <%--End October--%>
                                            
                                                <%--Start November--%>
                                                <asp:TemplateField HeaderText="MKT" HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetNovMKT" runat="server" Text='<%# Bind("NovTargetAmtMKT") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Corp." HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetNovCorp" runat="server" Text='<%# Bind("NovTargetAmtCorp") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <%--End November--%>

                                                <%--Start December--%>
                                                <asp:TemplateField HeaderText="MKT" HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetDecMKT" runat="server" Text='<%# Bind("DecTargetAmtMKT") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Corp." HeaderStyle-CssClass="centerAlign fontSize12" ItemStyle-CssClass="centerAlign fontSize12">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetDecCorp" runat="server" Text='<%# Bind("DecTargetAmtCorp") %>' onkeypress="return isNumberKey(this);" CssClass="form-control rightAlign fontSize12" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100"/>
                                                </asp:TemplateField>
                                                <%--End December--%>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

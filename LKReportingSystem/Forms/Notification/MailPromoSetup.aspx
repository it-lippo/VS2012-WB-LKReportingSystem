<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MailPromoSetup.aspx.cs" Inherits="LKReportingSystem.Forms.MailPromo.MailPromoSetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Component/fileupload/css/fileinput.css") %>">
    <script src="<%= Page.ResolveClientUrl("~/Component/fileupload/js/fileinput.js") %>" type="text/javascript"></script>

    <script>
        function pageLoad() {
            $(".datepicker").datetimepicker({
                format: 'DD/MM/YYYY',
                locale: 'en',
            });

            $(".uploadfile1").fileinput({
                allowedFileExtensions: ['jpg', 'png', 'gif'],
                overwriteInitial: false
            });

            $(".uploadfile2").fileinput({
                allowedFileExtensions: ['jpg', 'png', 'gif'],
                overwriteInitial: false
            });

            $(".uploadfile3").fileinput({
                allowedFileExtensions: ['jpg', 'png', 'gif'],
                overwriteInitial: false
            });
        }

        function SetImage(url1, url2, url3) {
            
            $("#dvPreview").show();
            $("#dvPreview").append("<img style='width: 250px;' src='" + url1 + "'/>");

            $("#dvPreview2").show();
            $("#dvPreview2").append("<img style='width: 250px;' src='" + url2 + "'/>");


            $("#dvPreview3").show();
            $("#dvPreview3").append("<img style='width: 250px;' src='" + url3 + "'/>");

        }

        function DisplayImage1() {
            $("#dvPreview").html("");
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
            if (regex.test($("[id$='fuImage1']").val().toLowerCase())) {
                    if (typeof (FileReader) != "undefined") {
                        $("#dvPreview").show();
                        $("#dvPreview").append("<img style='width: 250px;'/>");
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            $("#dvPreview img").attr("src", e.target.result);
                        }
                        reader.readAsDataURL($("[id$='fuImage1']")[0].files[0]);
                    } else {
                        alert("This browser does not support FileReader.");
                    }
            } else {
                alert("Please upload a valid image file.");
            }

        }

        function DisplayImage2() {
            $("#dvPreview2").html("");
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
            if (regex.test($("[id$='fuImage2']").val().toLowerCase())) {
                if (typeof (FileReader) != "undefined") {
                    $("#dvPreview2").show();
                    $("#dvPreview2").append("<img style='width: 250px;'/>");
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $("#dvPreview2 img").attr("src", e.target.result);
                    }
                    reader.readAsDataURL($("[id$='fuImage2']")[0].files[0]);
                } else {
                    alert("This browser does not support FileReader.");
                }
            } else {
                alert("Please upload a valid image file.");
            }
        }

        function DisplayImage3() {
            $("#dvPreview3").html("");
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
            if (regex.test($("[id$='fuImage3']").val().toLowerCase())) {
                if (typeof (FileReader) != "undefined") {
                    $("#dvPreview3").show();
                    $("#dvPreview3").append("<img style='width: 250px;'/>");
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $("#dvPreview3 img").attr("src", e.target.result);
                    }
                    reader.readAsDataURL($("[id$='fuImage3']")[0].files[0]);
                } else {
                    alert("This browser does not support FileReader.");
                }
            } else {
                alert("Please upload a valid image file.");
            }
        }


    </script>

    <div id="page-wrapper">
        <div class="container-fluid">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Setup <small>Mail Promo</small>
                    </h1>

                    <ol class="breadcrumb">
                        <li class="active">
                            <i class="fa fa-pencil"></i>&nbsp; Setup
                        </li>
                    </ol>
                </div>
            </div>

            
            <asp:UpdatePanel ID="updatePanelHtmlNotificationHistory" UpdateMode="Conditional" runat="server" class="col-md-6">
                <ContentTemplate>
                    <div><br /></div>

                    <div class="col-md-6" id="htmlNotificationHistory" runat="server"></div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div id="divContent" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="fa fa-newspaper-o fa-fw"></i>&nbsp;Promo 1</h3>
                            </div>
                            <div class="panel-body">
                                <div class="row martop5">
                                    <div class="col-lg-3 col-sm-3 col-md-2">Title&nbsp;<font style="color:red">*</font></div>
                                    <div class="col-lg-9 col-sm-9 col-md-10">
                                        <asp:TextBox ID="txtTitlePromo1" runat="server" CssClass="form-control fontSize12 tbNarrowPadding"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row martop5">
                                    <div class="col-lg-3 col-sm-3 col-md-2">URL</div>
                                    <div class="col-lg-9 col-sm-9 col-md-10">
                                        <asp:TextBox ID="txtURLPromo1" runat="server" CssClass="form-control fontSize12 tbNarrowPadding"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row martop5">
                                    <div class="col-lg-3 col-sm-3 col-md-2">Image&nbsp;<font style="color:red">*</font></div>
                                    <div class="col-lg-9 col-sm-9 col-md-10">
                                        <asp:HiddenField ID="hfImage1" runat="server" />
                                        <asp:HiddenField ID="hfArchiveImage1" runat="server" />
                                        <div id="dvPreview"></div>
                                        <div class="martop5"></div>
                                        <asp:FileUpload id="fuImage1" runat="server" class="file uploadfile btn btn-info" onchange="DisplayImage1();" type="file" data-min-file-count="1"/>
                                    </div>
                                </div>
                                <div class="row martop5">
                                    <div class="col-lg-3 col-sm-3 col-md-2">Start Date&nbsp;<font style="color:red">*</font></div>
                                    <div class="col-lg-9 col-sm-9 col-md-10">
                                        <asp:TextBox ID="txtStartDate1" runat="server" CssClass="form-control datepicker" MaxLength="11"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row martop5">
                                    <div class="col-lg-3 col-sm-3 col-md-2">End Date&nbsp;<font style="color:red">*</font></div>
                                    <div class="col-lg-9 col-sm-9 col-md-10">
                                        <asp:TextBox ID="txtEndDate1" runat="server" CssClass="form-control datepicker" MaxLength="11"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="fa fa-newspaper-o fa-fw"></i>&nbsp;Promo 2</h3>
                            </div>
                            <div class="panel-body">
                                <div class="row martop5">
                                    <div class="col-lg-3 col-sm-3 col-md-2">Title&nbsp;<font style="color:red">*</font></div>
                                    <div class="col-lg-9 col-sm-9 col-md-10">
                                        <asp:TextBox ID="txtTitlePromo2" runat="server" CssClass="form-control fontSize12 tbNarrowPadding"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row martop5">
                                    <div class="col-lg-3 col-sm-3 col-md-2">URL</div>
                                    <div class="col-lg-9 col-sm-9 col-md-10">
                                        <asp:TextBox ID="txtURLPromo2" runat="server" CssClass="form-control fontSize12 tbNarrowPadding"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row martop5">
                                    <div class="col-lg-3 col-sm-3 col-md-2">Image&nbsp;<font style="color:red">*</font></div>
                                    <div class="col-lg-9 col-sm-9 col-md-10">
                                        <asp:HiddenField ID="hfImage2" runat="server" />
                                        <asp:HiddenField ID="hfArchiveImage2" runat="server" />
                                        <div id="dvPreview2"></div>
                                        <div class="martop5"></div>
                                        <asp:FileUpload id="fuImage2" runat="server" class="file uploadfile btn btn-info" onchange="DisplayImage2();" type="file" data-min-file-count="1"/>
                                    </div>
                                </div>
                                <div class="row martop5">
                                    <div class="col-lg-3 col-sm-3 col-md-2">Start Date&nbsp;<font style="color:red">*</font></div>
                                    <div class="col-lg-9 col-sm-9 col-md-10">
                                        <asp:TextBox ID="txtStartDate2" runat="server" CssClass="form-control datepicker" MaxLength="11"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row martop5">
                                    <div class="col-lg-3 col-sm-3 col-md-2">End Date&nbsp;<font style="color:red">*</font></div>
                                    <div class="col-lg-9 col-sm-9 col-md-10">
                                        <asp:TextBox ID="txtEndDate2" runat="server" CssClass="form-control datepicker" MaxLength="11"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="fa fa-newspaper-o fa-fw"></i>&nbsp;Promo 3</h3>
                            </div>
                            <div class="panel-body">
                                <div class="row martop5">
                                    <div class="col-lg-3 col-sm-3 col-md-2">Title&nbsp;<font style="color:red">*</font></div>
                                    <div class="col-lg-9 col-sm-9 col-md-10">
                                        <asp:TextBox ID="txtTitlePromo3" runat="server" CssClass="form-control fontSize12 tbNarrowPadding"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row martop5">
                                    <div class="col-lg-3 col-sm-3 col-md-2">URL</div>
                                    <div class="col-lg-9 col-sm-9 col-md-10">
                                        <asp:TextBox ID="txtURLPromo3" runat="server" CssClass="form-control fontSize12 tbNarrowPadding"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row martop5">
                                    <div class="col-lg-3 col-sm-3 col-md-2">Image&nbsp;<font style="color:red">*</font></div>
                                    <div class="col-lg-9 col-sm-9 col-md-10">
                                        <asp:HiddenField ID="hfImage3" runat="server" />
                                        <asp:HiddenField ID="hfArchiveImage3" runat="server" />
                                        <div id="dvPreview3"></div>
                                        <div class="martop5"></div>
                                        <asp:FileUpload id="fuImage3" runat="server" class="file uploadfile btn btn-info" onchange="DisplayImage3();" type="file" data-min-file-count="1"/>
                                    </div>
                                </div>
                                <div class="row martop5">
                                    <div class="col-lg-3 col-sm-3 col-md-2">Start Date&nbsp;<font style="color:red">*</font></div>
                                    <div class="col-lg-9 col-sm-9 col-md-10">
                                        <asp:TextBox ID="txtStartDate3" runat="server" CssClass="form-control datepicker" MaxLength="11"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row martop5">
                                    <div class="col-lg-3 col-sm-3 col-md-2">End Date&nbsp;<font style="color:red">*</font></div>
                                    <div class="col-lg-9 col-sm-9 col-md-10">
                                        <asp:TextBox ID="txtEndDate3" runat="server" CssClass="form-control datepicker" MaxLength="11"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" Width="100" CssClass="btn btn-primary rightAlign"/>
                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>

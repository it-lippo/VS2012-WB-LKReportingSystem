<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LKReportingSystem.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login Reporting System</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Style/Login.css") %>">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Component/bootstrap/css/bootstrap.min.css") %>">
    <script src="<%= Page.ResolveClientUrl("~/Component/jquery/jquery.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Scripts/modernizr.custom.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Component/bootstrap/js/bootstrap.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Scripts/Common.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Component/bootbox/bootbox.min.js") %>" type="text/javascript"></script>
    <script src="<%= Page.ResolveClientUrl("~/Component/jquery-ui/jquery-ui.min.js") %>" type="text/javascript"></script>
       
</head>
<body>
    
    <form id="form1" runat="server">
        <div class="containerLogin">
          <div id="login-form">
            <div class="divHeaderLogin">
                <img src="./Images/logo-lippo.png" height="30px" />
            </div>
            <fieldset>
                <p>Please enter your logon information :</p>

              <form action="javascript:void(0);" method="get">
                <asp:DropDownList ID="ddlDomain" runat="server" CssClass="dropdown" Width="100%" Height="40">
                    <asp:ListItem Value="karawacinet">KARAWACINET </asp:ListItem>
                    <asp:ListItem Value="lippo-cikarang">LIPPO-CIKARANG</asp:ListItem>
                </asp:DropDownList>

                <asp:TextBox required ID="txtUsername" runat="server" Width="100%" Height="40" placeholder="Username" style="color: #8E8D8D; border: 1px solid #eee; border-radius: 2px; padding: 5px" ></asp:TextBox>
                <asp:TextBox required ID="txtPassword" TextMode="Password" runat="server" Width="100%" Height="40" placeholder="Password" style="color: #8E8D8D; border: 1px solid #eee; border-radius: 2px; padding: 5px" ></asp:TextBox>
                
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-lg btn-block" OnClick="btnLogin_Click" />
                <%--<footer class="clearfix">
                    <p><span class="info">?</span><a href="#">Forgot Password</a></p>
                </footer>--%>
              </form>
            </fieldset>
          </div> 
        </div>
    </form>
</body>
</html>

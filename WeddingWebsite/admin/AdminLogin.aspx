<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="WeddingWebsite.AdminLogin" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>M&K's Wedding Site (ADMIN MODE)</title>
    <link href="../Stylesheets/LandingPage.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <div class="bg-image">
            <br />
        </div>
        <div class="bg-text">
            <p>
                User Name:
            <asp:TextBox runat="server" ID="txtUserName" CssClass="a-textbox"></asp:TextBox>
            </p>

            <p>
                Last Name:
            <asp:TextBox runat="server" ID="txtLastName" CssClass="a-textbox"></asp:TextBox>
            </p>

            <p>
                Password:
            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="a-textbox"></asp:TextBox>
            </p>
            <br />
            <asp:Button runat="server" ID="btnLogin" Text="Login" OnClick="btnLogin_Click" CssClass="a-button" />
        </div>

        <div class="footer" id="footer">
                <p>
                    Click
                        <asp:LinkButton ID="lbEnterGuest" runat="server" OnClick="lbEnterGuest_Click">Here</asp:LinkButton>
                    to view site as a Guest.
                </p>
            </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuestLogin.aspx.cs" Inherits="WeddingWebsite.GuestLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Micah and Kirsten's Wedding Site</title>
    <link href="Stylesheets/LandingPage.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="bg-image">
            <br />
        </div>
        <div class="bg-text">
            <h1><asp:Label runat="server" ID="lblWelcomeGuest"></asp:Label></h1>
            <h3>Enter Password <asp:TextBox runat="server" ID="txtPassword" CssClass="lp-textbox" TextMode="Password"></asp:TextBox></h3>
            <asp:Button runat="server" ID="btnLogin" Text="Login" CssClass="lp-button" OnClick="btnLogin_Click" />
        </div>

        <div class="footer">
            <p>Click
                <asp:LinkButton ID="lbEnterAdmin" runat="server" OnClick="lbEnterAdmin_Click">Here</asp:LinkButton>
                to login as an Administrator </p>
        </div>
    </form>
</body>
</html>

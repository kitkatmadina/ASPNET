<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="WeddingWebsite.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Looks like there's been a mistake</title>
    <link href="Stylesheets/ErrorPage.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="bg-image"></div>
        <div class="bg-text">
            <h3>Looks Like there's been a mistake...</h3>
            <div id="IncorrectGuestInfo" runat="server" visible="false">
                <h4>Incorrect Guest Information</h4>
                <p>Looks like you're not in our database. Please make sure you've entered your information correctly and try again. If you think there is a mistake, please contact Kirsten Madina or Anmol Raoofi.</p>
            </div>

            <div id="ErrorGuestLogin" runat="server" visible="false">
                <h4>Error in Guest Login</h4>
                <p>Sorry about that. Something went wrong on our end. Please send a screenshot of this error to Kirsten Madina or Anmol Raoofi. Thank you!</p>
            </div>

            <div id="GeneralError" runat="server" visible="false">
                <h4>General Error</h4>
                <p>Sorry about that. Something went wrong on our end. Please send a screenshot of this error to Kirsten Madina or Anmol Raoofi. Thank you!</p>
                <asp:Label runat="server" ID="lblGEMessage"></asp:Label>
            </div>

            <div id="ErrorAdminLogin" runat="server" visible="false">
                <h4>Error in Admin</h4>
                <p>Looks like something went wrong. Make sure you've entered your information correctly and try again. Contact Kirsten Madina if you think this may be a mistake, or if you need to create an Administrator Profile.</p>
                <asp:Label runat="server" ID="lblEALMessage"></asp:Label>
            </div>

            <asp:Button ID="btnReturn" runat="server" Text="Return to Previous Page" OnClick="btnReturn_Click" CssClass="ep-button" />
        </div>

    </form>
</body>
</html>

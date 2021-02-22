<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Landing.aspx.cs" Inherits="WeddingWebsite.Landing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Micah and Kirsten's Wedding Site</title>
    <script src="Scripts/Countdown.js"></script>
    <link href="Stylesheets/LandingPage.css" rel="stylesheet" />
    <script>
        // Get the modal
        var modal = document.getElementById('AdminLogin');

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="bg-image">
            <br />
        </div>
        <div class="bg-text">
            <div id="Welcome">
                <asp:Label ID="lblWelcome" runat="server" CssClass="lp-label"> 

                    <h2>Welcome to the Official Website for the Wedding of </h2>
                    <p> Micah James Earl Erickson </p> 
                    &
                    <p> Kirsten Sofia Madina </p> 

                </asp:Label>
            </div>

            <br />


            <div id="Countdown">
                <asp:Label ID="lblCountdown" runat="server" CssClass="lp-label">Days Until "I Do" ...</asp:Label>
                <p id="WeddingCountdown"></p>
            </div>


            <div id="Enter">

                <asp:Label ID="lblEnterName" runat="server" CssClass="lp-label"> Please Enter Your First and Last Name to Enter Site :</asp:Label>
                <br />
                <div>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="lp-textbox" onfocus="this.value=''" Text="First Name"></asp:TextBox>

                    <asp:TextBox ID="txtLastName" runat="server" CssClass="lp-textbox" onfocus="this.value=''" Text="Last Name"></asp:TextBox>
                </div>

                <asp:Button ID="btnEnterGuest" runat="server" Text="Enter Website" CssClass="lp-button" OnClick="btnEnterGuest_Click" />

            </div>

        </div>

        <div class="footer">
            <p>Click
                <asp:LinkButton ID="lbEnterAdmin" runat="server" OnClick="lbEnterAdmin_Click">Here</asp:LinkButton>
                to login as an Administrator </p>
        </div>
    </form>
</body>
</html>

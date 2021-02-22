<%@ Page Title="" Language="C#" MasterPageFile="~/guest/Guest.Master" AutoEventWireup="true" CodeBehind="GuestNewPassword.aspx.cs" Inherits="WeddingWebsite.guest.GuestNewPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-main">
        <h3>Set Up Password</h3>
        <asp:Table runat="server" ID="tabPassword">
            <asp:TableRow>
                <asp:TableCell>
                    Enter New Password:
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtPassword1" CssClass="g-textbox2" TextMode="Password"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    Re-enter New Password:
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtPassword2" CssClass="g-textbox2" TextMode="Password"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button runat="server" ID="btnSubmit" CssClass="g-button" OnClick="btnSubmit_Click"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>

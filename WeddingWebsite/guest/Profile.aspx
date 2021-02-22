<%@ Page Title="" Language="C#" MasterPageFile="Guest.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WeddingWebsite.guest.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Your Profile<br /> <img src="../Images/LineDecorations1.PNG" /></h1>
    <div class="content-main">       
    <div class="row">
        <div class="column">
            <div class="card" id="Contact Info">
                <h3> Contact Info </h3>
                <div style="align-content:center; text-align:center">
                    <asp:Table runat="server" ID="tabContactInfo" CssClass="table" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell>
                            Email:
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="3">
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="g-textbox2"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Phone: 
                        </asp:TableCell>
                        <asp:TableCell>
                            (<asp:TextBox runat="server" ID="txtPhone1" CssClass="g-textbox2" Width="25%"></asp:TextBox>) 
                        </asp:TableCell>
                        <asp:TableCell>
                            <p><asp:TextBox runat="server" ID="txtPhone2" CssClass="g-textbox2" Width="25%"></asp:TextBox>-</p>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox runat="server" ID="txtPhone3" CssClass="g-textbox2" Width="40%"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Street Address:
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="3">
                            <asp:TextBox runat="server" ID="txtStreetAddress" CssClass="g-textbox2"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            City:
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="3">
                            <asp:TextBox runat="server" ID="txtCity" CssClass="g-textbox2"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            State:
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="3">
                            <asp:TextBox runat="server" ID="txtState" CssClass="g-textbox2"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Zip Code:
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="3">
                            <asp:TextBox runat="server" ID="txtZipCode" CssClass="g-textbox2"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="3">
                            <asp:Button ID="btnSaveContact" runat="server" OnClick="btnSaveContact_Click" Text="Save Contact Info" CssClass="g-button" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                </div>
                
                
            </div>
            

        </div>
        <div class="column">
            <div class="card" id="RSVPInfo">
                <h3>Your RSVP Info</h3>
            </div>
        </div>

        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="Guest.Master" AutoEventWireup="true" CodeBehind="GuestHome.aspx.cs" Inherits="WeddingWebsite.GuestHome1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
         <asp:Label ID="lblWelcomeGuest" runat="server"></asp:Label> <br /> <img src="../Images/LineDecorations1.PNG" /></h1>

    <div class="content-main">

        <%-- Password --%>
        <div id="newPassword" runat="server" visible="false" class="content-sub">
            <h3>First Things first...</h3>
            <p>Our records show that you have not set a password yet.</p>
            <asp:Button ID="btnNewPassword" runat="server" OnClick="btnNewPassword_Click" Text="Add Password" CssClass="g-button" />
        </div>

        <%-- Contact Info --%>
        <div id="contactinfo" runat="server" class="content-sub">
            <h3>Contact Information</h3>
            <h4>
                <asp:Label ID="lblContactMessage" runat="server"></asp:Label>
                You can edit your contact information in your profile.</h4>
            <asp:Table runat="server" HorizontalAlign="Center">
                <asp:TableRow>
                    <asp:TableCell>
                        Email:
                    </asp:TableCell>
                    <asp:TableCell>
                        <%=Email %>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        Phone:
                    </asp:TableCell>
                    <asp:TableCell>
                        <%=Phone %>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        Street Address:
                    </asp:TableCell>
                    <asp:TableCell>
                        <%=Address %>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        City:
                    </asp:TableCell>
                    <asp:TableCell>
                        <%=City %>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        State:
                    </asp:TableCell>
                    <asp:TableCell>
                        <%=State %>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        ZipCode:
                    </asp:TableCell>
                    <asp:TableCell>
                        <%=ZipCode %>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>


        </div>

        <%-- RSVP Info --%>
        <div id="RSVPInfo">
            <h3>RSVP Information</h3>

            <%-- General RSVP Info --%>
            <%if (RSVP == "True")
                { %>
            <h4>Looks like you have RSVP'd "Yes"!</h4>
            <p>We look forward to seeing you there!</p>
            <%}
                else if (RSVP == "False")
                {
            %>
            <h4>Looks like you've RSVP's "No".</h4>
            <p>We're sorry you can't make it :(</p>

            <%}
                else
                {%>
            <h4>It doesn't look like you have RSVP'd just yet.</h4>

            <%} %>

            <p style="font-size: smaller">You can change your RSVP status in your profile</p>


            <%-- Party RSVP Info --%>
            <%if (HasParty == "True")
                { %>
                <h4>You're part of a party! View those details here:</h4>
                <asp:GridView runat="server" ID="gvParty" AutoGenerateColumns="false" HorizontalAlign="Center" BackColor="White" ForeColor="Black">
                    <Columns>
                       <asp:TemplateField HeaderText="Name">
                           <ItemTemplate>
                               <asp:Label ID="lblName" runat="server" Text='<%#Eval("FirstName") + " " + Eval("LastName")%> '></asp:Label>
                           </ItemTemplate>                          
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="RSVP">
                           <ItemTemplate>
                               <asp:Label ID="lblRSVP" runat="server" Text='<%# String.IsNullOrEmpty(Eval("RSVP").ToString()) ? "No Response" : Eval("RSVP").ToString().Replace("True", "Yes").Replace("False", "No")%>'></asp:Label>
                           </ItemTemplate>                          
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="Plus One?">
                           <ItemTemplate>
                               <asp:Label ID="lblHasPlusOne" runat="server" Text='<%# Eval("HasPlusOne").ToString().Replace("True", "Yes").Replace("False", "No")%>'></asp:Label>
                           </ItemTemplate>                          
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="Plus One RSVP'd?">
                           <ItemTemplate>
                               <asp:Label ID="lbl" runat="server" Text='<%# String.IsNullOrEmpty(Eval("PlusOneRSVP").ToString()) ? "No Response" : Eval("PlusOneRSVP").ToString().Replace("True", "Yes").Replace("False", "No")%>'></asp:Label>
                           </ItemTemplate>                          
                       </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <p>You can edit the RSVP details of your party in your profile.</p>
            <%} %>
        </div>


    </div>

</asp:Content>

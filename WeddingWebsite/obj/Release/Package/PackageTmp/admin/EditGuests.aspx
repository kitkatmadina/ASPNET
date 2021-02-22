<%@ Page Title="" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeBehind="EditGuests.aspx.cs" Inherits="WeddingWebsite.admin.EditGuests" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Style the button that is used to open and close the collapsible content */
        .collapsible {
            background-color: #eee;
            color: #444;
            cursor: pointer;
            padding: 18px;
            width: 100%;
            border: none;
            text-align: left;
            outline: none;
            font-size: 15px;
        }

            /* Add a background color to the button if it is clicked on (add the .active class with JS), and when you move the mouse over it (hover) */
            .active, .collapsible:hover {
                background-color: #ccc;
            }

        /* Style the collapsible content. Note: hidden by default */
        .collapsible-content {
            padding: 0 18px;
            display: none;
            overflow: hidden;
            background-color: #f1f1f1;
        }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <%-- Parties --%>
        <div class="column-left">
            <div class="card-less-padding">
                <h2>Edit Parties</h2>
                <p>
                    Parties connect guests who are in the same family together.

                <asp:Table runat="server" ID="tabAddNewParty">
                    <asp:TableRow>
                        <asp:TableCell>
                            Party Name:
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox runat="server" ID="txtPartyName" CssClass="a-textbox2"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button runat="server" ID="btnAddParty" CssClass="a-button" Text="Add Party" OnClick="btnAddParty_Click" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                </p>
                <asp:Table runat="server" ID="tabPartyGV">
                    <asp:TableRow>
                        <asp:TableCell>
                            Sort by: 
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList runat="server" ID="ddlPartySortyBy" OnSelectedIndexChanged="ddlPartySortyBy_SelectedIndexChanged" AutoPostBack="true" CssClass="a-textbox2">
                                <asp:ListItem Selected="True" Text="A->Z" Value="PartyName asc"></asp:ListItem>
                                <asp:ListItem Text="Z->A" Value="PartyName desc"></asp:ListItem>
                                <asp:ListItem Text="Order Entered" Value="PartyID asc"></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button runat="server" ID="btnEnterPartyEditMode" Text="Edit Table" OnClick="btnEnterPartyEditMode_Click" CssClass="a-button" />
                            <asp:Button runat="server" ID="btnUpdatePartyEditMode" Text="Save Changes" OnClick="btnExitPartyEditMode_Click" Visible="false" CssClass="a-button" />

                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button runat="server" ID="btnCancelPartyEditMode" Text="Cancel" OnClick="btnExitPartyEditMode_Click" Visible="false" CssClass="a-button" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="4">
                            <asp:GridView runat="server" ID="gvParties" DatyKeyNames="PartyID" AutoGenerateColumns="false" OnRowDeleting="gvParties_RowDeleting" Width="100%">
                                <Columns>

                                    <asp:TemplateField HeaderText="Party Name">
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hidPartyID" Value='<%# Eval("PartyID") %>' />
                                            <asp:HiddenField runat="server" ID="hidPartyName" Value='<%# Eval("PartyName") %>' />
                                            <asp:Label runat="server" ID="lblPartyName" Visible='<%# !IsPartyEditMode %>' Text='<%# Eval("PartyName") %>'></asp:Label>
                                            <asp:TextBox runat="server" ID="txtPartyName" Visible='<%# IsPartyEditMode %>' Text='<%# Eval("PartyName") %>' OnTextChanged="gvPartyMultiRow_RowChanged"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <HeaderTemplate>
                                            Select All
                                            <br />
                                            <asp:CheckBox runat="server" ID="cbPartyCheckAll" OnCheckedChanged="cbPartyCheckAll_CheckedChanged" AutoPostBack="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="cbDelete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField DeleteText="Delete" ShowDeleteButton="true" HeaderText="Delete" ShowHeader="true" />
                                </Columns>
                            </asp:GridView>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">

                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button runat="server" ID="btnDeleteSelected" Text="Delete Selected" OnClick="btnDeleteSelected_Click" Visible="false" CssClass="a-button" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>


            </div>
        </div>
        <div class="column-right">
        <div class="card-less-padding">
                <h2>Edit Guests</h2>
                <button type="button" class="collapsible">Add New Guest</button>
                <div class="collapsible-content">
                    <div style="overflow-x:auto">
                        <asp:Table runat="server" ID="tabAddGuests">
                        <asp:TableRow>
                            <asp:TableCell>
                            First Name: 
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox runat="server" ID="txtFirstName" CssClass="a-textbox2"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                            Last Name: 
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox runat="server" ID="txtLastName" CssClass="a-textbox2"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                            Party: 
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList runat="server" ID="ddlParty" CssClass="a-textbox2"></asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell>
                           Has Plus One?
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:CheckBox runat="server" ID="cbPlusOne"></asp:CheckBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                           Street Address
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox runat="server" ID="txtAddress" CssClass="a-textbox2"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                            City:
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox runat="server" ID="txtCity" CssClass="a-textbox2"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                            State:
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox runat="server" ID="txtState" CssClass="a-textbox2"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                            ZipCode: 
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox runat="server" ID="txtZipCode" CssClass="a-textbox2"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                            Email:
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox runat="server" ID="txtEmail" CssClass="a-textbox2"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                            Phone:
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox runat="server" ID="txtPhone" CssClass="a-textbox2"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>
                            Notes: 
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox runat="server" ID="txtNotes" CssClass="a-textbox2"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="2">
                                <asp:Button runat="server" ID="btnAddGuest" Text="Add Guest" OnClick="btnAddGuest_Click" CssClass="a-button" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                <asp:FileUpload runat="server" ID="FileUpload1" />
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="2">
                                <asp:Button runat="server" ID="btnBulkAdd" OnClick="btnBulkAdd_Click" Text="Import Guest List From Excel" CssClass="a-button" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    </div>
                    
                </div>
            <div style="overflow-x:auto">
                <asp:Table runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            Search Guests:
                            <asp:TextBox runat="server" ID="txtFilterName" CssClass="a-textbox2" OnTextChanged="txtFilterName_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            Sort By:
                            <asp:DropDownList runat="server" ID="ddlSortGuests" OnSelectedIndexChanged="ddlSortGuests_SelectedIndexChanged" AutoPostBack="true" CssClass="a-textbox2">
                                <asp:ListItem Text="Last Name A->Z" Value="LastName asc, FirstName asc" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Last Name Z->A" Value="LastName desc, FirstName asc"></asp:ListItem>
                                <asp:ListItem Text="First Name A->Z" Value="FirstName asc, LastName asc"></asp:ListItem>
                                <asp:ListItem Text="First Name Z->A" Value="FirstName desc, LastName asc"></asp:ListItem>
                                <asp:ListItem Text="Party Name" Value="PartyName asc"></asp:ListItem>
                                <asp:ListItem Text="RSVP" Value="RSVP"></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button runat="server" ID="btnGuestEditMode" OnClick="btnGuestEditMode_Click" CssClass="a-button" Text="Edit Guests" />
                            <asp:Button runat="server" ID="btnGuestUpdate" OnClick="btnGuestUpdate_Click" CssClass="a-button" Text="Update" Visible="false" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button runat="server" ID="btnGuestEditCancel" OnClick="btnGuestUpdate_Click" CssClass="a-button" Text="Cancel" Visible="false" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button runat="server" ID="btnDeleteGuests" OnClick="btnDeleteGuests_Click" CssClass="a-button" Text="Delete Selected" Visible="false" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="5">
                            <asp:GridView runat="server" ID="gvGuests" AutoGenerateColumns="false">
                                <Columns>
                                    <%-- First Name --%>
                                    <asp:TemplateField HeaderText="First Name">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hidGuestID" runat="server" Value='<%# Eval("GuestID") %>' />
                                            <asp:HiddenField ID="hidFirstName" runat="server" Value='<%# Eval("FirstName") %>' />
                                            <asp:HiddenField ID="hidLastName" runat="server" Value='<%# Eval("LastName") %>' />
                                            <asp:Label ID="lblFirstName" runat="server" Text='<%#Eval("FirstName") %>' Visible='<%#!IsGuestEditMode %>'></asp:Label>
                                            <asp:TextBox ID="txtFirstName" runat="server" Text='<%#Eval("FirstName") %>' Visible='<%# IsGuestEditMode %>' CssClass="a-textbox-gv-small"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <%-- Last Name --%>
                                    <asp:TemplateField HeaderText="LastName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLastName" runat="server" Text='<%#Eval("LastName") %>' Visible='<%#!IsGuestEditMode %>'></asp:Label>
                                            <asp:TextBox ID="txtLastName" runat="server" Text='<%#Eval("LastName") %>' Visible='<%# IsGuestEditMode %>' CssClass="a-textbox-gv-small"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <%-- PartyName --%>
                                    <asp:TemplateField HeaderText="PartyName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyName" runat="server" Text='<%#Eval("PartyName") %>' Visible='<%#!IsGuestEditMode %>'></asp:Label>
                                            <asp:DropDownList ID="ddlPartyName" runat="server" Visible='<%# IsGuestEditMode %>' CssClass="a-textbox-gv-small"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <%-- RSVP --%>
                                    <asp:TemplateField HeaderText="RSVP?">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRSVP" runat="server" Text='<%# Eval("RSVP") %>' Visible='<%#!IsGuestEditMode %>'></asp:Label>
                                            <asp:DropDownList ID="ddlRSVP" runat="server" Visible='<%# IsGuestEditMode %>' CssClass="a-textbox-gv-small">
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Undecided" Value="NULL"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <%-- Has Plus One --%>
                                    <asp:TemplateField HeaderText="Has Plus One?">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlusOne" runat="server" Text='<%# Eval("HasPlusOne") %>' Visible='<%#!IsGuestEditMode %>'></asp:Label>
                                            <asp:DropDownList ID="ddlPlusOne" runat="server" Visible='<%# IsGuestEditMode %> ' CssClass="a-textbox-gv-small">
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%-- Plus One RSVP--%>
                                    <asp:TemplateField HeaderText="Plus One RSVP?">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlusOneRSVP" runat="server" Text='<%# Eval("RSVP") %>' Visible='<%#!IsGuestEditMode %>'></asp:Label>
                                            <asp:DropDownList ID="ddlPlusOneRSVP" runat="server" Visible='<%# IsGuestEditMode %>' CssClass="a-textbox-gv-small">
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Undecided" Value="NULL"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%-- Email --%>
                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>' Visible='<%#!IsGuestEditMode %>'></asp:Label>
                                            <asp:TextBox ID="txtEmail" runat="server" Text='<%#Eval("Email") %>' Visible='<%# IsGuestEditMode %>' CssClass="a-textbox-gv-large"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <%-- Phone  --%>
                                    <asp:TemplateField HeaderText="Phone">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPhone" runat="server" Text='<%#Eval("Phone") %>' Visible='<%#!IsGuestEditMode %>'></asp:Label>
                                            <asp:TextBox ID="txtPhone" runat="server" Text='<%#Eval("Phone") %>' Visible='<%# IsGuestEditMode %>' CssClass="a-textbox-gv-small"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <%-- Address --%>
                                    <asp:TemplateField HeaderText="Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("StreetAddress") %>' Visible='<%#!IsGuestEditMode %>'></asp:Label>
                                            <asp:TextBox ID="txtAddress" runat="server" Text='<%#Eval("StreetAddress") %>' Visible='<%# IsGuestEditMode %>' CssClass="a-textbox-gv-large"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <%-- City --%>
                                    <asp:TemplateField HeaderText="City">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCity" runat="server" Text='<%#Eval("City") %>' Visible='<%#!IsGuestEditMode %>'></asp:Label>
                                            <asp:TextBox ID="txtCity" runat="server" Text='<%#Eval("City") %>' Visible='<%# IsGuestEditMode %>' CssClass="a-textbox-gv-small"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <%-- State --%>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" runat="server" Text='<%#Eval("State") %>' Visible='<%#!IsGuestEditMode %>'></asp:Label>
                                            <asp:TextBox ID="txtState" runat="server" Text='<%#Eval("State") %>' Visible='<%# IsGuestEditMode %>' CssClass="a-textbox-gv-small"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <%-- ZipCode --%>
                                    <asp:TemplateField HeaderText="Zip Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblZipCode" runat="server" Text='<%#Eval("ZipCode") %>' Visible='<%#!IsGuestEditMode %>'></asp:Label>
                                            <asp:TextBox ID="txtZipCode" runat="server" Text='<%#Eval("ZipCode") %>' Visible='<%# IsGuestEditMode %>' CssClass="a-textbox-gv-small"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <%-- Notes --%>
                                    <asp:TemplateField HeaderText="Notes">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNotes" runat="server" Text='<%#Eval("Notes") %>' Visible='<%#!IsGuestEditMode %>'></asp:Label>
                                            <asp:TextBox ID="txtNotes" runat="server" Text='<%#Eval("Notes") %>' Visible='<%# IsGuestEditMode %>' CssClass="a-textbox-gv-small"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%-- Delete --%>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <HeaderTemplate>
                                            Select All
                                            <br />
                                            <asp:CheckBox runat="server" ID="cbGuestCheckAll" OnCheckedChanged="cbGuestCheckAll_CheckedChanged" AutoPostBack="true" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="cbDeleteGuests" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
                

            </div>
        </div>
        
    </div>
    <script>
        var coll = document.getElementsByClassName("collapsible");
        var i;

        for (i = 0; i < coll.length; i++) {
            coll[i].addEventListener("click", function () {
                this.classList.toggle("active");
                var content = this.nextElementSibling;
                if (content.style.display === "block") {
                    content.style.display = "none";
                } else {
                    content.style.display = "block";
                }
            });
        }
    </script>
</asp:Content>

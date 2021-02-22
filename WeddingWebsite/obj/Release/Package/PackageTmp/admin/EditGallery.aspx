<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.Master" AutoEventWireup="true" CodeBehind="EditGallery.aspx.cs" Inherits="WeddingWebsite.admin.EditGallery" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Edit Gallery</h1>


    <div class="row">
        <div class="column-left">
            <div class="card">
                <asp:Table runat="server" ID="tabUpload" CssClass="table">
                    <asp:TableRow>
                        <asp:TableCell>
                            Enter Image Title:
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox runat="server" ID="txtTitle" CssClass="a-textbox2"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            Enter Description:
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox runat="server" ID="txtDescription" CssClass="a-textbox2" TextMode="MultiLine"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <asp:FileUpload runat="server" ID="FileUpload" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <asp:Button runat="server" ID="btnUpload" Text="Upload Image" OnClick="btnUpload_Click" CssClass="a-button" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </div>
        <div class="column-right">
            <div class="card">
                <asp:GridView runat="server" ID="gvGalleryEdit" AutoGenerateColumns="false" DataKeyNames="ImageID,Image" OnRowDeleting="gvGalleryEdit_RowDeleting" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="ImgOrder" HeaderText="Order" />
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTitle" Text='<%# Eval("Title") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtTitle" Text='<%# Eval("Title") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDescription" Text='<%# Eval("Description") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtDescription" Text='<%# Eval("Description") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>                             
                                    <asp:Image runat="server" ID="imgGallery" ImageUrl='<%# ".."+Eval("Image") %>' Width="200px" Height="150px"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reorder">
                            <ItemStyle ForeColor="White" />
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbMoveUp" CommandName="MoveUp" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' OnCommand="lbMoveUp_Command" ForeColor="White"><i class="fa fa-arrow-circle-o-up fa-2x"></i></asp:LinkButton>
                                <br />
                                <asp:LinkButton runat="server" ID="lbMoveDown" CommandName="MoveDown" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'  OnCommand="lbMoveDown_Command" ForeColor="White"><i class="fa fa-arrow-circle-o-down fa-2x"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="Delete Image" DeleteText="Delete" ShowDeleteButton="true" ControlStyle-ForeColor="White" />
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </div>



</asp:Content>

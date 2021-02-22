<%@Page Language="C#" AutoEventWireup="true" CodeFile="GV_MultiRow_Manip.aspx.cs" Inherits="aspx_MultiRow_Manip" %>

<!DOCTYPE html>
<html>
<head>

</head>
<body>
    <form id="form1" runat="server">
        <asp:Table runat="server" ID="Table1">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button runat="server" ID="btnEdit" OnClick="btnEdit_OnClick" Text="Edit"></asp:Button>
                    <asp:Button runat="server" ID="btnUpdate" OnClick="btnUpdate_OnClick" Text="Update"></asp:Button>
                    <asp:Button runat="server" ID="btnCancel" OnClick="btnCancel_OnClick" Text="Cancel"></asp:Button>
                    <asp:Button runat="server" ID="btnDelete" OnClick="btnDelete_OnClick" Text="Delete"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblID" Text='<%# Eval("ID") %>' Visible='<% !IsEditMode %>'></asp:Label>
                                    <asp:TextBox runat="server" ID="txtID" Text='<%# Eval("ID") %>' Visible='<% IsEditMode %>' OnTextChanged="multiRowEdit_RowChanged" AutoPostBack="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbArchiveRow" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:Label runat="server" Text="Select All"></asp:Label>
                                    <br>
                                    <asp:CheckBox runat="server" ID="cbArchiveAll" OnCheckedChanged="cbArchiveAll_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                </HeaderTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

    </form>


</body>


</html>
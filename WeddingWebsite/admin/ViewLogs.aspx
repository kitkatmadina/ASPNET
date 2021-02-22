<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.Master" AutoEventWireup="true" CodeBehind="ViewLogs.aspx.cs" Inherits="WeddingWebsite.admin.ViewLogs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView runat="server" Width="100%" ID="gvLog" AutoGenerateColumns="true" ShowHeader="true"></asp:GridView>
</asp:Content>

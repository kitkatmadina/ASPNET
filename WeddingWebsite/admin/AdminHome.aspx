<%@ Page Title="" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeBehind="AdminHome.aspx.cs" Inherits="WeddingWebsite.AdminHome" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1> <asp:Label runat="server" ID="lblWelcomeAdmin"></asp:Label></h1>

    <div class="row">
        <div class="column">
            <div class="card" id="Statistics">
                <h4>Current Wedding Statistics</h4>
                <asp:GridView runat="server" ID="gvStatistics" AutoGenerateColumns="false" ShowHeader="false" Width="100%" BackColor="Gray" ForeColor="Black">
                    <Columns>
                        <%--<asp:BoundField DataField="Title" ItemStyle-Font-Bold="true" ReadOnly="true" />--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblTitle" Text='<%#Eval("Title") %>' Font-Bold='<%# Int16.Equals(1, Eval("Bold")) %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Stat" ReadOnly="true"/>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="card" id="EventLog">
                <h4>Log Preview</h4>
                <asp:LinkButton runat="server" ID="lbViewLogs" Font-Underline="false" ForeColor="Black" OnClick="lbViewLogs_Click" Text="Click to view log full log."></asp:LinkButton>
                <asp:GridView runat="server" ID="gvLog" AutoGenerateColumns="true" ShowHeader="true" Width="100%" BackColor="Gray" ForeColor="Black">
                </asp:GridView>
            </div>
        </div>
        <div class="column">
            <div class="card" id="ToDoList">
                <h4>To Do List</h4>
                <p><asp:TextBox runat="server" ID="txtNewTask" CssClass="a-textbox2"></asp:TextBox>  Priority:
                    <asp:DropDownList runat="server" ID="ddlPriority" CssClass="a-textbox2">
                        <asp:ListItem Text="Low" Value ="0"></asp:ListItem>
                        <asp:ListItem Text="Medium" Value ="1"></asp:ListItem>
                        <asp:ListItem Text="High" Value ="2"></asp:ListItem>
                        <asp:ListItem Text="Extreme" Value ="3"></asp:ListItem>
                    </asp:DropDownList>
                        <asp:Button runat="server" ID="btnNewTask" OnClick="btnNewTask_Click" CssClass="a-button" Text ="Add Task" /></p>
                <asp:GridView runat="server" ID="gvToDoList" AutoGenerateColumns="false" ShowHeader="true" Width="100%" DataKeyNames="ItemID" BackColor="Gray" ForeColor="Black">
                    <Columns>
                        <asp:TemplateField HeaderText="Mark Done">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="cbMarkDone" OnCheckedChanged="cbMarkDone_CheckedChanged" AutoPostBack="true"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ItemText" ReadOnly="true" HeaderText="Task" />
                        <asp:BoundField DataField="Priority" ReadOnly="true" HeaderText="Priority" />
                        <asp:BoundField DataField="DateAdded" ReadOnly="true" HeaderText="Date Added" />
                    </Columns>
                </asp:GridView>
                <p>Sort by: 
                    <asp:DropDownList runat="server" ID="ddlSortTasks" CssClass="a-textbox2" OnSelectedIndexChanged="ddlSortTasks_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="A->Z" Value="ItemText asc" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Z->A" Value="ItemText desc"></asp:ListItem>
                        <asp:ListItem Text="Priority" Value="Priority"></asp:ListItem>
                        <asp:ListItem Text="Date Added" Value="DateAdded"></asp:ListItem>
                    </asp:DropDownList>
                </p>
                <h4>Completed Tasks</h4>
                <asp:GridView runat="server" ID="gvCompleted" AutoGenerateColumns="false" ShowHeader="true" Width="100%" DataKeyNames="ItemID" BackColor="Gray" ForeColor="Black">
                    <Columns>
                        
                        <asp:BoundField DataField="ItemText" ReadOnly="true" HeaderText="Task" />
                        <asp:BoundField DataField="Priority" ReadOnly="true" HeaderText="Task" />
                        <asp:BoundField DataField="DateCompleted" ReadOnly="true" HeaderText="Task" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

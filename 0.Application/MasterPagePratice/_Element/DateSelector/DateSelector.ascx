<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DateSelector.ascx.cs" Inherits="DateSelector" %>
<asp:DropDownList ID="ddlYear" runat="server">
    <asp:ListItem Text="請選擇" Selected="True"></asp:ListItem>
</asp:DropDownList>
年
<asp:DropDownList ID="ddlMonth" runat="server" CssClass="Month">
    <asp:ListItem Text="請選擇" Selected="True"></asp:ListItem>
</asp:DropDownList>
月
<asp:DropDownList ID="ddlDay" runat="server" CssClass="Day">
    <asp:ListItem Text="請選擇" Selected="True"></asp:ListItem>
</asp:DropDownList>
日 
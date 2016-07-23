<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pagger.ascx.cs" Inherits="_Element_Pagger_Pagger" %>
<asp:Panel ID="Element_Pagger" CssClass="Element_Pagger" runat="server">
    <asp:LinkButton ID="btnFirstPage" runat="server" Text=" " OnClick="btnFirstPage_Click" CssClass="PaggerButton First"></asp:LinkButton>
    <asp:LinkButton ID="btnPreviousPage" runat="server" Text="上一頁" OnClick="btnPreviousPage_Click" CssClass="PaggerButton Previous" Visible="false"></asp:LinkButton>
    <asp:Repeater ID="RepeaterPagger" runat="server" OnItemDataBound="RepeaterPagger_ItemDataBound">
        <ItemTemplate>
            <asp:LinkButton ID="lknIndex" runat="server" OnClick="PageIndex_Click" Text="1" CssClass="PageNumber"></asp:LinkButton>
            <asp:Label ID="lblSeparate" runat="server" Text="/" CssClass="PageSeparate"></asp:Label>
        </ItemTemplate>
    </asp:Repeater>
    <asp:LinkButton ID="btnNextPage" runat="server" Text="下一頁" OnClick="btnNextPage_Click" CssClass="PaggerButton Next" Visible="false"></asp:LinkButton>
    <asp:LinkButton ID="btnLastPage" runat="server" Text=" " Visible="false" OnClick="btnLastPage_Click" CssClass="PaggerButton Last"> </asp:LinkButton>
</asp:Panel>

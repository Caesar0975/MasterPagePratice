<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/_MasterPagePratice/Base.master" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--head的ContentPlaceHolder裡面--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    body的ContentPlaceHolder裡面

    <div>
        <h2>Default裡面的Hello World</h2>
        123
    </div>
    <div>
        <table>
            <colgroup>
                <col style="width: 60px" />
                <col style="width: 60px;" />
                <col style="width: 60px;" />
                <col style="width: 60px;" />
                <col style="width: 60px;" />
                <col style="width: 60px;" />
                <col style="width: 60px;" />
            </colgroup>
            <thead>
                <tr>
                    <td>Account</td>
                    <td>Name</td>
                    <td>Id</td>
                    <td>Subject</td>
                    <td>Content</td>
                    <td>Visible</td>
                    <td>UpdateTime</td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rpt1StRecordList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Literal ID="vAccount" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:Label ID="vName" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="vId" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="vSubject" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="vContent" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="vVisible" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="vUpdateTime" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>

</asp:Content>

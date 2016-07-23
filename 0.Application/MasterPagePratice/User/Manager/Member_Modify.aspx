<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Member_Modify.aspx.cs" Inherits="User_Manager_Member_Modify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/User/Manager/_Css/Member_Modify.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div id="Member_Modify">
        <table class="Table2">
            <colgroup>
                <col style="width: 120px" />
            </colgroup>
            <tbody>
                <tr>
                    <th>帳號</th>
                    <td>
                        <asp:TextBox ID="vAccount" runat="server" placeholder="請輸入電子信箱" Width="150px" MaxLength="12"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>密碼</th>
                    <td>
                        <asp:TextBox ID="vPassword" runat="server" placeholder="請輸入密碼" MaxLength="12" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>姓名</th>
                    <td>
                        <asp:TextBox ID="vName" runat="server" placeholder="請輸入姓名" Width="150px" MaxLength="10"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>聯絡電話</th>
                    <td>
                        <asp:TextBox ID="vPhone" runat="server" placeholder="請輸入連絡電話" Width="150px" MaxLength="10"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>備註</th>
                    <td>
                        <asp:TextBox ID="vRemark" runat="server" placeholder="備註" Width="98%" MaxLength="100" Rows="6" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
            </tbody>
            <tfoot>
                <tr>

                    <td colspan="2">
                        <asp:LinkButton ID="vDelete" CssClass="Button2 Red" runat="server" OnClick="vDelete_Click" >刪除</asp:LinkButton>
                        <asp:LinkButton ID="vSave" CssClass="Button2 Blue" runat="server" OnClick="vSave_Click">儲存</asp:LinkButton>
                        <div style="clear: both; height: 1px"></div>
                </tr>
            </tfoot>
        </table>
    </div>
</asp:Content>


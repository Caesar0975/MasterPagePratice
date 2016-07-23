<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Member_List.aspx.cs" Inherits="User_Manager_Member_List" %>

<%@ Register Src="/_Element/Pagger/Pagger.ascx" TagName="Pagger" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/User/Manager/_Css/Member_List.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="MemberBlock">
        <div class="Title1">會員</div>
        <div id="Member_List">
            <table class="Table1 Detail">
                <colgroup>
                    <col style="width: 120px" />
                    <col style="width: 80px;" />
                    <col style="width: 120px;" />
                    <col style="width: auto;" />
                    <col style="width: 20px;" />
                </colgroup>
                <thead>
                    <tr>
                        <th>帳號</th>
                        <th>姓名</th>
                        <th>電話</th>
                        <th>備註</th>

                        <th style="text-align: center;">
                            <asp:HyperLink ID="vAdd" runat="server" NavigateUrl="/User/Manager/Member_Modify.aspx" CssClass="Button1 Add fancybox" Text=" "></asp:HyperLink>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="vMemberList" runat="server" OnItemDataBound="vMemberList_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Literal ID="vAccount" runat="server" Text="Account"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="vName" runat="server" Text="趙弘仁"></asp:Literal>
                                </td>
                                <td style="text-align: center;">
                                    <asp:Literal ID="vPhone" runat="server" Text="0932857752"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="vRemark" runat="server" Text="一二三四五六七八九十一二三四五六七八九十一二三四五六七八九十一二三"></asp:Literal>
                                </td>

                                <td style="text-align: center;">
                                    <asp:HyperLink ID="vEdit" runat="server" NavigateUrl="/User/Manager/Member_Modify.aspx" CssClass="Button1 Edit fancybox" Text=" "></asp:HyperLink>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <div>
                <uc1:Pagger ID="Pagger1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LeftHand.MemberShip;
using System.Text;

public partial class User_Manager_Member_Modify : System.Web.UI.Page
{
    Dictionary<string, string> _Argument { get { return (Dictionary<string, string>)ViewState["_Argument"]; } set { ViewState["_Argument"] = value; } }

    User _Member;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        { _Argument = LeftHand.Gadget.Encoder.DictionaryDecoder(Server.UrlDecode(Request.QueryString.ToString())); }

        switch (_Argument["Mode"])
        {
            case "Add":
                _Member = UserManager.GetNewUser("", "");
                break;

            case "Edit":
                _Member = UserManager.GetUser(_Argument["Account"]);
                break;
        }
    }

    protected void vDelete_Click(object sender, EventArgs e)
    {
        try
        {
            UserManager.RemoveUser(_Member);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('刪除成功');window.parent.$.fancybox.close();window.parent.__doPostBack('', '');", true);
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void vSave_Click(object sender, EventArgs e)
    {
        try
        {
            //帳號
            string Account = this.vAccount.Text.Trim();
            if (string.IsNullOrWhiteSpace(Account) == true) { throw new Exception("帳號不能為空"); }
            if (_Argument["Mode"] == "Add" && UserManager.GetUser(Account) != null) { throw new Exception("帳號已經被使用"); }

            //密碼
            string Password = this.vPassword.Text.Trim();
            if (_Argument["Mode"] == "Add" && string.IsNullOrWhiteSpace(Password) == true) { throw new Exception("密碼不能為空"); }

            //姓名
            string Name = this.vName.Text.Trim();
            if (string.IsNullOrWhiteSpace(Name) == true) { throw new Exception("姓名不能為空"); }

            //電話
            string Phone = this.vPhone.Text.Trim();
            if (string.IsNullOrWhiteSpace(Phone) == true) { throw new Exception("電話不能為空"); }

            //備註
            string Remark = this.vRemark.Text.Trim();

            List<RoleKey> RoleKeys = new List<RoleKey>();
            RoleKeys.Add(RoleKey.Member);
            RoleKeys.Add(RoleKey.Login);

            if (_Argument["Mode"] == "Add") { _Member.Account = Account; }
            if (string.IsNullOrWhiteSpace(Password) == false) { _Member.Password = Password; }

            _Member.Profiles[ProfileKey.Name] = Name;
            _Member.Profiles[ProfileKey.Phone] = Phone;
            _Member.Profiles[ProfileKey.Remark] = Remark;

            _Member.Roles = RoleKeys;

            UserManager.SaveUser(_Member);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('儲存成功');window.parent.$.fancybox.close();window.parent.__doPostBack('', '');", true);
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_UserItem();
    }

    private void Render_UserItem()
    {
        if (this.Page.IsPostBack == true) { return; }

        //帳號
        this.vAccount.Text = _Member.Account;
        this.vAccount.ReadOnly = _Argument["Mode"] == "Edit";
        //姓名
        this.vName.Text = _Member.Profiles[ProfileKey.Name];
        //電話
        this.vPhone.Text = _Member.Profiles[ProfileKey.Phone];
        //備註
        this.vRemark.Text = _Member.Profiles[ProfileKey.Remark];

        this.vDelete.OnClientClick = "return confirm('你確定要刪除此使用者');";
        this.vDelete.Visible = true;
    }
}
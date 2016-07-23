using LeftHand.MemberShip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Member_ChangePassword : System.Web.UI.Page
{
    User EditUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        EditUser = SwitchUserManager.GetCurrentUser();
    }

    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string Password = this.tbxPassword.Text.Trim();
            string PasswordConfirm = this.tbxPasswordConfirm.Text.Trim();

            if (Password != PasswordConfirm) { throw new Exception("密碼與確認密碼不相同"); }

            if (Password == "") { throw new Exception("密碼不可空白"); }

            EditUser.Password = Password;

            UserManager.SaveUser(EditUser);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('儲存成功');window.parent.$.fancybox.close();", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('" + ex.Message + "');", true);
        }

    }
}
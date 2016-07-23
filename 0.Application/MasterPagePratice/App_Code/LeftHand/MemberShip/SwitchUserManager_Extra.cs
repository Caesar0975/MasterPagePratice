using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeftHand.MemberShip
{
    public static partial class SwitchUserManager
    {
        //切換使用不含密碼、驗證碼
        public static User SwitchUser(string Account, bool AllowDuplicateLogin = false, string RedirectPath = "")
        {
            Account = Account.ToLower().Trim();
            
            User LoginUser = UserManager.GetUser(Account);

            //驗證帳號是否存在
            if (LoginUser == null) { throw new Exception("帳號不存在"); }

            //驗正是否可以登入
            if (LoginUser.Roles.Contains(RoleKey.Login) == false) { CreateSwitchUserLog(LoginUser, "帳號已凍結"); throw new Exception("帳號已凍結"); }
            
            //登入成功============================================

            //發與驗證票
            PublishPassCard(LoginUser);

            //加入線上人員統計
            UpdateLoginTag(LoginUser);

            //產生登入紀錄
            CreateSwitchUserLog(LoginUser, "登入成功");

            //導頁到指定頁面
            if (RedirectPath != "") { HttpContext.Current.Response.Redirect(RedirectPath); }

            return LoginUser;
        }
    }
}
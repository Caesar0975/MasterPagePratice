using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeftHand.MemberShip
{
    public static partial class SwitchUserManager
    {
        //切換使用者含驗證碼(未完成)
        public static User SwitchUser(string Account, string Password, string InputValidCode, bool AllowDuplicateLogin = false, string RedirectPath = "")
        {
            Account = Account.ToLower().Trim();
            Password = Password.ToLower().Trim();
            InputValidCode = InputValidCode.Trim();

            //if (InputValidCode == "") { throw new Exception("驗證碼不能空白"); }

            User LoginUser = UserManager.GetUser(Account);

            //驗證帳號是否存在
            if (LoginUser == null) { throw new Exception("帳號不存在"); }

            //驗正是否可以登入
            if (LoginUser.Roles.Contains(RoleKey.Login) == false) { CreateSwitchUserLog(LoginUser, "帳號已凍結"); throw new Exception("帳號已凍結"); }

            //驗證驗證碼           
            if (CompareValidCode(InputValidCode) == false) { CreateSwitchUserLog(LoginUser, "驗證碼錯誤"); throw new Exception("驗證碼錯誤"); }

            //驗證帳號密碼
            if (LoginUser.Password != Password) { CreateSwitchUserLog(LoginUser, "密碼錯誤"); throw new Exception("密碼錯誤"); }

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

        //切換使用不含驗證碼
        public static User SwitchUser(string Account, string Password, bool AllowDuplicateLogin = false, string RedirectPath = "")
        {
            User LoginUser = SwitchUser(Account, Password, GetCurrentValidCode(), AllowDuplicateLogin, RedirectPath);

            return LoginUser;
        }

        //切換成Visitor
        public static void SwitchToVisitor(string RedirectPath = "")
        {
            //發與驗證票
            User Visitor = UserManager.GetUser("visitor");

            PublishPassCard(Visitor);

            if (RedirectPath != "")
            { HttpContext.Current.Response.Redirect(RedirectPath); }

        }

        //取得目前的使用者
        public static User GetCurrentUser()
        {
            User CurrentUser = UserManager.GetUser(GetPassCardValue());

            if (CurrentUser == null)
            {
                PublishPassCard(UserManager.GetUser("Visitor"));
                CurrentUser = UserManager.GetUser("Visitor");
            }

            return CurrentUser;
        }

        //發與驗證票
        private static void PublishPassCard(User User)
        {
            HttpCookie PassCard = new HttpCookie("PassCard");
            PassCard.HttpOnly = true;
            PassCard.Value = LeftHand.Gadget.Encoder.AES_Encryption(User.Account);
            //PassCard.Expires = DateTime.Now.AddDays(1); //註解的話關掉視窗就會登出

            HttpContext.Current.Response.Cookies.Add(PassCard);
        }

        //取得驗證票的值
        private static string GetPassCardValue()
        {
            HttpCookie PassCard = HttpContext.Current.Request.Cookies["PassCard"];

            if (PassCard == null) { return ""; }

            return LeftHand.Gadget.Encoder.AES_Decryption(PassCard.Value);
        }
    }
}
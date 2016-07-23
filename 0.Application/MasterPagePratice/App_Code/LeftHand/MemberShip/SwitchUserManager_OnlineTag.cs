using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace LeftHand.MemberShip
{
    public static partial class SwitchUserManager
    {
        //取得所有線上人員紀錄
        public static List<OnlineTag> GetAllLoginTag()
        {
            return LoginTagCache.Values.ToList();
        }

        //取得線上人員紀錄透過User
        public static OnlineTag GetAllLoginTag(User User)
        {
            return LoginTagCache[User];
        }

        //更新線上人員記錄
        public static void UpdateLoginTag(User User)
        {
            if (LoginTagCache.ContainsKey(User) == false)
            { LoginTagCache.Add(User, GetNewLoginTag(User)); }

            OnlineTag OnlineTag = LoginTagCache[User];
            OnlineTag.SessionId = HttpContext.Current.Session.SessionID;
            OnlineTag.ReflashTime = DateTime.Now;
        }

        //取得新的LoginTag
        private static OnlineTag GetNewLoginTag(User User)
        {
            OnlineTag UserOnlineTag = new OnlineTag();
            UserOnlineTag.Account = User.Account;
            UserOnlineTag.SessionId = HttpContext.Current.Session.SessionID;
            UserOnlineTag.ReflashTime = DateTime.Now;
            UserOnlineTag.CreateTime = DateTime.Now;

            return UserOnlineTag;
        }

        //驗證是否重複登入
        public static bool IsDuplicateLogin(User User)
        {
            if (User.Roles.Contains(RoleKey.Visitor)) { return false; }

            OnlineTag UserOnlineTag = null;
            if (LoginTagCache.TryGetValue(User, out UserOnlineTag) == false) { return false; }
            if (UserOnlineTag.SessionId != HttpContext.Current.Session.SessionID) { return true; }

            return false;
        }

    }
}
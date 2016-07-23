using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LeftHand.MemberShip
{
    public static partial class UserManager
    {
        public static string UploadPath = "/Upload/User/";
        public static string PhysicalUploadPath = HttpContext.Current.Server.MapPath(UploadPath);

        //取得新的空User
        public static User GetNewUser(string Account, string Password)
        {
            if (UserCache.ContainsKey(Account) == true) { throw new Exception("帳號已經存在"); }

            User NewUser = new User();
            NewUser.Account = Account;
            NewUser.Password = Password;

            NewUser.Roles = new List<RoleKey>();
            NewUser.Roles.Add(RoleKey.Login);

            NewUser.Profiles = new Dictionary<ProfileKey, string>();
            foreach (ProfileKey Type in Enum.GetValues(typeof(ProfileKey)))
            { NewUser.Profiles.Add(Type, ""); }

            NewUser.UpdateTime = DateTime.Now;
            NewUser.CreateTime = DateTime.Now;

            return NewUser;
        }

        //透過帳號取得單一User，取不到東西會回傳null
        public static User GetUser(string Account)
        {
            Account = (Account == null) ? "" : Account.ToLower();

            User OldUser = null;
            UserCache.TryGetValue(Account, out OldUser);

            return OldUser;
        }

        //透過帳號集合取得對應的Users
        public static List<User> GetUser(List<string> Accounts)
        {
            return UserCache.Values.Where(u => Accounts.Contains(u.Account.ToLower()) == true).ToList();
        }

        //取得所有的User
        public static List<User> GetAllUser()
        {
            return UserCache.Values.ToList();
        }

        //儲存單一User
        public static void SaveUser(User User)
        {
            SaveUser(new List<User> { User });
        }

        //儲存多個User
        public static void SaveUser(List<User> Users)
        {
            Users.ForEach(u => u.UpdateTime = DateTime.Now);

            UserAccessor.UpdateInsert(Users);

            foreach (User User in Users)
            {
                if (UserCache.ContainsKey(User.Account) == true) { continue; }
                UserCache.Add(User.Account, User);
            }

            UserCache = UserCache
                .OrderBy(u => u.Key)
                .ToDictionary(k => k.Key, v => v.Value);
        }

        //移除單一User
        public static void RemoveUser(User User)
        {
            RemoveUser(new List<User> { User });
        }

        //移除多個User
        public static void RemoveUser(List<User> Users)
        {
            foreach (User User in Users)
            {
                UserCache.Remove(User.Account);
            }

            UserAccessor.Delete(Users);
        }
    }
}
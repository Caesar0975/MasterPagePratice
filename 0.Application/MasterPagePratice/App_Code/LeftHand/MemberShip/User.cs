using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeftHand.MemberShip
{
    //RoleKey
    public enum RoleKey { Login, Visitor, Manager, Member }
    public enum RoleName { 可登入, 訪客, 系統管理者, 會員 }

    //ProfileKey
    public enum ProfileKey { Name, Phone, Remark }
    public enum ProfileName { 姓名, 聯絡電話, 備註 }

    //User
    public class User
    {
        public User Parent { get; set; }

        private string _Account;
        public string Account { get { return _Account; } set { _Account = value.ToLower(); } }

        private string _Password;
        public string Password { get { return _Password; } set { _Password = value.ToLower(); } }

        public List<RoleKey> Roles { get; set; }

        public Dictionary<ProfileKey, string> Profiles { get; set; }

        public DateTime UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }

        //轉呼叫GetChilds
        public List<User> Childs { get { return UserManager.GetChilds(this); } }

        public User(User Parent, string Account, string Password, List<RoleKey> Roles, Dictionary<ProfileKey, string> Profiles)
        {
            this.Parent = Parent;
            this.Account = Account;
            this.Password = Password;
            this.Roles = Roles;
            this.Profiles = Profiles;
            this.UpdateTime = DateTime.Now;
            this.CreateTime = DateTime.Now;
        }

        internal User()
        { }
    }
}
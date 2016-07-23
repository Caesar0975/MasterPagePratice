using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace LeftHand.MemberShip
{
    public static partial class UserManager
    {
        //快取UserCache<Account , User>
        private static Dictionary<string, User> UserCache = new Dictionary<string, User>();

        //初始化
        public static void Initial()
        {
            Dictionary<string, string> Relations = new Dictionary<string, string>();

            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                SqlCommand SqlCommand = SqlConnection.CreateCommand();

                SqlCommand.CommandText = "SELECT "
                                       + " * "
                                       + "FROM "
                                       + " MemberShip_User ";
                SqlConnection.Open();

                SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();

                while (SqlDataReader.Read())
                {
                    User User = new User();
                    User.Parent = null;
                    User.Account = SqlDataReader["Account"].ToString();
                    User.Password = LeftHand.Gadget.Encoder.AES_Decryption(SqlDataReader["Password"].ToString());

                    User.Roles = new List<RoleKey>();
                    string RoleString = SqlDataReader["Roles"].ToString();
                    if (RoleString != "")
                    {
                        User.Roles = RoleString.Split(',')
                                                    .Select(r => (RoleKey)Enum.Parse(typeof(RoleKey), r))
                                                    .ToList();
                    }

                    User.Profiles = new Dictionary<ProfileKey, string>();
                    string ProfileString = SqlDataReader["Profiles"].ToString();
                    if (ProfileString != "")
                    {
                        User.Profiles = ProfileString
                            .Split('，')
                            .ToDictionary(p => (ProfileKey)Enum.Parse(typeof(ProfileKey), p.Split('：')[0]), p => p.Split('：')[1]);
                    }

                    //將所有的ProfileKey都加上
                    foreach (ProfileKey ProfileKey in Enum.GetValues(typeof(ProfileKey)))
                    {
                        if (User.Profiles.Keys.Contains(ProfileKey) == false)
                        { User.Profiles.Add(ProfileKey, ""); }
                    }

                    User.UpdateTime = (DateTime)SqlDataReader["UpdateTime"];
                    User.CreateTime = (DateTime)SqlDataReader["CreateTime"];

                    UserCache.Add(User.Account, User);
                    Relations.Add(User.Account, SqlDataReader["Parent"].ToString());
                }
            }

            //建立Parent關聯
            foreach (KeyValuePair<string, string> Child_Parent in Relations)
            {
                string Child = Child_Parent.Key;
                string Parent = Child_Parent.Value;

                if (string.IsNullOrWhiteSpace(Parent)) { continue; }

                UserCache[Child].Parent = UserCache[Parent];
            }
        }


        //預設定(建立資料庫、插入預設值)
        public static void PreSet()
        {
            CreateDeafultTable();
            CreateDeafultUser();
        }

        //建立資料庫結構
        private static void CreateDeafultTable()
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                SqlCommand SqlCommand = SqlConnection.CreateCommand();

                string TableName = "MemberShip_User";

                SqlCommand.CommandText += "IF EXISTS"
                                       + "("
                                       + "SELECT "
                                       + " TABLE_NAME "
                                       + "FROM "
                                       + " INFORMATION_SCHEMA.TABLES "
                                       + "WHERE "
                                       + " TABLE_NAME = '" + TableName + "'"
                                       + ")"
                                       + "DROP TABLE " + TableName + "; "

                                       + "CREATE TABLE " + TableName
                                       + "("
                                       + " Parent varchar(50) NOT NULL, "
                                       + " Account varchar(50) NOT NULL, "
                                       + " Password varchar(50) NOT NULL, "
                                       + " Roles varchar(300) NOT NULL, "
                                       + " Profiles nvarchar(MAX) NOT NULL, "
                                       + " UpdateTime datetime NOT NULL, "
                                       + " CreateTime datetime NOT NULL, "

                                       + " Primary Key (Account) "
                                       + "); ";

                SqlConnection.Open();
                SqlCommand.ExecuteNonQuery();
            }
        }

        //插入預設帳號資料
        private static void CreateDeafultUser()
        {
            List<User> DefaultUsers = new List<User>();

            User User1 = new User();
            User1.Parent = null;
            User1.Account = "Visitor";
            User1.Password = "Visitor";
            User1.Roles = new List<RoleKey>() { RoleKey.Visitor };
            User1.Profiles = new Dictionary<ProfileKey, string>();
            User1.Profiles.Add(ProfileKey.Name, "訪客");
            User1.UpdateTime = DateTime.Now;
            User1.CreateTime = DateTime.Now;
            DefaultUsers.Add(User1);


            User User2 = new User();
            User2.Parent = null;
            User2.Account = "MANAGER";
            User2.Password = "MANAGER";
            User2.Roles = new List<RoleKey>() { RoleKey.Login, RoleKey.Manager };
            User2.Profiles = new Dictionary<ProfileKey, string>();
            User2.Profiles.Add(ProfileKey.Name, "系統管理者");
            User2.UpdateTime = DateTime.Now;
            User2.CreateTime = DateTime.Now;
            DefaultUsers.Add(User2);

            SaveUser(DefaultUsers);
        }
    }
}
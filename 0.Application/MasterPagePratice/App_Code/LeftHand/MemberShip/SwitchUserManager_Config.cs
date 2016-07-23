using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Data.SqlClient;

namespace LeftHand.MemberShip
{
    public static partial class SwitchUserManager
    {
        //線上人數紀錄快取
        private static Dictionary<User, OnlineTag> LoginTagCache = new Dictionary<User, OnlineTag>();

        //初始化
        public static void Initial()
        {
            //背景執行緒檢查LoginTagCache，將愈時的使用者資料移出
            Thread AutoCheckThread = new Thread(CycleCheckLoginTag);
            AutoCheckThread.Start();
        }

        //循環檢查
        private static void CycleCheckLoginTag()
        {
            int AvaliableMinute = 10;

            //延遲十分鐘
            Thread.Sleep(AvaliableMinute * 60 * 1000);

            if (LoginTagCache.Count() > 0)
            {
                lock (LoginTagCache)
                {
                    LoginTagCache = LoginTagCache
                                                             .Where(t => t.Value.ReflashTime >= DateTime.Now.AddMinutes(-1 * AvaliableMinute))
                                                             .ToDictionary(k => k.Key, v => v.Value);
                }
            }

            CycleCheckLoginTag();
        }


        //預設定
        public static void PreSet()
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                SqlCommand SqlCommand = SqlConnection.CreateCommand();

                string TableName = "MemberShip_SwitchUserLog";

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
                                       + " Id varchar(50) NOT NULL, "
                                       + " Account varchar(50) NOT NULL, "
                                       + " Ip varchar(20) NOT NULL, "
                                       + " Remark nvarchar(20) NOT NULL, "
                                       + " CreateTime datetime NOT NULL,"

                                       + " Primary Key (Id) "
                                       + "); ";

                SqlConnection.Open();
                SqlCommand.ExecuteNonQuery();
            }
        }
    }
}
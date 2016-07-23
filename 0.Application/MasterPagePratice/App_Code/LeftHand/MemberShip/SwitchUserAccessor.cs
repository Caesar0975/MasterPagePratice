using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;

namespace LeftHand.MemberShip
{
    internal static partial class SwitchUserAccessor
    {
        //取得Recorders
        internal static List<SwitchUserLog> Select(User User)
        {
            List<SwitchUserLog> LoginRecoders = new List<SwitchUserLog>();

            using (System.Data.SqlClient.SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                SqlCommand SqlCommand = SqlConnection.CreateCommand();

                SqlCommand.CommandText = "SELECT "
                                       + " * "
                                       + "FROM "
                                       + " MemberShip_SwitchUserLog "
                                       + "WHERE "
                                       + " Account = @Account ";

                SqlCommand.Parameters.AddWithValue("Account", User.Account);

                SqlConnection.Open();

                SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();

                while (SqlDataReader.Read())
                {
                    string Id = SqlDataReader["Id"].ToString();
                    string Account = SqlDataReader["Account"].ToString();
                    string Ip = SqlDataReader["Ip"].ToString();
                    string Remark = SqlDataReader["Remark"].ToString();
                    DateTime CreateTime = (DateTime)SqlDataReader["CreateTime"];

                    LoginRecoders.Add(new SwitchUserLog(Id, Account, Ip, Remark, CreateTime));
                }
            }

            return LoginRecoders;
        }

        //寫入一筆新的LoginRecoder , 並刪除超過保留時間的Recoder
        internal static void InsertDelete(SwitchUserLog Log, int KeepDays)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                SqlCommand SqlCommand = SqlConnection.CreateCommand();

                SqlCommand.CommandText = "INSERT INTO "
                                       + " MemberShip_SwitchUserLog "
                                       + " ( Id, Account, Ip, Remark, CreateTime ) "
                                       + "VALUES "
                                       + " ( @Id, @Account, @Ip, @Remark, @CreateTime ) ; "

                                       + "DELETE "
                                       + " MemberShip_SwitchUserLog "
                                       + "WHERE "
                                       + " CreateTime <= @LimitTime ; ";

                SqlCommand.Parameters.AddWithValue("Id", Log.Id);
                SqlCommand.Parameters.AddWithValue("Account", Log.Account);
                SqlCommand.Parameters.AddWithValue("Ip", Log.Ip);
                SqlCommand.Parameters.AddWithValue("Remark", Log.Remark);
                SqlCommand.Parameters.AddWithValue("CreateTime", Log.CreateTime);

                SqlCommand.Parameters.AddWithValue("LimitTime", DateTime.Now.AddDays(KeepDays * -1).ToShortDateString());

                SqlConnection.Open();
                SqlCommand.ExecuteNonQuery();
            }
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;

namespace LeftHand.MemberShip
{
    internal static partial class UserAccessor
    {
        //更新多個User UpdateInsert
        internal static void UpdateInsert(List<User> Users)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                SqlConnection.Open();

                for (int i = 0; i < (Users.Count / 100) + (Users.Count % 100 == 0 ? 0 : 1); i++)
                {
                    List<User> PartUsers = Users.Skip(i * 100).Take(100).ToList();

                    SqlCommand SqlCommand = SqlConnection.CreateCommand();
                    StringBuilder Commandtext = new StringBuilder();

                    for (int ii = 0; ii < PartUsers.Count(); ii++)
                    {
                        Commandtext.Append("UPDATE "
                                         + " MemberShip_User "
                                         + " SET "
                                         + " Parent = @Parent" + ii
                                         + " ,Password = @Password" + ii
                                         + " ,Roles = @Roles" + ii
                                         + " ,Profiles = @Profiles" + ii
                                         + " ,UpdateTime=@UpdateTime" + ii
                                         + " WHERE "
                                         + " Account = @Account" + ii

                                         + " IF @@ROWCOUNT = 0 "
                                         + " BEGIN "

                                         + " INSERT INTO "
                                         + " MemberShip_User ( "
                                         + " Parent, "
                                         + " Account, "
                                         + " Password, "
                                         + " Roles, "
                                         + " Profiles, "
                                         + " UpdateTime, "
                                         + " CreateTime "
                                         + ") VALUES ( "
                                         + " @Parent" + ii
                                         + " ,@Account" + ii
                                         + " ,@Password" + ii
                                         + " ,@Roles" + ii
                                         + " ,@Profiles" + ii
                                         + " ,@UpdateTime" + ii
                                         + " ,@CreateTime" + ii
                                         + " ) END "
                                         );

                        SqlCommand.Parameters.AddWithValue("Parent" + ii, PartUsers[ii].Parent == null ? "" : PartUsers[ii].Parent.Account);
                        SqlCommand.Parameters.AddWithValue("Account" + ii, PartUsers[ii].Account);
                        SqlCommand.Parameters.AddWithValue("Password" + ii, LeftHand.Gadget.Encoder.AES_Encryption(PartUsers[ii].Password));
                        SqlCommand.Parameters.AddWithValue("Roles" + ii, string.Join(",", PartUsers[ii].Roles.Select(r => r.ToString())));
                        SqlCommand.Parameters.AddWithValue("Profiles" + ii, string.Join("，", PartUsers[ii].Profiles.Select(p => p.Key + "：" + p.Value.Replace("，", ",").Replace("：", ":"))));
                        SqlCommand.Parameters.AddWithValue("UpdateTime" + ii, PartUsers[ii].UpdateTime);
                        SqlCommand.Parameters.AddWithValue("CreateTime" + ii, PartUsers[ii].CreateTime);
                    }

                    SqlCommand.CommandText = Commandtext.ToString();
                    SqlCommand.ExecuteNonQuery();
                }  
            
            }
        }

        //刪除User
        internal static void Delete(List<User> Users)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                SqlCommand SqlCommand = SqlConnection.CreateCommand();
                SqlCommand.CommandText = string.Format("DELETE "
                                       + " MemberShip_User "
                                       + "WHERE "
                                       + " Account IN  ('{0}') ; ", string.Join("','", Users.Select(u => u.Account))
                                       );

                SqlConnection.Open();
                SqlCommand.ExecuteNonQuery();
            }
        }
    }
}
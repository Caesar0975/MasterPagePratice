using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Cart1.Product
{
    internal static class TagGroupItemAccessor
    {
        private static List<TagGroupItem> Get(SqlCommand SqlCommand)
        {
            List<TagGroupItem> TagGroupItems = new List<TagGroupItem>();

            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand.Connection = SqlConnection)
                {
                    SqlConnection.Open();

                    SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();

                    while (SqlDataReader.Read())
                    {
                        string Id = (string)SqlDataReader["Id"];
                        string Name = (string)SqlDataReader["Name"];
                        List<string> Tags = SqlDataReader["Tags"].ToString().Split(',').ToList();
                        int Sort = (int)SqlDataReader["Sort"];
                        DateTime UpdateTime = (DateTime)SqlDataReader["UpdateTime"];
                        DateTime CreateTime = (DateTime)SqlDataReader["CreateTime"];

                        TagGroupItem TagGroupItem = new TagGroupItem(Id, Name, Tags, Sort, UpdateTime, CreateTime);
                        TagGroupItems.Add(TagGroupItem);
                    }
                }
            }

            return TagGroupItems;
        }

        internal static List<TagGroupItem> SelectAll()
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " Product_TagGroupItem ";

            return Get(SqlCommand);
        }

        internal static void UpdateInsert(TagGroupItem TagGroupItem)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "UPDATE "
                                             + " Product_TagGroupItem "
                                             + "SET "
                                             + " Name = @Name "
                                             + " ,Tags = @Tags "
                                             + " ,Sort = @Sort "
                                             + " ,UpdateTime = @UpdateTime "
                                             + " ,CreateTime = @CreateTime  "
                                             + "WHERE "
                                             + " Id = @Id "

                                             + "IF @@ROWCOUNT = 0 "
                                             + "BEGIN "

                                             + "INSERT INTO "
                                             + " Product_TagGroupItem "
                                             + "( Id, Name, Tags, Sort, UpdateTime, CreateTime ) "
                                             + "VALUES "
                                             + "( @Id, @Name, @Tags, @Sort, @UpdateTime, @CreateTime ) "
                                             + "END ";

                    SqlCommand.Parameters.AddWithValue("Id", TagGroupItem.Id);
                    SqlCommand.Parameters.AddWithValue("Name", TagGroupItem.Name);
                    SqlCommand.Parameters.AddWithValue("Tags", string.Join(",", TagGroupItem.Tags));
                    SqlCommand.Parameters.AddWithValue("Sort", TagGroupItem.Sort);
                    SqlCommand.Parameters.AddWithValue("UpdateTime", TagGroupItem.UpdateTime);
                    SqlCommand.Parameters.AddWithValue("CreateTime", TagGroupItem.CreateTime);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }

        internal static void Delete(TagGroupItem TagGroupItem)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "Delete "
                                           + " Product_TagGroupItem "
                                           + "WHERE "
                                           + " Id = @Id ";

                    SqlCommand.Parameters.AddWithValue("Id", TagGroupItem.Id);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
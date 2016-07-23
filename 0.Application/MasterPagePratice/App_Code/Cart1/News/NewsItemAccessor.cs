using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Cart1.News
{
    internal static class NewsItemAccessor
    {
        internal static void SelectAll(ref List<NewsItem> NewsItemCache)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "SELECT "
                                           + " * "
                                           + "FROM "
                                           + " News_NewsItem ";

                    SqlConnection.Open();

                    SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();

                    while (SqlDataReader.Read())
                    {
                        string Id = (string)SqlDataReader["Id"];
                        string Title = (string)SqlDataReader["Title"];
                        string Content = (string)SqlDataReader["Content"];
                        DateTime OnShelfTime = (DateTime)SqlDataReader["OnShelfTime"];
                        DateTime OffShelfTime = (DateTime)SqlDataReader["OffShelfTime"];
                        DateTime UpdateTime = (DateTime)SqlDataReader["UpdateTime"];
                        DateTime CreateTime = (DateTime)SqlDataReader["CreateTime"];

                        NewsItem NewsItem = new NewsItem(Id, Title, Content, OnShelfTime, OffShelfTime, UpdateTime, CreateTime);
                        NewsItemCache.Add(NewsItem);
                    }
                }
            }
        }

        internal static void UpdateInsert(NewsItem NewsItem)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "UPDATE "
                                             + " News_NewsItem "
                                             + "SET "
                                             + " Title = @Title "
                                             + " ,[Content] = @Content "
                                             + " ,OnShelfTime = @OnShelfTime "
                                             + " ,OffShelfTime = @OffShelfTime "
                                             + " ,UpdateTime = @UpdateTime "
                                             + " ,CreateTime = @CreateTime "
                                             + "WHERE "
                                             + " Id = @Id "

                                             + "IF @@ROWCOUNT = 0 "
                                             + "BEGIN "

                                             + "INSERT INTO "
                                             + " News_NewsItem "
                                             + "( Id, Title, [Content], OnShelfTime, OffShelfTime, UpdateTime, CreateTime ) "
                                             + "VALUES "
                                             + "( @Id, @Title, @Content, @OnShelfTime, @OffShelfTime, @UpdateTime, @CreateTime ) "

                                             + "END ";

                    SqlCommand.Parameters.AddWithValue("Id", NewsItem.Id);
                    SqlCommand.Parameters.AddWithValue("Title", NewsItem.Title);
                    SqlCommand.Parameters.AddWithValue("Content", NewsItem.Content);
                    SqlCommand.Parameters.AddWithValue("OnShelfTime", NewsItem.OnShelfTime);
                    SqlCommand.Parameters.AddWithValue("OffShelfTime", NewsItem.OffShelfTime);
                    SqlCommand.Parameters.AddWithValue("UpdateTime", NewsItem.UpdateTime);
                    SqlCommand.Parameters.AddWithValue("CreateTime", NewsItem.CreateTime);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }

        internal static void Delete(NewsItem NewsItem)
        {
            Delete(new List<NewsItem> { NewsItem });
        }

        internal static void Delete(List<NewsItem> NewsItems)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "Delete "
                                           + " News_NewsItem "
                                           + "WHERE "
                                           + " Id IN ('" + string.Join("','", NewsItems.Select(c => c.Id)) + "') ";
                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Cart1.Product
{
    internal static class ProductItemAccessor
    {
        private static List<ProductItem> Get(SqlCommand SqlCommand)
        {
            List<ProductItem> ProductItems = new List<ProductItem>();

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
                        string OriginalPrice = (string)SqlDataReader["OriginalPrice"];
                        string Discount = (string)SqlDataReader["Discount"];
                        decimal SalePrice = (decimal)SqlDataReader["SalePrice"];
                        int Inventory = (int)SqlDataReader["Inventory"];
                        int Sort = (int)SqlDataReader["Sort"];
                        DateTime UpdateTime = (DateTime)SqlDataReader["UpdateTime"];
                        DateTime CreateTime = (DateTime)SqlDataReader["CreateTime"];

                        ProductItem ProductItem = new ProductItem(Id, Name, Tags, OriginalPrice, Discount, SalePrice, Inventory, Sort, UpdateTime, CreateTime);
                        ProductItems.Add(ProductItem);
                    }
                }
            }

            return ProductItems;
        }

        internal static List<ProductItem> SelectAll()
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " Product_ProductItem ";

            return Get(SqlCommand);
        }

        internal static void UpdateInsert(ProductItem ProductItem)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "UPDATE "
                                             + " Product_ProductItem "
                                             + "SET "
                                             + " Name = @Name "
                                             + " ,Tags = @Tags "
                                             + " ,OriginalPrice = @OriginalPrice "
                                             + " ,Discount = @Discount "
                                             + " ,SalePrice = @SalePrice "
                                             + " ,Inventory = @Inventory "
                                             + " ,Sort = @Sort "
                                             + " ,UpdateTime = @UpdateTime "
                                             + " ,CreateTime = @CreateTime "
                                             + "WHERE "
                                             + " Id = @Id "

                                             + "IF @@ROWCOUNT = 0 "
                                             + "BEGIN "

                                             + "INSERT INTO "
                                             + " Product_ProductItem "
                                             + "( Id, Name, Tags, OriginalPrice, Discount, SalePrice, Inventory, Sort, UpdateTime, CreateTime ) "
                                             + "VALUES "
                                             + "( @Id, @Name, @Tags, @OriginalPrice, @Discount, @SalePrice, @Inventory, @Sort, @UpdateTime, @CreateTime ) "
                                             + "END ";

                    SqlCommand.Parameters.AddWithValue("Id", ProductItem.Id);
                    SqlCommand.Parameters.AddWithValue("Name", ProductItem.Name);
                    SqlCommand.Parameters.AddWithValue("Tags", string.Join(",", ProductItem.Tags.Select(t => t.Replace(",", "¡A"))));
                    SqlCommand.Parameters.AddWithValue("OriginalPrice", ProductItem.OriginalPrice);
                    SqlCommand.Parameters.AddWithValue("Discount", ProductItem.Discount);
                    SqlCommand.Parameters.AddWithValue("SalePrice", ProductItem.SalePrice);
                    SqlCommand.Parameters.AddWithValue("Inventory", ProductItem.Inventory);
                    SqlCommand.Parameters.AddWithValue("Sort", ProductItem.Sort);
                    SqlCommand.Parameters.AddWithValue("UpdateTime", ProductItem.UpdateTime);
                    SqlCommand.Parameters.AddWithValue("CreateTime", ProductItem.CreateTime);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }

        internal static void UpdateInsert(List<ProductItem> ProductItems)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                SqlConnection.Open();

                for (int i = 0; i < (ProductItems.Count / 100) + (ProductItems.Count % 100 == 0 ? 0 : 1); i++)
                {
                    List<ProductItem> PartProductItems = ProductItems.Skip(i * 100).Take(100).ToList();
                    StringBuilder CommandText = new StringBuilder();

                    SqlCommand SqlCommand = SqlConnection.CreateCommand();

                    for (int j = 0; j < PartProductItems.Count; j++)
                    {
                        CommandText.Append("UPDATE"
                                          + " Product_ProductItem "
                                          + "SET "
                                          + " Name = @Name" + j
                                          + " ,Tags = @Tags" + j
                                          + " ,OriginalPrice = @OriginalPrice" + j
                                          + " ,Discount = @Discount" + j
                                          + " ,SalePrice = @SalePrice" + j
                                          + " ,Inventory = @Inventory" + j
                                          + " ,Sort = @Sort" + j
                                          + " ,UpdateTime = @UpdateTime" + j
                                          + " ,CreateTime = @CreateTime" + j
                                          + " WHERE "
                                          + " Id = @Id" + j

                                          + " IF @@ROWCOUNT = 0 "
                                          + "BEGIN "

                                          + "INSERT INTO "
                                          + " Product_ProductItem "
                                          + "( Id, Name, Tags, OriginalPrice, Discount, SalePrice, Inventory, Sort, UpdateTime, CreateTime ) "
                                          + "VALUES "
                                          + "( @Id" + j + " ,@Name" + j + " ,@Tags" + j + " ,@OriginalPrice" + j + " ,@Discount" + j + " ,@SalePrice" + j + " ,@Inventory" + j + " ,@Sort" + j + " ,@UpdateTime" + j + " ,@CreateTime" + j + " )"
                                          + " END "
                                        );

                        SqlCommand.Parameters.AddWithValue("Id" + j, PartProductItems[j].Id);
                        SqlCommand.Parameters.AddWithValue("Name" + j, PartProductItems[j].Name);
                        SqlCommand.Parameters.AddWithValue("Tags" + j, string.Join(",", PartProductItems[j].Tags.Select(t => t.Replace(",", "¡A"))));
                        SqlCommand.Parameters.AddWithValue("OriginalPrice" + j, PartProductItems[j].OriginalPrice);
                        SqlCommand.Parameters.AddWithValue("Discount" + j, PartProductItems[j].Discount);
                        SqlCommand.Parameters.AddWithValue("SalePrice" + j, PartProductItems[j].SalePrice);
                        SqlCommand.Parameters.AddWithValue("Inventory" + j, PartProductItems[j].Inventory);
                        SqlCommand.Parameters.AddWithValue("Sort" + j, PartProductItems[j].Sort);
                        SqlCommand.Parameters.AddWithValue("UpdateTime" + j, PartProductItems[j].UpdateTime);
                        SqlCommand.Parameters.AddWithValue("CreateTime" + j, PartProductItems[j].CreateTime);
                    }

                    SqlCommand.CommandText = CommandText.ToString();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }

        internal static void Delete(ProductItem ProductItem)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "Delete "
                                           + " Product_ProductItem "
                                           + "WHERE "
                                           + " Id = @Id ";

                    SqlCommand.Parameters.AddWithValue("Id", ProductItem.Id);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }


    }
}
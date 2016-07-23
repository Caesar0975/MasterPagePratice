
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;


namespace OrderSystem.Shop
{
    //靜態類別不會有建構式
    //public DrinkShopItemAccessor()
    //{

    //}

    //類似工具箱概念，網頁一開始就提供方法給使用者使用
    public static class ShopItemAccessor
    {
        private static List<ShopItem> SelectByDataReader(SqlCommand SqlCommand)
        {
            List<ShopItem> ShopItems = new List<ShopItem>();

            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand.Connection = SqlConnection)
                {
                    SqlConnection.Open();

                    SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();

                    while (SqlDataReader.Read())
                    {
                        int Id = (int)SqlDataReader["Id"];
                        //OrderStateType OrderState = (OrderStateType)Enum.Parse(typeof(OrderStateType), (string)SqlDataReader["OrderState"]);
                        ShopType ShopType = (ShopType)Enum.Parse(typeof(ShopType), (string)SqlDataReader["ShopTpye"]);
                        string Name = (string)SqlDataReader["Name"];
                        string Memo = (string)SqlDataReader["Memo"];
                        string PhoneNumber = (string)SqlDataReader["PhoneNumber"];
                        string Address = (string)SqlDataReader["Address"];
                        DateTime UpdateTime = (DateTime)SqlDataReader["UpdateTime"];
                        DateTime CreateTime = (DateTime)SqlDataReader["CreateTime"];

                        ShopItem ShopItem = new ShopItem(Id, ShopType, Name, Memo, PhoneNumber, Address, UpdateTime, CreateTime);
                        ShopItems.Add(ShopItem);
                    }
                    return ShopItems;
                }
            }
        }

        internal static List<ShopItem> SelectAll()
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " Shop_ShopItem ";

            return SelectByDataReader(SqlCommand);
        }

        public static void UpdateInsert(ShopItem ShopItem)
        {
            UpdateInsert(new List<ShopItem>() { ShopItem });
        }

        public static void UpdateInsert(List<ShopItem> ShopItems)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                SqlConnection.Open();

                for (int i = 0; i < (ShopItems.Count / 100) + (ShopItems.Count % 100 == 0 ? 0 : 1); i++)
                {
                    List<ShopItem> PartShopItems = ShopItems.Skip(i *100).Take(100).ToList();
                    StringBuilder CommandText = new StringBuilder();

                    SqlCommand SqlCommand = SqlConnection.CreateCommand();

                    for(int j = 0; j < PartShopItems.Count; j++)
                    {
                        CommandText.Append("UPDATE "
                                            + " Shop_ShopItem "
                                            + "SET "
                                            + " Id = @Id" + j
                                            + " ,ShopType = @ShopType" + j
                                            + " ,Name = @Name" + j
                                            + " ,Memo = @Memo" + j
                                            + " ,PhoneNumber = @PhoneNumber" + j
                                            + " ,Address = @Address" + j
                                            + " ,UpdateTime = @UpdateTime" + j
                                            + " ,CreateTime = @CreateTime" + j
                                            + " WHERE "
                                            + " Id = @Id" + j

                                            + " IF @@ROWCOUNT = 0 "
                                            + "BEGIN "

                                            + "INSERT INTO "
                                            + " Shop_ShopItem "
                                            + "( Id, ShopType, Name, Memo, PhoneNumber, Address, UpdateTime, CreateTime) "
                                            + "VALUES "
                                            + "( @Id" + j + " , @ShopType" + j + " , @Name" + j + " , @Memo" + j + " , @PhoneNumber" + j + " , @Address" + j + " , @UpdateTime" + j + " , @CreateTime" + j + " )"

                                            + "END "
                                            );

                        if (PartShopItems[j].Id == -1) { PartShopItems[j].Id = ShopItemManager.GetNewId(); }

                        SqlCommand.Parameters.AddWithValue("Id" + j, PartShopItems[j].Id);

                        SqlCommand.Parameters.AddWithValue("ShopType" + j, PartShopItems[j].ShopType.ToString());

                        SqlCommand.Parameters.AddWithValue("Name" + j, PartShopItems[j].Name);
                        SqlCommand.Parameters.AddWithValue("Memo" + j, PartShopItems[j].Memo);
                        SqlCommand.Parameters.AddWithValue("PhoneNumber" + j, PartShopItems[j].PhoneNumber);
                        SqlCommand.Parameters.AddWithValue("Address" + j, PartShopItems[j].Address);
                        SqlCommand.Parameters.AddWithValue("UpdateTime" + j, PartShopItems[j].UpdateTime);
                        SqlCommand.Parameters.AddWithValue("CreateTime" + j, PartShopItems[j].CreateTime);

                        SqlCommand.CommandText = CommandText.ToString();
                        SqlCommand.ExecuteNonQuery();

                    }
                }
            }
        }

        public static void Delete(ShopItem ShopItem)
        {
            Delete(new List<ShopItem> { ShopItem });
        }

        public static void Delete(List<ShopItem> ShopItems)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "Delete "
                                           + " Shop_ShopItem "
                                           + "WHERE "
                                           + " Id IN ('" + string.Join("','", ShopItems.Select(p => p.Id)) + "') ";
                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}

using System;
using LeftHand.MemberShip;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Cart1.Order
{
    public static class OrderItemManager
    {
        private static int _CurrentId = 0;

        public static void Inital()
        {
            _CurrentId = GetLastId();
        }

        private static List<OrderItem> GetByDataReader(SqlCommand SqlCommand)
        {
            List<OrderItem> OrderItems = new List<OrderItem>();

            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand)
                {
                    SqlCommand.Connection = SqlConnection;
                    SqlConnection.Open();

                    SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();

                    while (SqlDataReader.Read())
                    {
                        int Id = (int)SqlDataReader["Id"];
                        bool IsCancel = (bool)SqlDataReader["IsCancel"];

                        CashFlowStateType CashFlowState = (CashFlowStateType)Enum.Parse(typeof(CashFlowStateType), (string)SqlDataReader["CashFlowState"]);
                        LogisticsStateType LogisticsState = (LogisticsStateType)Enum.Parse(typeof(CashFlowStateType), (string)SqlDataReader["LogisticsState"]);

                        string UserAccount = (string)SqlDataReader["UserAccount"];
                        string UserName = (string)SqlDataReader["UserName"];
                        string UserPhone = (string)SqlDataReader["UserPhone"];

                        string UserRemark = (string)SqlDataReader["UserRemark"];
                        string ManagerRemark = (string)SqlDataReader["ManagerRemark"];

                        DateTime UpdateTime = (DateTime)SqlDataReader["UpdateTime"];
                        DateTime CreateTime = (DateTime)SqlDataReader["CreateTime"];

                        List<OrderProductItem> OrderProductItems = OrderProductPaser((string)SqlDataReader["OrderProducts"]);

                        OrderItem OrderItem = new OrderItem(Id, IsCancel, UserAccount, UserName, UserPhone, OrderProductItems, CashFlowState, LogisticsState, UserRemark, ManagerRemark, UpdateTime, CreateTime);
                        OrderItems.Add(OrderItem);
                    }
                }
            }

            return OrderItems;
        }

        private static object GetByScalar(SqlCommand SqlCommand)
        {
            object Result = "";

            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand)
                {
                    SqlCommand.Connection = SqlConnection;

                    SqlConnection.Open();

                    Result = SqlCommand.ExecuteScalar();
                }
            }

            return Result;
        }

        public static OrderItem Get(int Id)
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " Order_OrderItem "
                                   + "WHERE "
                                   + " Id = @Id ";

            SqlCommand.Parameters.AddWithValue("Id", Id);

            return GetByDataReader(SqlCommand).FirstOrDefault();
        }

        public static List<OrderItem> Get(DateTime StartTime, DateTime EndTime)
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " Order_OrderItem "
                                   + "WHERE "
                                   + " CreateTime >= @StartDate "
                                   + "AND "
                                   + " CreateTime <= @EndDate ";

            SqlCommand.Parameters.AddWithValue("StartDate", StartTime.Date);
            SqlCommand.Parameters.AddWithValue("EndDate", EndTime.Date.AddDays(1).AddSeconds(-1));

            return GetByDataReader(SqlCommand);
        }

        public static List<OrderItem> Get(User User, int StartIndex, int EndIndex)
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "WITH ItemByPagger AS "
                                   + "( "
                                   + "SELECT "
                                   + " ROW_NUMBER() OVER( ORDER BY CreateTime DESC ) AS RowNumber , * "
                                   + "FROM "
                                   + " Order_OrderItem "
                                   + "WHERE "
                                   + " UserAccount = @UserAccount "
                                   + ") "

                                   + "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " ItemByPagger "
                                   + "WHERE "

                                   + " RowNumber BETWEEN " + StartIndex + " AND " + EndIndex;

            SqlCommand.Parameters.AddWithValue("UserAccount", User.Account);

            return GetByDataReader(SqlCommand);
        }

        public static List<OrderItem> Get(User User)
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " Order_OrderItem "
                                   + "WHERE "
                                   + " UserAccount = @UserAccount "
                                   + "ORDER BY"
                                   + " CreateTime DESC";

            SqlCommand.Parameters.AddWithValue("UserAccount", User.Account);

            return GetByDataReader(SqlCommand);
        }

        public static int GetAmount()
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " Count(*) "
                                   + "FROM "
                                   + " Order_OrderItem ";

            return (int)GetByScalar(SqlCommand);
        }

        public static void Save(OrderItem OrderItem)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "UPDATE "
                                             + " Order_OrderItem "
                                             + "SET "
                                             + " IsCancel = @IsCancel "
                                             + " ,UserAccount = @UserAccount "
                                             + " ,UserName = @UserName "
                                             + " ,UserPhone = @UserPhone "
                                             + " ,OrderProducts = @OrderProducts "
                                             + " ,CashFlowState = @CashFlowState "
                                             + " ,LogisticsState = @LogisticsState "
                                             + " ,TotalPrice = @TotalPrice"
                                             + " ,UserRemark = @UserRemark "
                                             + " ,ManagerRemark = @ManagerRemark "
                                             + " ,UpdateTime = @UpdateTime "
                                             + " ,CreateTime = @CreateTime  "
                                             + "WHERE "
                                             + " Id = @Id "

                                             + "IF @@ROWCOUNT = 0 "
                                             + "BEGIN "

                                             + "INSERT INTO "
                                             + " Order_OrderItem "
                                             + "( Id, IsCancel, UserAccount, UserName, UserPhone, OrderProducts, CashFlowState, LogisticsState, TotalPrice, UserRemark, ManagerRemark, UpdateTime, CreateTime ) "
                                             + "VALUES "
                                             + "( @Id, @IsCancel, @UserAccount, @UserName, @UserPhone, @OrderProducts, @CashFlowState, @LogisticsState, @TotalPrice, @UserRemark, @ManagerRemark, @UpdateTime, @CreateTime ) "
                                             + "END ";

                    if (OrderItem.Id == -1) { OrderItem.Id = GetNewId(); }
                    SqlCommand.Parameters.AddWithValue("Id", OrderItem.Id);
                    SqlCommand.Parameters.AddWithValue("IsCancel", OrderItem.IsCancel);

                    SqlCommand.Parameters.AddWithValue("UserAccount", OrderItem.UserAccount);
                    SqlCommand.Parameters.AddWithValue("UserName", OrderItem.UserName);
                    SqlCommand.Parameters.AddWithValue("UserPhone", OrderItem.UserPhone);

                    SqlCommand.Parameters.AddWithValue("OrderProducts", OrderProductStringPaser(OrderItem.OrderProductItems));
                    SqlCommand.Parameters.AddWithValue("CashFlowState", OrderItem.CashFlowState);
                    SqlCommand.Parameters.AddWithValue("LogisticsState", OrderItem.LogisticsState);

                    SqlCommand.Parameters.AddWithValue("TotalPrice", OrderItem.TotalPrice);

                    SqlCommand.Parameters.AddWithValue("UserRemark", OrderItem.UserRemark);
                    SqlCommand.Parameters.AddWithValue("ManagerRemark", OrderItem.ManagerRemark);

                    SqlCommand.Parameters.AddWithValue("UpdateTime", OrderItem.UpdateTime);
                    SqlCommand.Parameters.AddWithValue("CreateTime", OrderItem.CreateTime);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }

        public static void Remove(OrderItem OrderItem)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "Delete "
                                           + " Order_OrderItem "
                                           + "WHERE "
                                           + " Id = @Id ";

                    SqlCommand.Parameters.AddWithValue("Id", OrderItem.Id);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }

        private static int GetLastId()
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " ISNULL(MAX(Id),0) "
                                   + "FROM "
                                   + " Order_OrderItem ";

            return (int)GetByScalar(SqlCommand);
        }

        private static int GetNewId()
        {
            return (_CurrentId += 1);
        }

        private static string OrderProductStringPaser(List<OrderProductItem> OrderProductItems)
        {
            List<string> OrderProductInfos = new List<string>();
            foreach (OrderProductItem OrderProductItem in OrderProductItems)
            {
                Dictionary<string, string> Infos = new Dictionary<string, string>();
                Infos.Add("ProductItemId", OrderProductItem.ProductItemId);
                Infos.Add("Id", OrderProductItem.Id);
                Infos.Add("Name", OrderProductItem.Name.Replace(",", "，").Replace(":", "：").Replace("|", "│"));
                Infos.Add("OriginalPrice", OrderProductItem.OriginalPrice.Replace(",", "，").Replace(":", "：").Replace("|", "│"));
                Infos.Add("SalePrice", OrderProductItem.SalePrice.ToString());
                Infos.Add("Amount", OrderProductItem.Amount.ToString());

                OrderProductInfos.Add(string.Join(",", Infos.Select(i => i.Key + ":" + i.Value)));
            }

            return string.Join("|", OrderProductInfos);
        }

        private static List<OrderProductItem> OrderProductPaser(string OrderProductItemsString)
        {
            List<OrderProductItem> OrderProductItems = new List<OrderProductItem>();
            foreach (string InfosString in OrderProductItemsString.ToString().Split('|'))
            {
                Dictionary<string, string> Infos = new Dictionary<string, string>();

                foreach (string Info in InfosString.Split(','))
                {
                    string[] KeyValue = Info.Split(':');
                    Infos.Add(KeyValue[0], KeyValue[1]);
                }

                string ProductItemId = Infos["ProductItemId"];
                string ProductId = Infos["Id"];
                string Name = Infos["Name"];
                string OriginalPrice = Infos["OriginalPrice"];
                decimal SalePrice = decimal.Parse(Infos["SalePrice"]);
                int Amount = int.Parse(Infos["Amount"]);

                OrderProductItem OrderProductItem = new OrderProductItem(ProductItemId, ProductId, Name, OriginalPrice, SalePrice, Amount);

                OrderProductItems.Add(OrderProductItem);
            }

            return OrderProductItems;
        }
    }
}
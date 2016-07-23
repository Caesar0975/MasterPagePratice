using LeftHand.MemberShip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cart1.Order
{
    public enum OrderStateType { 新訂單, 處理中, 已完成, 已取消 };
    public enum CashFlowStateType { 未收款, 已收款, 退款 };
    public enum LogisticsStateType { 未出貨, 已出貨, 七天無退貨, 退貨 };

    /*
     新訂單：未收款 + 未出貨
     處理中：已收款 or 已出貨
     已完成：已收款 + 七天無退貨
     已取消：退款 or 退貨     
     */
    public class OrderItem
    {
        public int Id { get; set; }
        public bool IsCancel { get; set; }

        public string UserAccount { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }

        public List<OrderProductItem> OrderProductItems { get; set; }

        public OrderStateType OrderState
        {
            get
            {
                OrderStateType OrderState = new OrderStateType();

                if ((this.CashFlowState == CashFlowStateType.未收款) && (this.LogisticsState == LogisticsStateType.未出貨)) { OrderState = OrderStateType.新訂單; }
                else if ((this.CashFlowState == CashFlowStateType.已收款) && (this.LogisticsState == LogisticsStateType.七天無退貨)) { OrderState = OrderStateType.已完成; }
                else if ((this.CashFlowState == CashFlowStateType.退款) || (this.LogisticsState == LogisticsStateType.退貨)) { OrderState = OrderStateType.已取消; }
                else { OrderState = OrderStateType.處理中; }

                return OrderState;
            }
        }
        public CashFlowStateType CashFlowState { get; set; }
        public LogisticsStateType LogisticsState { get; set; }
        public decimal TotalPrice { get { return this.OrderProductItems.Sum(o => o.SubPrice); } }

        public string UserRemark { get; set; }
        public string ManagerRemark { get; set; }

        public DateTime UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }

        public OrderItem(User User)
        {
            this.Id = -1;
            this.IsCancel = false;
            this.UserAccount = User.Account;
            this.UserName = User.Profiles[ProfileKey.Name];
            this.UserPhone = User.Profiles[ProfileKey.Phone];

            this.OrderProductItems = new List<OrderProductItem>();
            this.CashFlowState = CashFlowStateType.未收款;
            this.LogisticsState = LogisticsStateType.未出貨;

            this.UserRemark = "";
            this.ManagerRemark = "";

            this.UpdateTime = DateTime.Now;
            this.CreateTime = DateTime.Now;
        }

        internal OrderItem(int Id, bool IsCancel, string UserAccount, string UserName, string UserPhone, List<OrderProductItem> OrderProductItems, CashFlowStateType CashFlowState, LogisticsStateType LogisticsState, string UserRemark, string ManagerRemark, DateTime UpdateTime, DateTime CreateTime)
        {
            this.Id = Id;
            this.IsCancel = IsCancel;
            this.UserAccount = UserAccount;
            this.UserName = UserName;
            this.UserPhone = UserPhone;

            this.OrderProductItems = OrderProductItems;
            this.CashFlowState = CashFlowState;
            this.LogisticsState = LogisticsState;

            this.UserRemark = UserRemark;
            this.ManagerRemark = ManagerRemark;

            this.UpdateTime = UpdateTime;
            this.CreateTime = CreateTime;
        }
    }
}
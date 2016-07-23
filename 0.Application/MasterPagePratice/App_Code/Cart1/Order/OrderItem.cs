using LeftHand.MemberShip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cart1.Order
{
    public enum OrderStateType { �s�q��, �B�z��, �w����, �w���� };
    public enum CashFlowStateType { ������, �w����, �h�� };
    public enum LogisticsStateType { ���X�f, �w�X�f, �C�ѵL�h�f, �h�f };

    /*
     �s�q��G������ + ���X�f
     �B�z���G�w���� or �w�X�f
     �w�����G�w���� + �C�ѵL�h�f
     �w�����G�h�� or �h�f     
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

                if ((this.CashFlowState == CashFlowStateType.������) && (this.LogisticsState == LogisticsStateType.���X�f)) { OrderState = OrderStateType.�s�q��; }
                else if ((this.CashFlowState == CashFlowStateType.�w����) && (this.LogisticsState == LogisticsStateType.�C�ѵL�h�f)) { OrderState = OrderStateType.�w����; }
                else if ((this.CashFlowState == CashFlowStateType.�h��) || (this.LogisticsState == LogisticsStateType.�h�f)) { OrderState = OrderStateType.�w����; }
                else { OrderState = OrderStateType.�B�z��; }

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
            this.CashFlowState = CashFlowStateType.������;
            this.LogisticsState = LogisticsStateType.���X�f;

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace OrderSystem.Shop
{
    public enum ShopType { 飲料店, 午餐店, 路邊攤 };

    public class ShopItem
    {
        public int Id { get; set; }
        public ShopType ShopType { get; set; }
        public string Name { get; set; }
        public string Memo { get; set; }
        
        ////表達11:00~13:00
        //public List<OpenTimeItem> OpenTimeItems { get; set; }
        
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }

        public ShopItem(ShopType ShopType, string Name, string Memo, string PhoneNumber, string Address)
        {
            this.Id = -1; //流水號
            this.ShopType = ShopType;
            this.Name = Name;
            this.Memo = Memo;


            this.PhoneNumber = PhoneNumber;
            this.Address = Address;
            this.UpdateTime = DateTime.Now;
            this.CreateTime = DateTime.Now;
        }

        internal ShopItem(int Id, ShopType ShopType, string Name, string Memo, string PhoneNumber, string Address, DateTime UpdateTime, DateTime CreateTime)
        {
            this.Id = Id; //流水號
            this.ShopType = ShopType;
            this.Name = Name;
            this.Memo = Memo;


            this.PhoneNumber = PhoneNumber;
            this.Address = Address;
            this.UpdateTime = UpdateTime;
            this.CreateTime = CreateTime;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Cart1.Order
{
    public class OrderProductItem
    {
        public string ProductItemId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string OriginalPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int Amount { get; set; }
        public decimal SubPrice { get { return SalePrice * Amount; } }

        public OrderProductItem(string ProductItemId, string Name, string OriginalPrice, decimal SalePrice, int Amount)
        {
            this.ProductItemId = ProductItemId;
            this.Id = Guid.NewGuid().ToString();
            this.Name = Name;
            this.OriginalPrice = OriginalPrice;
            this.SalePrice = SalePrice;
            this.Amount = Amount;
        }

        internal OrderProductItem(string ProductItemId, string Id, string Name, string OriginalPrice, decimal SalePrice, int Amount)
        {
            this.ProductItemId = ProductItemId;
            this.Id = Id;
            this.Name = Name;
            this.OriginalPrice = OriginalPrice;
            this.SalePrice = SalePrice;
            this.Amount = Amount;
        }
    }
}
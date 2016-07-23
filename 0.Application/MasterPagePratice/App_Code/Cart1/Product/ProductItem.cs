using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cart1.Product
{

    public class ProductItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public string OriginalPrice { get; set; }
        public string Discount { get; set; }
        public decimal SalePrice { get; set; }
        public int Inventory { get; set; }
        public int Sort { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }

        public ProductItem(string Name, List<string> Tags, string OriginalPrice, string Discount, decimal SalePrice, int Inventory, int Sort)
        {
            this.Id = "-1";
            this.Name = Name;
            this.Tags = Tags;
            this.OriginalPrice = OriginalPrice;
            this.Discount = Discount;
            this.SalePrice = SalePrice;
            this.Inventory = Inventory;
            this.Sort = Sort;
            this.UpdateTime = DateTime.Now;
            this.CreateTime = DateTime.Now;
        }

        internal ProductItem(string Id, string Name, List<string> Tags, string OriginalPrice, string Discount, decimal SalePrice, int Inventory, int Sort, DateTime UpdateTime, DateTime CreateTime)
        {
            this.Id = Id;
            this.Name = Name;
            this.Tags = Tags;
            this.OriginalPrice = OriginalPrice;
            this.Discount = Discount;
            this.SalePrice = SalePrice;
            this.Inventory = Inventory;
            this.Sort = Sort;
            this.UpdateTime = UpdateTime;
            this.CreateTime = CreateTime;
        }
    }
}
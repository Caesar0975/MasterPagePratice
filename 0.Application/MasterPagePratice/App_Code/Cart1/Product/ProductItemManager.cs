using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cart1.Product
{
    public static class ProductItemManager
    {
        private static List<ProductItem> ProductItemCache;

        public static void Inital()
        {
            ProductItemCache = ProductItemAccessor
                .SelectAll()
                .OrderByDescending(c => c.CreateTime)
                .ToList();
        }

        public static List<ProductItem> GetAll()
        {
            return ProductItemCache.ToList();
        }

        public static ProductItem Get(string Id)
        {
            return ProductItemCache.FirstOrDefault(c => c.Id == Id);
        }


        public static List<ProductItem> GetByTag(List<string> Tags, int Startindex, int EndIndex)
        {
            return ProductItemCache
                .Where(p => p.Tags.Intersect(Tags).Count() > 0)
                .Skip(Startindex)
                .Take(EndIndex - Startindex)
                .ToList();
        }

        public static int GetAmountByTag(List<string> Tags)
        {
            return ProductItemCache
                .Where(p => p.Tags.Intersect(Tags).Count() > 0)
                .Count();
        }

        public static void Save(ProductItem ProductItem)
        {
            Save(new List<ProductItem>() { ProductItem });
        }

        public static void Save(List<ProductItem> ProductItems)
        {
            foreach (ProductItem ProductItem in ProductItems)
            {
                ProductItem.UpdateTime = DateTime.Now;

                if (ProductItem.Id != "-1") { continue; }

                ProductItem.Id = Guid.NewGuid().ToString();
                ProductItem.Sort = GetNewSort();
            }

            //更新資料庫
            ProductItemAccessor.UpdateInsert(ProductItems);

            //更新記憶体
            ProductItemCache = ProductItemCache
                .Union(ProductItems)
                .OrderByDescending(c => c.CreateTime)
                .ToList();
        }

        private static int GetNewSort()
        {
            if (ProductItemCache.Count == 0) { return 1; }

            return ProductItemCache.Max(p => p.Sort) + 1;
        }

        public static void Remove(ProductItem ProductItem)
        {
            //更新資料庫
            ProductItemAccessor.Delete(ProductItem);

            //更新記憶体
            ProductItemCache.Remove(ProductItem);
        }
    }
}
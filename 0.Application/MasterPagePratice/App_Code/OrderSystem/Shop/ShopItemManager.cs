using OrderSystem.Shop;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrderSystem.Shop
{

    public static class ShopItemManager
    {
        private static int LastestId;
        public static List<ShopItem> ShopItemCache;

        public static void Inital()
        {
            LastestId = (GetAll().Last() == null) ? 0 : GetAll().Last().Id;
            ShopItemCache = ShopItemAccessor.SelectAll().OrderByDescending(ShopItem => ShopItem.Id).ToList();
        }

        public static List<ShopItem> GetAll()
        {
            return ShopItemCache.OrderByDescending(ShopItem => ShopItem.Id).ToList();
        }

        public static ShopItem Get(int Id)
        {
            return ShopItemCache.FirstOrDefault(ShopItem => ShopItem.Id == Id);
        }

        public static void Save(ShopItem ShopItem)
        {
            Save(new List<ShopItem>() { ShopItem });
        }

        public static void Save(List<ShopItem> ShopItems)
        {
            foreach(ShopItem ShopItem in ShopItems)
            {
                ShopItem.UpdateTime = DateTime.Now;

                if(ShopItem.Id != -1) { continue; }

                ShopItem.Id = GetNewId();
            }

            //更新資料庫
            ShopItemAccessor.UpdateInsert(ShopItems);

            //更新快取
            ShopItemCache = ShopItemCache.Union(ShopItems).OrderByDescending(ShopItem => ShopItem.Id).ToList();

        }

        public static void Remove(ShopItem ShopItem)
        {
            //更新資料庫
            ShopItemAccessor.Delete(ShopItem);

            //更新記憶体
            ShopItemCache.Remove(ShopItem);
        }

        internal static int GetNewId()
        {
            LastestId += 1;
            return LastestId;
        }
    }
}
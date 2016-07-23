using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cart1.Product
{
    public static class TagGroupItemManager
    {
        private static List<TagGroupItem> TagGroupItemCache;

        public static void Inital()
        {
            TagGroupItemCache = TagGroupItemAccessor
                .SelectAll()
                .OrderBy(c => c.Sort)
                .ThenBy(c => c.CreateTime)
                .ToList();
        }

        public static List<TagGroupItem> GetAll()
        {
            return TagGroupItemCache.ToList();
        }

        public static TagGroupItem Get(string Id)
        {
            return TagGroupItemCache.FirstOrDefault(c => c.Id == Id);
        }

        public static TagGroupItem GetByName(string Name)
        {
            return TagGroupItemCache.FirstOrDefault(c => c.Name == Name);
        }

        public static List<TagGroupItem> Get(int Startindex, int EndIndex)
        {
            return TagGroupItemCache.Skip(Startindex).Take(EndIndex - Startindex).ToList();
        }

        public static void Save(TagGroupItem TagGroupItem)
        {
            if (TagGroupItem.Id == "-1") { TagGroupItem.Id = Guid.NewGuid().ToString(); }

            TagGroupItem.UpdateTime = DateTime.Now;

            //更新資料庫
            TagGroupItemAccessor.UpdateInsert(TagGroupItem);

            //更新記憶体
            TagGroupItemCache.Remove(TagGroupItem);
            TagGroupItemCache.Add(TagGroupItem);

            TagGroupItemCache = TagGroupItemCache.OrderBy(c => c.Sort).ThenBy(c => c.CreateTime).ToList();
        }

        public static void Remove(TagGroupItem TagGroupItem)
        {
            //更新資料庫
            TagGroupItemAccessor.Delete(TagGroupItem);

            //更新記憶体
            TagGroupItemCache.Remove(TagGroupItem);
        }
    }
}
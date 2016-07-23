using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cart1.News
{
    public class NewsItemManager
    {
        private static List<NewsItem> NewsItemCache = new List<NewsItem>();

        public static void Inital()
        {
            NewsItemAccessor.SelectAll(ref NewsItemCache);

            NewsItemCache = NewsItemCache.OrderByDescending(n => n.CreateTime).ToList();
        }

        public static List<NewsItem> GetAll()
        {
            return NewsItemCache.ToList();
        }

        public static List<NewsItem> Get(int StartIndex, int EndIndex)
        {
            StartIndex--;
            EndIndex--;

            return NewsItemCache.Where((a, index) => index >= StartIndex && index <= EndIndex).ToList();
        }

        public static NewsItem Get(string Id)
        {
            return NewsItemCache.FirstOrDefault(n => n.Id == Id);
        }

        public static List<NewsItem> Get(DateTime StartTime)
        {
            return NewsItemCache
                .FindAll(n => StartTime <= n.OnShelfTime)
                .FindAll(n => n.OnShelfTime <= DateTime.Now && n.OffShelfTime >= DateTime.Now);
        }

        public static int GetAmount()
        {
            return NewsItemCache.Count;
        }


        public static void Save(NewsItem NewsItem)
        {
            //資料庫
            NewsItemAccessor.UpdateInsert(NewsItem);

            //記憶體
            NewsItemCache.Remove(NewsItem);
            NewsItemCache.Add(NewsItem);

            NewsItemCache.OrderByDescending(n => n.CreateTime).ToList();
        }

        public static void Remove(NewsItem NewsItem)
        {
            //資料庫
            NewsItemAccessor.Delete(NewsItem);

            //記憶體
            NewsItemCache.Remove(NewsItem);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cart1.News
{
    public class NewsItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime OnShelfTime { get; set; }
        public DateTime OffShelfTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }

        public NewsItem(string Title, string Content, DateTime OnShelfTime, DateTime OffShelfTime)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Title = Title;
            this.Content = Content;
            this.OnShelfTime = OnShelfTime;
            this.OffShelfTime = OffShelfTime;
            this.UpdateTime = DateTime.Now;
            this.CreateTime = DateTime.Now;
        }

        internal NewsItem(string Id, string Title, string Content, DateTime OnShelfTime, DateTime OffShelfTime, DateTime UpdateTime, DateTime CreateTime)
        {
            this.Id = Id;
            this.Title = Title;
            this.Content = Content;
            this.OnShelfTime = OnShelfTime;
            this.OffShelfTime = OffShelfTime;
            this.UpdateTime = UpdateTime;
            this.CreateTime = CreateTime;
        }
    }
}
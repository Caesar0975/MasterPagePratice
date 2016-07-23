using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cart1.Product
{

    public class TagGroupItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public int Sort { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }

        public TagGroupItem(string Name, int Sort)
        {
            this.Id = "-1";
            this.Name = Name;
            this.Tags = new List<string>();
            this.Sort = Sort;
            this.UpdateTime = DateTime.Now;
            this.CreateTime = DateTime.Now;
        }

        internal TagGroupItem(string Id, string Name, List<string> Tags, int Sort, DateTime UpdateTime, DateTime CreateTime)
        {
            this.Id = Id;
            this.Name = Name;
            this.Tags = Tags;
            this.Sort = Sort;
            this.UpdateTime = UpdateTime;
            this.CreateTime = CreateTime;
        }
    }
}
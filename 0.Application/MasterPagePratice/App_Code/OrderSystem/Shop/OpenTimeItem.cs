using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace OrderSystem.Shop
{

    public class OpenTimeItem
    {
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        

        public OpenTimeItem(DateTime OpenTime, DateTime CloseTime)
        {
            this.OpenTime = OpenTime;
            this.CloseTime = CloseTime;
        }

        //internal OpenTimeItem(DateTime OpenTime, DateTime OpenTime)
        //{
            
        //}
    }
}
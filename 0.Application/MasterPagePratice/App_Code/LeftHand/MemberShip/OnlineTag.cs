using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeftHand.MemberShip
{
    public class OnlineTag
    {
        public string Account { get; internal set; }
        public string SessionId { get; internal set; }
        public DateTime ReflashTime { get; internal set; }
        public DateTime CreateTime { get; internal set; }

        public TimeSpan OnlineTime { get { return ReflashTime.Subtract(CreateTime); } }//在線時間(可能會有最多10分鐘的誤差)
    }
}
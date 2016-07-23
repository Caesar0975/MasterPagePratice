using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeftHand.MemberShip
{
    public class SwitchUserLog
    {
        public string Id { get; internal set; }
        public string Account { get; internal set; }
        public string Ip { get; internal set; }
        public string Remark { get; internal set; }
        public DateTime CreateTime { get; internal set; }

        internal SwitchUserLog(string Id, string Account, string Ip, string Remark, DateTime CreateTime)
        {
            this.Id = Id;
            this.Account = Account;
            this.Ip = Ip;
            this.Remark = Remark;
            this.CreateTime = CreateTime;

        }
    }
}
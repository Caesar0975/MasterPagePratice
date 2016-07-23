using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeftHand.MemberShip
{
    public static partial class SwitchUserManager
    {
        //建立一個新的登入紀錄(因為一次登入只會建立一筆登入紀錄，所以就不採用GetNew的方式撰寫了)
        private static SwitchUserLog CreateSwitchUserLog(User User, string Remark)
        {
            SwitchUserLog NewSwitchUserLog = new SwitchUserLog(Guid.NewGuid().ToString(), User.Account, HttpContext.Current.Request.UserHostAddress, Remark, DateTime.Now);

            //寫入資料庫，並限制最多保留3天內的紀錄就好
            SwitchUserAccessor.InsertDelete(NewSwitchUserLog, 3);

            return NewSwitchUserLog;
        }

        //取得User的LoginRecoder        
        public static List<SwitchUserLog> GetSwitchUserLog(User User)
        {
            return SwitchUserAccessor.Select(User);
        }
    }
}
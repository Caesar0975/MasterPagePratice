using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeftHand.MemberShip
{
    public static partial class UserManager
    {
        //取得Role中所有包含的User
        public static List<User> GetRoleUsers(RoleKey RoleKey)
        {
            return UserCache.Values
                               .Where(u => u.Roles.Contains(RoleKey))
                               .ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeftHand.MemberShip
{
    public static partial class UserManager
    {
        //取得Child
        public static List<User> GetChilds(User Parent)
        {
            return UserCache.Values.Where(u => u.Parent == Parent).ToList();
        }

        //取得AllParent
        public static List<User> GetAllParent(User Child, bool WithSelf = true)
        {
            List<User> Parents = new List<User>();

            if (WithSelf == true) { Parents.Add(Child); }

            FindAllParent(ref Parents, Child);

            return Parents;
        }

        //找出所有的Parent
        private static void FindAllParent(ref List<User> Parents, User Child)
        {
            User Parent = Child.Parent;

            if (Parent == null) { return; }

            Parents.Add(Parent);

            FindAllParent(ref Parents, Parent);
        }

        //取得AllChild
        public static List<User> GetAllChilds(User Child, bool WithSelf = true)
        {
            List<User> Childs = new List<User>();

            if (WithSelf == true) { Childs.Add(Child); }

            FindAllChilds(ref Childs, Child);

            return Childs;
        }

        //根據RoleKey取得相關的AllChild
        public static List<User> GetAllChilds(User Child, RoleKey RoleKey, bool WithSelf = true)
        {
            List<User> Childs = new List<User>();

            if (WithSelf == true) { Childs.Add(Child); }

            FindAllChilds(ref Childs, Child);

            return Childs.Where(c => c.Roles.Contains(RoleKey)).ToList();
        }

        //找出所有的Child
        private static void FindAllChilds(ref List<User> ChildList, User Parent)
        {
            List<User> Childs = GetChilds(Parent);

            ChildList.AddRange(Childs);

            foreach (User Child in Childs)
            {
                FindAllChilds(ref ChildList, Child);
            }
        }
    }
}
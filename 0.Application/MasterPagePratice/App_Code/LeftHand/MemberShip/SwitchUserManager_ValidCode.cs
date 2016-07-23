using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeftHand.MemberShip
{
    public static partial class SwitchUserManager
    {
        //產生新的驗證碼
        public static string GetNewValidCode(bool OnlyNumber = true)
        {
            string ValidCode = "";

            switch (OnlyNumber)
            {
                case true:
                    ValidCode = new Random().Next(1111, 9999).ToString();
                    break;

                case false:
                    ValidCode = Guid.NewGuid().ToString().Split('-')[0].Substring(0, 4);
                    break;
            }

            ValidCode = ValidCode.ToUpper();

            HttpContext.Current.Session["LoginManager_ValidCode"] = ValidCode;
            return ValidCode;
        }

        //取得現在的驗證碼
        public static string GetCurrentValidCode()
        {
            object SessionState = HttpContext.Current.Session["LoginManager_ValidCode"];

            if (SessionState == null) { return ""; }

            return SessionState.ToString(); 
        }

        //檢查驗證碼是否正確
        public static bool CompareValidCode(string UserInputValidCode)
        {
            UserInputValidCode = UserInputValidCode.ToUpper();

            if (UserInputValidCode != GetCurrentValidCode()) { return false; }

            return true;
        }

    }
}
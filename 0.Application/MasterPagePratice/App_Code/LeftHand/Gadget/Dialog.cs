using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace LeftHand.Gadget
{
    public class Dialog
    {
        public static void AlertWithCloseFancybox(string Message)
        {
            Alert(Message);

            Page Page = (Page)HttpContext.Current.Handler;
            ScriptManager.RegisterStartupScript(Page, typeof(string), System.Guid.NewGuid().ToString(), "window.parent.$.fancybox.close();", true);
        }

        public static void Alert(string Message)
        {
            Message = HttpContext.Current.Server.HtmlEncode(Message);

            Page Page = (Page)HttpContext.Current.Handler;
            ScriptManager.RegisterStartupScript(Page, typeof(string), System.Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
        }

    }
}
using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Drawing;
using System.Text.RegularExpressions;

namespace LeftHand.Gadget
{
    public class String
    {
        public static string RemoveHtmlTag(string DirtyString)
        {
            return Regex.Replace(DirtyString, "(?is)<.+?>", "");

        }
    }
}
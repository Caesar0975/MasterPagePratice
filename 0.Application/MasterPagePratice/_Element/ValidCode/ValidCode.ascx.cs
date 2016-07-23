using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Element_RandomNumber_RandomNumber : System.Web.UI.UserControl
{
    string _Code = "";

    public string SetCode
    {
        set
        {
            string EncoderCode = LeftHand.Gadget.Encoder.AES_Encryption(value);
            _Code = EncoderCode;
        }
    }

    protected void Page_Prerender(object sender, EventArgs e)
    {
        //只有數字
        //int RandomCode = new Random().Next(1111, 9999);

        //英文+數字
        //Session["RandomNumber_Code"] = Guid.NewGuid().ToString().ToUpper().Replace("0", "8").Replace("O", "Q").Substring(0, 4);
        this.Image1.ImageUrl = "~/_Element/ValidCode/RandomNumberImage.ashx?Code=" + Server.UrlEncode(this._Code);

        //測試的時候使用，正式的時候要刪掉或註解掉
        //Session["RandomNumber_Code"] = "111";
    }


}

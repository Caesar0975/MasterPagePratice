<%@ WebHandler Language="C#" Class="RandomNumberImage" %>

using System;
using System.Web;
using System.Drawing;
using System.Web.SessionState;

public class RandomNumberImage : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string PlainCode = LeftHand.Gadget.Encoder.AES_Decryption(context.Request["Code"]);
        CreateCheckCodeImage(PlainCode, context);
    }

    private void CreateCheckCodeImage(string checkCode, HttpContext context)
    {
        System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 17.0)), 18);
        Graphics g = Graphics.FromImage(image);
        Random random = new Random();

        //清空背景色
        g.Clear(Color.White);

        //增加背景雜線
        for (int i = 0; i < 15; i++)
        {
            int x1 = random.Next(image.Width);
            int x2 = random.Next(image.Width);
            int y1 = random.Next(image.Height);
            int y2 = random.Next(image.Height);

            g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
        }

        Font font = new System.Drawing.Font("Arial", 13, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
        System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Red, Color.Red, 1.2f, true);
        g.DrawString(checkCode, font, brush, 12, 0);

        //躁點產生
        for (int i = 0; i < 100; i++)
        {
            int x = random.Next(image.Width);
            int y = random.Next(image.Height);
            image.SetPixel(x, y, Color.FromArgb(random.Next()));
        }

        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        context.Response.ClearContent();
        context.Response.ContentType = "image/Gif";
        context.Response.BinaryWrite(ms.ToArray());
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
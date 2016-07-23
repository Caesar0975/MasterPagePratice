using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Drawing;

namespace LeftHand.Gadget
{
    public class Graphics
    {
        public enum ResizeType { InSide, OutSide }

        public static Bitmap Resize(Bitmap original_image, int NewWidth, int NewHeight)
        {
            int width = original_image.Width;
            int height = original_image.Height;

            //計算圖片要縮小的比例
            //decimal ResizePercentage = 0;
            //decimal WithResizePercentage = (decimal)width / NewWidth;
            //decimal HeightResizePercentage = (decimal)height / NewHeight;

            //if (WithResizePercentage < HeightResizePercentage)
            //{ ResizePercentage = HeightResizePercentage; }
            //else
            //{ ResizePercentage = WithResizePercentage; }
            decimal ResizePercentage = (decimal)width / NewWidth;

            //強制要管寬就好了

            //取得小圖要繪製的部分
            int CutWidth = Convert.ToInt32(width / ResizePercentage);
            int CutHight = Convert.ToInt32(height / ResizePercentage);

            //填入要縮小的寬度與高度
            Bitmap bmp = new Bitmap(CutWidth, CutHight);


            //高品質縮圖
            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, CutWidth, CutHight);
            gr.DrawImage(original_image, rectDestination, 0, 0, width, height, GraphicsUnit.Pixel);

            return bmp;
        }
    }
}
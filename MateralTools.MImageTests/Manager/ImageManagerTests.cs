using Microsoft.VisualStudio.TestTools.UnitTesting;
using MateralTools.MImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Web;

namespace MateralTools.MImage.Tests
{ 
    [TestClass()]
    public class ImageManagerTests
    {
        [TestMethod()]
        public void AddWaterMarkTest()
        {
            Bitmap WaterMarkImg = new Bitmap(@"D:\Images\WaterMarkImg.png");
            Bitmap BackImg = new Bitmap(@"D:\Images\banner.png");
            int Width = BackImg.Width - WaterMarkImg.Width;
            int Height = BackImg.Height - WaterMarkImg.Height - 15;
            if (Width > 0 && Height > 0)
            {
                Point p = new Point(Width, Height);
                Bitmap img = ImageManager.AddWaterMark(BackImg, WaterMarkImg, p);
                img.Save(@"D:\Images\test.jpg");
            }
        }

        [TestMethod()]
        public void GetWaterMarkImageByStrTest()
        {
            Bitmap img = ImageManager.GetWaterMarkImageByStr("Materal", new Size(100, 100));
            img.Save(@"D:\Images\test.jpg");
        }
    }
}
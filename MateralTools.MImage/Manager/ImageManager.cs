using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MImage
{
    public class ImageManager
    {
        /// <summary>
        /// 添加水印
        /// </summary>
        /// <param name="img">要添加水印的图片</param>
        /// <param name="waterMarkImg">水印图片</param>
        /// <param name="waterPosition">水印图片位置</param>
        /// <returns>添加过水印的图片</returns>
        public static Bitmap AddWaterMark(Bitmap img, Bitmap waterMarkImg, Point waterPosition)
        {
            Graphics graphics = Graphics.FromImage(img);
            int width = waterMarkImg.Width;
            int height = waterMarkImg.Height;
            graphics.DrawImage(waterMarkImg, new Rectangle(waterPosition.X, waterPosition.Y, width, height), 0, 0, width, height, GraphicsUnit.Pixel);
            return img;
        }
        /// <summary>
        /// 添加水印
        /// </summary>
        /// <param name="imgPath">要添加水印的图片地址</param>
        /// <param name="waterMarkImg">水印图片</param>
        /// <param name="waterPosition">水印图片位置</param>
        /// <returns>添加过水印的图片</returns>
        public static Bitmap AddWaterMark(string imgPath, Bitmap waterMarkImg, Point waterPosition)
        {
            Bitmap img = new Bitmap(imgPath);
            return AddWaterMark(img, waterMarkImg, waterPosition);
        }
        ///// <summary>
        ///// 水印位置枚举
        ///// </summary>
        //public enum WaterMarkPositionEnum
        //{
        //    /// <summary>
        //    /// 上方
        //    /// </summary>
        //    Top,
        //    /// <summary>
        //    /// 左上
        //    /// </summary>
        //    TopLeft,
        //    /// <summary>
        //    /// 右上
        //    /// </summary>
        //    TopRight,
        //    /// <summary>
        //    /// 下方
        //    /// </summary>
        //    Lower,
        //    /// <summary>
        //    /// 左下
        //    /// </summary>
        //    LowerLeft,
        //    /// <summary>
        //    /// 右下
        //    /// </summary>
        //    LowerRight,
        //    /// <summary>
        //    /// 左方
        //    /// </summary>
        //    Left,
        //    /// <summary>
        //    /// 右方
        //    /// </summary>
        //    Right,
        //    /// <summary>
        //    /// 中间
        //    /// </summary>
        //    Middle
        //}
        ///// <summary>
        ///// 添加水印
        ///// </summary>
        ///// <param name="imgPath">要添加水印的图片地址</param>
        ///// <param name="waterMarkStr">水印文字</param>
        ///// <param name="waterPosition">水印图片位置</param>
        ///// <param name="waterSize">水印图片大小</param>
        ///// <returns>添加过水印的图片</returns>
        //public static Bitmap AddWaterMark(string imgPath, string waterMarkStr, Point waterPosition, Size waterSize)
        //{
        //    Bitmap img = new Bitmap(imgPath);
        //    Bitmap waterMarkImg = GetWaterMarkImageByStr(waterMarkStr, waterSize);
        //    return AddWaterMark(img, waterMarkImg, waterPosition);
        //}
        ///// <summary>
        ///// 添加水印
        ///// </summary>
        ///// <param name="img">要添加水印的图片</param>
        ///// <param name="waterMarkStr">水印文字</param>
        ///// <param name="waterPosition">水印图片位置</param>
        ///// <param name="waterSize">水印图片大小</param>
        ///// <returns>添加过水印的图片</returns>
        //public static Bitmap AddWaterMark(Bitmap img, string waterMarkStr, Point waterPosition, Size waterSize)
        //{
        //    Bitmap waterMarkImg = GetWaterMarkImageByStr(waterMarkStr, waterSize);
        //    return AddWaterMark(img, waterMarkImg, waterPosition);
        //}
        /// <summary>
        /// 根据水印文字获得水印图片
        /// </summary>
        /// <param name="waterMarkStr">水印文字</param>
        /// <param name="waterPosition">水印图片位置</param>
        /// <param name="waterSize">水印图片大小</param>
        /// <returns>水印图片</returns>
        public static Bitmap GetWaterMarkImageByStr(string waterMarkStr, Size waterSize)
        {
            Bitmap img = new Bitmap(waterSize.Width, waterSize.Height);
            Graphics g = Graphics.FromImage(img);
            g.FillRectangle(Brushes.White, new Rectangle() { X = 0, Y = 0, Height = waterSize.Height, Width = waterSize.Width });
            Font font = new Font("宋体", 10);
            g.DrawString(waterMarkStr, font, Brushes.Black, new PointF() { X = 0, Y = 0 });
            return img;
        }
    }
}

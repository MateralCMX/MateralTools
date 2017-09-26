using Microsoft.VisualStudio.TestTools.UnitTesting;
using MateralTools.MVerify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using MateralTools.MVerify.Model;

namespace MateralTools.MVerify.Tests
{
    [TestClass()]
    public class VerifyCodeManagerTests
    {
        [TestMethod()]
        public void GetVeifyCodeModelTest()
        {
            VerifyCodeManager vcMa = new VerifyCodeManager();
            /*背景是否为图片还是纯色*/
            vcMa.TextConfigM.BackIsImage = true;
            /*背景图片路径(BackIsImage==true时生效)*/
            vcMa.TextConfigM.ImageBackgroundPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\Content\Images";
            /*背景颜色库(BackIsImage==false时生效)*/
            vcMa.TextConfigM.BackgroundColors.Add(Color.Ivory);
            /*值颜色库*/
            vcMa.TextConfigM.ValueColors.Add(Color.Red);
            /*文本库(AllowRandomChinese==true时失效)*/
            vcMa.TextConfigM.TextLibrary.Add("陈明旭中华人民共和国");
            /*采用随机中文模式*/
            vcMa.TextConfigM.AllowRandomChinese = true;
            /*值数量*/
            vcMa.TextConfigM.ValueCount = 4;
            /*混淆数量*/
            vcMa.TextConfigM.ConfusionCount = 10;
            /*图片大小*/
            vcMa.TextConfigM.ImageSize = new Size(120, 30);
            /*字体大小*/
            vcMa.TextConfigM.FontSize = 18;
            /*获取验证码*/
            VerifyCodeModel vcM = vcMa.GetVeifyCodeModel();
            if (vcM.Images.Count > 0)
            {
                string value = vcM.Value;
                vcM.Images[0].Save(@"D:\Images\Temp\VerifyCode.png");
            }
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MateralTools.MWeChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MateralTools.MWeChat.Tests
{
    [TestClass()]
    public class WeChatWorkManagerTests
    {
        private WeChatWorkManager weChatWorkMa;
        public WeChatWorkManagerTests()
        {
            weChatWorkMa = new WeChatWorkManager("wwa064532e24a2cde8", "IG3fgeQkADQgpAb0VQhJpsmT7JPB3CnC2TJhiOmH06U");
        }
        [TestMethod()]
        public void GetAccessTokenTest()
        {
            WeChatWorkAccessTokenModel model = weChatWorkMa.GetAccessToken();
        }
        [TestMethod()]
        public void GetUserInfoTest()
        {
            WeChatWorkUserInfoModel model = weChatWorkMa.GetUserInfo("FAjYwe-YGYw51GBTJk0dEHC93VvsAEF8mnldRqIsNOg");
        }

        [TestMethod()]
        public void GetJsapiTicketTest()
        {
            WeChatWorkJsapiTicketModel model = weChatWorkMa.GetJsapiTicket();
        }

        [TestMethod()]
        public void GetJSSDKConfigInfoTest()
        {
            string[] jsApiLists = { "qwe", "asdf" };
            WeChatWorkJSSDKConfigModel model = weChatWorkMa.GetJSSDKConfigInfo("http://mp.weixin.qq.com", jsApiLists);
        }

        [TestMethod()]
        public void GetSignatureTest()
        {
            string signature = weChatWorkMa.GetSignature(1511861392,
                "http://localhost:39182/Views/EditBluePrint.html?BluePrintDetailedID=8f24a615-9634-45ff-844b-8d3618a19ebe",
                "Materal",
                "HoagFKDcsGMVCIY2vOjf9puj5Nu6JtjQf83kBqTzXnSckHDZq6dfz_zVKxiwys8g_Y4Wyw-n1PISK3v2tjsioA");
        }

        [TestMethod()]
        public void DownloadImageTest()
        {
            weChatWorkMa.DownloadImage("1EQdQmLe24mQqgAKtJyq8rBymYZcC-SYrO8HbstEI3uae1fQ-eF6V4UDj9mgoM4EM", "D:\\Images\\Temp\\1.jpg");
        }
    }
}
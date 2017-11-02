using Microsoft.VisualStudio.TestTools.UnitTesting;
using MateralTools.MWeChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MateralTools.MConvert;
using MateralTools.MCache;

namespace MateralTools.MWeChat.Tests
{
    [TestClass()]
    public class WeChatManagerTests
    {
        private WeChatManager WXMa = new WeChatManager();
        public WeChatManagerTests()
        {
            #region 测试代码--微信Token
            WeChatManager.APPID = "wxf4984c388e518385";
            WeChatManager.AppSecret = "1115885c9c4c4dd55915aadd4d2fa7bf";
            string resStr = "{\"access_token\":\"zWtGZHO2EHfig-nBhIUb0XtQrh88kqDXV4RalOUvZi22tFELCKieW30O85aQafsH8DOKX1HS-J1Scb7Bdi7jShgrLg4Jc8_sBxcFuqYnGyf5QplcIZLXwOt7tHlcxDKEJPFaAIAXDT\",\"expires_in\":7200}";
            WeChatTokenModel tempTokenM = ConvertManager.JsonToModel<WeChatTokenModel>(resStr);
            WebCacheManager.Set("MATERALWECHATTOKENKEY", tempTokenM, DateTimeOffset.Now.AddSeconds(tempTokenM.expires_in - 60));
            #endregion
        }
        [TestMethod()]
        public void GetWeChatTokenTest()
        {
            WeChatTokenModel tokenM = WXMa.GetWeChatToken();
        }

        [TestMethod()]
        public void SetWeChatMenuTest()
        {
            WeChatMenuModel wxMenuM = new WeChatMenuModel
            {
                button = new WeChatMenuButtonModel[1]
            };
            wxMenuM.button[0] = new WeChatMenuButtonModel
            {
                name = "测试菜单",
                type = "view",
                url = "https://www.gudianbu.com"
            };
            WXMa.SetWeChatMenu(wxMenuM);
        }
    }
}
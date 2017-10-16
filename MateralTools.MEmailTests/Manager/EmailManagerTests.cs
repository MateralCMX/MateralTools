using Microsoft.VisualStudio.TestTools.UnitTesting;
using MateralTools.MEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace MateralTools.MEmail.Tests
{
    [TestClass()]
    public class EmailManagerTests
    {
        [TestMethod()]
        public void SendTest()
        {
            string[] targetEmail =
            {
                "cloomcmx1554@hotmail.com",
                "van.w@126.com"
            };
            string Title = ".NET发送过来的邮件";
            string Contents = "这是一封通过.NET发送过来的邮件，发送时间: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            EmailManager EmailMa = null;
            EmailMa =new EmailManager("Materal", "342860484@qq.com", targetEmail);
            EmailMa.QQSend(Title, Contents, "fmxibipkrbakcajh");
            EmailMa = new EmailManager("全球什么值得买", "noreply@qsmzdm.com", targetEmail);
            EmailMa.Send(Title, Contents,  "Wb2229900", "smtp.mxhichina.com", 25);
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MateralTools.MEncryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MateralTools.MEncryption.Tests
{
    [TestClass()]
    public class EncryptionManagerTests
    {
        [TestMethod()]
        public void MD5Encode_32Test()
        {
            string str = EncryptionManager.MD5Encode_32("1032");
        }

        [TestMethod()]
        public void QRCodeEncodeTest()
        {
            string inputStr = "http://119.23.71.44:8900/Paper/ExamIndex?ClassID=18db7ac7-b60d-406c-9d81-b74be46ae4aa&PaperID=6d08e87f-672a-4b20-aa55-eb35774f48a4";
            Bitmap img = EncryptionManager.QRCodeEncode(inputStr);
        }
    }
}
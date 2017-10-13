using Microsoft.VisualStudio.TestTools.UnitTesting;
using MateralTools.MEncryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MateralTools.MVerify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MVerify.Tests
{
    [TestClass()]
    public class VerifyManagerTests
    {
        [TestMethod()]
        public void IsIDCardForChinaTest()
        {
            bool resM = VerifyManager.IsIDCardForChina("532101199304200035", true);
        }
    }
}
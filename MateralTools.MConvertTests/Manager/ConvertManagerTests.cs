using Microsoft.VisualStudio.TestTools.UnitTesting;
using MateralTools.MConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MConvert.Tests
{
    [TestClass()]
    public class ConvertManagerTests
    {
        [TestMethod()]
        public void StrToBinaryStrTest()
        {
            string str = ConvertManager.StrToBinaryStr("PT这个废物");
        }
    }
}
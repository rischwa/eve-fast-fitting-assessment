using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrestSharp.Infrastructure;
using CrestSharp.Model;
using CrestSharp.Model.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrestSharpTests
{
    [TestClass]
    public class CrestJsonConverterTest
    {
        [TestMethod]
        public void TestAllTypeMappingsAreDetected()
        {
            Assert.AreEqual(33, CrestJsonConverter.CONVERSIONS.Keys.Count);
            Assert.AreEqual(typeof(CrestSolarSystem),CrestJsonConverter.CONVERSIONS[typeof(ICrestSolarSystem)]);
        }
    }
}

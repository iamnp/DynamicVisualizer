using DynamicVisualizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicVisualizerTest
{
    [TestClass]
    public class DoubleToStrTest
    {
        [TestMethod]
        public void TestDoubleToStr()
        {
            Assert.AreEqual("1", 1.0.Str());
            Assert.AreEqual("-1", (-1.0).Str());

            Assert.AreEqual("0,5", 0.5.Str());
            Assert.AreEqual("-0,5", (-0.5).Str());

            Assert.AreEqual("1000", 1000.0.Str());
            Assert.AreEqual("-1000", (-1000.0).Str());

            Assert.AreEqual("0", 1e-13.Str());
            Assert.AreEqual("0", (-1e-13).Str());

            Assert.AreEqual("0,00000001", 1e-8.Str());
            Assert.AreEqual("-0,00000001", (-1e-8).Str());

            Assert.AreEqual("10000000000", 1e10.Str());
            Assert.AreEqual("-10000000000", (-1e10).Str());
        }
    }
}
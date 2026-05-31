using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace tarkov_settings.Tests
{
    [TestClass]
    public class NvidiaTests
    {
        // Tests for the saturation clamping logic mirrored in SaturationHelper.
        // The actual NVIDIA.Saturation setter calls DisplayApi.SetDVCLevel which
        // requires physical GPU hardware, so we test the extracted clamping in isolation.

        [TestMethod]
        public void ClampSaturation_WhenAboveMax_ReturnsMax()
        {
            Assert.AreEqual(100, SaturationHelper.ClampSaturation(150, -50, 100));
        }

        [TestMethod]
        public void ClampSaturation_WhenBelowMin_ReturnsMin()
        {
            Assert.AreEqual(-50, SaturationHelper.ClampSaturation(-100, -50, 100));
        }

        [TestMethod]
        public void ClampSaturation_WhenWithinRange_ReturnsUnchanged()
        {
            Assert.AreEqual(25, SaturationHelper.ClampSaturation(25, -50, 100));
        }

        [TestMethod]
        public void ClampSaturation_WhenExactlyAtMin_ReturnsMin()
        {
            Assert.AreEqual(-50, SaturationHelper.ClampSaturation(-50, -50, 100));
        }

        [TestMethod]
        public void ClampSaturation_WhenExactlyAtMax_ReturnsMax()
        {
            Assert.AreEqual(100, SaturationHelper.ClampSaturation(100, -50, 100));
        }

        [TestMethod]
        public void ClampSaturation_WhenZeroWithinTypicalRange_ReturnsZero()
        {
            Assert.AreEqual(0, SaturationHelper.ClampSaturation(0, -50, 100));
        }

        [TestMethod]
        public void ClampSaturation_WhenMinEqualsMax_ReturnsThatValue()
        {
            Assert.AreEqual(50, SaturationHelper.ClampSaturation(99, 50, 50));
        }
    }
}

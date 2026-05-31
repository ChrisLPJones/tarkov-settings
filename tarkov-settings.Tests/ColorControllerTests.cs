using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace tarkov_settings.Tests
{
    [TestClass]
    public class ColorControllerTests
    {
        // CalculateLUT(brightness, contrast, gamma)
        // brightness/contrast: 0–1 (0.5 = neutral)
        // gamma: clamped to [0.4, 2.8]
        // output: 256 ushort values in [0, 65535]

        [TestMethod]
        public void CalculateLUT_ReturnsExactly256Entries()
        {
            var lut = ColorLogic.CalculateLUT();
            Assert.AreEqual(256, lut.Length);
        }

        [TestMethod]
        public void CalculateLUT_AllValuesInUshortRange()
        {
            var lut = ColorLogic.CalculateLUT(0.5, 0.5, 1.0);
            foreach (var v in lut)
                Assert.IsTrue(v >= 0 && v <= ushort.MaxValue, $"Value {v} out of range");
        }

        [TestMethod]
        public void CalculateLUT_NeutralSettings_FirstValueIsZero()
        {
            // brightness=0.5, contrast=0.5, gamma=1.0 → linear curve, starts at 0
            var lut = ColorLogic.CalculateLUT(0.5, 0.5, 1.0);
            Assert.AreEqual(0, lut[0]);
        }

        [TestMethod]
        public void CalculateLUT_NeutralSettings_LastValueIsMax()
        {
            var lut = ColorLogic.CalculateLUT(0.5, 0.5, 1.0);
            Assert.AreEqual(ushort.MaxValue, lut[255]);
        }

        [TestMethod]
        public void CalculateLUT_NeutralSettings_IsApproximatelyLinear()
        {
            // gamma=1, brightness=0.5, contrast=0.5 → identity-like LUT
            var lut = ColorLogic.CalculateLUT(0.5, 0.5, 1.0);
            // midpoint should be close to half of ushort.MaxValue
            Assert.AreEqual(32896, lut[128], 200);
        }

        [TestMethod]
        public void CalculateLUT_HighBrightness_ShadowsAreBrighter()
        {
            var neutral = ColorLogic.CalculateLUT(0.5, 0.5, 1.0);
            var bright  = ColorLogic.CalculateLUT(1.0, 0.5, 1.0);
            Assert.IsTrue(bright[10] > neutral[10], "Shadows should be brighter at brightness=1.0");
        }

        [TestMethod]
        public void CalculateLUT_LowBrightness_MidtonesAreDarker()
        {
            var neutral = ColorLogic.CalculateLUT(0.5, 0.5, 1.0);
            var dark    = ColorLogic.CalculateLUT(0.0, 0.5, 1.0);
            Assert.IsTrue(dark[128] < neutral[128], "Midtones should be darker at brightness=0.0");
        }

        [TestMethod]
        public void CalculateLUT_HighContrast_UpperMidtonesAreHigher()
        {
            var neutral  = ColorLogic.CalculateLUT(0.5, 0.5, 1.0);
            var contrast = ColorLogic.CalculateLUT(0.5, 1.0, 1.0);
            Assert.IsTrue(contrast[200] > neutral[200], "Upper midtones should be higher at high contrast");
        }

        [TestMethod]
        public void CalculateLUT_LowContrast_UpperMidtonesAreLower()
        {
            var neutral  = ColorLogic.CalculateLUT(0.5, 0.5, 1.0);
            var contrast = ColorLogic.CalculateLUT(0.5, 0.0, 1.0);
            Assert.IsTrue(contrast[200] < neutral[200], "Upper midtones should be lower at low contrast");
        }

        [TestMethod]
        public void CalculateLUT_GammaBelowMinimum_IsClampedTo04()
        {
            // gamma 0.1 and 0.4 should produce identical output (both clamped to 0.4)
            var clamped   = ColorLogic.CalculateLUT(0.5, 0.5, 0.1);
            var atMinimum = ColorLogic.CalculateLUT(0.5, 0.5, 0.4);
            CollectionAssert.AreEqual(clamped, atMinimum);
        }

        [TestMethod]
        public void CalculateLUT_GammaAboveMaximum_IsClampedTo28()
        {
            // gamma 5.0 and 2.8 should produce identical output (both clamped to 2.8)
            var clamped   = ColorLogic.CalculateLUT(0.5, 0.5, 5.0);
            var atMaximum = ColorLogic.CalculateLUT(0.5, 0.5, 2.8);
            CollectionAssert.AreEqual(clamped, atMaximum);
        }

        [TestMethod]
        public void CalculateLUT_BrightnessAbove1_IsClampedTo1()
        {
            var clamped = ColorLogic.CalculateLUT(2.0, 0.5, 1.0);
            var atMax   = ColorLogic.CalculateLUT(1.0, 0.5, 1.0);
            CollectionAssert.AreEqual(clamped, atMax);
        }

        [TestMethod]
        public void CalculateLUT_BrightnessBelow0_IsClampedTo0()
        {
            var clamped = ColorLogic.CalculateLUT(-1.0, 0.5, 1.0);
            var atMin   = ColorLogic.CalculateLUT(0.0,  0.5, 1.0);
            CollectionAssert.AreEqual(clamped, atMin);
        }

        [TestMethod]
        public void CalculateLUT_ContrastAbove1_IsClampedTo1()
        {
            var clamped = ColorLogic.CalculateLUT(0.5, 2.0, 1.0);
            var atMax   = ColorLogic.CalculateLUT(0.5, 1.0, 1.0);
            CollectionAssert.AreEqual(clamped, atMax);
        }

        [TestMethod]
        public void CalculateLUT_ContrastBelow0_IsClampedTo0()
        {
            var clamped = ColorLogic.CalculateLUT(0.5, -1.0, 1.0);
            var atMin   = ColorLogic.CalculateLUT(0.5,  0.0, 1.0);
            CollectionAssert.AreEqual(clamped, atMin);
        }

        [TestMethod]
        public void CalculateLUT_HigherGamma_BrightensLowMidtones()
        {
            // The formula uses pow(factor, 1/gamma). For 0 < factor < 1, increasing gamma
            // raises the exponent toward 0, which pushes values closer to 1 (brighter).
            // gamma=2.8 produces higher output values than gamma=1.0 in the midrange.
            var low  = ColorLogic.CalculateLUT(0.5, 0.5, 1.0);
            var high = ColorLogic.CalculateLUT(0.5, 0.5, 2.8);
            Assert.IsTrue(high[128] > low[128], "Higher gamma value brightens midtones via pow(x, 1/gamma)");
        }
    }
}

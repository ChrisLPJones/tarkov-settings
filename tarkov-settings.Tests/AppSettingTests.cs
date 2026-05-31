using Microsoft.VisualStudio.TestTools.UnitTesting;
using tarkov_settings.Setting;

namespace tarkov_settings.Tests
{
    [TestClass]
    public class AppSettingTests
    {
        [TestMethod]
        public void DefaultBrightness_IsHalf()
        {
            Assert.AreEqual(0.5, new AppSetting().brightness, 0.001);
        }

        [TestMethod]
        public void DefaultContrast_IsHalf()
        {
            Assert.AreEqual(0.5, new AppSetting().contrast, 0.001);
        }

        [TestMethod]
        public void DefaultGamma_IsOne()
        {
            Assert.AreEqual(1.0, new AppSetting().gamma, 0.001);
        }

        [TestMethod]
        public void DefaultSaturation_IsSentinel()
        {
            // -1 means "unset — MainForm reads from GPU on first run so Always On causes no shift"
            Assert.AreEqual(-1, new AppSetting().saturation);
        }

        [TestMethod]
        public void DefaultMinimizeOnStart_IsFalse()
        {
            Assert.IsFalse(new AppSetting().minimizeOnStart);
        }

        [TestMethod]
        public void DefaultDisplay_IsPrimaryMonitor()
        {
            Assert.AreEqual(@"\\.\DISPLAY1", new AppSetting().display);
        }

        [TestMethod]
        public void DefaultPTargets_ContainsEscapeFromTarkov()
        {
            Assert.IsTrue(new AppSetting().pTargets.Contains("EscapeFromTarkov"),
                "Default process target list must include EscapeFromTarkov");
        }

        [TestMethod]
        public void DefaultPTargets_HasExactlyOneEntry()
        {
            Assert.AreEqual(1, new AppSetting().pTargets.Count);
        }

        [TestMethod]
        public void DefaultPTargets_StoredAsMixedCase()
        {
            // ProcessMonitor lowercases at lookup time; the stored name is mixed-case
            var targets = new AppSetting().pTargets;
            Assert.IsTrue(targets.Contains("EscapeFromTarkov"));
            Assert.IsFalse(targets.Contains("escapefromtarkov"),
                "Stored name is mixed-case; ProcessMonitor lowercases at lookup time");
        }
    }
}

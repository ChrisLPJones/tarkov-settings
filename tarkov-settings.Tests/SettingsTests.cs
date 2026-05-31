using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using tarkov_settings.Setting;

namespace tarkov_settings.Tests
{
    [TestClass]
    public class SettingsTests
    {
        private string _tempFile;

        [TestInitialize]
        public void Setup()
        {
            _tempFile = Path.GetTempFileName();
        }

        [TestCleanup]
        public void Teardown()
        {
            if (File.Exists(_tempFile))
                File.Delete(_tempFile);
        }

        [TestMethod]
        public void Load_WhenFileDoesNotExist_ReturnsDefaultInstance()
        {
            File.Delete(_tempFile);
            var result = AppSetting.Load(_tempFile);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Load_WhenFileDoesNotExist_DefaultBrightnessIsHalf()
        {
            File.Delete(_tempFile);
            var result = AppSetting.Load(_tempFile);
            Assert.AreEqual(0.5, result.brightness, 0.001);
        }

        [TestMethod]
        public void Save_CreatesFile()
        {
            File.Delete(_tempFile);
            new AppSetting().Save(_tempFile);
            Assert.IsTrue(File.Exists(_tempFile));
        }

        [TestMethod]
        public void SaveAndLoad_RoundTrip_PreservesBrightness()
        {
            var original = new AppSetting { brightness = 0.75 };
            original.Save(_tempFile);
            Assert.AreEqual(0.75, AppSetting.Load(_tempFile).brightness, 0.001);
        }

        [TestMethod]
        public void SaveAndLoad_RoundTrip_PreservesContrast()
        {
            var original = new AppSetting { contrast = 0.8 };
            original.Save(_tempFile);
            Assert.AreEqual(0.8, AppSetting.Load(_tempFile).contrast, 0.001);
        }

        [TestMethod]
        public void SaveAndLoad_RoundTrip_PreservesGamma()
        {
            var original = new AppSetting { gamma = 1.5 };
            original.Save(_tempFile);
            Assert.AreEqual(1.5, AppSetting.Load(_tempFile).gamma, 0.001);
        }

        [TestMethod]
        public void SaveAndLoad_RoundTrip_PreservesSaturation()
        {
            var original = new AppSetting { saturation = 42 };
            original.Save(_tempFile);
            Assert.AreEqual(42, AppSetting.Load(_tempFile).saturation);
        }

        [TestMethod]
        public void SaveAndLoad_RoundTrip_PreservesDisplay()
        {
            var original = new AppSetting { display = @"\\.\DISPLAY2" };
            original.Save(_tempFile);
            Assert.AreEqual(@"\\.\DISPLAY2", AppSetting.Load(_tempFile).display);
        }

        [TestMethod]
        public void SaveAndLoad_RoundTrip_PreservesMinimizeOnStart()
        {
            var original = new AppSetting { minimizeOnStart = true };
            original.Save(_tempFile);
            Assert.IsTrue(AppSetting.Load(_tempFile).minimizeOnStart);
        }

        [TestMethod]
        public void SaveAndLoad_RoundTrip_PreservesCustomPTargets()
        {
            var original = new AppSetting();
            original.pTargets = new HashSet<string> { "SomeOtherGame", "AnotherGame" };
            original.Save(_tempFile);
            CollectionAssert.AreEquivalent(
                new[] { "SomeOtherGame", "AnotherGame" },
                new List<string>(AppSetting.Load(_tempFile).pTargets)
            );
        }

        [TestMethod]
        public void Save_WritesValidJson()
        {
            new AppSetting { brightness = 0.6 }.Save(_tempFile);
            string json = File.ReadAllText(_tempFile);
            JsonConvert.DeserializeObject(json); // must not throw
            StringAssert.Contains(json, "brightness");
        }

        [TestMethod]
        public void Load_WhenFileContainsPartialJson_FillsMissingWithDefaults()
        {
            // Only override brightness — rest should be defaults
            File.WriteAllText(_tempFile, @"{ ""brightness"": 0.9 }");
            var loaded = AppSetting.Load(_tempFile);
            Assert.AreEqual(0.9, loaded.brightness, 0.001);
            Assert.AreEqual(0.5, loaded.contrast, 0.001);
        }
    }
}

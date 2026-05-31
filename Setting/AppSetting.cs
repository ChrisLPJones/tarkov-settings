using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tarkov_settings.Setting
{
    class AppSetting : Settings<AppSetting>
    {
        public double brightness = 0.5;
        public double contrast = 0.5;
        public double gamma = 1.0;
        public int saturation = -1; // -1 = unset; initialised from GPU on first run
        public HashSet<string> pTargets = new HashSet<string>{
            "EscapeFromTarkov"
        };
        public string display = @"\\.\DISPLAY1";
        public bool minimizeOnStart = false;
        public bool alwaysOn = false;
        public bool darkMode = false;

        public List<Profile> profiles = new List<Profile>
        {
            new Profile { name = "Profile 1" },
            new Profile { name = "Profile 2" }
        };
        public int activeProfile = 0;
    }
}

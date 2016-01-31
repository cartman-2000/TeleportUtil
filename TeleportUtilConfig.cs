using Rocket.API;

namespace TeleportUtil
{
    public class TeleportUtilConfig : IRocketPluginConfiguration
    {
        public bool PrintToRCON = false;

        public void LoadDefaults() { }
    }
}
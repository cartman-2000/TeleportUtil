using System;
using Rocket.Unturned.Plugins;
using System.Collections.Generic;

namespace TeleportUtil
{
    public class TeleportUtil : RocketPlugin
    {
        public static TeleportUtil Instance;

        protected override void Load()
        {
            Instance = this;
        }

        public override Dictionary<string, string> DefaultTranslations
        {
            get
            {
                return new Dictionary<string, string>
                {
                    {
                        "location_on_map",
                        "Your location on the map is: x:{0}, y:{1}, z:{2}"
                    }
                };
            }
        }
    }
}

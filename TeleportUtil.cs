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
                        "can't_locate_player",
                        "Can't locate player."
                    },
                    {
                        "locate_other_not_allowed",
                        "You're not allowed to use locate on another player."
                    },
                    {
                        "location_on_map",
                        "Your location on the map is: x:{0}, y:{1}, z:{2}"
                    },
                    {
                        "location_on_map_other",
                        "Player: \"{0}\" location on the map is: x:{1}, y:{2}, z:{3}"
                    }
                };
            }
        }
    }
}

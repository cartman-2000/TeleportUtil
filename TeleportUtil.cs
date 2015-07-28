using Rocket.API.Collections;
using Rocket.Core.Plugins;

namespace TeleportUtil
{
    public class TeleportUtil : RocketPlugin<TeleportUtilConfig>
    {
        public static TeleportUtil Instance;

        protected override void Load()
        {
            Instance = this;
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList
                {
                    { "invalid_arg", "Invalid Arguments." },
                    { "can't_locate_player", "Can't locate player." },
                    { "locate_other_not_allowed", "You're not allowed to use locate on another player." },
                    { "location_on_map", "Your location on the map is: x:{0}, y:{1}, z:{2}" },
                    { "location_on_map_other", "Player: \"{0}\" location on the map is: x:{1}, y:{2}, z:{3}" },
                    { "tprel_help", "<x> <y> <z> - Teleport to x,y,z coords relative to yourself." },
                    { "tp_fail", "Failed to teleport: Can't teleport while in a car." },
                    { "tp_success", "You have been teleported to: x:{0}, y:{1}, z:{2}" },
                    { "tpto_help", "[<u|d|n|s|w|e>distance] [...] [...] - Teleport by direction:distance." }
                };
            }
        }
    }
}

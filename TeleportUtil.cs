using Rocket.API.Collections;
using Rocket.Core.Plugins;

namespace TeleportUtil
{
    public class TeleportUtil : RocketPlugin
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
                    { "tpto_help", CommandTpto.syntax + " - " + CommandTpto.help },
                    { "tprel_help", CommandTprel.syntax + " - " + CommandTprel.help },
                    { "tpall_help", CommandTpall.syntax + " - " + CommandTpall.help },
                    { "invalid_arg", "Invalid Arguments." },
                    { "can't_locate_player", "Can't locate player." },
                    { "locate_other_not_allowed", "You're not allowed to use locate on another player." },
                    { "location_on_map", "Your location on the map is: {0}" },
                    { "location_on_map_other", "Player: \"{0}\" location on the map is: {1}" },
                    { "tp_success", "You have been teleported to: {0}" },
                    { "tp_fail_vehicle", "Failed to teleport vehicle: There must be no driver in the car." },
                    { "tp_fail_obstructed", "Failed to teleport to location, obstructed by nearby elements." },
                    { "can't_find_location", "There's no players or locations by that name on the server." },
                    { "no_players_tpall", "There were no players found to teleport." },
                    { "tpall_num_teleported", "{0} players have been teleported to: {1}, excluded players: {2}"}
                };
            }
        }
    }
}

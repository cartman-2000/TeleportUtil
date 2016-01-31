using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core;
using Rocket.Core.Plugins;
using Rocket.Core.RCON;
using Rocket.Unturned.Chat;

namespace TeleportUtil
{
    public class TeleportUtil : RocketPlugin<TeleportUtilConfig>
    {
        public static TeleportUtil Instance;

        protected override void Load()
        {
            Instance = this;
            Instance.Configuration.Save();
        }

        internal static void RconPrint(IRocketPlayer caller, string msg)
        {
            if (caller is ConsolePlayer && R.Settings.Instance.RCON.Enabled && Instance.Configuration.Instance.PrintToRCON)
                RCONServer.Broadcast(msg);
            UnturnedChat.Say(caller, msg);
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
                    { "tp_fail", "Failed to teleport: Can't teleport while in a car." },
                    { "tp_success", "You have been teleported to: {0}" },
                    { "can't_find_location", "There's no players or locations by that name on the server." },
                    { "no_players_tpall", "There were no players found to teleport." },
                    { "tpall_num_teleported", "{0} players have been teleported to: {1}, excluded players: {2}"}
                };
            }
        }
    }
}

using System;
using System.Collections.Generic;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using Rocket.Unturned;


namespace TeleportUtil
{
    class CommandLocate : IRocketCommand
    {
        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public void Execute(RocketPlayer caller, string[] command)
        {
            if ((caller == null && command.Length < 1) || command.Length > 1)
            {
                RocketChat.Say(caller, TeleportUtil.Instance.Translate("can't_locate_player", new object[] {}));
                return;
            }
            if (command.Length == 1)
            {
                if (!caller.HasPermission("locate.other"))
                {
                    RocketChat.Say(caller, TeleportUtil.Instance.Translate("locate_other_not_allowed", new object[] { }));
                    return;
                }
                RocketPlayer target = RocketPlayer.FromName(command[0]);
                if (target == null)
                {
                    RocketChat.Say(caller, TeleportUtil.Instance.Translate("can't_locate_player", new object[] { }));
                    return;
                }
                else
                {
                    RocketChat.Say(caller, TeleportUtil.Instance.Translate("location_on_map_other", new object[] { target.CharacterName.Truncate(14), Math.Round(target.Position.x, 2), Math.Round(target.Position.y, 2), Math.Round(target.Position.z, 2) }));
                }
            }
            else
            {
                RocketChat.Say(caller, TeleportUtil.Instance.Translate("location_on_map", new object[] { Math.Round(caller.Position.x, 2), Math.Round(caller.Position.y, 2), Math.Round(caller.Position.z, 2) }));
            }
        }

        public string Help
        {
            get { return "Prints out the location of a player on the map for usage in the /tp command."; }
        }

        public string Name
        {
            get { return "locate"; }
        }

        public bool RunFromConsole
        {
            get { return true; }
        }

        public string Syntax
        {
            get { return "[\"name\"]"; }
        }
    }
}

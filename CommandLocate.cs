using System;
using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;

namespace TeleportUtil
{
    public class CommandLocate : IRocketCommand
    {
        public bool AllowFromConsole
        {
            get { return true; }
        }

        public string Name
        {
            get { return "locate"; }
        }

        public string Help
        {
            get { return "Prints out the location of a player on the map for usage in the /tp command."; }
        }

        public string Syntax
        {
            get { return "[\"name\"]"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public List<string> Permissions
        {
            get { return new List<string>() { "TeleportUtil.locate" }; }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if ((caller is ConsolePlayer && command.Length < 1) || command.Length > 1)
            {
                UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("can't_locate_player"));
                return;
            }
            if (command.Length == 1)
            {
                if (!caller.HasPermission("locate.other"))
                {
                    UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("locate_other_not_allowed"));
                    return;
                }
                UnturnedPlayer target = command.GetUnturnedPlayerParameter(0);
                if (target == null)
                {
                    UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("can't_locate_player"));
                    return;
                }
                else
                {
                    UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("location_on_map_other", target.CharacterName.Truncate(14), Math.Round(target.Position.x, 2), Math.Round(target.Position.y, 2), Math.Round(target.Position.z, 2)));
                }
            }
            else
            {
                UnturnedPlayer unturnedCaller = UnturnedPlayer.FromName(caller.Id);
                UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("location_on_map", Math.Round(unturnedCaller.Position.x, 2), Math.Round(unturnedCaller.Position.y, 2), Math.Round(unturnedCaller.Position.z, 2)));
            }
        }
    }
}

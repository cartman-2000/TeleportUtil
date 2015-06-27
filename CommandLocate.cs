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
            if (caller == null)
            {
                RocketChat.Say(caller, "Can't locate player.");
                return;
            }
            RocketChat.Say(caller, TeleportUtil.Instance.Translate("location_on_map", new object[] { Math.Round(caller.Position.x, 2), Math.Round(caller.Position.y, 2), Math.Round(caller.Position.z, 2)}));
        }

        public string Help
        {
            get { return "Prints out the location of the player on the map for usage in the /tp command."; }
        }

        public string Name
        {
            get { return "locate"; }
        }

        public bool RunFromConsole
        {
            get { return false; }
        }

        public string Syntax
        {
            get { return ""; }
        }
    }
}

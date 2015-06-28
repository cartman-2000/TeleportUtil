using System;
using System.Collections.Generic;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using Rocket.Unturned;
using UnityEngine;
using SDG.Unturned;

namespace TeleportUtil
{
    public class CommandTpto : IRocketCommand
    {
        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public void Execute(RocketPlayer caller, string[] command)
        {
            if (command.Length == 0)
            {
                RocketChat.Say(caller, TeleportUtil.Instance.Translate("tpto_help", new object[] { }));
                return;
            }
            if (command.Length > 3)
            {
                RocketChat.Say(caller, TeleportUtil.Instance.Translate("invalid_arg", new object[] { }));
                return;
            }
            if (caller.Stance == EPlayerStance.DRIVING)
            {
                RocketChat.Say(caller, TeleportUtil.Instance.Translate("tp_fail", new object[] { }));
                return;
            }
            Vector3 newLocation = caller.Position;
            foreach (string part in command)
            {
                if (part.Length < 2)
                {
                    RocketChat.Say(caller, TeleportUtil.Instance.Translate("invalid_arg", new object[] { }));
                    return;
                }
                float distance;
                if (!float.TryParse(part.Substring(1, part.Length - 1), out distance))
                {
                    RocketChat.Say(caller, TeleportUtil.Instance.Translate("invalid_arg", new object[] { }));
                    return;
                }
                switch (part.Substring(0, 1).ToLower())
                {
                    case "u":
                        newLocation.y += distance;
                        break;
                    case "d":
                        newLocation.y -= distance;
                        break;
                    case "n":
                        newLocation.z += distance;
                        break;
                    case "s":
                        newLocation.z -= distance;
                        break;
                    case "e":
                        newLocation.x += distance;
                        break;
                    case "w":
                        newLocation.x -= distance;
                        break;
                    default:
                        RocketChat.Say(caller, TeleportUtil.Instance.Translate("invalid_arg", new object[] { }));
                        return;
                }
            }
            caller.Teleport(newLocation, caller.Rotation);
            RocketChat.Say(caller, TeleportUtil.Instance.Translate("tp_success", new object[] { Math.Round(newLocation.x, 2), Math.Round(newLocation.y, 2), Math.Round(newLocation.z, 2) }));
        }

        public string Help
        {
            get { return "Teleport by direction:distance."; }
        }

        public string Name
        {
            get { return "tpto"; }
        }

        public bool RunFromConsole
        {
            get { return false; }
        }

        public string Syntax
        {
            get { return "[<u|d|n|s|w|e>distance] [...] [...]"; }
        }
    }
}

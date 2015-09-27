using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using UnityEngine;
using SDG.Unturned;

namespace TeleportUtil
{
    public class CommandTprel : IRocketCommand
    {
        public static string help = "Teleport to x,y,z coords relative to yourself.";
        public static string syntax = "<x> <y> <z>";
        public bool AllowFromConsole
        {
            get { return false; }
        }

        public string Name
        {
            get { return "tprel"; }
        }

        public string Help
        {
            get { return help; }
        }

        public string Syntax
        {
            get { return syntax; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public List<string> Permissions
        {
            get { return new List<string>() { "TeleportUtil.tprel" }; }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length == 0)
            {
                UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("tprel_help"));
                return;
            }

            // Only allow the command to be ran with all three parameters.
            if (command.Length != 3)
            {
                UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("invalid_arg"));
                return;
            }
            else
            {
                float? x = command.GetFloatParameter(0);
                float? y = command.GetFloatParameter(1);
                float? z = command.GetFloatParameter(2);
                UnturnedPlayer unturnedCaller = (UnturnedPlayer)caller;

                if (x.HasValue && y.HasValue && z.HasValue)
                {
                    // Don't allow the player to teleport if they are in a car.
                    if (unturnedCaller.Stance == EPlayerStance.DRIVING || unturnedCaller.Stance == EPlayerStance.SITTING)
                    {
                        UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("tp_fail"));
                        return;
                    }

                    // Compute new location from the relative location parameters entered into the command.
                    Vector3 newLocation = new Vector3(unturnedCaller.Position.x + x.Value, unturnedCaller.Position.y + y.Value, unturnedCaller.Position.z + z.Value);
                    unturnedCaller.Teleport(newLocation, unturnedCaller.Rotation);
                    UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("tp_success", newLocation.xyz_Location()));
                }
                else
                {
                    UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("invalid_arg"));
                    return;
                }
            }
        }
    }
}

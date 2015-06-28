using System;
using System.Collections.Generic;
using Rocket.Unturned;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using UnityEngine;
using SDG.Unturned;


namespace TeleportUtil
{
    public class CommandTprel : IRocketCommand
    {
        public System.Collections.Generic.List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public void Execute(RocketPlayer caller, string[] command)
        {
            if (command.Length == 0)
            {
                RocketChat.Say(caller, TeleportUtil.Instance.Translate("tprel_help", new object[] { }));
                return;
            }
            if (command.Length != 3)
            {
                RocketChat.Say(caller, TeleportUtil.Instance.Translate("invalid_arg", new object[] { }));
                return;
            }
            else
            {
                float? x = command.GetFloatParameter(0);
                float? y = command.GetFloatParameter(1);
                float? z = command.GetFloatParameter(2);

                if (x.HasValue && y.HasValue && z.HasValue)
                {
                    if (caller.Stance == EPlayerStance.DRIVING)
                    {
                        RocketChat.Say(caller, TeleportUtil.Instance.Translate("tprel_fail", new object[] { }));
                        return;
                    }
                    Vector3 newLocation = new Vector3(caller.Position.x + x.Value, caller.Position.y + y.Value, caller.Position.z + z.Value);
                    caller.Teleport(newLocation, caller.Rotation);
                    RocketChat.Say(caller, TeleportUtil.Instance.Translate("tprel_sucsess", new object[] { Math.Round(newLocation.x, 2), Math.Round(newLocation.y, 2), Math.Round(newLocation.z, 2) }));
                }
                else
                {
                    RocketChat.Say(caller, TeleportUtil.Instance.Translate("invalid_arg", new object[] { }));
                    return;
                }
            }
        }

        public string Help
        {
            get { return "Teleport to x,y,z coords relative to yourself."; }
        }

        public string Name
        {
            get { return "tprel"; }
        }

        public bool RunFromConsole
        {
            get { return false; }
        }

        public string Syntax
        {
            get { return "<x> <y> <z>"; }
        }
    }
}

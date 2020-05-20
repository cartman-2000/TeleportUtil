using System.Collections.Generic;
using UnityEngine;
using SDG.Unturned;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;

namespace TeleportUtil
{
    public class CommandTpto : IRocketCommand
    {
        public static string help = "Teleport by direction:distance.";
        public static string syntax = "[<u|d|n|s|w|e>distance] [...] [...]";
        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public string Name
        {
            get { return "tpto"; }
        }

        public string Help
        {
            get { return help; }
        }

        public string Syntax
        {
            get { return "[<u|d|n|s|w|e>distance] [...] [...]"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public List<string> Permissions
        {
            get { return new List<string>() { "TeleportUtil.tpto" }; }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length == 0)
            {
                UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("tpto_help"));
                return;
            }
            if (command.Length > 3)
            {
                UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("invalid_arg"));
                return;
            }

            // Don't allow teleport if the player is in a car.
            UnturnedPlayer unturnedCaller = (UnturnedPlayer)caller;
            InteractableVehicle vehicle = null;
            Vector3 newLocation;
            if (unturnedCaller.IsInVehicle)
            {
                vehicle = unturnedCaller.CurrentVehicle;
                newLocation = vehicle.transform.position;
            }
            else
                newLocation = unturnedCaller.Position;
            // Parse through the list of parameters, and compute new location.
            foreach (string part in command)
            {
                if (part.Length < 2)
                {
                    UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("invalid_arg"));
                    return;
                }
                if (!float.TryParse(part.Substring(1, part.Length - 1), out float distance))
                {
                    UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("invalid_arg"));
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
                        UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("invalid_arg"));
                        return;
                }
            }
            if (unturnedCaller.IsInVehicle)
                if (vehicle.TeleportCar(unturnedCaller, newLocation))
                    UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("tp_success", newLocation.xyz_Location()));
                else
                    UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("tp_fail_vehicle"));
            else
            {
                if (!unturnedCaller.Player.teleportToLocation(newLocation, unturnedCaller.Rotation))
                {
                    if (caller.IsAdmin)
                    {
                        unturnedCaller.Player.teleportToLocationUnsafe(newLocation, unturnedCaller.Rotation);
                        return;
                    }
                    UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("tp_fail_obstructed"));
                    return;
                }
                UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("tp_success", newLocation.xyz_Location()));
            }
        }
    }
}

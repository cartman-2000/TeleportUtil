using Rocket.API;
using Rocket.API.Extensions;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TeleportUtil
{
    public class CommandTpall : IRocketCommand
    {
        public static string help = "Teleports all players to a player or location.";
        public static string syntax = "<playername | place | x y z>";
        public List<string> Aliases
        {
            get { return new List<string>(); }
        }

        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Both; }
        }

        public string Help
        {
            get { return help; }
        }

        public string Name
        {
            get { return "tpall"; }
        }

        public List<string> Permissions
        {
            get { return new List<string> { "TeleportUtil.tpall" }; }
        }

        public string Syntax
        {
            get { return syntax; }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length == 0)
            {
                UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("tpall_help"));
                return;
            }
            if (command.Length != 1 && command.Length != 3)
            {
                UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("invalid_arg"));
                return;
            }
            // Teleport by x, y, z.
            if (command.Length == 3)
            {
                float? x = command.GetFloatParameter(0);
                float? y = command.GetFloatParameter(1);
                float? z = command.GetFloatParameter(2);
                if (x.HasValue && y.HasValue && z.HasValue)
                    Teleport(new Vector3(x.Value, y.Value, z.Value), 0, caller);
                else
                {
                    UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("invalid_arg"));
                    return;
                }
            }
            else
            {
                // Teleport to a player.
                UnturnedPlayer target = command.GetUnturnedPlayerParameter(0);
                if (target != null)
                    Teleport(new Vector3(target.Position.x, target.Position.y + .5f, target.Position.z), target.Rotation, caller, target.CharacterName);
                else
                {
                    // Teleport to a map info node.
                    Node infonode = (from n in LevelNodes.nodes
                                 where n.type == ENodeType.LOCATION && ((LocationNode)n).name.ToLower().Contains(command[0].ToLower())
                                 select n).FirstOrDefault();
                    if (infonode != null)
                        Teleport(new Vector3(infonode.point.x, infonode.point.y + .5f, infonode.point.z), 0f, caller, ((LocationNode)infonode).name);
                    else
                    {
                        UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("can't_find_location"));
                        return;
                    }
                }
            }
        }

        private void Teleport(Vector3 location, float rotation, IRocketPlayer caller, string name = null)
        {
            int numPlayers = Provider.clients.Count;
            List<string> excluded = new List<string>();
            if (numPlayers == 0)
            {
                UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("no_players_tpall"));
                return;
            }
            if (name == null)
                name = location.xyz_Location();
            foreach (SteamPlayer player in Provider.clients)
            {
                // Don't teleport the player to teleport if they are in a car.
                if (player.player.stance.stance == EPlayerStance.DRIVING || player.player.stance.stance == EPlayerStance.SITTING)
                {
                    numPlayers--;
                    excluded.Add(player.playerID.characterName);
                    continue;
                }

                if (!player.player.teleportToLocation(location, rotation))
                {
                    if (caller.IsAdmin)
                    {
                        player.player.teleportToLocationUnsafe(location, rotation);
                        continue;
                    }
                    UnturnedChat.Say(player.playerID.steamID, TeleportUtil.Instance.Translate("tp_fail_obstructed"));
                    return;
                }
                UnturnedChat.Say(player.playerID.steamID, TeleportUtil.Instance.Translate("tp_success", name));
            }
            UnturnedChat.Say(caller, TeleportUtil.Instance.Translate("tpall_num_teleported", numPlayers, name, string.Join(", ", excluded.ToArray())));
        }
    }
}

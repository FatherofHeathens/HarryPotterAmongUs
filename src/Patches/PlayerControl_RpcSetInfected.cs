﻿﻿using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using HarryPotter.Classes;
  using HarryPotter.Classes.Roles;
  using Reactor.Extensions;
using UnityEngine;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcSetInfected))]
    class PlayerControl_RpcSetInfected
    {
        static void Prefix(ref UnhollowerBaseLib.Il2CppReferenceArray<GameData.PlayerInfo> __0)
        {
            if (Main.Instance.Config.SelectRoles)
            {
                List<string> impRolesToAssign = new List<string> { "Voldemort", "Bellatrix" };
                List<string> crewRolesToAssign = new List<string> { "Harry", "Hermione", "Ron" };

                List<GameData.PlayerInfo> allImpostors = new List<GameData.PlayerInfo>();
                List<GameData.PlayerInfo> exemptPlayers = new List<GameData.PlayerInfo>();

                foreach (Pair<PlayerControl, string> roleTuple in Main.Instance.PlayersWithRequestedRoles)
                {
                    if (crewRolesToAssign.Contains(roleTuple.Item2))
                    {
                        System.Console.WriteLine("Prefix: " + roleTuple.Item2);

                        crewRolesToAssign.Remove(roleTuple.Item2);
                        exemptPlayers.Add(roleTuple.Item1.Data);
                    }

                    if (impRolesToAssign.Contains(roleTuple.Item2))
                    {
                        System.Console.WriteLine("Prefix: " + roleTuple.Item2);

                        impRolesToAssign.Remove(roleTuple.Item2);
                        allImpostors.Add(roleTuple.Item1.Data);
                    }
                }

                while (allImpostors.Count > PlayerControl.GameOptions.GetAdjustedNumImpostors(GameData.Instance.PlayerCount))
                    allImpostors.Remove(allImpostors.Random());

                while (allImpostors.Count < PlayerControl.GameOptions.GetAdjustedNumImpostors(GameData.Instance.PlayerCount))
                    allImpostors.Add(PlayerControl.AllPlayerControls.ToArray().Where(x => allImpostors.All(y => y.PlayerId != x.PlayerId) && exemptPlayers.All(y => y.PlayerId != x.PlayerId)).Random().Data);

                System.Console.WriteLine(allImpostors.Count + ":" + PlayerControl.GameOptions.GetAdjustedNumImpostors(GameData.Instance.PlayerCount));

                __0 = allImpostors.ToArray();
            }
        }

        static void Postfix()
        {
            List<ModdedPlayerClass> allImp =
                Main.Instance.AllPlayers.Where(x => x._Object.Data.IsImpostor).ToList();
            
            List<ModdedPlayerClass> allCrew =
                Main.Instance.AllPlayers.Where(x => !x._Object.Data.IsImpostor).ToList();

            List<string> impRolesToAssign = new List<string> { "Voldemort", "Bellatrix" };
            List<string> crewRolesToAssign = new List<string> { "Harry", "Hermione", "Ron" };

            if (Main.Instance.Config.SelectRoles)
            {
                foreach (Pair<PlayerControl, string> roleTuple in Main.Instance.PlayersWithRequestedRoles)
                {
                    if (roleTuple.Item1.Data.IsImpostor)
                    {
                        if (impRolesToAssign.Contains(roleTuple.Item2))
                        {
                            allCrew.Remove(Main.Instance.ModdedPlayerById(roleTuple.Item1.PlayerId));
                            allImp.Remove(Main.Instance.ModdedPlayerById(roleTuple.Item1.PlayerId));
                            impRolesToAssign.Remove(roleTuple.Item2);

                            if (roleTuple.Item2 == "Voldemort")
                                Main.Instance.RpcAssignRole(Main.Instance.ModdedPlayerById(roleTuple.Item1.PlayerId), new Voldemort(Main.Instance.ModdedPlayerById(roleTuple.Item1.PlayerId)));
                            else if (roleTuple.Item2 == "Bellatrix")
                                Main.Instance.RpcAssignRole(Main.Instance.ModdedPlayerById(roleTuple.Item1.PlayerId), new Bellatrix(Main.Instance.ModdedPlayerById(roleTuple.Item1.PlayerId)));
                        }
                    }
                    else
                    {
                        if (crewRolesToAssign.Contains(roleTuple.Item2))
                        {
                            allCrew.Remove(Main.Instance.ModdedPlayerById(roleTuple.Item1.PlayerId));
                            allImp.Remove(Main.Instance.ModdedPlayerById(roleTuple.Item1.PlayerId));
                            crewRolesToAssign.Remove(roleTuple.Item2);

                            if (roleTuple.Item2 == "Harry")
                                Main.Instance.RpcAssignRole(Main.Instance.ModdedPlayerById(roleTuple.Item1.PlayerId), new Harry(Main.Instance.ModdedPlayerById(roleTuple.Item1.PlayerId)));
                            else if (roleTuple.Item2 == "Ron")
                                Main.Instance.RpcAssignRole(Main.Instance.ModdedPlayerById(roleTuple.Item1.PlayerId), new Ron(Main.Instance.ModdedPlayerById(roleTuple.Item1.PlayerId)));
                            else if (roleTuple.Item2 == "Hermione")
                                Main.Instance.RpcAssignRole(Main.Instance.ModdedPlayerById(roleTuple.Item1.PlayerId), new Hermione(Main.Instance.ModdedPlayerById(roleTuple.Item1.PlayerId)));
                        }
                    }
                }

                Main.Instance.PlayersWithRequestedRoles.Clear();
            }

            while (allImp.Count > 0 && impRolesToAssign.Count > 0)
            {
                ModdedPlayerClass rolePlayer = allImp.Random();
                allImp.Remove(rolePlayer);

                if (impRolesToAssign.Contains("Voldemort"))
                {
                    impRolesToAssign.Remove("Voldemort");
                    Main.Instance.RpcAssignRole(rolePlayer, new Voldemort(rolePlayer));
                    continue;
                }

                if (impRolesToAssign.Contains("Bellatrix"))
                {
                    impRolesToAssign.Remove("Bellatrix");
                    Main.Instance.RpcAssignRole(rolePlayer, new Bellatrix(rolePlayer));
                    continue;
                }

            }

            while (allCrew.Count > 0 && crewRolesToAssign.Count > 0)
            {
                ModdedPlayerClass rolePlayer = allCrew.Random();
                allCrew.Remove(rolePlayer);
                
                if (crewRolesToAssign.Contains("Harry"))
                {
                    crewRolesToAssign.Remove("Harry");
                    Main.Instance.RpcAssignRole(rolePlayer, new Harry(rolePlayer));
                    continue;
                }
                
                if (crewRolesToAssign.Contains("Ron"))
                {
                    crewRolesToAssign.Remove("Ron");
                    Main.Instance.RpcAssignRole(rolePlayer, new Ron(rolePlayer));
                    continue;
                }
                
                if (crewRolesToAssign.Contains("Hermione"))
                {
                    crewRolesToAssign.Remove("Hermione");
                    Main.Instance.RpcAssignRole(rolePlayer, new Hermione(rolePlayer));
                    continue;
                }
            }
        }
    }
}
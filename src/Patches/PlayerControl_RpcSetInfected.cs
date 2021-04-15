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
        static void Postfix()
        {
            List<ModdedPlayerClass> allImp =
                Main.Instance.AllPlayers.Where(x => x._Object.Data.IsImpostor).ToList();
            
            List<ModdedPlayerClass> allCrew =
                Main.Instance.AllPlayers.Where(x => !x._Object.Data.IsImpostor).ToList();

            List<string> impRolesToAssign = new List<string> { "Voldemort", "Bellatrix" };
            List<string> crewRolesToAssign = new List<string> { "Harry", "Hermione", "Ron" };

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
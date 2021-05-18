using HarmonyLib;
using HarryPotter.Classes;
using HarryPotter.Classes.Roles;
using HarryPotter.Classes.UI;
using UnityEngine;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(KillButtonManager), nameof(KillButtonManager.PerformKill))]
    class KillButtonManager_PerformKill
    {
        static bool Prefix(KillButtonManager __instance)
        {
            if (__instance == HudManager.Instance.KillButton && Main.Instance.GetLocalModdedPlayer()?.Role?.RoleName == "Bellatrix" && ((Bellatrix)Main.Instance.GetLocalModdedPlayer().Role).MindControlledPlayer != null)
            {
                PlayerControl killer = ((Bellatrix)Main.Instance.GetLocalModdedPlayer().Role).MindControlledPlayer._Object;
                if (HudManager.Instance.KillButton.CurrentTarget != null && !Main.Instance.ControlKillUsed)
                {
                    Main.Instance.ControlKillUsed = true;
                    Main.Instance.RpcKillPlayer(killer, HudManager.Instance.KillButton.CurrentTarget, false);
                }
                return false;
            }
            
            if (!PlayerControl.LocalPlayer.CanMove) return false;
            if (Main.Instance.GetLocalModdedPlayer()?.Role != null)
                return Main.Instance.GetLocalModdedPlayer().Role.PerformKill(__instance);
            return true;
        }
    }
}
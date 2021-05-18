using HarmonyLib;
using HarryPotter.Classes;
using HarryPotter.Classes.UI;
using UnityEngine;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcMurderPlayer))]
    public class PlayerControl_RpcMurderPlayer
    {
        static bool Prefix(PlayerControl __instance, PlayerControl __0)
        {
            if (Main.Instance.ModdedPlayerById(__instance.PlayerId).VigilanteShotEnabled)
            {
                Main.Instance.ModdedPlayerById(__instance.PlayerId).VigilanteShotEnabled = false;
                HudManager.Instance.KillButton.gameObject.SetActive(false);
            }
            
            if (Main.Instance.ModdedPlayerById(__0.PlayerId).Immortal)
            {
                PopupTMPHandler.Instance.CreatePopup("When using his ability, Ron cannot be killed.\nYour cooldown was reset.", Color.white, Color.black);
                PlayerControl.LocalPlayer.SetKillTimer(PlayerControl.GameOptions.KillCooldown);
                Main.Instance.GetLocalModdedPlayer()?.Role?.ResetCooldowns();
                return false;
            }

            __instance.SetKillTimer(PlayerControl.GameOptions.KillCooldown);
            Main.Instance.RpcKillPlayer(__instance, __0, false, true);
            return false;
        }
    }
}
using HarmonyLib;
using HarryPotter.Classes;
using Il2CppSystem;
using UnityEngine;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(OverlayKillAnimation), nameof(OverlayKillAnimation.Begin))]
    public class OverlayKillAnimation_Begin
    {
        static void Prefix(OverlayKillAnimation __instance,  ref GameData.PlayerInfo __0, GameData.PlayerInfo __1)
        {
            if (__0 == null || __1 == null || __instance == null) return;
            if (__0.PlayerId != __1.PlayerId) return;
            ModdedPlayerClass harry = Main.Instance.FindPlayerOfRole("Harry");
            if (harry == null) return;
            
            __0 = harry._Object.Data;
            __instance.killerParts.Body.transform.localScale = new Vector3(0.4f, 0.4f);
            __instance.killerParts.Body.transform.position -= new Vector3(0.3f, 0f, 0f);
        }
    }
}
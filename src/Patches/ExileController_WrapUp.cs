using HarmonyLib;
using UnityEngine;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.WrapUp))]
    public class ExileController_WrapUp
    {
        static bool Prefix(ExileController __instance)
        {
            if (__instance.exiled != null)
            {
                PlayerControl @object = __instance.exiled.Object;
                if (@object) @object.Exiled();
            }
            if (DestroyableSingleton<TutorialManager>.InstanceExists || !ShipStatus.Instance.IsGameOverDueToDeath())
            {
                DestroyableSingleton<HudManager>.Instance.StartCoroutine(DestroyableSingleton<HudManager>.Instance.CoFadeFullScreen(Color.black, Color.clear, 0.2f));
                PlayerControl.LocalPlayer.SetKillTimer(PlayerControl.GameOptions.KillCooldown);
                ShipStatus.Instance.EmergencyCooldown = (float)PlayerControl.GameOptions.EmergencyCooldown;
                Camera.main.GetComponent<FollowerCamera>().Locked = false;
                DestroyableSingleton<HudManager>.Instance.SetHudActive(true);
                ControllerManager.Instance.ResetAll();
            }
            Object.Destroy(__instance.gameObject);

            return false;
        }
    }
}
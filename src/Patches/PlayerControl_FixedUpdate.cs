using HarmonyLib;
using HarryPotter.Classes;
using InnerNet;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    public class PlayerControl_FixedUpdate
    {
        static void Postfix(PlayerControl __instance)
        {
            if (__instance.AmOwner)
            {
                foreach (WorldItem wItem in Main.Instance.AllItems)
                {
                    wItem.DrawWorldIcon();
                    wItem.Update();
                }
    
                Main.Instance.AllItems.RemoveAll(x => x.IsPickedUp);
            }
        }
    }
}
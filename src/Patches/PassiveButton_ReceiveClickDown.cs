using HarmonyLib;
using HarryPotter.Classes;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(PassiveButton), nameof(PassiveButton.ReceiveClickDown))]
    public class PassiveButton_ReceiveClickDown
    {
        static void Prefix(PassiveButton __instance)
        {
            if (__instance == null || __instance.name == null) return;

            if (__instance.name == "SnitchButton" && Main.Instance.GetLocalModdedPlayer().HasItem(3))
                Main.Instance.RpcForceAllVotes(__instance.transform.GetComponentInParent<PlayerVoteArea>().TargetPlayerId);
        }
    }
}
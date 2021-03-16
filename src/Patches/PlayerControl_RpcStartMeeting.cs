using HarmonyLib;
using HarryPotter.Classes;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcStartMeeting))]
    public class PlayerControl_RpcStartMeeting
    {
        static void Postfix()
        {
            Main.Instance.CurrentStage++;
        }
    }
}
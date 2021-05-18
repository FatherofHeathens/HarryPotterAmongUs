using HarmonyLib;
using HarryPotter.Classes;
using Hazel;
using Il2CppSystem;
using UnhollowerBaseLib;
using UnityEngine;
using Object = Il2CppSystem.Object;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Exiled))]
    public class PlayerControl_Exiled
    {
        static bool Prefix(PlayerControl __instance)
        {
            __instance.Visible = false;
            if (__instance.AmOwner)
            {
                if (Main.Instance.ModdedPlayerById(__instance.PlayerId).ShouldRevive)
                    Main.Instance.RpcFakeKill(__instance);
                else
                {
                    Main.Instance.PlayerDie(__instance);
                    
                    StatsManager instance = StatsManager.Instance;
                    uint timesEjected = instance.TimesEjected;
                    instance.TimesEjected = timesEjected + 1U;
                    DestroyableSingleton<HudManager>.Instance.ShadowQuad.gameObject.SetActive(false);
                    ImportantTextTask importantTextTask = new GameObject("_Player").AddComponent<ImportantTextTask>();
                    importantTextTask.transform.SetParent(__instance.transform, false);
                    if (__instance.Data.IsImpostor)
                    {
                        __instance.ClearTasks();
                        importantTextTask.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GhostImpostor, new Il2CppReferenceArray<Object>(0));
                    }
                    else if (!PlayerControl.GameOptions.GhostsDoTasks)
                    {
                        __instance.ClearTasks();
                        importantTextTask.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GhostIgnoreTasks, new Il2CppReferenceArray<Object>(0));
                    }
                    else
                    {
                        importantTextTask.Text = DestroyableSingleton<TranslationController>.Instance.GetString(StringNames.GhostDoTasks, new Il2CppReferenceArray<Object>(0));
                    }
                    __instance.myTasks.Insert(0, importantTextTask);
                    
                    MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.FinallyDie, SendOption.Reliable);
                    writer.Write(__instance.PlayerId);
                    writer.EndMessage();
                }
            }
            return false;
        }
    }
}
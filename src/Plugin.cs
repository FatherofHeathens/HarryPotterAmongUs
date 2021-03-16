using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;
using HarryPotter.Classes;
using System.Collections.Generic;

namespace HarryPotter
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    [BepInDependency(Essentials.EssentialsPlugin.Id)]
    public class Plugin : BasePlugin
    {
        public const string Id = "harry.potter.mod";
        public Harmony Harmony { get; } = new Harmony(Id);

        public override void Load()
        {
            Main.Instance = new Main { Config = new Config(), Rpc = new CustomRpc(), Assets = new Asset(), AllPlayers = new List<ModdedPlayerClass>(), AllItems = new List<WorldItem>()};
            Harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(StatsManager), nameof(StatsManager.AmBanned), MethodType.Getter)]
    public static class StatsManager_AmBanned
    {
        public static void Postfix(out bool __result)
        {
            __result = false;
        }
    }
}
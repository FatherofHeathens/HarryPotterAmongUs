using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;
using HarryPotter.Classes;
using System.Collections.Generic;
using Essentials;
using InnerNet;
using UnityEngine;

namespace HarryPotter
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    [BepInDependency(EssentialsPlugin.Id)]
    public class Plugin : BasePlugin
    {
        public const string Id = "harry.potter.mod";
        public Harmony Harmony { get; } = new Harmony(Id);

        public override void Load()
        {
            Main.Instance = new Main { Config = new Config(), Rpc = new CustomRpc(), Assets = new Asset(), AllPlayers = new List<ModdedPlayerClass>(), AllItems = new List<WorldItem>() };
            TaskInfoHandler.Instance = new TaskInfoHandler { AllInfo = new List<ImportantTextTask>() };
            Essentials.Options.CustomOption.ShamelessPlug = false;
            Harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(StatsManager), nameof(StatsManager.AmBanned), MethodType.Getter)]
    public static class StatsManager_AmBanned
    { 
        static void Postfix(out bool __result)
        {
            __result = false;
        }
    }
    
    [HarmonyPatch(typeof(GameOptionsMenu), nameof(GameOptionsMenu.Update))]
    [HarmonyAfter(EssentialsPlugin.Id)]
    public class GameOptionsMenuPatchUpdate
    {
        static void Postfix(GameOptionsMenu __instance)
        {
            __instance.GetComponentInParent<Scroller>().YBounds.max = (__instance.Children.Length - 7) * 0.5F + 0.13F;
        }
    }

    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingTracker_Update
    {
        static void Postfix(PingTracker __instance)
        {
            __instance.text.RightAligned = true;
            __instance.text.transform.localPosition = new Vector3(0, 0, 0);
            Vector3 topright = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
            __instance.text.transform.position = new Vector3(topright.x - 0.1f, topright.y - 1.6f);
            __instance.text.Text += "\nCreated by [7289DAFF]Hunter101#1337";
            __instance.text.Text += "\n[FFFFFFFF]Original Design by [88FF00FF]npc & friends";
            __instance.text.Text += "\n[FFFFFFFF]Art by [E67E22FF]PhasmoFireGod";
            __instance.text.Text += "\n[F96854FF]www.patreon.com/HunterMuir";
            __instance.text.Text += "\n[0645ADFF]www.computable.us";
        }
    }
}
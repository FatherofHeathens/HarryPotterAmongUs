using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;
using HarryPotter.Classes;
using System.Collections.Generic;
using HarryPotter.Classes.UI;
using hunterlib;
using InnerNet;
using TMPro;
using UnityEngine;

namespace HarryPotter
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]

    //Imagine not having a custom options library? Couldn't be me
    [BepInDependency(HunterPlugin.Id)]

    public class Plugin : BasePlugin
    {
        public const string Id = "harry.potter.mod";
        public Harmony Harmony { get; } = new Harmony(Id);

        public override void Load()
        {
            //I'm too lazy to move these unnecessary assignments to the actual classes I am creating
            Main.Instance = new Main { Config = new Config(), Rpc = new CustomRpc(), Assets = new Asset(), AllPlayers = new List<ModdedPlayerClass>(), AllItems = new List<WorldItem>() };
            TaskInfoHandler.Instance = new TaskInfoHandler { AllInfo = new List<ImportantTextTask>() };
            PopupTMPHandler.Instance = new PopupTMPHandler { AllPopups = new List<TextMeshPro>() };

            HunterPlugin.DrawHudString = true;
            HunterPlugin.HudScale = 0.8f;
            RegisterInIl2CppAttribute.Register();
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

    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingTracker_Update
    {
        static void Postfix(PingTracker __instance)
        {
            __instance.text.alignment = TMPro.TextAlignmentOptions.BaselineRight;
            __instance.text.margin = new Vector4(0, 0, 0.5f, 0);
            __instance.text.fontSize = 2.5f;
            __instance.text.transform.localPosition = new Vector3(0, 0, 0);
            Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
            __instance.text.transform.position = new Vector3(topRight.x - 0.1f, topRight.y - 1.6f);
            __instance.text.text += "\nCreated by: <#7289DAFF>Hunter101#1337";
            __instance.text.text += "\n<#FFFFFFFF>Download at: <#00DDFFFF>www.computable.us";
            __instance.text.text += "\n<#FFFFFFFF>Original Design by: <#88FF00FF>npc & friends";
            __instance.text.text += "\n<#FFFFFFFF>Art by: <#E67E22FF>PhasmoFireGod";
            //__instance.text.text += "\n<#FFFFFFFF>Support projects like these at:";
            //__instance.text.text += "\n<#F96854FF>www.patreon.com/HunterMuir";
        }
    }
    
    //TODO: add tooltips to all the ability buttons and lobby settings
    //TODO: add new items
    //TODO: add system that lets users select 3 items to make available for quick-use without opening inventory
    //TODO: fix specific hats being blurry for no reason
    //TODO: fix the hat icons going outside of the box (center them and set them to only draw inside a mask? not sure)
}
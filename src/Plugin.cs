using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;
using HarryPotter.Classes;
using System.Collections.Generic;
using System.Linq;
using HarryPotter.Classes.Hats;
using HarryPotter.Classes.Helpers;
using HarryPotter.Classes.Helpers.UI;
using HarryPotter.Classes.UI;
using hunterlib;
using hunterlib.Classes;
using InnerNet;
using Reactor.Extensions;
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
            RegisterInIl2CppAttribute.Register();
            
            Main.Instance = new Main();

            TaskInfoHandler.Instance = new TaskInfoHandler { AllInfo = new List<ImportantTextTask>() };
            PopupTMPHandler.Instance = new PopupTMPHandler { AllPopups = new List<TextMeshPro>() };

            Hat.AllHats = new List<Hat>();
            
            Hat.AllHats.Add(new ScarfHat1());
            Hat.AllHats.Add(new ScarfHat2());
            Hat.AllHats.Add(new ScarfHat3());
            Hat.AllHats.Add(new ScarfHat4());
            Hat.AllHats.Add(new HairHat1());
            Hat.AllHats.Add(new HairHat2());
            Hat.AllHats.Add(new HairHat3());
            Hat.AllHats.Add(new EarsHat1());
            Hat.AllHats.Add(new EarsHat2());
            Hat.AllHats.Add(new DevilHat());
            Hat.AllHats.Add(new FireHat());
            Hat.AllHats.Add(new GlitchHat());
            Hat.AllHats.Add(new GlitchWizardHat());
            Hat.AllHats.Add(new PinkeeHat());
            Hat.AllHats.Add(new RaccoonHat());
            Hat.AllHats.Add(new SnakeHat());
            Hat.AllHats.Add(new WizardHat());
            Hat.AllHats.Add(new PenguinHat());
            Hat.AllHats.Add(new ElephantHat());
            Hat.AllHats.Add(new PiratePandaHat());
            Hat.AllHats.Add(new FlowerHat());
            Hat.AllHats.Add(new HairHat4());

            HunterPlugin.DrawHudString = false;
            HunterPlugin.HudScale = 0.8f;

            Harmony.PatchAll();

            if (Main.Instance.Config.UseCustomRegion)
            {
                IRegionInfo newRegion = new DnsRegionInfo("51.222.158.63", "Private", StringNames.NoTranslation, new ServerInfo[]
                {
                    new ServerInfo("Private-1", "51.222.158.63", 22023)
                }).Cast<IRegionInfo>();

                ServerManager.DefaultRegions = new UnhollowerBaseLib.Il2CppReferenceArray<IRegionInfo>(1);
                ServerManager.DefaultRegions[0] = newRegion;

                ServerManager.Instance.AvailableRegions = ServerManager.DefaultRegions;
                ServerManager.Instance.SaveServers();

                ServerManager.Instance.StartCoroutine(ServerManager.Instance.ReselectRegionFromDefaults());
            }
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
    
    [HarmonyPatch(typeof(GameOptionsData), nameof(GameOptionsData.ToHudString))]
    public class GameOptionsDataPatch
    {
        public static void Postfix(GameOptionsData __instance, ref string __result)
        {
            List<string> resultLines = __result.Split("\n").ToList();
            resultLines.RemoveAt(0);
            resultLines.Insert(0, "Game Settings:");
            
            __result = string.Join("\n", resultLines);
            __result += "\n<#EEFFB3FF>Mod settings:";
            if (Main.Instance.Config.ShowPopups) __result += "\n(Hover over a setting for more info)";
        }
    }

    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingTracker_Update
    {
        static void Postfix(PingTracker __instance)
        {
            if (Main.Instance.Config.SimplerWatermark)
            {
                __instance.text.transform.localPosition = new Vector3(1.575f, 2.6f, __instance.text.transform.localPosition.z);
                __instance.text.fontSize = 2.5f;
                __instance.text.text += "\n<#7289DAFF>Hunter101#1337";
                __instance.text.text += "\n<#00DDFFFF>www.computable.us";
            }
            else
            {
                __instance.text.alignment = TextAlignmentOptions.BaselineRight;
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
    }
}
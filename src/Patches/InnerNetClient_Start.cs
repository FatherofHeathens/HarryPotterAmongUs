using HarmonyLib;
using HarryPotter.Classes;
using InnerNet;
using System.Linq;
using HarryPotter.Classes.Helpers;
using HarryPotter.Classes.Helpers.UI;
using UnityEngine;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(InnerNetClient), nameof(InnerNetClient.Start))]
    public static class InnerNetClient_Start
    { 
        static void Postfix()
        {
            //SoundManager.Instance.PlaySound(Main.Instance.Assets.HPTheme, false, 1f);

            foreach (Hat customHat in Hat.AllHats)
            {
                HatBehaviour newHat = Object.Instantiate(HatManager.Instance.AllHats.ToArray().First());
                newHat.MainImage = customHat.MainSprite;
                newHat.NoBounce = !customHat.Bounce;
                newHat.ChipOffset = customHat.ChipOffset;
                HatManager.Instance.AllHats.Insert(1, newHat);
            }
            
            new GameObject().AddComponent<InventoryUI>();
            new GameObject().AddComponent<MindControlMenu>();
            new GameObject().AddComponent<HotbarUI>();

            Main.Instance.ResetCustomOptions();
        }
    }
}
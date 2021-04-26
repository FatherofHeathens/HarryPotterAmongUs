using HarmonyLib;
using HarryPotter.Classes;
using InnerNet;
using System.Linq;
using HarryPotter.Classes.Helpers.UI;
using UnityEngine;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(InnerNetClient), nameof(InnerNetClient.Start))]
    public static class InnerNetClient_Start
    { 
        static void Postfix()
        {
            SoundManager.Instance.PlaySound(Main.Instance.Assets.HPTheme, false, 1f);

            foreach (Sprite customHat in Main.Instance.Assets.AllCustomHats)
            {
                HatBehaviour newHat = UnityEngine.Object.Instantiate(HatManager.Instance.AllHats.ToArray().First());
                newHat.MainImage = customHat;
                System.Console.WriteLine(customHat.name);
                if (customHat.name != "hat (6)") newHat.NoBounce = true;
                HatManager.Instance.AllHats.Insert(1, newHat);
            }
            
            new GameObject().AddComponent<InventoryUI>();
        }
    }
}
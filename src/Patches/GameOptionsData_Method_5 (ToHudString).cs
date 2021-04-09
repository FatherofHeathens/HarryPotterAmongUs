using HarmonyLib;

namespace HarryPotter.Patches
{
    public class GameOptionsData_Method_5__ToHudString_
    {
        [HarmonyPatch(typeof(GameOptionsData), nameof(GameOptionsData.ToString))]
        class GameOptionsData_ToHudString
        {
            static void Postfix()
            {
                HudManager.Instance.GameSettings.scale = 0.5f;
            }
        }
    }
}
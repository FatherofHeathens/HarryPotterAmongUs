﻿﻿using HarmonyLib;
using HarryPotter.Classes;
using HarryPotter.Classes.Roles;
using UnityEngine;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(IntroCutscene._CoBegin_d__11), nameof(IntroCutscene._CoBegin_d__11.MoveNext))]
    class IntroCutscene_CoBegin__d_MoveNext
    {
        static void Prefix(IntroCutscene._CoBegin_d__11 __instance)
        {
            __instance.__this.IntroStinger = Main.Instance.Assets.HPTheme;
        }
        
        static void Postfix(IntroCutscene._CoBegin_d__11 __instance)
        {
            ModdedPlayerClass localPlayer = Main.Instance.GetLocalModdedPlayer();
            FontCache.Instance.SetFont(__instance.__this.ImpostorText, "Arial");
            __instance.__this.ImpostorText.transform.localScale = new Vector3(1.5f, 1.5f);
            if (localPlayer.Role == null) return;
            localPlayer.Role.ResetCooldowns();
            __instance.__this.ImpostorText.gameObject.SetActive(true);
            __instance.__this.Title.Text = localPlayer.Role.RoleName;
            __instance.__this.Title.Color = localPlayer.Role.RoleColor;
            __instance.__this.ImpostorText.Text = localPlayer.Role.IntroString;
            __instance.__this.BackgroundBar.material.color = localPlayer.Role.RoleColor2;
        }
    }
}

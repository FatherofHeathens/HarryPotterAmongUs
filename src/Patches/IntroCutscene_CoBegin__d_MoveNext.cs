﻿﻿using HarmonyLib;
using HarryPotter.Classes;
using HarryPotter.Classes.Roles;
using UnityEngine;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(IntroCutscene.Nested_0), nameof(IntroCutscene.Nested_0.MoveNext))]
    class IntroCutscene_CoBegin__d_MoveNext
    {
        static void Prefix(IntroCutscene.Nested_0 __instance)
        {
            __instance.__this.IntroStinger = Main.Instance.Assets.HPTheme;
        }
        
        static void Postfix(IntroCutscene.Nested_0 __instance)
        {
            ModdedPlayerClass localPlayer = Main.Instance.GetLocalModdedPlayer();
            if (!localPlayer._Object.Data.IsImpostor) __instance.__this.Title.text = "Muggle";
            if (localPlayer.Role == null) return;
            localPlayer.Role.ResetCooldowns();
            __instance.__this.ImpostorText.gameObject.SetActive(true);
            __instance.__this.ImpostorText.transform.localScale = new Vector3(0.7f, 0.7f);
            __instance.__this.Title.text = localPlayer.Role.RoleName;
            __instance.__this.Title.color = localPlayer.Role.RoleColor;
            __instance.__this.ImpostorText.text = localPlayer.Role.IntroString;
            __instance.__this.BackgroundBar.material.color = localPlayer.Role.RoleColor2;
        }
    }
}

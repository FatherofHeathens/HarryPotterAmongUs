﻿﻿using HarmonyLib;
  using HarryPotter.Classes;
  using HarryPotter.Classes.Roles;
  using Reactor.Net;

  namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(IntroCutscene.CoBegin__d), nameof(IntroCutscene.CoBegin__d.MoveNext))]
    class IntroCutscene_CoBegin__d_MoveNext
    {
        static void Postfix(IntroCutscene.CoBegin__d __instance)
        {
            ModdedPlayerClass localPlayer = Main.Instance.GetLocalModdedPlayer();
            if (localPlayer.Role == null)
                return;

            localPlayer.Role.ResetCooldowns();
            __instance.__this.ImpostorText.gameObject.SetActive(true);
            __instance.__this.Title.Text = localPlayer.Role.RoleName;
            __instance.__this.Title.Color = localPlayer.Role.RoleColor;
            __instance.__this.ImpostorText.Text = localPlayer.Role.IntroString;
            __instance.__this.BackgroundBar.material.color = localPlayer.Role.RoleColor;
        }
    }
}

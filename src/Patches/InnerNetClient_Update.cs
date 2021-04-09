﻿﻿using System;
  using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using HarryPotter.Classes;
  using HarryPotter.Classes.Items;
  using HarryPotter.Classes.WorldItems;
  using InnerNet;
  using UnityEngine;

  namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.LateUpdate))]
    class InnerNetClient_Update
    {
        static void Postfix(PlayerPhysics __instance)
        {
            if (!__instance?.AmOwner == true) return;

            if (Input.GetKeyDown(KeyCode.C))
            {
                System.Console.WriteLine($"new Tuple<byte, Vector2>({AmongUsClient.Instance.TutorialMapId}, new Vector2({PlayerControl.LocalPlayer.myRend.bounds.center.x}, {PlayerControl.LocalPlayer.myRend.bounds.center.y})),");
            }

            Main.Instance?.Config?.ReloadSettings();
            
            if ((AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Started || PlayerControl.LocalPlayer == null) && Main.Instance != null)
            {
                foreach (WorldItem wItem in Main.Instance.AllItems) wItem.Delete();
                DeluminatorWorld.HasSpawned = false;
                MaraudersMapWorld.HasSpawned = false;
                PortKeyWorld.HasSpawned = false;
                TheGoldenSnitchWorld.HasSpawned = false;
                Main.Instance.CurrentStage = 0;
                Main.Instance.AllItems.Clear();
                Main.Instance.AllPlayers.Clear();
                Main.Instance.PossibleItemPositions = Main.Instance.DefaultItemPositons;
                TaskInfoHandler.Instance.AllInfo.Clear();
                return;
            }

            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                if (Main.Instance?.AllPlayers.Where(x => x?._Object == player).ToList().Count == 0)
                    Main.Instance?.AllPlayers.Add(new ModdedPlayerClass(player, null, new List<Item>()));

            foreach (ModdedPlayerClass player in Main.Instance?.AllPlayers.ToList())
                if (player == null || player._Object == null || player._Object.Data.Disconnected) Main.Instance?.AllPlayers.Remove(player);

            Main.Instance?.GetLocalModdedPlayer().Update();
        }
    }
}

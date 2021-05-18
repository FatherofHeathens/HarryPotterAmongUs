using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using HarryPotter.Classes;
using HarryPotter.Classes.UI;
using HarryPotter.Classes.WorldItems;
using hunterlib.Classes;
using InnerNet;
using Reactor.Extensions;
using TMPro;
using UnityEngine;

namespace HarryPotter.Patches
{
    
    [HarmonyPatch(typeof(InnerNetClient), nameof(InnerNetClient.Update))]
    class InnerNetClient_Update
    {
        static void Postfix()
        {
            Reactor.Coroutines.Start(LateUpdate());
        }

        static IEnumerator LateUpdate()
        {
            yield return new WaitForEndOfFrame();
            RunUpdate();
        }

        static void RunUpdate()
        {
            if (AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Joined)
            {
                if (Main.Instance.Config.SelectRoles)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                        Main.Instance.RpcRequestRole("Harry");
                    if (Input.GetKeyDown(KeyCode.Alpha2))
                        Main.Instance.RpcRequestRole("Hermione");
                    if (Input.GetKeyDown(KeyCode.Alpha3))
                        Main.Instance.RpcRequestRole("Ron");
                    if (Input.GetKeyDown(KeyCode.Alpha4))
                        Main.Instance.RpcRequestRole("Voldemort");
                    if (Input.GetKeyDown(KeyCode.Alpha5))
                        Main.Instance.RpcRequestRole("Bellatrix");
                }
            }
            
            Main.Instance?.Config?.ReloadSettings();
                        
            foreach (TextMeshPro lobbySettingTMP in Main.Instance.CustomOptions)
            {
                if (!HudManager.InstanceExists || HudManager.Instance.GameSettings == null)
                {
                    lobbySettingTMP.gameObject.active = false;
                    continue;
                }

                Vector2 pos = HudManager.Instance.GameSettings.transform.position;
                
                pos.x += lobbySettingTMP.renderedWidth / 2;
                pos.y -= lobbySettingTMP.renderedHeight / 2;
                pos.y -= HudManager.Instance.GameSettings.renderedHeight;
                pos.y -= lobbySettingTMP.renderedHeight * Main.Instance.CustomOptions.IndexOf(lobbySettingTMP);
                
                lobbySettingTMP.gameObject.transform.position = pos;
                lobbySettingTMP.gameObject.active = HudManager.Instance.GameSettings.isActiveAndEnabled;

                lobbySettingTMP.text = Main.Instance.GetOptionTextByName(lobbySettingTMP.gameObject.name);
            }

            if (!AmongUsClient.Instance.IsGameStarted && Main.Instance != null)
            {
                foreach (WorldItem wItem in Main.Instance.AllItems) wItem.Delete();
                DeluminatorWorld.HasSpawned = false;
                MaraudersMapWorld.HasSpawned = false;
                PortKeyWorld.HasSpawned = false;
                TheGoldenSnitchWorld.HasSpawned = false;
                GhostStoneWorld.HasSpawned = false;
                ButterBeerWorld.HasSpawned = false;
                ElderWandWorld.HasSpawned = false;
                BasWorldItem.HasSpawned = false;
                SortingHatWorld.HasSpawned = false;
                PhiloStoneWorld.HasSpawned = false;
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
using System;
using System.Collections.Generic;
using HarmonyLib;
using HarryPotter.Classes;
using HarryPotter.Classes.Roles;
using Hazel;
using UnityEngine;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.FixedUpdate))]
    class PlayerPhysics_FixedUpdate
    {
        static bool Prefix(PlayerPhysics __instance)
        {
            ModdedPlayerClass moddedController = Main.Instance.ModdedPlayerById(__instance.myPlayer.PlayerId);
            
            if (__instance.myPlayer != PlayerControl.LocalPlayer || moddedController?.Role?.RoleName != "Bellatrix" || ((Bellatrix)moddedController.Role).MindControlledPlayer == null)
                return true;
            
            PlayerPhysics controlledPlayer = ((Bellatrix)moddedController.Role).MindControlledPlayer._Object.MyPhysics;
            
            GameData.PlayerInfo data = moddedController._Object.Data;
            bool flag = data != null && data.IsDead;

            Vector2 vel = HudManager.Instance.joystick.Delta * __instance.TrueSpeed;
            
            controlledPlayer.body.velocity = vel;

            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.MoveControlledPlayer, Hazel.SendOption.Reliable);
            writer.Write(controlledPlayer.myPlayer.PlayerId);
            writer.Write(vel.x);
            writer.Write(vel.y);
            writer.EndMessage();
            return false;
        }
    }
}
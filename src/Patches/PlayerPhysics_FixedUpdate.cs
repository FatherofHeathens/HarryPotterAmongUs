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
            if (!AmongUsClient.Instance.IsGameStarted) return true;
            ModdedPlayerClass localModded = Main.Instance.ModdedPlayerById(__instance.myPlayer.PlayerId);
            if (localModded == null) return true;
            ModdedPlayerClass mindControlledPlayer;

            if (localModded.Role != null && localModded.Role.RoleName == "Bellatrix")
                mindControlledPlayer = ((Bellatrix) localModded.Role).MindControlledPlayer;
            else
                mindControlledPlayer = null;

            ModdedPlayerClass movingPlayer = mindControlledPlayer == null ? localModded : mindControlledPlayer;
            PlayerPhysics movingPhysics = movingPlayer._Object.MyPhysics;
            
            GameData.PlayerInfo data = movingPhysics.myPlayer.Data;
            bool flag = data != null && data.IsDead;
            movingPhysics.HandleAnimation(flag);

            float normalSpeed = (flag ? movingPhysics.TrueGhostSpeed : movingPhysics.TrueSpeed);
            normalSpeed *= movingPlayer.SpeedMultiplier;
            Vector2 directionalStick = DestroyableSingleton<HudManager>.Instance.joystick.Delta;
            if (localModded.ReverseDirectionalControls) directionalStick *= -1;
            Vector2 vel = directionalStick * normalSpeed;

            if ((movingPhysics.AmOwner || mindControlledPlayer != null) && movingPhysics.myPlayer.CanMove && GameData.Instance)
                movingPhysics.body.velocity = vel;

            if (mindControlledPlayer == null)
                return false;
            
            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.MoveControlledPlayer, SendOption.Reliable);
            writer.Write(movingPhysics.myPlayer.PlayerId);
            writer.Write(vel.x);
            writer.Write(vel.y);
            writer.Write(movingPhysics.body.position.x);
            writer.Write(movingPhysics.body.position.y);
            writer.EndMessage();
            
            return false;
        }
    }
}
using HarmonyLib;
using HarryPotter.Classes;
using System.Linq;
using UnityEngine;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(CustomNetworkTransform), nameof(CustomNetworkTransform.FixedUpdate))]
    class CustomNetworkTransform_FixedUpdate
    {
        static bool Prefix(CustomNetworkTransform __instance)
        {
            if (!__instance.AmOwner)
            {
                if (Main.Instance.AllPlayers.Any(x => x._Object.NetTransform == __instance && x.ControllerOverride == Main.Instance.GetLocalModdedPlayer())) return false;
                
                if (__instance.interpolateMovement != 0f)
                {
                    Vector2 vector = __instance.targetSyncPosition - __instance.body.position;
                    if (vector.sqrMagnitude >= 0.0001f)
                    {
                        float num = __instance.interpolateMovement / __instance.sendInterval;
                        vector.x *= num;
                        vector.y *= num;
                        if (PlayerControl.LocalPlayer)
                        {
                            vector = Vector2.ClampMagnitude(vector, PlayerControl.LocalPlayer.MyPhysics.TrueSpeed);
                        }
                        __instance.body.velocity = vector;
                    }
                    else
                    {
                        __instance.body.velocity = Vector2.zero;
                    }
                }
                __instance.targetSyncPosition += __instance.targetSyncVelocity * Time.fixedDeltaTime * 0.1f;
                return false;
            }
            return true;
        }
    }
}
﻿﻿using HarmonyLib;
using HarryPotter.Classes;
using Hazel;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
    class PlayerControl_HandleRpc
    {
        static void Postfix(byte __0, MessageReader __1)
        {
            Main.Instance.Rpc.Handle(__0, __1);
        }
    }
}

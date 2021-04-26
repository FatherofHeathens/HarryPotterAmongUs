using HarryPotter.Classes.WorldItems;
using Hazel;
using UnityEngine;

namespace HarryPotter.Classes.Items
{
    public class PortKey : Item
    {
        public PortKey(ModdedPlayerClass owner)
        {
            this.Owner = owner;
            this.ParentInventory = owner.Inventory;
            this.Id = 2;
            this.Icon = Main.Instance.Assets.ItemIcons[Id];
            this.Name = "Port Key";
            this.Tooltip = "Port Key:\nTeleports the user to\nthe emergency button.";
        }

        public override void Use()
        {
            if (AmongUsClient.Instance.AmHost)
                PortKeyWorld.HasSpawned = false;
            else
            {
                MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.UseItem, SendOption.Reliable);
                writer.Write(Id);
                writer.EndMessage();
            }
            System.Console.WriteLine("Used Port Key");
            this.Delete();
            
            Main.Instance.RpcTeleportPlayer(Owner._Object, PlayerControl.GameOptions.MapId == 4 ? new Vector2(7.620923f, 15.0479f) : ShipStatus.Instance.MeetingSpawnCenter);
        }
    }
}
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
        }

        public override void Use()
        {
            System.Console.WriteLine("Used Port Key");
            this.Delete();

            Main.Instance.RpcTeleportPlayer(Owner._Object, PlayerControl.GameOptions.MapId == 4 ? ShipStatus.Instance.MeetingSpawnCenter2 : ShipStatus.Instance.MeetingSpawnCenter);
        }
    }
}
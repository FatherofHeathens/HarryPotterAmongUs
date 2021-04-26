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
            this.Tooltip = "Teleports the user to\nthe Emergency button.";
        }

        public override void Use()
        {
            System.Console.WriteLine("Used Port Key");
            this.Delete();

            Main.Instance.RpcTeleportPlayer(Owner._Object, PlayerControl.GameOptions.MapId == 4 ? new Vector2(7.620923f, 15.0479f) : ShipStatus.Instance.MeetingSpawnCenter);
        }
    }
}
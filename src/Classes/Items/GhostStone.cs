using HarryPotter.Classes.WorldItems;
using Hazel;

namespace HarryPotter.Classes.Items
{
    public class GhostStone : Item
    {
        public GhostStone(ModdedPlayerClass owner)
        {
            this.Owner = owner;
            this.ParentInventory = owner.Inventory;
            this.Id = 4;
            this.Icon = Main.Instance.Assets.WorldItemIcons[Id];
            this.Name = "Resurrection Stone";
            this.Tooltip = "Resurrection Stone:\nAllows the holder to see ghosts.\n<#FF0000FF>This item cannot be consumed.";
            this.IsSpecial = true;
        }
    }
}
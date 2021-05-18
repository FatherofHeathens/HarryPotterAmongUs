using HarryPotter.Classes.WorldItems;
using Hazel;

namespace HarryPotter.Classes.Items
{
    public class PhiloStone : Item
    {
        public PhiloStone(ModdedPlayerClass owner)
        {
            this.Owner = owner;
            this.ParentInventory = owner.Inventory;
            this.Id = 9;
            this.Icon = Main.Instance.Assets.WorldItemIcons[Id];
            this.Name = "Philosopher's Stone";
            this.Tooltip = "Philosopher's Stone:\nThis item will revive you when you die.\n<#FF0000FF>This item will be automatically consumed.";
            this.IsSpecial = true;
        }
    }
}
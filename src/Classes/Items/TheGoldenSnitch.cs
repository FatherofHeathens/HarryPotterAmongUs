using Hazel;
using UnityEngine.Events;

namespace HarryPotter.Classes.Items
{
    public class TheGoldenSnitch : Item
    {
        public TheGoldenSnitch(ModdedPlayerClass owner)
        {
            this.Owner = owner;
            this.ParentInventory = owner.Inventory;
            this.Id = 3;
            this.Icon = Main.Instance.Assets.ItemIcons[Id];
            this.Name = "The Golden Snitch";
            this.IsSpecial = true;
        }
    }
}
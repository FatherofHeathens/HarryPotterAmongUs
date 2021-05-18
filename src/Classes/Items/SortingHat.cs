namespace HarryPotter.Classes.Items
{

    public class SortingHat : Item
    {
        public SortingHat(ModdedPlayerClass owner)
        {
            this.Owner = owner;
            this.ParentInventory = owner.Inventory;
            this.Id = 8;
            this.Icon = Main.Instance.Assets.WorldItemIcons[Id];
            this.Name = "Sorting Hat";
            this.IsSpecial = true;
            this.Tooltip = "Sorting Hat:\nReveals the role of the targeted player.\n<#FF0000FF>Can only be used in meetings!";
        }
    }
}
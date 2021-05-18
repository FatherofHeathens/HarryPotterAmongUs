namespace HarryPotter.Classes.Items
{
    public class BasItem : Item
    {
        public BasItem(ModdedPlayerClass owner)
        {
            Owner = owner;
            ParentInventory = owner.Inventory;
            Id = 7;
            Name = "Basilisk";
            Tooltip = "";
            IsTrap = true;
        }

        public override void Use()
        {
            Delete();
            Reactor.Coroutines.Start(Main.Instance.CoStunPlayer(Owner._Object));
        }
    }
}
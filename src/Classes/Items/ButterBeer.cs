using HarryPotter.Classes.WorldItems;
using Hazel;

namespace HarryPotter.Classes.Items
{
    public class ButterBeer : Item
    {
        public ButterBeer(ModdedPlayerClass owner)
        {
            this.Owner = owner;
            this.ParentInventory = owner.Inventory;
            this.Id = 5;
            this.Name = "Butter Beer";
            this.Tooltip = "";
            this.IsTrap = true;
        }

        public override void Use()
        {
            this.Delete();
            Reactor.Coroutines.Start(Main.Instance.CoActivateButterBeer(Owner._Object));
        }
    }
}
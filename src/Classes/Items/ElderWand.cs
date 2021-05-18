namespace HarryPotter.Classes.Items
{
    public class ElderWand : Item
    {
        public ElderWand(ModdedPlayerClass owner)
        {
            Owner = owner;
            ParentInventory = owner.Inventory;
            Id = 6;
            Icon = Main.Instance.Assets.ItemIcons[Id];
            Name = "Elder Wand";
            Tooltip = "Elder Wand:\nIf you are an Impostor, this\nitem will reset your cooldowns to zero.\nOtherwise, you will get a single use button\nto kill anyone you think might be an Impostor";
        }

        public override void Use()
        {
            Delete();

            if (Owner._Object.Data.IsImpostor)
                Owner.Role?.RemoveCooldowns();
            else
                Owner.VigilanteShotEnabled = true;
        }
    }
}
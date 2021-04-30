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
            this.Icon = Main.Instance.Assets.ItemIcons[Id];
            this.Name = "Butter Beer";
            this.Tooltip = "Butter Beer:\nMakes the user very fast, but also\nreverses their directional controls.";
        }

        public override void Use()
        {
            if (AmongUsClient.Instance.AmHost)
                ButterBeerWorld.HasSpawned = false;
            else
            {
                MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.UseItem, SendOption.Reliable);
                writer.Write(Id);
                writer.EndMessage();
            }
            System.Console.WriteLine("Used Butter Beer");
            this.Delete();
            Reactor.Coroutines.Start(Main.Instance.CoActivateButterBeer(Owner._Object));
        }
    }
}
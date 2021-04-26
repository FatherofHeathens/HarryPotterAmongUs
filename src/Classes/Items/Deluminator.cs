using System.Runtime.CompilerServices;
using HarryPotter.Classes.WorldItems;
using Hazel;

namespace HarryPotter.Classes.Items
{
    public class Deluminator : Item
    {
        public Deluminator(ModdedPlayerClass owner)
        {
            this.Owner = owner;
            this.ParentInventory = owner.Inventory;
            this.Id = 0;
            this.Icon = Main.Instance.Assets.ItemIcons[Id];
            this.Name = "Deluminator";
            this.Tooltip = "Deluminator:\nToggles the status of the lights.";
        }
        public override void Use()
        {
            if (AmongUsClient.Instance.AmHost)
                DeluminatorWorld.HasSpawned = false;
            else
            {
                MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.UseItem, SendOption.Reliable);
                writer.Write(Id);
                writer.EndMessage();
            }
            System.Console.WriteLine("Used Deluminator");
            this.Delete();
            
            switch (Main.Instance.IsLightsSabotaged())
            {
                case true:
                    var switchSystem = ShipStatus.Instance.Systems[SystemTypes.Electrical].Cast<SwitchSystem>();
                    switchSystem.ActualSwitches = switchSystem.ExpectedSwitches;
                    MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.FixLightsRpc, SendOption.Reliable);
                    writer.EndMessage();
                    break;
                case false:
                    byte b = 4;
                    for (var i = 0; i < 5; i++)
                        if (new System.Random().Next(0, 2) == 0)
                            b |= (byte)(1 << i);
                    ShipStatus.Instance.RpcRepairSystem(SystemTypes.Electrical, b | 128);
                    break;
            }
        }
    }
}
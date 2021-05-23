using System.Linq;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using hunterlib.Classes;

namespace HarryPotter.Classes.WorldItems
{
    public class ElderWandWorld : WorldItem
    {
        public static System.Random ItemRandom { get; set; } = new System.Random();
        public static float ItemSpawnChance { get; set; } = 30;
        public static bool HasSpawned { get; set; }
        
        public ElderWandWorld(Vector2 position)
        {
            Position = position;
            Id = 6;
            Icon = Main.Instance.Assets.WorldItemIcons[Id];
            Name = "Elder Wand";
        }

        public static void WorldSpawn()
        {
            if (!CanSpawn())
                return;

            if (!ShipStatus.Instance)
                return;
            
            Vector2 pos = Main.Instance.GetAllApplicableItemPositions().Random();
            Main.Instance.RpcSpawnItem(6, pos);
            HasSpawned = true;
        }
        
        public static bool CanSpawn()
        {
            if (Main.Instance.AllItems.Where(x => x.Id == 6).ToList().Count > 0) return false;
            if (MeetingHud.Instance) return false;
            if (!AmongUsClient.Instance.IsGameStarted) return false;
            if (ItemRandom.Next(0, 100000) > ItemSpawnChance) return false;
            if (Main.Instance.CurrentStage < 1) return false;
            if (HasSpawned) return false;

            return true;
        }
    }
}
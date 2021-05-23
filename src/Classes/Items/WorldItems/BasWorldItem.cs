using System.Linq;
using UnityEngine;
using hunterlib.Classes;

namespace HarryPotter.Classes.WorldItems
{
    public class BasWorldItem : WorldItem
    {
        public static System.Random ItemRandom { get; set; } = new System.Random();
        public static float ItemSpawnChance { get; set; } = 30;
        public static bool HasSpawned { get; set; }
        
        public BasWorldItem(Vector2 position)
        {
            this.Position = position;
            this.Id = 7;
            this.Icon = Main.Instance.Assets.WorldItemIcons[Id];
            this.Name = "Basilisk";
        }

        public static void WorldSpawn()
        {
            if (!CanSpawn())
                return;
            
            if (!ShipStatus.Instance)
                return;

            Vector2 pos = Main.Instance.GetAllApplicableItemPositions().Random();
            Main.Instance.RpcSpawnItem(7, pos);
            HasSpawned = true;
        }
        
        public static bool CanSpawn()
        {
            if (Main.Instance.AllItems.Where(x => x.Id == 7).ToList().Count > 0) return false;
            if (MeetingHud.Instance) return false;
            if (!AmongUsClient.Instance.IsGameStarted) return false;
            if (ItemRandom.Next(0, 100000) > ItemSpawnChance) return false;
            if (Main.Instance.CurrentStage != 0) return false;
            if (HasSpawned) return false;

            return true;
        }
    }
}
using System.Linq;
using Reactor.Extensions;
using UnityEngine;

namespace HarryPotter.Classes.WorldItems
{
    public class ButterBeerWorld : WorldItem
    {
        public static System.Random ItemRandom { get; set; } = new System.Random();
        public static float ItemSpawnChance { get; set; } = 30;
        public static bool HasSpawned { get; set; }
        
        public ButterBeerWorld(Vector2 position)
        {
            this.Position = position;
            this.Id = 5;
            this.Icon = Main.Instance.Assets.WorldItemIcons[Id];
            this.Name = "Butter Beer";
        }

        public static void WorldSpawn()
        {
            if (!CanSpawn())
                return;
            
            if (!ShipStatus.Instance)
                return;

            Vector2 pos = Main.Instance.GetAllApplicableItemPositions().Random();
            Main.Instance.RpcSpawnItem(5, pos);
            HasSpawned = true;
        }
        
        public static bool CanSpawn()
        {
            if (Main.Instance.AllItems.Where(x => x.Id == 5).ToList().Count > 0) return false;
            if (MeetingHud.Instance) return false;
            if (!AmongUsClient.Instance.IsGameStarted) return false;
            if (ItemRandom.Next(0, 100000) > ItemSpawnChance) return false;

            return true;
        }
    }
}
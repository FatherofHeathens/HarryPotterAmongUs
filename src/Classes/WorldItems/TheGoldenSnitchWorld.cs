using System.Linq;
using Reactor.Extensions;
using UnityEngine;

namespace HarryPotter.Classes.WorldItems
{
    public class TheGoldenSnitchWorld : WorldItem
    {
        public static System.Random ItemRandom { get; set; } = new System.Random();
        public static float ItemSpawnChance { get; set; } = 15;
        public static bool HasSpawned { get; set; }
        
        public TheGoldenSnitchWorld(Vector2 position)
        {
            this.Position = position;
            this.Id = 3;
            this.Icon = Main.Instance.Assets.WorldItemIcons[Id];
            this.Name = "The Golden Snitch";
        }

        public static void WorldSpawn()
        {
            if (!CanSpawn())
                return;
            
            if (!ShipStatus.Instance)
                return;
            
            Vector2 pos = Main.Instance.GetAllApplicableItemPositions().Random();
            Main.Instance.PossibleItemPositions.RemoveAll(x => x.Item2 == pos);
            Main.Instance.RpcSpawnItem(3, pos);
            HasSpawned = true;
        }
        
        public static bool CanSpawn()
        {
            if (Main.Instance.AllItems.Where(x => x.Id == 3).ToList().Count > 0)
                return false;

            if (MeetingHud.Instance)
                return false;

            if (!AmongUsClient.Instance.IsGameStarted)
                return false;

            if (ItemRandom.Next(0, 100000) > ItemSpawnChance)
                return false;
            
            if (HasSpawned)
                return false;

            if (Main.Instance.CurrentStage < 2)
                return false;

            return true;
        }
    }
}
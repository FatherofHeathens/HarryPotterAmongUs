using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HarryPotter.Classes.Hats;
using HarryPotter.Classes.Helpers.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using hunterlib.Classes;

namespace HarryPotter.Classes
{
    class Asset
    {
        public List<Sprite> ItemIcons { get; }
        public Sprite SmallSnitchSprite { get; }
        public Sprite SmallSortSprite { get; }
        public List<Sprite> AbilityIcons { get; }
        public List<Sprite> WorldItemIcons { get; }
        public List<Sprite> CrucioSprite { get;  }
        public List<Sprite> CurseSprite { get; }
        public List<Sprite> AllHatSprites { get; }
        public PhysicsMaterial2D SnitchMaterial { get; }
        public AudioClip HPTheme { get; }
        public Material GenericOutlineMat { get; }
        public Asset()
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(Directory.GetCurrentDirectory() + "\\Assets\\harrypotter");
            
            ItemIcons = new List<Sprite>();
            AbilityIcons = new List<Sprite>();
            WorldItemIcons = new List<Sprite>();
            CrucioSprite = new List<Sprite>();
            CurseSprite = new List<Sprite>();
            AllHatSprites = new List<Sprite>();

            AbilityIcons.Add(bundle.LoadAsset<Sprite>("CurseButton").DontUnload());
            AbilityIcons.Add(bundle.LoadAsset<Sprite>("CrucioButton").DontUnload());
            AbilityIcons.Add(bundle.LoadAsset<Sprite>("ImperioButton").DontUnload());
            AbilityIcons.Add(bundle.LoadAsset<Sprite>("DDButton").DontUnload());
            AbilityIcons.Add(bundle.LoadAsset<Sprite>("InvisButton").DontUnload());
            AbilityIcons.Add(bundle.LoadAsset<Sprite>("HourglassButton").DontUnload());
            AbilityIcons.Add(bundle.LoadAsset<Sprite>("MarkButton").DontUnload());

            ItemIcons.Add(bundle.LoadAsset<Sprite>("DelumIco").DontUnload());
            ItemIcons.Add(bundle.LoadAsset<Sprite>("MapIco").DontUnload());
            ItemIcons.Add(bundle.LoadAsset<Sprite>("KeyIco").DontUnload());
            ItemIcons.Add(null); //golden snitch
            ItemIcons.Add(null); //res stone
            ItemIcons.Add(null); //butter beer
            ItemIcons.Add(bundle.LoadAsset<Sprite>("ElderWandIco").DontUnload());
            ItemIcons.Add(null); //basilisk
            ItemIcons.Add(null); //sorting hat
            ItemIcons.Add(null); //philo stone
            
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("DelumWorldIcon").DontUnload());
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("MapWorldIcon").DontUnload());
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("KeyWorldIcon").DontUnload());
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("SnitchWorldIcon").DontUnload());
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("GhostStoneWorldIcon").DontUnload());
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("BeerWorldIcon").DontUnload());
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("ElderWandWorldIcon").DontUnload());
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("BasWorldIcon").DontUnload());
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("SortingHatWorldIcon").DontUnload());
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("PhiloStoneWorldIcon").DontUnload());
            
            CrucioSprite.Add(bundle.LoadAsset<Sprite>("CrucioF1").DontUnload());
            CrucioSprite.Add(bundle.LoadAsset<Sprite>("CrucioF2").DontUnload());
            
            CurseSprite.Add(bundle.LoadAsset<Sprite>("CurseF1").DontUnload());
            CurseSprite.Add(bundle.LoadAsset<Sprite>("CurseF2").DontUnload());

            for (var i = 0; i <= 21; i++)
            {
                AllHatSprites.Add(bundle.LoadAsset<Sprite>($"hat_{i}").DontUnload());
                System.Console.WriteLine(AllHatSprites[i].name);
            }

            SmallSortSprite = bundle.LoadAsset<Sprite>("SmallSortIco").DontUnload();
            SmallSnitchSprite = bundle.LoadAsset<Sprite>("SmallSnitchIco").DontUnload();
            SnitchMaterial = bundle.LoadAsset<PhysicsMaterial2D>("SnitchMaterial").DontUnload();
            HPTheme = bundle.LoadAsset<AudioClip>("HPTheme").DontUnload();
            InventoryUI.PanelPrefab = bundle.LoadAsset<GameObject>("InventoryPanel").DontUnload();
            MindControlMenu.PanelPrefab = bundle.LoadAsset<GameObject>("ControlPanel").DontUnload();
            HotbarUI.PanelPrefab = bundle.LoadAsset<GameObject>("Hotbar").DontUnload();
            GenericOutlineMat = bundle.LoadAsset<Material>("GenericOutline").DontUnload();
        }
    }
}

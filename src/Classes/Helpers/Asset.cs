using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HarryPotter.Classes.Helpers.UI;
using Reactor.Extensions;
using Reactor.Unstrip;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

namespace HarryPotter.Classes
{
    class Asset
    {
        public List<Sprite> ItemIcons { get; }
        public Sprite SmallSnitchSprite { get; }
        public List<Sprite> AbilityIcons { get; }
        public List<Sprite> WorldItemIcons { get; }
        public List<Sprite> CrucioSprite { get;  }
        public List<Sprite> CurseSprite { get; }
        public List<Sprite> AllCustomHats { get; }
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
            AllCustomHats = new List<Sprite>();

            AbilityIcons.Add(bundle.LoadAsset<Sprite>("CurseButton").DontUnload());
            AbilityIcons.Add(bundle.LoadAsset<Sprite>("CrucioButton").DontUnload());
            AbilityIcons.Add(bundle.LoadAsset<Sprite>("ImperioButton").DontUnload());
            AbilityIcons.Add(bundle.LoadAsset<Sprite>("DDButton").DontUnload());
            AbilityIcons.Add(bundle.LoadAsset<Sprite>("InvisButton").DontUnload());
            AbilityIcons.Add(bundle.LoadAsset<Sprite>("HourglassButton").DontUnload());
            
            ItemIcons.Add(bundle.LoadAsset<Sprite>("DelumIco").DontUnload());
            ItemIcons.Add(bundle.LoadAsset<Sprite>("MapIco").DontUnload());
            ItemIcons.Add(bundle.LoadAsset<Sprite>("KeyIco").DontUnload());
            ItemIcons.Add(bundle.LoadAsset<Sprite>("SnitchIco").DontUnload());
            ItemIcons.Add(bundle.LoadAsset<Sprite>("GhostStoneIco").DontUnload());
            ItemIcons.Add(bundle.LoadAsset<Sprite>("BeerIcon").DontUnload());
            
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("DelumWorldIcon").DontUnload());
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("MapWorldIcon").DontUnload());
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("KeyWorldIcon").DontUnload());
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("SnitchWorldIcon").DontUnload());
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("GhostStoneWorldIcon").DontUnload());
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("BeerWorldIcon").DontUnload());
            
            CrucioSprite.Add(bundle.LoadAsset<Sprite>("CrucioF1").DontUnload());
            CrucioSprite.Add(bundle.LoadAsset<Sprite>("CrucioF2").DontUnload());
            
            CurseSprite.Add(bundle.LoadAsset<Sprite>("CurseF1").DontUnload());
            CurseSprite.Add(bundle.LoadAsset<Sprite>("CurseF2").DontUnload());

            AllCustomHats.Add(bundle.LoadAsset<Sprite>("hat (1)").DontUnload());
            AllCustomHats.Add(bundle.LoadAsset<Sprite>("hat (2)").DontUnload());
            AllCustomHats.Add(bundle.LoadAsset<Sprite>("hat (3)").DontUnload());
            AllCustomHats.Add(bundle.LoadAsset<Sprite>("hat (4)").DontUnload());
            AllCustomHats.Add(bundle.LoadAsset<Sprite>("hat (5)").DontUnload());
            AllCustomHats.Add(bundle.LoadAsset<Sprite>("hat (6)").DontUnload());

            SmallSnitchSprite = bundle.LoadAsset<Sprite>("SmallSnitchIco").DontUnload();
            SnitchMaterial = bundle.LoadAsset<PhysicsMaterial2D>("SnitchMaterial").DontUnload();
            HPTheme = bundle.LoadAsset<AudioClip>("HPTheme").DontUnload();
            InventoryUI.PanelPrefab = bundle.LoadAsset<GameObject>("InventoryPanel").DontUnload();
            GenericOutlineMat = bundle.LoadAsset<Material>("GenericOutline").DontUnload();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Reactor.Extensions;
using Reactor.Unstrip;
using UnityEngine;
using UnityEngine.Video;

namespace HarryPotter.Classes
{
    class Asset
    {
        public List<Sprite> ItemIcons { get; }
        public Sprite CurseSprite { get; }
        public Sprite SmallSnitchSprite { get; }
        public List<Sprite> AbilityIcons { get; }
        public List<Sprite> WorldItemIcons { get; }

        public Asset()
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(Directory.GetCurrentDirectory() + "\\Assets\\harrypotter");
            ItemIcons = new List<Sprite>();
            AbilityIcons = new List<Sprite>();
            WorldItemIcons = new List<Sprite>();
            
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
            
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("DelumWorldIcon").DontUnload());
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("MapWorldIcon").DontUnload());
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("KeyWorldIcon").DontUnload());
            WorldItemIcons.Add(bundle.LoadAsset<Sprite>("SnitchWorldIcon").DontUnload());
            
            CurseSprite = bundle.LoadAsset<Sprite>("Curse").DontUnload();
            SmallSnitchSprite = bundle.LoadAsset<Sprite>("SmallSnitchIco").DontUnload();
        }
    }
}

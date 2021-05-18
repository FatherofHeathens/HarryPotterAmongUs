using System.Collections.Generic;
using UnityEngine;

namespace HarryPotter.Classes
{
    public class Hat
    {
        public static List<Hat> AllHats { get; set; }
        public bool Bounce { get; set; }
        public Vector2 ChipOffset { get; set; }
        public Sprite MainSprite { get; set; }
    }
}
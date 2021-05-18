using System.Linq;
using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class DevilHat : Hat
    {
        public DevilHat()
        {
            Bounce = false;
            ChipOffset = new Vector2(0, 0.3f);
            MainSprite = Main.Instance.Assets.AllHatSprites[2];
        }
    }
}
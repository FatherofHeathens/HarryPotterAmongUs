using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class ElephantHat : Hat
    {
        public ElephantHat()
        {
            Bounce = false;
            ChipOffset = Vector2.zero;
            MainSprite = Main.Instance.Assets.AllHatSprites[18];
        }
    }
}
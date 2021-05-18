using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class FireHat : Hat
    {
        public FireHat()
        {
            Bounce = false;
            ChipOffset = Vector2.zero;
            MainSprite = Main.Instance.Assets.AllHatSprites[0];
        }
    }
}
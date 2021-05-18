using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class FlowerHat : Hat
    {
        public FlowerHat()
        {
            Bounce = false;
            ChipOffset = new Vector2(0, 0.15f);
            MainSprite = Main.Instance.Assets.AllHatSprites[20];
        }
    }
}
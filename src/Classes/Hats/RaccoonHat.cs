using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class RaccoonHat : Hat
    {
        public RaccoonHat()
        {
            Bounce = false;
            ChipOffset = new Vector2(0, 0.45f);
            MainSprite = Main.Instance.Assets.AllHatSprites[1];
        }
    }
}
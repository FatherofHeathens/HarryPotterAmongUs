using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class PinkeeHat : Hat
    {
        public PinkeeHat()
        {
            Bounce = false;
            ChipOffset = new Vector2(0, 0.3f);
            MainSprite = Main.Instance.Assets.AllHatSprites[4];
        }
    }
}
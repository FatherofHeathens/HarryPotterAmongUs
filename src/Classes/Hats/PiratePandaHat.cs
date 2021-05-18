using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class PiratePandaHat : Hat
    {
        public PiratePandaHat()
        {
            Bounce = false;
            ChipOffset = new Vector2(-0.06f, 0.35f);
            MainSprite = Main.Instance.Assets.AllHatSprites[19];
        }
    }
}
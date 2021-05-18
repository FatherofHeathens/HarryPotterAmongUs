using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class HairHat2 : Hat
    {
        public HairHat2()
        {
            Bounce = false;
            ChipOffset = new Vector2(0, 0.3f);
            MainSprite = Main.Instance.Assets.AllHatSprites[8];
        }
    }
}
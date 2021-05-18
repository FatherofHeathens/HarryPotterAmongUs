using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class HairHat1 : Hat
    {
        public HairHat1()
        {
            Bounce = false;
            ChipOffset = new Vector2(0, 0.3f);
            MainSprite = Main.Instance.Assets.AllHatSprites[7];
        }
    }
}
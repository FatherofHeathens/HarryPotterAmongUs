using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class ScarfHat1 : Hat
    {
        public ScarfHat1()
        {
            Bounce = false;
            ChipOffset = new Vector2(0, 0.65f);
            MainSprite = Main.Instance.Assets.AllHatSprites[9];
        }
    }
}
using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class ScarfHat2 : Hat
    {
        public ScarfHat2()
        {
            Bounce = false;
            ChipOffset = new Vector2(0, 0.65f);
            MainSprite = Main.Instance.Assets.AllHatSprites[10];
        }
    }
}
using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class ScarfHat3 : Hat
    {
        public ScarfHat3()
        {
            Bounce = false;
            ChipOffset = new Vector2(0, 0.65f);
            MainSprite = Main.Instance.Assets.AllHatSprites[11];
        }
    }
}
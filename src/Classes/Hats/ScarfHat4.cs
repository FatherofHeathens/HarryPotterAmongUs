using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class ScarfHat4 : Hat
    {
        public ScarfHat4()
        {
            Bounce = false;
            ChipOffset = new Vector2(0, 0.65f);
            MainSprite = Main.Instance.Assets.AllHatSprites[12];
        }
    }
}
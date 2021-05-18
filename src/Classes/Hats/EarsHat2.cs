using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class EarsHat2 : Hat
    {
        public EarsHat2()
        {
            Bounce = false;
            ChipOffset = new Vector2(0, 0.3f);
            MainSprite = Main.Instance.Assets.AllHatSprites[15];
        }
    }
}
using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class EarsHat1 : Hat
    {
        public EarsHat1()
        {
            Bounce = false;
            ChipOffset = new Vector2(0, 0.3f);
            MainSprite = Main.Instance.Assets.AllHatSprites[3];
        }
    }
}
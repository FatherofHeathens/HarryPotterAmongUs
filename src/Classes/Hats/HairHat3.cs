using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class HairHat3 : Hat
    {
        public HairHat3()
        {
            Bounce = false;
            ChipOffset = new Vector2(0, 0.3f);
            MainSprite = Main.Instance.Assets.AllHatSprites[14];
        }
    }
}
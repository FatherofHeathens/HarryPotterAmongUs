using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class HairHat4 : Hat
    {
        public HairHat4()
        {
            Bounce = false;
            ChipOffset = new Vector2(-0.05f, 0.3f);
            MainSprite = Main.Instance.Assets.AllHatSprites[21];
        }
    }
}
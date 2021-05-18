using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class SnakeHat : Hat
    {
        public SnakeHat()
        {
            Bounce = false;
            ChipOffset = new Vector2(-0.075f, 0.425f);
            MainSprite = Main.Instance.Assets.AllHatSprites[6];
        }
    }
}
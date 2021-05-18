using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class GlitchHat : Hat
    {
        public GlitchHat()
        {
            Bounce = true;
            ChipOffset = Vector2.zero; //TODO
            MainSprite = Main.Instance.Assets.AllHatSprites[5];
        }
    }
}
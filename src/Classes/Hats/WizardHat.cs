using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class WizardHat : Hat
    {
        public WizardHat()
        {
            Bounce = true;
            ChipOffset = new Vector2(-0.05f, 0);
            MainSprite = Main.Instance.Assets.AllHatSprites[13];
        }
    }
}
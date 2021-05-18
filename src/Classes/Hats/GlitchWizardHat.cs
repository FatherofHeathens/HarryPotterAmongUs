using UnityEngine;

namespace HarryPotter.Classes.Hats
{
    public class GlitchWizardHat : Hat
    {
        public GlitchWizardHat()
        {
            Bounce = true;
            ChipOffset = new Vector2(0, 0.3f);
            MainSprite = Main.Instance.Assets.AllHatSprites[17];
        }
    }
}
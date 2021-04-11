using Essentials.Options;

namespace HarryPotter.Classes
{
    class Config
    {
        private CustomToggleOption Option1 = CustomOption.AddToggle("Order of the Impostors", false);
        private CustomToggleOption Option3 = CustomOption.AddToggle("Can Spells be Used In Vents", false);
        private CustomNumberOption Option9 = CustomOption.AddNumber("Defensive Duelist Cooldown", 20f, 10f, 40f, 2.5f);
        private CustomNumberOption Option10 = CustomOption.AddNumber("Invisibility Cloak Cooldown", 20f, 10f, 40f, 2.5f);
        private CustomNumberOption Option11 = CustomOption.AddNumber("Time Turner Cooldown", 20f, 10f, 40f, 2.5f);
        private CustomNumberOption Option12 = CustomOption.AddNumber("Crucio Cooldown", 20f, 10f, 40f, 2.5f);
        private CustomToggleOption Option14 = CustomOption.AddToggle("Each Item Spawns Only Once Per Game", true);

        public bool OrderOfTheImp { get; private set; }
        public float MapDuration { get { return 10; } }
        public float DefensiveDuelistDuration { get { return 10; } }
        public float InvisCloakDuration { get { return 10; } }
        public float HourglassTimer { get { return 10; } }
        public float CrucioDuration { get { return 10; } }
        public float DefensiveDuelistCooldown { get; private set; }
        public float InvisCloakCooldown { get; private set; }
        public float HourglassCooldown { get; private set; }
        public float CrucioCooldown { get; private set; }
        public bool SpellsInVents { get; private set; }
        public float ImperioDuration { get { return 10; } }
        public bool SingleItem { get; private set; }
        
        public void ReloadSettings()
        {
            OrderOfTheImp = Option1.GetValue();
            SpellsInVents = Option3.GetValue();
            DefensiveDuelistCooldown = Option9.GetValue();
            InvisCloakCooldown = Option10.GetValue();
            HourglassCooldown = Option11.GetValue();
            CrucioCooldown = Option12.GetValue();
            SingleItem = Option14.GetValue();
        }
    }
}

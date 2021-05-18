using hunterlib.Classes;

namespace HarryPotter.Classes
{
    class Config
    {
        private CustomToggleOption Option1 = CustomToggleOption.Create("Order of the Impostors", false);
        private CustomToggleOption Option3 = CustomToggleOption.Create("Can Spells be Used In Vents", false);
        private CustomToggleOption Option4 = CustomToggleOption.Create("Show Info Popups/Tooltips");
        private CustomToggleOption Option5 = CustomToggleOption.Create("Shared Voldemort Cooldowns");
        private CustomNumberOption Option9 = CustomNumberOption.Create("Defensive Duelist Cooldown", 20f, 40f, 10, 2.5f);
        private CustomNumberOption Option10 = CustomNumberOption.Create("Invisibility Cloak Cooldown", 20f, 40f, 10, 2.5f);
        private CustomNumberOption Option11 = CustomNumberOption.Create("Time Turner Cooldown", 20f, 40f, 10f, 2.5f);
        private CustomNumberOption Option12 = CustomNumberOption.Create("Crucio Cooldown", 20f, 40f, 10f, 2.5f);

        public bool OrderOfTheImp { get; private set; }
        public float MapDuration { get { return 10; } }
        public float DefensiveDuelistDuration { get { return 10; } }
        public float InvisCloakDuration { get { return 10; } }
        public float HourglassTimer { get { return 10; } }
        public float BeerDuration { get { return 10; } }
        public float CrucioDuration { get { return 10; } }
        public float DefensiveDuelistCooldown { get; private set; }
        public float InvisCloakCooldown { get; private set; }
        public float HourglassCooldown { get; private set; }
        public float CrucioCooldown { get; private set; }
        public bool SpellsInVents { get; private set; }
        public float ImperioDuration { get { return 10; } }
        public bool ShowPopups { get; private set; }
        public bool SeparateCooldowns { get; private set; }
        public bool SimplerWatermark { get { return false; } }
        public bool SelectRoles { get { return false; } }
        public bool UseCustomRegion { get { return false; } }
        
        public void ReloadSettings()
        {
            OrderOfTheImp = Option1.Value;
            SpellsInVents = Option3.Value;
            DefensiveDuelistCooldown = Option9.Value;
            InvisCloakCooldown = Option10.Value;
            HourglassCooldown = Option11.Value;
            CrucioCooldown = Option12.Value;
            ShowPopups = Option4.Value;
            SeparateCooldowns = !Option5.Value;
        }
    }
}

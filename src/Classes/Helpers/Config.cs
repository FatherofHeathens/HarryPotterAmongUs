using Essentials.Options;

namespace HarryPotter.Classes
{
    class Config
    {
        private CustomToggleOption Option1 = CustomOption.AddToggle("Order of the Impostors", false);
        private CustomToggleOption Option2 = CustomOption.AddToggle("Order of the Crewmates", false);
        private CustomToggleOption Option3 = CustomOption.AddToggle("Can Spells be Used In Vents", false);
        private CustomNumberOption Option4 = CustomOption.AddNumber("Marauders Map Duration", 5f, 5f, 30f, 1f);
        private CustomNumberOption Option5 = CustomOption.AddNumber("Defensive Duelist Duration", 5f, 5f, 30f, 1f);
        private CustomNumberOption Option6 = CustomOption.AddNumber("Invisibility Cloak Duration", 5f, 5f, 30f, 1f);
        private CustomNumberOption Option7 = CustomOption.AddNumber("Time Turner Timer", 5f, 5f, 30f, 1f);
        private CustomNumberOption Option8 = CustomOption.AddNumber("Crucio Duration", 5f, 5f, 30f, 1f);
        private CustomNumberOption Option13 = CustomOption.AddNumber("Imperio Duration", 5f, 5f, 30f, 1f);
        private CustomNumberOption Option9 = CustomOption.AddNumber("Defensive Duelist Cooldown", 20f, 10f, 40f, 2.5f);
        private CustomNumberOption Option10 = CustomOption.AddNumber("Invisibility Cloak Cooldown", 20f, 10f, 40f, 2.5f);
        private CustomNumberOption Option11 = CustomOption.AddNumber("Time Turner Cooldown", 20f, 10f, 40f, 2.5f);
        private CustomNumberOption Option12 = CustomOption.AddNumber("Crucio Cooldown", 20f, 10f, 40f, 2.5f);
        private CustomToggleOption Option14 = CustomOption.AddToggle("Each Item Spawns Only Once Per Game", true);

        public bool OrderOfTheImp { get; private set; }
        public bool OrderOfTheCrew { get; private set; }
        public float MapDuration { get; private set; }
        public float DefensiveDuelistDuration { get; private set; }
        public float InvisCloakDuration { get; private set; }
        public float HourglassTimer { get; private set; }
        public float CrucioDuration { get; private set; }
        public float DefensiveDuelistCooldown { get; private set; }
        public float InvisCloakCooldown { get; private set; }
        public float HourglassCooldown { get; private set; }
        public float CrucioCooldown { get; private set; }
        public bool SpellsInVents { get; private set; }
        public float ImperioDuration { get; private set; }
        public bool SingleItem { get; private set; }
        
        public void ReloadSettings()
        {
            OrderOfTheImp = Option1.GetValue();
            OrderOfTheCrew = Option2.GetValue();
            SpellsInVents = Option3.GetValue();
            MapDuration = Option4.GetValue();
            DefensiveDuelistDuration = Option5.GetValue();
            InvisCloakDuration = Option6.GetValue();
            HourglassTimer = Option7.GetValue();
            CrucioDuration = Option8.GetValue();
            DefensiveDuelistCooldown = Option9.GetValue();
            InvisCloakCooldown = Option10.GetValue();
            HourglassCooldown = Option11.GetValue();
            CrucioCooldown = Option12.GetValue();
            ImperioDuration = Option13.GetValue();
            SingleItem = Option14.GetValue();
        }
    }
}

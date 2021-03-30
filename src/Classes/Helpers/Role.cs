using UnityEngine;

namespace HarryPotter.Classes
{
    public class Role
    {
        public string RoleName { get; set; }
        public string IntroString { get; set; }
        public Color RoleColor { get; set; }
        public Color RoleColor2 { get; set; }
        public ModdedPlayerClass Owner { get; set; }
        public virtual void Update() { }

        public virtual bool PerformKill(KillButtonManager __instance)
        {
            return false;
        }
        
        public virtual void ResetCooldowns() { }

        public virtual bool ShouldDrawCustomButtons() 
        {
            return false;
        }
    }
}

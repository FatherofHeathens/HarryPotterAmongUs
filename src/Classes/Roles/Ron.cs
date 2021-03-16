using System;
using UnityEngine;

namespace HarryPotter.Classes.Roles
{
    public class Ron : Role
    {
        public KillButtonManager DDButton { get; set; }
        public DateTime LastCloak { get; set; }
        
        public Ron(ModdedPlayerClass owner)
        {
            RoleName = "Ron";
            RoleColor = Palette.Orange;
            IntroString = "Hello World";
            Owner = owner;
            DDButton = KillButtonManager.Instantiate(HudManager.Instance.KillButton);
            DDButton.renderer.enabled = true;
        }

        public override void ResetCooldowns()
        {
            LastCloak = DateTime.UtcNow;
        }

        public override void Update()
        {
            if (!Owner._Object.AmOwner)
                return;
            
            if (!HudManager.Instance)
                return;
            
            DrawButtons();
        }
        
        public override bool PerformKill(KillButtonManager __instance)
        {
            if (__instance == DDButton)
                ActivateDefensiveDuelist();
            else
                return true;
            return true;
        }

        public void ActivateDefensiveDuelist()
        {
            if (DDButton.isCoolingDown)
                return;

            if (!DDButton.isActiveAndEnabled)
                return;

            if (Owner._Object.Data.IsDead)
                return;

            ResetCooldowns();
            Main.Instance.RpcDefensiveDuelist(Owner._Object);
        }
        
        public void DrawButtons()
        {
            Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
            
            DDButton.gameObject.SetActive(HudManager.Instance.UseButton.isActiveAndEnabled);
            DDButton.renderer.sprite = Main.Instance.Assets.AbilityIcons[3];
            DDButton.transform.position = new Vector2(bottomLeft.x + 0.75f, bottomLeft.y + 0.75f);
            DDButton.SetTarget(null);
            DDButton.SetCoolDown(Main.Instance.Config.DefensiveDuelistCooldown - (float)(DateTime.UtcNow - LastCloak).TotalSeconds, Main.Instance.Config.DefensiveDuelistCooldown);
            
            bool isDead = Owner._Object.Data.IsDead;
            if (isDead)
                DDButton.SetCoolDown(0, 1);
            
            if (!DDButton.isCoolingDown && !isDead)
            {
                DDButton.renderer.material.SetFloat("_Desat", 0f);
                DDButton.renderer.color = Palette.EnabledColor;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace HarryPotter.Classes.Roles
{
    public class Hermione : Role
    {
        public KillButtonManager HourglassButton { get; set; }
        public DateTime LastHourglass { get; set; }

        public Hermione(ModdedPlayerClass owner)
        {
            RoleName = "Hermione";
            RoleColor = Palette.Orange;
            RoleColor2 = Palette.Orange;
            IntroString = "We could all have been\nkilled - or worse, expelled.";
            Owner = owner;
            
            if (!Owner._Object.AmOwner)
                return;
            
            HourglassButton = KillButtonManager.Instantiate(HudManager.Instance.KillButton);
            HourglassButton.renderer.enabled = true;
        }

        public override void ResetCooldowns()
        {
            LastHourglass = DateTime.UtcNow;
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
            if (__instance == HourglassButton)
                ActivateHourglass();
            else
                return true;
            return true;
        }

        public void ActivateHourglass()
        {
            if (HourglassButton.isCoolingDown)
                return;

            if (!HourglassButton.isActiveAndEnabled)
                return;

            if (Owner._Object.Data.IsDead)
                return;

            if (Owner.ControllerOverride != null)
                return;

            Main.Instance.UseHourglass(Owner._Object);
        }
        
        public void DrawButtons()
        {
            Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
            
            HourglassButton.gameObject.SetActive(HudManager.Instance.UseButton.isActiveAndEnabled);
            HourglassButton.renderer.sprite = Main.Instance.Assets.AbilityIcons[5];
            HourglassButton.transform.position = new Vector2(bottomLeft.x + 0.75f, bottomLeft.y + 0.75f);
            HourglassButton.SetTarget(null);
            HourglassButton.SetCoolDown(Main.Instance.Config.HourglassCooldown - (float)(DateTime.UtcNow - LastHourglass).TotalSeconds, Main.Instance.Config.HourglassCooldown);
            
            bool isDead = Owner._Object.Data.IsDead;
            if (isDead)
                HourglassButton.SetCoolDown(0, 1);
            
            if (!HourglassButton.isCoolingDown && !isDead)
            {
                HourglassButton.renderer.material.SetFloat("_Desat", 0f);
                HourglassButton.renderer.color = Palette.EnabledColor;
            }
        }
    }
}
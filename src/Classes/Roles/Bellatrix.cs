using System;
using System.Collections;
using UnityEngine;
using System.IO;
using System.Reflection;
using HarryPotter.Classes.Items;
using UnityEngine.Video;

namespace HarryPotter.Classes.Roles
{
    public class Bellatrix : Role
    {
        public KillButtonManager CrucioButton { get; set; }
        public KillButtonManager MindControlButton { get; set; }
        public MindControlMenu ControlMenu { get; set; }
        public ModdedPlayerClass MindControlledPlayer { get; set; }
        public DateTime LastCrucio { get; set; }
        
        public Bellatrix(ModdedPlayerClass owner)
        {
            RoleName = "Bellatrix";
            RoleColor = Palette.ImpostorRed;
            RoleColor2 = Palette.ImpostorRed;
            IntroString = "Oh, he knows how to play,\nlittle bitty baby Potter.";
            Owner = owner;

            if (!Owner._Object.AmOwner)
                return;

            CrucioButton = KillButtonManager.Instantiate(HudManager.Instance.KillButton);
            CrucioButton.renderer.enabled = true;
            
            MindControlButton = KillButtonManager.Instantiate(HudManager.Instance.KillButton);
            MindControlButton.renderer.enabled = true;

            ControlMenu = new MindControlMenu();
        }

        public override void ResetCooldowns()
        {
            LastCrucio = DateTime.UtcNow;
        }

        public override void Update()
        {
            if (!Owner._Object.AmOwner)
                return;
            
            if (!HudManager.Instance)
                return;

            if (Owner._Object.inVent || Owner._Object.Data.IsDead || MeetingHud.Instance ||
                !MindControlButton.isActiveAndEnabled || MindControlButton.isCoolingDown)
            {
                ControlMenu?.CloseMenu();
            }
            
            DrawButtons();

            if (MindControlledPlayer != null)
                return;
            
            ControlMenu.Update();
            if (Input.GetMouseButtonDown(1))
                PerformKill(CrucioButton);
        }

        public override bool ShouldDrawCustomButtons()
        {
            return HudManager.Instance.UseButton.isActiveAndEnabled && MindControlledPlayer == null;
        }

        public override bool PerformKill(KillButtonManager __instance)
        {
            if (__instance != CrucioButton && __instance != MindControlButton)
                return true;

            if (__instance == CrucioButton)
                CastCrucio();
            else if (__instance == MindControlButton)
                ToggleMindControlMenu();

            return false;
        }

        public void ToggleMindControlMenu()
        {
            if (MindControlButton.isCoolingDown)
                return;

            if (!MindControlButton.isActiveAndEnabled)
                return;

            if (Owner._Object.Data.IsDead)
                return;

            if (Owner._Object.inVent)
                return;

            ControlMenu?.ToggleMenu();
        }

        public void CastCrucio()
        {
            if (CrucioButton.isCoolingDown)
                return;

            if (!CrucioButton.isActiveAndEnabled)
                return;

            if (Owner._Object.Data.IsDead)
                return;

            if (Owner._Object.inVent && !Main.Instance.Config.SpellsInVents)
                return;
            
            ResetCooldowns();
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Main.Instance.RpcCreateCrucio(mouseWorld, Owner);
        }

        public void DrawButtons()
        {
            Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
            
            CrucioButton.gameObject.SetActive(ShouldDrawCustomButtons());
            CrucioButton.renderer.sprite = Main.Instance.Assets.AbilityIcons[1];
            CrucioButton.transform.position = new Vector2(bottomLeft.x + 0.75f, bottomLeft.y + 0.75f);
            CrucioButton.SetTarget(null);
            CrucioButton.SetCoolDown(Main.Instance.Config.CrucioCooldown - (float)(DateTime.UtcNow - LastCrucio).TotalSeconds, Main.Instance.Config.CrucioCooldown);
            
            MindControlButton.gameObject.SetActive(ShouldDrawCustomButtons());
            MindControlButton.renderer.sprite = Main.Instance.Assets.AbilityIcons[2];
            MindControlButton.transform.position = new Vector2(bottomLeft.x + MindControlButton.renderer.size.x + 0.75f, bottomLeft.y + 0.75f);
            MindControlButton.SetTarget(null);
            MindControlButton.SetCoolDown(Owner._Object.killTimer, PlayerControl.GameOptions.KillCooldown);

            bool isDead = Owner._Object.Data.IsDead;
            if (isDead)
                MindControlButton.SetCoolDown(0, 1);
            
            if (!MindControlButton.isCoolingDown && !isDead)
            {
                MindControlButton.renderer.material.SetFloat("_Desat", 0f);
                MindControlButton.renderer.color = Palette.EnabledColor;
            }

            if (!CrucioButton.isCoolingDown && !isDead)
            {
                CrucioButton.renderer.material.SetFloat("_Desat", 0f);
                CrucioButton.renderer.color = Palette.EnabledColor;
            }
        }
    }
}
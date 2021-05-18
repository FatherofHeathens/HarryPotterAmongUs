using System;
using UnityEngine;
using System.Collections.Generic;
using HarryPotter.Classes.Helpers.UI;
using HarryPotter.Classes.UI;
using Reactor.Extensions;

namespace HarryPotter.Classes.Roles
{
    public class Bellatrix : Role
    {
        public KillButtonManager CrucioButton { get; set; }
        public KillButtonManager MindControlButton { get; set; }
        public KillButtonManager MarkButton { get; set; }
        public ModdedPlayerClass MindControlledPlayer { get; set; }
        public List<PlayerControl> MarkedPlayers { get; set; }
        public DateTime LastCrucio { get; set; }
        public DateTime LastMark { get; set; }
        
        public Bellatrix(ModdedPlayerClass owner)
        {
            RoleName = "Bellatrix";
            RoleColor = Palette.ImpostorRed;
            RoleColor2 = Palette.ImpostorRed;
            IntroString = "Oh, he knows how to play,\nlittle bitty baby Potter.";
            Owner = owner;
            MarkedPlayers = new List<PlayerControl>();

            if (!Owner._Object.AmOwner)
                return;
            
            CrucioButton = UnityEngine.Object.Instantiate(HudManager.Instance.KillButton);
            CrucioButton.renderer.enabled = true;
            
            Tooltip tt = CrucioButton.gameObject.AddComponent<Tooltip>();
            tt.TooltipText = "Crucio:\nA spell which will blind and stun any target it hits\n<#FF0000FF>Right click to shoot this spell in the direction of your cursor";
            
            MindControlButton = UnityEngine.Object.Instantiate(HudManager.Instance.KillButton);
            MindControlButton.renderer.enabled = true;
            
            Tooltip tt2 = MindControlButton.gameObject.AddComponent<Tooltip>();
            tt2.TooltipText = "Imperio:\nOpens a menu which allows you to choose a player to mind-control\n<#FF0000FF>The mind-controlled player MUST be previously 'marked'";
            
            MarkButton = UnityEngine.Object.Instantiate(HudManager.Instance.KillButton);
            MarkButton.renderer.enabled = true;
            
            Tooltip tt3 = MarkButton.gameObject.AddComponent<Tooltip>();
            tt3.TooltipText = "Mark:\nWill 'mark' the target player to make them vulnerable to 'Imperio'";
        }

        public override void RemoveCooldowns()
        {
            LastCrucio = DateTime.UtcNow.AddSeconds(Main.Instance.Config.CrucioCooldown * -1);
            LastMark = DateTime.UtcNow.AddSeconds(10 * -1);
            Owner._Object.SetKillTimer(0);
        }
        
        public override void ResetCooldowns()
        {
            LastCrucio = DateTime.UtcNow;
            LastMark = DateTime.UtcNow;
        }

        public override void Update()
        {
            if (!Owner._Object.AmOwner)
                return;
            
            if (!HudManager.Instance)
                return;

            MarkedPlayers.RemoveAll(x => x.Data.IsDead || x.Data.Disconnected);
            foreach (PlayerControl player in MarkedPlayers)
            {
                player.myRend?.material?.SetFloat("_Outline", 1f);
                player.myRend?.material?.SetColor("_OutlineColor", Color.yellow);
            }
            
            DrawButtons();

            if (MindControlledPlayer != null)
                return;

            if (Input.GetMouseButtonDown(1)) CastCrucio();
        }

        public override bool ShouldDrawCustomButtons()
        {
            return HudManager.Instance.UseButton.isActiveAndEnabled && MindControlledPlayer == null;
        }

        public override bool PerformKill(KillButtonManager __instance)
        {
            if (__instance == CrucioButton)
                return false;
            
            if (__instance == MindControlButton)
                ToggleMindControlMenu();
            else if (__instance == MarkButton)
                MarkPlayer();
            else
                return true;

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

            MindControlMenu.Instance.ToggleMenu();
        }

        public void MarkPlayer()
        {
            if (MarkButton.isCoolingDown)
                return;

            if (!MarkButton.isActiveAndEnabled)
                return;

            if (Owner._Object.Data.IsDead)
                return;

            if (MarkButton.CurrentTarget == null)
                return;

            if (MarkedPlayers.Contains(MarkButton.CurrentTarget))
                return;

            LastMark = DateTime.UtcNow;
            MarkedPlayers.Add(MarkButton.CurrentTarget);
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

            if (InventoryUI.Instance.IsOpen || InventoryUI.Instance.IsOpeningOrClosing)
                return;

            LastCrucio = DateTime.UtcNow;
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
            
            MarkButton.gameObject.SetActive(ShouldDrawCustomButtons());
            MarkButton.renderer.sprite = Main.Instance.Assets.AbilityIcons[2];
            MarkButton.transform.position = new Vector2(bottomLeft.x + MindControlButton.renderer.size.x + MarkButton.renderer.size.x + 0.75f, bottomLeft.y + 0.75f);
            MarkButton.SetTarget(Main.Instance.GetClosestTarget(Owner._Object, true, MarkedPlayers.ToArray()));
            MarkButton.SetCoolDown(10f - (float)(DateTime.UtcNow - LastMark).TotalSeconds, 10f);

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
using System;
using UnityEngine;

namespace HarryPotter.Classes.Roles
{
    public class Voldemort : Role
    {
        public KillButtonManager CurseButton { get; set; }
        public DateTime LastCurse { get; set; }

        public Voldemort(ModdedPlayerClass owner)
        {
            RoleName = "Voldemort";
            RoleColor = Palette.ImpostorRed;
            RoleColor2 = Palette.ImpostorRed;
            IntroString = "There is no good and evil. There is only\npower, and those too weak to seek it.";
            Owner = owner;

            if (!Owner._Object.AmOwner)
                return;
            
            CurseButton = UnityEngine.Object.Instantiate(HudManager.Instance.KillButton);
            CurseButton.renderer.enabled = true;
        }

        public override void Update()
        {
            if (!Owner._Object.AmOwner)
                return;
            
            if (!HudManager.Instance)
                return;
            
            Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));

            CurseButton.gameObject.SetActive(HudManager.Instance.KillButton.isActiveAndEnabled);
            CurseButton.renderer.sprite = Main.Instance.Assets.AbilityIcons[0];
            CurseButton.transform.position = new Vector2(bottomLeft.x + 0.75f, bottomLeft.y + 0.75f);
            CurseButton.SetTarget(null);
            CurseButton.SetCoolDown(PlayerControl.GameOptions.KillCooldown - (float)(DateTime.UtcNow - LastCurse).TotalSeconds, PlayerControl.GameOptions.KillCooldown);

            bool isDead = Owner._Object.Data.IsDead;
            if (isDead)
                CurseButton.SetCoolDown(0, 1);
            
            if (!CurseButton.isCoolingDown && !isDead)
            {
                CurseButton.renderer.material.SetFloat("_Desat", 0f);
                CurseButton.renderer.color = Palette.EnabledColor;
            }
            
            if (Input.GetMouseButtonDown(1))
                PerformKill(CurseButton);
        }

        public override void ResetCooldowns()
        {
            LastCurse = DateTime.UtcNow;
        }

        public override bool PerformKill(KillButtonManager __instance)
        {
            if (__instance != CurseButton)
                return true;

            if (CurseButton.isCoolingDown)
                return false;

            if (!CurseButton.isActiveAndEnabled)
                return false;
            
            if (Owner._Object.Data.IsDead)
                return false;
            
            if (Owner._Object.inVent && !Main.Instance.Config.SpellsInVents)
                return false;

            ResetCooldowns();
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Main.Instance.RpcCreateCurse(mouseWorld, Owner);
            return false;
        }
    }
}

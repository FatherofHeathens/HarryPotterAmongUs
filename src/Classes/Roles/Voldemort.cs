using UnityEngine;

namespace HarryPotter.Classes.Roles
{
    public class Voldemort : Role
    {
        public KillButtonManager CurseButton { get; set; }
            
        public Voldemort(ModdedPlayerClass owner)
        {
            RoleName = "Voldemort";
            RoleColor = Palette.ImpostorRed;
            IntroString = "Hello World";
            Owner = owner;

            CurseButton = KillButtonManager.Instantiate(HudManager.Instance.KillButton);
            CurseButton.renderer.enabled = true;
        }

        public override void Update()
        {
            if (!Owner._Object.AmOwner)
                return;
            
            if (!HudManager.Instance)
                return;
            
            Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
            
            CurseButton.gameObject.SetActive(HudManager.Instance.KillButton.gameObject.active);
            CurseButton.renderer.sprite = Main.Instance.Assets.AbilityIcons[0];
            CurseButton.transform.position = new Vector2(bottomLeft.x + 0.75f, bottomLeft.y + 0.75f);
            CurseButton.SetTarget(null);
            CurseButton.SetCoolDown(Owner._Object.killTimer, PlayerControl.GameOptions.KillCooldown);

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

            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var v = mouseWorld - PlayerControl.LocalPlayer.myRend.bounds.center;
            var dist = Vector2.Distance(mouseWorld, PlayerControl.LocalPlayer.myRend.bounds.center);
            var d = v * 3f * (2f / dist);
            
            Main.Instance.RpcCreateCurse(d, Owner);
            return false;
        }
    }
}

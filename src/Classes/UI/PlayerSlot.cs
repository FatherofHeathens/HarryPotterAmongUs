using System;
using System.Linq;
using HarryPotter.Classes.Roles;
using HarryPotter.Classes.UI;
using Reactor;
using Reactor.Extensions;
using UnityEngine;

namespace HarryPotter.Classes.Helpers.UI
{
    [RegisterInIl2Cpp]
    class PlayerSlot : MonoBehaviour
    {
        public PlayerSlot(IntPtr ptr) : base(ptr)
        {
        }

        private void Awake()
        {
            GameObject itemButtonObj = gameObject.transform.GetChild(0).gameObject;
            PlayerButton = itemButtonObj.gameObject.AddComponent<CustomButton>();
            PlayerButton.OnClick += TryControlTargetedPlayer;
            PlayerTooltip = itemButtonObj.gameObject.AddComponent<Tooltip>();
        }

        public void TryControlTargetedPlayer()
        {
            ModdedPlayerClass localModdedPlayer = Main.Instance.GetLocalModdedPlayer();
            
            if (localModdedPlayer == null) return;
            if (((Bellatrix) localModdedPlayer.Role).MindControlledPlayer != null) return;
            if (((Bellatrix) localModdedPlayer.Role).MindControlButton.isCoolingDown) return;
            if (Main.Instance.ModdedPlayerById(TargetedPlayer.PlayerId).Immortal) return;
            if (PlayerControl.LocalPlayer.Data.IsDead) return;
            if (TargetedPlayer.Data.IsDead) return;
            if (TargetedPlayer.Data.Disconnected) return;
            if (Main.Instance.GetPlayerRoleName(localModdedPlayer) == "Bellatrix")
                if (((Bellatrix) localModdedPlayer.Role).MarkedPlayers.All(x => x.PlayerId != TargetedPlayer.PlayerId)) return;
            if (PlayerControl.LocalPlayer.inVent) return;
            if (MeetingHud.Instance) return;
            if (ExileController.Instance) return;
            if (!PlayerButton.Enabled) return;
            if (PlayerButton.HoverColor != Color.yellow) return;

            MindControlMenu.Instance.CloseMenu();
            Main.Instance.RpcControlPlayer(PlayerControl.LocalPlayer, TargetedPlayer);
        }

        public void ResetIcon()
        {
            PlayerButton.Enabled = false;
            PlayerTooltip.Enabled = false;
            
            ModdedPlayerClass localModdedPlayer = Main.Instance.GetLocalModdedPlayer();
            if (Main.Instance.GetPlayerRoleName(localModdedPlayer) != "Bellatrix")
            {
                if (Icon != null)
                {
                    Icon.HatSlot.Destroy();
                    Icon.SkinSlot.Destroy();
                    Icon.Body.Destroy();
                    Icon.gameObject.Destroy();
                    Icon.Destroy();
                }
                Icon = null;
                return;
            }
            
            if (((Bellatrix) localModdedPlayer.Role).MarkedPlayers.Count < PlayerIndex + 1)
            {
                if (Icon != null)
                {
                    Icon.HatSlot.Destroy();
                    Icon.SkinSlot.Destroy();
                    Icon.Body.Destroy();
                    Icon.gameObject.Destroy();
                    Icon.Destroy();
                }
                Icon = null;
                return;
            }

            TargetedPlayer = ((Bellatrix) localModdedPlayer.Role).MarkedPlayers.ToArray()[PlayerIndex];
            GameData.PlayerInfo data = TargetedPlayer.Data;

            PlayerButton.Enabled = true;
            PlayerButton.SetColor(Color.yellow);

            PlayerTooltip.Enabled = true;
            PlayerTooltip.TooltipText = data.PlayerName;

            if (Icon == null)
            {
                Icon = Instantiate(HudManager.Instance.IntroPrefab.PlayerPrefab, gameObject.transform).DontDestroy();
                Icon.gameObject.layer = 5;
                Icon.Body.sortingOrder = 5;
                Icon.SkinSlot.sortingOrder = 6;
                Icon.HatSlot.BackLayer.sortingOrder = 4;
                Icon.HatSlot.FrontLayer.sortingOrder = 6;
                Icon.name = data.PlayerName;
                Icon.SetFlipX(true);
                Icon.transform.localScale = Vector3.one * 2f;
            }

            PlayerControl.SetPlayerMaterialColors(data.ColorId, Icon.Body);
            DestroyableSingleton<HatManager>.Instance.SetSkin(Icon.SkinSlot, data.SkinId);
            Icon.HatSlot.SetHat(data.HatId, data.ColorId);
            PlayerControl.SetPetImage(data.PetId, data.ColorId, Icon.PetSlot);
            Icon.NameText.gameObject.SetActive(false);
        }

        public void LateUpdate()
        {
            if (MindControlMenu.Instance.IsOpen) ResetIcon();
        }
        
        public Tooltip PlayerTooltip { get; set; }
        public CustomButton PlayerButton { get; set; }
        public int PlayerIndex { get; set; }
        public PlayerControl TargetedPlayer { get; set; }
        public PoolablePlayer Icon { get; set; }
    }
}
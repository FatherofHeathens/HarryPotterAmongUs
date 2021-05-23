using System;
using System.Collections;
using System.Linq;
using HarryPotter.Classes.Helpers.UI;
using hunterlib.Classes;
using HarryPotter.Classes.Roles;
using HarryPotter.Classes.UI;
using InnerNet;
using UnityEngine;

namespace HarryPotter.Classes
{
    [RegisterInIl2Cpp]
    public class MindControlMenu: MonoBehaviour
    {
        public MindControlMenu(IntPtr ptr) : base(ptr)
        {
        }
        
        private void Awake()
        {
            if (Instance != null)
            {
                Instance.Destroy();
                Instance = null;
            }

            Instance = this;
            
            gameObject.DontDestroy();
            Panel = Instantiate(PanelPrefab).DontDestroy();
            Panel.transform.SetParent(null);
            
            Transform closeButtonObj = Panel.transform.FindChild("CloseButton");
            
            CustomButton closeButton = closeButtonObj.gameObject.AddComponent<CustomButton>();
            closeButton.HoverColor = Color.green;
            closeButton.OnClick += CloseMenu;

            Tooltip closeTooltip = closeButtonObj.gameObject.AddComponent<Tooltip>();
            closeTooltip.TooltipText = "Close Menu";
            
            for (var i = 0; i < Panel.transform.FindChild("Players").childCount; i++)
            {
                Transform inventoryButton = Panel.transform.FindChild("Players").GetChild(i);
                PlayerSlot slot = inventoryButton.gameObject.AddComponent<PlayerSlot>();
                slot.PlayerIndex = i;
            }

            IsOpen = false;
            Panel.active = false;
        }
        
        private void LateUpdate()
        {
            Panel.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2));
            IsOpen = Panel.active;

            if (!IsOpen) return;

            if (Minigame.Instance) Minigame.Instance.ForceClose();
            if (Main.Instance.GetPlayerRoleName(Main.Instance.GetLocalModdedPlayer()) != "Bellatrix") CloseMenu();
            if (Input.GetKeyDown(KeyCode.Escape)) CloseMenu();
            if (MeetingHud.Instance) CloseMenu();
            if (ExileController.Instance) CloseMenu();
            if (!AmongUsClient.Instance.IsGameStarted) CloseMenu();
            if (HudManager.Instance?.UseButton?.isActiveAndEnabled == false) CloseMenu();
            if (DestroyableSingleton<IntroCutscene>.InstanceExists) CloseMenu();
        }

        public void OpenMenu()
        {
            hunterlib.Classes.Coroutines.Start(CoOpen());
        }

        public void CloseMenu()
        {
            hunterlib.Classes.Coroutines.Start(CoClose());
        }
        
        public IEnumerator CoOpen()
        {
            if (IsOpeningOrClosing) yield break;
            
            IsOpeningOrClosing = true;
            IsOpen = true;
            Panel.active = true;
            Vector2 initalScale = Panel.transform.localScale;
            float perc = 0.2f;
            
            while (perc < 1f)
            {
                Panel.transform.localScale = new Vector2( initalScale.x * perc, initalScale.y * perc);
                perc += 0.2f;
                yield return null;
            }

            Panel.transform.localScale = initalScale;
            IsOpeningOrClosing = false;
        }

        public IEnumerator CoClose()
        {
            if (IsOpeningOrClosing) yield break;
            
            IsOpeningOrClosing = true;
            Vector2 initalScale = Panel.transform.localScale;
            float perc = 1f;
            
            while (perc > 0f)
            {
                Panel.transform.localScale = new Vector2( initalScale.x * perc, initalScale.y * perc);
                perc -= 0.2f;
                yield return null;
            }
            
            IsOpen = false;
            Panel.active = false;
            Panel.transform.localScale = initalScale;
            IsOpeningOrClosing = false;
        }

        public void ToggleMenu()
        {
            if (Main.Instance.GetPlayerRoleName(Main.Instance.GetLocalModdedPlayer()) != "Bellatrix") return;
            if (!AmongUsClient.Instance.IsGameStarted) return;
            if (HudManager.Instance?.UseButton?.isActiveAndEnabled == false) return;
            if (DestroyableSingleton<IntroCutscene>.InstanceExists) return;
            if (MeetingHud.Instance) return;
            if (Minigame.Instance) return;
            if (ExileController.Instance) return;
            if (InventoryUI.Instance.IsOpeningOrClosing) return;
            if (InventoryUI.Instance.IsOpen) return;
            if (!PlayerControl.LocalPlayer.CanMove) return;

            if (IsOpen) CloseMenu();
            else OpenMenu();
        }
        
        public bool IsOpen { get; set; }
        public bool IsOpeningOrClosing { get; set; }
        public GameObject Panel { get; set; }
        public static GameObject PanelPrefab { get; set; }
        public static MindControlMenu Instance { get; set; }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HarryPotter.Classes.UI;
using UnityEngine;
using UnityEngine.Events;
using hunterlib.Classes;

namespace HarryPotter.Classes.Helpers.UI
{
    [RegisterInIl2Cpp]
    class InventoryUI : MonoBehaviour
    {
        public InventoryUI(IntPtr ptr) : base(ptr)
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
            closeButton.OnClick += Close;

            Tooltip closeTooltip = closeButtonObj.gameObject.AddComponent<Tooltip>();
            closeTooltip.TooltipText = "Close Menu";

            for (var i = 0; i < Panel.transform.FindChild("Inventory").childCount; i++)
            {
                Transform inventoryButton = Panel.transform.FindChild("Inventory").GetChild(i);
                InventorySlot slot = inventoryButton.gameObject.AddComponent<InventorySlot>();
                slot.InventoryIndex = i;
            }
            
            IsOpen = false;
            Panel.active = false;
            FavouritedItems = new List<Item>();
        }

        private void LateUpdate()
        {
            if (!AmongUsClient.Instance.IsGameStarted) FavouritedItems.Clear();
            FavouritedItems.RemoveAll(x => x.ParentInventory == null);
            
            Panel.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2));
            IsOpen = Panel.active;
            if (Input.GetKeyDown(KeyCode.C)) Toggle();
            if (!IsOpen) return;
            if (Minigame.Instance) Minigame.Instance.ForceClose();
            if (Input.GetKeyDown(KeyCode.Escape)) Close();
            if (MeetingHud.Instance) Close();
            if (ExileController.Instance) Close();
            if (!AmongUsClient.Instance.IsGameStarted) Close();
            if (HudManager.Instance?.UseButton?.isActiveAndEnabled == false) Close();
            if (DestroyableSingleton<IntroCutscene>.InstanceExists) Close();
            if (!PlayerControl.LocalPlayer.CanMove) Close();
        }

        public void Open()
        {
            hunterlib.Classes.Coroutines.Start(CoOpen());
        }
        
        public void Close()
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

        public void Toggle()
        {
            if (!AmongUsClient.Instance.IsGameStarted) return;
            if (HudManager.Instance?.UseButton?.isActiveAndEnabled == false) return;
            if (DestroyableSingleton<IntroCutscene>.InstanceExists) return;
            if (MeetingHud.Instance) return;
            if (Minigame.Instance) return;
            if (ExileController.Instance) return;
            if (MindControlMenu.Instance.IsOpeningOrClosing) return;
            if (MindControlMenu.Instance.IsOpen) return;
            if (!PlayerControl.LocalPlayer.CanMove) return;

            if (IsOpen) Close();
            else Open();
        }
        
        public bool IsOpen { get; set; }
        public bool IsOpeningOrClosing { get; set; }
        public GameObject Panel { get; set; }
        public static GameObject PanelPrefab { get; set; }
        public static InventoryUI Instance { get; set; }
        public List<Item> FavouritedItems { get; set; }
    }
}

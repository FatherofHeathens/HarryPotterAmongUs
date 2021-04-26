using Reactor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Reactor.Extensions;
using UnityEngine;
using UnityEngine.Events;

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
            closeButton.TooltipEnabled = true;
            closeButton.Tooltip = "Hello World!\nTest.";
            closeButton.OnClick += Close;

            for (var i = 0; i < Panel.transform.FindChild("Inventory").childCount; i++)
            {
                Transform inventoryButton = Panel.transform.FindChild("Inventory").GetChild(i);
                InventorySlot slot = inventoryButton.gameObject.AddComponent<InventorySlot>();
                slot.InventoryIndex = i;
            }
            
            IsOpen = false;
            Panel.active = false;
        }

        private void LateUpdate()
        {
            Panel.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2));

            IsOpen = Panel.active;
            if (Input.GetKeyDown(KeyCode.C)) Toggle();
            
            if (!IsOpen) return;
            
            if (Minigame.Instance) Minigame.Instance.ForceClose();
            if (Input.GetKeyDown(KeyCode.Escape)) Close();
        }

        public void Open()
        {
            if (HudManager.Instance?.UseButton?.isActiveAndEnabled == false) return;
            Coroutines.Start(CoOpen());
        }
        
        public void Close()
        {
            Coroutines.Start(CoClose());
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
            if (DestroyableSingleton<IntroCutscene>.InstanceExists) return;
            if (MeetingHud.Instance) return;
            if (Minigame.Instance) return;
            if (ExileController.Instance) return;

            if (IsOpen) Close();
            else Open();
        }
        
        public bool IsOpen { get; set; }
        public bool IsOpeningOrClosing { get; set; }
        public GameObject Panel { get; set; }
        
        public static GameObject PanelPrefab { get; set; }
        public static InventoryUI Instance { get; set; }
    }
}

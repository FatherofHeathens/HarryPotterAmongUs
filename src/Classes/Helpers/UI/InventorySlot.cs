using System;
using Reactor;
using UnityEngine;

namespace HarryPotter.Classes.Helpers.UI
{
    [RegisterInIl2Cpp]
    class InventorySlot : MonoBehaviour
    {
        public InventorySlot(IntPtr ptr) : base(ptr)
        {
        }

        public void Awake()
        {
            GameObject itemSlotObj = gameObject.transform.GetChild(0).gameObject;
            ItemSpriteRenderer = itemSlotObj.GetComponent<SpriteRenderer>();
            
            GameObject itemButtonObj = gameObject.transform.GetChild(1).gameObject;
            ItemButton = itemButtonObj.gameObject.AddComponent<CustomButton>();
            ItemButton.TooltipEnabled = true;
            ItemButton.OnClick += TryUseTargetedItem;
        }

        public void LateUpdate()
        {
            TargetedItem = null;
            ItemButton.Enabled = false;
            ItemButton.Tooltip = "";
            ItemSpriteRenderer.sprite = null;
            
            ModdedPlayerClass localPlayer = Main.Instance.GetLocalModdedPlayer();
            if (localPlayer == null) return;
            if (InventoryIndex >= localPlayer.Inventory.Count) return;

            ItemButton.Enabled = true;
            TargetedItem = localPlayer.Inventory[InventoryIndex];
            ItemSpriteRenderer.sprite = TargetedItem.Icon;

            ItemButton.Tooltip = TargetedItem.Tooltip;
            ItemButton.SetColor(TargetedItem.IsSpecial ? Color.gray : Color.yellow);
        }

        public void TryUseTargetedItem()
        {
            if (!InventoryUI.Instance.IsOpen) return;
            if (TargetedItem == null) return;
            if (TargetedItem.IsSpecial) return;
            
            TargetedItem.Use();
            InventoryUI.Instance.Close();
        }

        public SpriteRenderer ItemSpriteRenderer { get; set; }
        public CustomButton ItemButton { get; set; }
        public int InventoryIndex { get; set; }
        public Item TargetedItem { get; set; }
    }
}
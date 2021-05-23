using System;
using System.Linq;
using HarryPotter.Classes.UI;
using UnityEngine;
using hunterlib.Classes;

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
            ItemButton.OnClick += TryUseTargetedItem;
            ItemButton.OnRightClick += TryFavoriteTargetedItem;
            
            ItemTooltip = itemButtonObj.gameObject.AddComponent<Tooltip>();
        }

        public void LateUpdate()
        {
            TargetedItem = null;
            ItemButton.Enabled = false;
            ItemTooltip.TooltipText = "";
            ItemSpriteRenderer.sprite = null;
            
            ModdedPlayerClass localPlayer = Main.Instance.GetLocalModdedPlayer();
            if (localPlayer == null) return;
            if (InventoryIndex >= localPlayer.Inventory.Count) return;

            ItemButton.Enabled = true;
            TargetedItem = localPlayer.Inventory[InventoryIndex];
            ItemSpriteRenderer.sprite = TargetedItem.Icon;

            ItemTooltip.TooltipText = TargetedItem.Tooltip;
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

        public void TryFavoriteTargetedItem()
        {
            if (!InventoryUI.Instance.IsOpen) return;
            if (TargetedItem == null) return;
            if (TargetedItem.IsSpecial) return;

            if (InventoryUI.Instance.FavouritedItems.RemoveAll(x => x.Id == TargetedItem.Id) == 0)
                if (InventoryUI.Instance.FavouritedItems.Count < 3)
                    InventoryUI.Instance.FavouritedItems.Add(TargetedItem);
        }

        public SpriteRenderer ItemSpriteRenderer { get; set; }
        public CustomButton ItemButton { get; set; }
        public Tooltip ItemTooltip { get; set; }
        public int InventoryIndex { get; set; }
        public Item TargetedItem { get; set; }
    }
}
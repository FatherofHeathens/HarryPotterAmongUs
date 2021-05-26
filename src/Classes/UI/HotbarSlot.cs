using System;
using UnityEngine;
using hunterlib.Classes;

namespace HarryPotter.Classes.Helpers.UI
{
    [RegisterInIl2Cpp]
    class HotbarSlot : MonoBehaviour
    {
        public HotbarSlot(IntPtr ptr) : base(ptr)
        {
        }

        public void Awake()
        {
            GameObject itemSlotObj = gameObject.transform.GetChild(0).gameObject;
            ItemSpriteRenderer = itemSlotObj.GetComponent<SpriteRenderer>();
            itemSlotObj.transform.localScale = Vector3.one * 2.5f;
            
            GameObject itemButtonObj = gameObject.transform.GetChild(1).gameObject;
            ItemButton = itemButtonObj.gameObject.AddComponent<CustomButton>();
            ItemButton.OnClick += TryUseTargetedItem;
        }

        public void Update()
        {
            TargetedItem = null;
            ItemButton.Enabled = false;
            ItemSpriteRenderer.sprite = null;
            
            ModdedPlayerClass localPlayer = Main.Instance.GetLocalModdedPlayer();
            if (localPlayer == null) return;
            if (FavoriteIndex >= InventoryUI.Instance.FavouritedItems.Count) return;

            ItemButton.Enabled = true;
            TargetedItem = InventoryUI.Instance.FavouritedItems[FavoriteIndex];
            ItemSpriteRenderer.sprite = TargetedItem.Icon;
            ItemButton.SetColor(Color.yellow);
        }

        public void TryUseTargetedItem()
        {
            if (TargetedItem == null) return;
            if (TargetedItem.IsSpecial) return;
            
            TargetedItem.Use();
        }
        
        public SpriteRenderer ItemSpriteRenderer { get; set; }
        public CustomButton ItemButton { get; set; }
        public int FavoriteIndex { get; set; }
        public Item TargetedItem { get; set; }
    }
}
using System;
using UnityEngine;
using hunterlib.Classes;

namespace HarryPotter.Classes.Helpers.UI
{
    [RegisterInIl2Cpp]
    class HotbarUI : MonoBehaviour
    {
        public HotbarUI(IntPtr ptr) : base(ptr)
        {
        }

        private void Awake()
        {
            if (Instance != null) Instance.Destroy();

            Instance = this;
            
            gameObject.DontDestroy();
            Panel = Instantiate(PanelPrefab).DontDestroy();

            for (var i = 0; i < Panel.transform.FindChild("Buttons").childCount; i++)
            {
                Transform inventoryButton = Panel.transform.FindChild("Buttons").GetChild(i);
                HotbarSlot slot = inventoryButton.gameObject.AddComponent<HotbarSlot>();
                slot.FavoriteIndex = i;
            }
        }

        private void Update()
        {
            Panel.active = false;
            
            if (!AmongUsClient.Instance.IsGameStarted) return;
            if (HudManager.Instance?.UseButton?.isActiveAndEnabled == false) return;
            if (DestroyableSingleton<IntroCutscene>.InstanceExists) return;
            if (MeetingHud.Instance) return;
            if (Minigame.Instance) return;
            if (ExileController.Instance) return;
            if (!PlayerControl.LocalPlayer.CanMove) return;
            if (PlayerControl.LocalPlayer.Data.IsDead) return;
            if (HudManager.Instance?.shhhEmblem.isActiveAndEnabled == true) return;

            Panel.active = true;

            Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2f, Camera.main.pixelHeight / 2f));
            newPos.y -= 2f;
            Panel.transform.position = newPos;
        }

        public GameObject Panel { get; set; }
        public static GameObject PanelPrefab { get; set; }
        public static HotbarUI Instance { get; set; }
    }
}

using System.Linq;
using HarryPotter.Classes.Roles;
using InnerNet;
using Reactor.Extensions;
using UnityEngine;

namespace HarryPotter.Classes
{
    public class MindControlMenu
    {
        public ChatController MenuObject { get; set; }
        
        public void OpenMenu()
        {
            MenuObject = ChatController.Instantiate(HudManager.Instance.Chat);

            MenuObject.transform.SetParent(Camera.main.transform);
            MenuObject.SetVisible(true);
            MenuObject.Toggle();

            MenuObject.TextBubble.enabled = false;
            MenuObject.TextBubble.gameObject.SetActive(false);

            MenuObject.TextArea.enabled = false;
            MenuObject.TextArea.gameObject.SetActive(false);

            MenuObject.BanButton.enabled = false;
            MenuObject.BanButton.gameObject.SetActive(false);

            MenuObject.CharCount.enabled = false;
            MenuObject.CharCount.gameObject.SetActive(false);

            MenuObject.BackgroundImage.enabled = false;

            foreach (SpriteRenderer rend in MenuObject.Content.GetComponentsInChildren<SpriteRenderer>())
            {
                if (rend.name == "SendButton" || rend.name == "QuickChatButton" || rend.name == "QuickChatIcon")
                {
                    rend.enabled = false;
                    rend.gameObject.SetActive(false);
                }
            }

            foreach (PoolableBehavior bubble in MenuObject.chatBubPool.activeChildren)
            {
                bubble.enabled = false;
                bubble.gameObject.SetActive(false);
            }
            MenuObject.chatBubPool.activeChildren.Clear();

            foreach (PlayerControl player in PlayerControl.AllPlayerControls.ToArray().Where(x => x != PlayerControl.LocalPlayer))
            {
                bool oldDead = player.Data.IsDead;
                player.Data.IsDead = false;
                MenuObject.AddChat(player, "");
                player.Data.IsDead = oldDead;
            }
        }

        public void CloseMenu()
        {
            MenuObject?.ForceClosed();
            MenuObject?.SetVisible(false);
            MenuObject?.Destroy();
            MenuObject = null;
        }

        public void ToggleMenu()
        {
            if (MenuObject != null && MenuObject.IsOpen)
                CloseMenu();
            else
                OpenMenu();
        }

        public void ClickPlayer(PlayerControl target)
        {
            if (((Bellatrix) Main.Instance.GetLocalModdedPlayer().Role).MindControlledPlayer != null)
                return;

            if (((Bellatrix) Main.Instance.GetLocalModdedPlayer().Role).MindControlButton.isCoolingDown)
                return;

            if (Main.Instance.ModdedPlayerById(target.PlayerId).Immortal)
                return;
                
            if (target.Data.IsDead)
                return;

            if (Main.Instance.GetLocalModdedPlayer()._Object.Data.IsDead)
                return;
            
            if (Main.Instance.GetLocalModdedPlayer()._Object.inVent)
                return;

            CloseMenu();
            Main.Instance.RpcControlPlayer(PlayerControl.LocalPlayer, target);
        }

        public void Update()
        {
            if (MenuObject == null || !MenuObject.IsOpen || MeetingHud.Instance || AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Started)
            {
                CloseMenu();
                return;
            }

            if (Minigame.Instance)
                Minigame.Instance.Close();
            
            foreach (PoolableBehavior bubble in MenuObject.chatBubPool.activeChildren)
            {
                Vector2 ScreenMin = Camera.main.WorldToScreenPoint(bubble.Cast<ChatBubble>().Background.bounds.min);
                Vector2 ScreenMax = Camera.main.WorldToScreenPoint(bubble.Cast<ChatBubble>().Background.bounds.max);
                
                if (Input.mousePosition.x < ScreenMin.x || Input.mousePosition.x > ScreenMax.x)
                    continue;
                
                if (Input.mousePosition.y < ScreenMin.y || Input.mousePosition.y > ScreenMax.y)
                    continue;

                if (Input.GetMouseButtonUp(0))
                {
                    ClickPlayer(PlayerControl.AllPlayerControls.ToArray().Where(x => x.Data.PlayerName == bubble.Cast<ChatBubble>().NameText.text).FirstOrDefault());
                    break;
                }
            }
        }
    }
}
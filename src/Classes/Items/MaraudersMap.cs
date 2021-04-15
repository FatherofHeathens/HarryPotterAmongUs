using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Discord;
using UnityEngine;
using InnerNet;

namespace HarryPotter.Classes.Items
{
    public class MaraudersMap : Item
    {
        public MaraudersMap(ModdedPlayerClass owner)
        {
            this.Owner = owner;
            this.ParentInventory = owner.Inventory;
            this.Id = 1;
            this.Icon = Main.Instance.Assets.ItemIcons[Id];
            this.Name = "Marauder's Map";
        }
        public override void Use()
        {
            System.Console.WriteLine("Used Marauders Map");
            this.Delete();
            
            Reactor.Coroutines.Start(ZoomOut());
        }

        public IEnumerator ZoomOut()
        {
            DateTime now = DateTime.UtcNow;
            Camera.main.orthographicSize *= 4f;
            
            bool oldActive = HudManager.Instance.ShadowQuad.gameObject.active;
            bool oldActiveKill = HudManager.Instance.KillButton.gameObject.active;
            bool oldActiveUse = HudManager.Instance.UseButton.gameObject.active;
            bool oldActiveReport = HudManager.Instance.ReportButton.gameObject.active;
            bool oldUseConsoles = Owner.CanUseConsoles;
            HudManager.Instance.ShadowQuad.gameObject.SetActive(false);
            HudManager.Instance.KillButton.gameObject.SetActive(false);
            HudManager.Instance.UseButton.gameObject.SetActive(false);
            HudManager.Instance.ReportButton.gameObject.SetActive(false);
            Owner.CanUseConsoles = false;

            while (true)
            {
                if (Minigame.Instance)
                    Minigame.Instance.Close();
                
                if ((now.AddSeconds(Main.Instance.Config.MapDuration) - DateTime.UtcNow).TotalMilliseconds < 0)
                    break;

                if (MeetingHud.Instance)
                {
                    oldActiveKill = false;
                    oldActiveReport = false;
                    oldActiveUse = false;
                    break;
                }

                if (AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Started)
                    break;

                yield return null;
            }

            Camera.main.orthographicSize /= 4f;
            
            HudManager.Instance.ShadowQuad.gameObject.SetActive(oldActive);
            HudManager.Instance.KillButton.gameObject.SetActive(oldActiveKill);
            HudManager.Instance.UseButton.gameObject.SetActive(oldActiveUse);
            HudManager.Instance.ReportButton.gameObject.SetActive(oldActiveReport);
            Owner.CanUseConsoles = oldUseConsoles;

            yield break;
        }
    }
}
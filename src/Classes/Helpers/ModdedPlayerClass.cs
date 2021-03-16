using System.Collections.Generic;
using System.Linq;
using HarryPotter.Classes.Items;
using HarryPotter.Classes.WorldItems;
using Hazel;
using Reactor.Extensions;
using Rewired;
using Rewired.ComponentControls;
using UnityEngine;
using UnityEngine.Purchasing.Security;

namespace HarryPotter.Classes
{
    public class ModdedPlayerClass
    {
        public ModdedPlayerClass(PlayerControl orgPlayer, Role role, List<Item> inventory)
        {
            _Object = orgPlayer;
            Role = role;
            Inventory = inventory;
        }

        public void Update()
        {
            if (_Object.Data.IsDead)
                ClearItems();

            HandleNameColors();
            PopulateButtons();
            Role?.Update();

            if (AmongUsClient.Instance.AmHost)
            {
                DeluminatorWorld.WorldSpawn();
                MaraudersMapWorld.WorldSpawn();
                PortKeyWorld.WorldSpawn();
                TheGoldenSnitchWorld.WorldSpawn();
            }

            foreach (WorldItem wItem in Main.Instance.AllItems)
            {
                wItem.DrawWorldIcon();
                wItem.Update();
            }

            Main.Instance.AllItems.RemoveAll(x => x.IsPickedUp);

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                for (var i = 0; i < 4; i++)
                {
                    if (HasItem(i))
                        continue;
                    GiveItem(i);
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                    if (player.Data.IsDead)
                        player.Revive();

                MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.ReviveEveryone, SendOption.Reliable);
                writer.EndMessage();
            }
        }
        
        public void HandleNameColors()
        {
            if (_Object.Data.IsDead)
                foreach (ModdedPlayerClass moddedPlayer in Main.Instance.AllPlayers)
                    if (moddedPlayer.Role != null)
                        Main.Instance.SetNameColor(moddedPlayer._Object, moddedPlayer.Role.RoleColor);

            if (Role == null)
                return;

            Main.Instance.SetNameColor(_Object, Role.RoleColor);

            if (!Main.Instance.Config.OrderOfTheCrew)
                return;

            if (Main.Instance.IsPlayerRole(this, "Harry") || 
                Main.Instance.IsPlayerRole(this, "Hermione") ||
                Main.Instance.IsPlayerRole(this, "Ron"))
            {
                foreach (ModdedPlayerClass moddedPlayer in Main.Instance.AllPlayers)
                {
                    if (Main.Instance.IsPlayerRole(moddedPlayer, "Harry") ||
                        Main.Instance.IsPlayerRole(moddedPlayer, "Hermione") ||
                        Main.Instance.IsPlayerRole(moddedPlayer, "Ron"))
                    {
                        Main.Instance.SetNameColor(moddedPlayer._Object, moddedPlayer.Role.RoleColor);
                    }
                }
            }
        }

        public void PopulateButtons()
        {
            float itemCount = 0;
            foreach (var item in Inventory)
            {
                if (item.IsSpecial)
                    continue;
                
                item.DrawIcon(HudManager.Instance.ReportButton.renderer.bounds.max.x - 0.375f, HudManager.Instance.ReportButton.renderer.bounds.max.y + 0.375f + (itemCount * 0.6f), HudManager.Instance.KillButton.transform.position.z);
                itemCount++;
            }
        }

        public bool HasItem(int id)
        {
            return Inventory.FindAll(x => x.Id == id).Count > 0;
        }

        public void GiveItem(int id)
        {
            switch (id)
            {
                case 0:
                    Inventory.Add(new Deluminator(this));
                    break;
                case 1: 
                    Inventory.Add(new MaraudersMap(this));
                    break;
                case 2: 
                    Inventory.Add(new PortKey(this));
                    break;
                case 3: 
                    Inventory.Add(new TheGoldenSnitch(this));
                    break;
            }
        }

        public void ClearItems()
        {
            while (Inventory.Count > 0)
                Inventory[0].Delete();
        }
        
        public PlayerControl _Object { get; set; }
        public Role Role { get; set; }
        public ModdedPlayerClass ControllerOverride { get; set; }
        public List<Item> Inventory { get; set; }
        public bool Immortal { get; set; }
        public bool KilledByCurse { get; set; }
        public bool CanUseConsoles { get; set; } = true;
    }
}
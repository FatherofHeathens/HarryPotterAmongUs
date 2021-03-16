using System.Collections.Generic;
using System.Linq;
using HarryPotter.Classes.Items;
using Hazel;
using Reactor.Extensions;
using UnityEngine;

namespace HarryPotter.Classes
{
    public class WorldItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Sprite Icon { get; set; }
        public Vector2 Position { get; set; }
        public GameObject ItemWorldObject { get; set; }
        public bool IsPickedUp { get; set; }

        public void Update()
        {
            if (ItemWorldObject == null)
                return;

            if (ItemWorldObject.GetComponent<SpriteRenderer>().bounds.Intersects(PlayerControl.LocalPlayer.myRend.bounds))
                PickUp();
        }

        public void DrawWorldIcon()
        {
            if (ItemWorldObject == null)
            {
                ItemWorldObject = new GameObject();
                ItemWorldObject.AddComponent<SpriteRenderer>();
                System.Console.WriteLine("Drawing item " + Name + " at: " + Position.x + ":" + Position.y);
            }

            SpriteRenderer itemRender = ItemWorldObject.GetComponent<SpriteRenderer>();
            ItemWorldObject.SetActive(true);
            itemRender.enabled = true;
            itemRender.sprite = Icon;
            itemRender.transform.localScale = new Vector2(0.5f, 0.5f);
            ItemWorldObject.transform.position = Position;
        }

        public void PickUp()
        {
            if (AmongUsClient.Instance.AmHost)
                Main.Instance.GiveGrabbedItem(this.Id);
            else
            {
                this.Delete();
                MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.TryPickupItem, SendOption.Reliable);
                writer.Write(PlayerControl.LocalPlayer.PlayerId);
                writer.Write(this.Id);
                writer.EndMessage();
            }
        }

        public void Delete()
        {
            IsPickedUp = true;
            if (ItemWorldObject != null) ItemWorldObject.Destroy();

            if (AmongUsClient.Instance.AmHost)
            {
                MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.DestroyItem, SendOption.Reliable);
                writer.Write(this.Id);
                writer.EndMessage();
            }
        }
    }
}
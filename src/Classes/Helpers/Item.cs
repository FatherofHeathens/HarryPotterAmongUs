using System.Collections.Generic;
using HarryPotter.Classes.Items;
using UnityEngine;

namespace HarryPotter.Classes
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public KillButtonManager Button { get; set; }
        public Sprite Icon { get; set; }
        public List<Item> ParentInventory { get; set; }
        public ModdedPlayerClass Owner { get; set; }
        public bool IsSpecial { get; set; }
        
        public virtual void Use() { }

        public void DrawIcon(float x, float y, float z)
        {
            if (Button == null)
            {
                Button = KillButtonManager.Instantiate(HudManager.Instance.KillButton);
                Button.renderer.enabled = true;
            }
            Button.gameObject.SetActive(Main.Instance.GetLocalModdedPlayer().Role?.RoleName == "Bellatrix" ? Main.Instance.GetLocalModdedPlayer().Role.ShouldDrawCustomButtons() : HudManager.Instance.UseButton.isActiveAndEnabled);
            Button.SetCoolDown(0f, 10f);
            Button.renderer.material.SetFloat("_Desat", 0f);
            Button.renderer.color = Palette.EnabledColor;
            Button.renderer.sprite = Icon;
            Button.transform.position = new Vector3(x, y, z);
            Button.transform.localScale = new Vector2(0.5f, 0.5f);
        }

        public void Delete()
        {
            Button?.gameObject.SetActive(false);
            ParentInventory.Remove(this);
        }
    }
}
using System.Collections.Generic;
using HarryPotter.Classes.Items;
using UnityEngine;

namespace HarryPotter.Classes
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Sprite Icon { get; set; }
        public List<Item> ParentInventory { get; set; }
        public ModdedPlayerClass Owner { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsTrap { get; set; }
        public string Tooltip { get; set; }
        
        public virtual void Use() { }
        
        public void Delete()
        {
            ParentInventory.Remove(this);
            ParentInventory = null;
        }
    }
}
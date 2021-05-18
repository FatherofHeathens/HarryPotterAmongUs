using Reactor;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace HarryPotter.Classes.Helpers.UI
{
    public delegate void ClickEvent();
    
    [RegisterInIl2Cpp]
    public class CustomButton : MonoBehaviour
    {
        public event ClickEvent OnClick;
        public event ClickEvent OnRightClick;
        public Color HoverColor { get; set; }
        public bool Enabled { get; set; }
        public SpriteRenderer Renderer { get; set; }
        
        public CustomButton(IntPtr ptr) : base(ptr)
        {
        }

        public void SetColor(Color color)
        {
            HoverColor = color;
            Renderer.material.SetColor("_OutlineColor", color);
        }
        
        private void Start()
        {
            Enabled = true;
            Renderer = gameObject.GetComponent<SpriteRenderer>();
            Renderer.material.shader = Shader.Find("Sprites/Outline");
            Renderer.material.SetColor("_OutlineColor", HoverColor);
            gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
        }

        public void Update()
        {
            if (!Enabled) Renderer.material.SetFloat("_Outline", 0f);
        }

        private void OnMouseDown()
        {
            if (!Enabled) return;
            OnClick?.Invoke();
        }

        public void OnMouseOver()
        {
            if (!Input.GetMouseButtonDown(1)) return;
            if (!Enabled) return;
            OnRightClick?.Invoke();
        }

        private void OnMouseEnter()
        {
            if (!Enabled) return;
            Renderer.material.SetFloat("_Outline", 1f);
        }

        private void OnMouseExit()
        {
            if (!Enabled) return;
            Renderer.material.SetFloat("_Outline", 0f);
        }
    }
}
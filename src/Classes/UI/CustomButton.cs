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
        public Color HoverColor { get; set; }
        public bool TooltipEnabled { get; set; }
        public string Tooltip { get; set; }
        public GameObject TooltipObj { get; set; }
        public TextMeshPro TooltipTMP { get; set; }
        public RectTransform TooltipTransform { get; set; }
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
            
            TooltipObj = new GameObject();
            
            TooltipObj.transform.SetParent(InventoryUI.Instance.Panel.transform);
            
            TooltipObj.layer = 5;

            TooltipTMP = TooltipObj.AddComponent<TextMeshPro>();
            TooltipTMP.fontSize = 1.5f;
            TooltipTMP.alignment = TextAlignmentOptions.BottomLeft;
            TooltipTMP.fontWeight = FontWeight.Bold;

            MeshRenderer tooltipRenderer = TooltipObj.GetComponent<MeshRenderer>();
            tooltipRenderer.sortingOrder = 5;
            
            TooltipObj.active = false;

            TooltipTransform = TooltipObj.GetComponent<RectTransform>();
        }

        public void LateUpdate()
        {
            TooltipTransform.sizeDelta = TooltipTMP.GetPreferredValues(Tooltip);
            TooltipTMP.text = Tooltip;

            if (!Enabled)
            {
                Renderer.material.SetFloat("_Outline", 0f);
                return;
            }
            
            Vector2 mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TooltipObj.transform.position = new Vector3(mousePositon.x + TooltipTransform.rect.size.x / 2, mousePositon.y);
            TooltipObj.transform.localScale = new Vector3(1, 1.2f);
        }

        private void OnMouseDown()
        {
            if (!Enabled) return;
            
            OnClick?.Invoke();
        }

        private void OnMouseEnter()
        {
            if (!Enabled) return;
            
            Renderer.material.SetFloat("_Outline", 1f);
            if (TooltipEnabled) TooltipObj.active = true;
        }

        private void OnMouseExit()
        {
            if (!Enabled) return;
            
            Renderer.material.SetFloat("_Outline", 0f);
            TooltipObj.active = false;
        }
    }
}
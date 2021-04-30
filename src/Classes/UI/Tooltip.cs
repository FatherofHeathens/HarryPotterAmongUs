using System;
using System.Linq;
using Reactor;
using TMPro;
using UnityEngine;

namespace HarryPotter.Classes.UI
{
    [RegisterInIl2Cpp]
    class Tooltip : MonoBehaviour
    {
        public Tooltip(IntPtr ptr) : base(ptr)
        {
        }
        
        public GameObject TooltipObj { get; set; }
        public TextMeshPro TooltipTMP { get; set; }
        public RectTransform TooltipTransform { get; set; }
        public MeshRenderer TooltipRenderer { get; set; }
        public bool Enabled { get; set; }
        public string TooltipText { get; set; }
        
        private void Start()
        {
            Enabled = true;

            TooltipObj = new GameObject();
            TooltipObj.transform.SetParent(Camera.main.transform);
            TooltipObj.layer = 5;

            TooltipTMP = TooltipObj.AddComponent<TextMeshPro>();
            TooltipTMP.fontSize = 1.7f;
            TooltipTMP.alignment = TextAlignmentOptions.BottomLeft;
            TooltipTMP.overflowMode = TextOverflowModes.Overflow;
            TooltipTMP.maskable = false;
            
            TooltipRenderer = TooltipObj.GetComponent<MeshRenderer>();
            TooltipRenderer.sortingOrder = 1000;
            TooltipRenderer.rendererPriority = 1000;

            TooltipTransform = TooltipObj.GetComponent<RectTransform>();
            TooltipObj.active = false;
        }

        public void LateUpdate()
        {
            if (!Enabled || !Main.Instance.Config.ShowPopups)
            {
                TooltipObj.active = false;
                return;
            }

            TooltipTMP.fontMaterial = Main.Instance.Assets.GenericOutlineMat;
            TooltipTMP.fontMaterial.SetFloat("_UnderlayDilate", 0.75f);

            TooltipTransform.sizeDelta = TooltipTMP.GetPreferredValues(TooltipText);
            TooltipTMP.text = TooltipText;
            
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TooltipObj.transform.position = new Vector3(mousePosition.x + (TooltipTMP.renderedWidth / 2) + 0.1f, mousePosition.y);
        }
        
        private void OnMouseEnter()
        {
            if (!Enabled || !Main.Instance.Config.ShowPopups) return;
            TooltipObj.active = true;
        }

        private void OnMouseExit()
        {
            TooltipObj.active = false;
        }
    }
}
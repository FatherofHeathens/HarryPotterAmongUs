using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using hunterlib.Classes;

namespace HarryPotter.Classes.UI
{
    public class PopupTMPHandler
    {
        public static PopupTMPHandler Instance { get; set; }
        public List<TextMeshPro> AllPopups { get; set; }

        public void CreatePopup(string message, Color color, Color outlineColor, float delay = 0)
        {
            if (!Main.Instance.Config.ShowPopups) return;
            Coroutines.Start(CoCreatePopup(message, color, outlineColor, delay));
        }

        public IEnumerator CoCreatePopup(string message, Color color, Color outlineColor, float delay = 0)
        {
            if (delay != 0) yield return new WaitForSeconds(delay);
            
            var popupObj = new GameObject();
            popupObj.layer = 5;
            popupObj.active = true;
            popupObj.transform.SetParent(Camera.main.transform);

            TextMeshPro popupTMP = popupObj.AddComponent<TextMeshPro>();
            popupTMP.fontSize = 2f;
            popupTMP.alignment = TextAlignmentOptions.Center;
            popupTMP.fontStyle = FontStyles.Bold;
            popupTMP.fontMaterial = Main.Instance.Assets.GenericOutlineMat;
            popupTMP.fontMaterial.SetFloat("_UnderlayDilate", 0.6f);
            popupTMP.fontMaterial.SetColor("_UnderlayColor", outlineColor);
            
            AllPopups.Add(popupTMP);

            MeshRenderer tooltipRenderer = popupObj.GetComponent<MeshRenderer>();
            tooltipRenderer.sortingOrder = 1000;

            float perc = 0f;
            float offsetCount = AllPopups.Count - 1;
            
            while (perc < 1f)
            {
                string colorHex = $"<#{(byte)(color.r * 255f):X2}{(byte)(color.g * 255f):X2}{(byte)(color.b * 255f):X2}{(byte)(255f * perc):X2}>";
                float percFast = Math.Clamp(perc * 1.75f, 0f, 1f);
                popupObj.transform.localPosition = new Vector3(0, offsetCount * 0.3f + 1.25f + 2 * (1 - percFast));
                popupTMP.text = $"{colorHex}{message}";
                perc += 0.05f;
                yield return null;
            }

            yield return new WaitForSeconds(3);
            
            perc = 1f;

            while (perc > 0f)
            {
                string colorHex = $"<#{(byte)(color.r * 255f):X2}{(byte)(color.g * 255f):X2}{(byte)(color.b * 255f):X2}{(byte)(255f * perc):X2}>";
                popupTMP.text = $"{colorHex}{message}";
                perc -= 0.05f;
                yield return null;
            }
            
            AllPopups.Remove(popupTMP);
            popupObj.Destroy();
        }
    }
}
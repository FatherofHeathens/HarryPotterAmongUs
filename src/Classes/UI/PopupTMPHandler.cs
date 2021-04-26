using System;
using System.Collections;
using System.Collections.Generic;
using HarryPotter.Classes.Helpers.UI;
using Reactor.Extensions;
using TMPro;
using UnityEngine;

namespace HarryPotter.Classes.UI
{
    public class PopupTMPHandler
    {
        public static PopupTMPHandler Instance { get; set; }
        public List<TextMeshPro> AllPopups { get; set; }

        public void CreatePopup(string message, Color color)
        {
            Reactor.Coroutines.Start(CoCreatePopup(message, color));
        }

        public IEnumerator CoCreatePopup(string message, Color color)
        {
            var popupObj = new GameObject();
            popupObj.layer = 5;
            popupObj.active = true;
            popupObj.transform.SetParent(Camera.main.transform);

            TextMeshPro popupTMP = popupObj.AddComponent<TextMeshPro>();
            popupTMP.fontSize = 2f;
            popupTMP.alignment = TextAlignmentOptions.Center;
            popupTMP.fontWeight = FontWeight.Bold;
            
            var popupOutlineObj = new GameObject();
            popupOutlineObj.layer = 5;
            popupOutlineObj.active = true;
            popupOutlineObj.transform.SetParent(Camera.main.transform);
            TextMeshPro popupOutlineTMP = popupOutlineObj.AddComponent<TextMeshPro>();
            popupOutlineTMP.fontSize = 2f;
            popupOutlineTMP.alignment = TextAlignmentOptions.Center;
            popupOutlineTMP.fontWeight = FontWeight.Bold;
            
            var popupOutlineObj2 = new GameObject();
            popupOutlineObj2.layer = 5;
            popupOutlineObj2.active = true;
            popupOutlineObj2.transform.SetParent(Camera.main.transform);
            TextMeshPro popupOutlineTMP2 = popupOutlineObj2.AddComponent<TextMeshPro>();
            popupOutlineTMP2.fontSize = 2f;
            popupOutlineTMP2.alignment = TextAlignmentOptions.Center;
            popupOutlineTMP2.fontWeight = FontWeight.Bold;
            
            var popupOutlineObj3 = new GameObject();
            popupOutlineObj3.layer = 5;
            popupOutlineObj3.active = true;
            popupOutlineObj3.transform.SetParent(Camera.main.transform);
            TextMeshPro popupOutlineTMP3 = popupOutlineObj3.AddComponent<TextMeshPro>();
            popupOutlineTMP3.fontSize = 2f;
            popupOutlineTMP3.alignment = TextAlignmentOptions.Center;
            popupOutlineTMP3.fontWeight = FontWeight.Bold;
            
            var popupOutlineObj4 = new GameObject();
            popupOutlineObj4.layer = 5;
            popupOutlineObj4.active = true;
            popupOutlineObj4.transform.SetParent(Camera.main.transform);
            TextMeshPro popupOutlineTMP4 = popupOutlineObj4.AddComponent<TextMeshPro>();
            popupOutlineTMP4.fontSize = 2f;
            popupOutlineTMP4.alignment = TextAlignmentOptions.Center;
            popupOutlineTMP4.fontWeight = FontWeight.Bold;

            AllPopups.Add(popupTMP);

            MeshRenderer tooltipRenderer = popupObj.GetComponent<MeshRenderer>();
            tooltipRenderer.sortingOrder = 5;

            float perc = 0f;
            float offsetCount = AllPopups.Count - 1;
            
            while (perc < 1f)
            {
                string colorHex = $"<#{(byte)(color.r * 255f):X2}{(byte)(color.g * 255f):X2}{(byte)(color.b * 255f):X2}{(byte)(255f * perc):X2}>";
                string colorHexBlack = $"<#000000{(byte)(255f * perc):X2}>";
                
                float percFast = Math.Clamp(perc * 1.75f, 0f, 1f);
                popupObj.transform.localPosition = new Vector3(0, offsetCount * 0.3f + 1.25f + 2 * (1 - percFast));
                popupTMP.text = $"{colorHex}{message}";
                
                popupOutlineObj.transform.localPosition = new Vector3(0, offsetCount * 0.3f + 1.25f + 2 * (1 - percFast) + 0.015f);
                popupOutlineTMP.text = $"{colorHexBlack}{message}";
                popupOutlineObj2.transform.localPosition = new Vector3(0, offsetCount * 0.3f + 1.25f + 2 * (1 - percFast) - 0.015f);
                popupOutlineTMP2.text = $"{colorHexBlack}{message}";
                popupOutlineObj3.transform.localPosition = new Vector3(0 + 0.015f, offsetCount * 0.3f + 1.25f + 2 * (1 - percFast));
                popupOutlineTMP3.text = $"{colorHexBlack}{message}";
                popupOutlineObj4.transform.localPosition = new Vector3(0 - 0.015f, offsetCount * 0.3f + 1.25f + 2 * (1 - percFast));
                popupOutlineTMP4.text = $"{colorHexBlack}{message}";
                
                perc += 0.05f;
                yield return null;
            }

            yield return new WaitForSeconds(3);
            
            perc = 1f;

            while (perc > 0f)
            {
                string colorHex = $"<#{(byte)(color.r * 255f):X2}{(byte)(color.g * 255f):X2}{(byte)(color.b * 255f):X2}{(byte)(255f * perc):X2}>";
                string colorHexBlack = $"<#000000{(byte)(255f * perc):X2}>";
                
                popupTMP.text = $"{colorHex}{message}";
                popupOutlineTMP.text = $"{colorHexBlack}{message}";
                popupOutlineTMP2.text = $"{colorHexBlack}{message}";
                popupOutlineTMP3.text = $"{colorHexBlack}{message}";
                popupOutlineTMP4.text = $"{colorHexBlack}{message}";
                perc -= 0.05f;
                yield return null;
            }
            
            AllPopups.Remove(popupTMP);
            popupObj.Destroy();
            popupOutlineObj.Destroy();
            popupOutlineObj2.Destroy();
            popupOutlineObj3.Destroy();
            popupOutlineObj4.Destroy();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using HarryPotter.Classes;
using HarryPotter.Classes.Items;
using Reactor.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]  
    public class MeetingHud_Start
    {
        static void Prefix(MeetingHud __instance)
        {
            if (!Main.Instance.GetLocalModdedPlayer().HasItem(3))
                return;

            foreach (PlayerVoteArea voteArea in __instance.playerStates)
            {
                GameObject confirmButton = voteArea.Buttons.transform.GetChild(0).gameObject;
                GameObject cancelButton = voteArea.Buttons.transform.GetChild(1).gameObject;
                GameObject snitchButton = Object.Instantiate(confirmButton);
                snitchButton.name = "SnitchButton";
                snitchButton.GetComponent<SpriteRenderer>().sprite = Main.Instance.Assets.SmallSnitchSprite;
                snitchButton.transform.SetParent(voteArea.Buttons.transform);
                snitchButton.transform.localPosition = new Vector3(
                    confirmButton.transform.localPosition.x -
                    (cancelButton.transform.localPosition.x - confirmButton.transform.localPosition.x),
                    confirmButton.transform.localPosition.y, confirmButton.transform.localPosition.z);
                snitchButton.transform.localScale = confirmButton.transform.localScale;
                snitchButton.SetActive(true);
            }
        }
    }
}
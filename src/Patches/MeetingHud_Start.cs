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
            if (Main.Instance.GetLocalModdedPlayer().HasItem(3))
            {
                foreach (PlayerVoteArea voteArea in __instance.playerStates)
                {
                    GameObject confirmButton = voteArea.Buttons.transform.GetChild(0).gameObject;
                    GameObject cancelButton = voteArea.Buttons.transform.GetChild(1).gameObject;
                    GameObject snitchButton = Object.Instantiate(confirmButton);
                    snitchButton.name = "SnitchButton";
                    snitchButton.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
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

            if (Main.Instance.GetLocalModdedPlayer().HasItem(8))
            {
                foreach (PlayerVoteArea voteArea in __instance.playerStates)
                {
                    GameObject confirmButton = voteArea.Buttons.transform.GetChild(0).gameObject;
                    GameObject cancelButton = voteArea.Buttons.transform.GetChild(1).gameObject;
                    GameObject sortButton = Object.Instantiate(cancelButton);
                    sortButton.name = "SortButton";
                    sortButton.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(195f / 255f, 0f, 1f);
                    sortButton.GetComponent<SpriteRenderer>().sprite = Main.Instance.Assets.SmallSortSprite;
                    sortButton.transform.SetParent(voteArea.Buttons.transform);

                    sortButton.transform.localPosition = new Vector3(
                        confirmButton.transform.localPosition.x -
                        (cancelButton.transform.localPosition.x - confirmButton.transform.localPosition.x * (voteArea.Buttons.transform.childCount == 4 ? 0.12f : 1f)),
                        confirmButton.transform.localPosition.y, confirmButton.transform.localPosition.z);

                    sortButton.transform.localScale = confirmButton.transform.localScale;
                    sortButton.SetActive(true);
                }
            }
        }
    }
}
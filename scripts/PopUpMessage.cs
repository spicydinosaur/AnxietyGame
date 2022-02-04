using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpMessage : MonoBehaviour
{
    //variables interactable via the Unity GUI to designate a dialogbox (usually the image child of the canvas child of the npc/sign/whevs) As well as a yes and no button if necessary.
    public GameObject dialogBox;
    public GameObject YesButton;
    public GameObject NoButton;

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("Player"))
        {
            dialogBox.SetActive(true);
            YesButton.SetActive(true);
            NoButton.SetActive(true);
            GetComponentInChildren<TextMeshProUGUI>().SetText("Pet the dog?");
            GetComponentInChildren<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top;

        }
        //Debug.Log("This is working");
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            dialogBox.SetActive(false);
            YesButton.SetActive(false);
            NoButton.SetActive(false);
        }
    }
   public void OnYesClick()
   {

        YesButton.SetActive(false);
        NoButton.SetActive(false);
        GetComponentInChildren<TextMeshProUGUI>().SetText("BARK! BARK!");
        GetComponentInChildren<TextMeshProUGUI>().alignment = TextAlignmentOptions.Midline;

    }

    public void OnNoClick()
    {

        YesButton.SetActive(false);
        NoButton.SetActive(false);
        GetComponentInChildren<TextMeshProUGUI>().SetText("*whine*");
        GetComponentInChildren<TextMeshProUGUI>().alignment = TextAlignmentOptions.Midline;

    }
}

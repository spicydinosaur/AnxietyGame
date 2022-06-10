using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DogPopUpMessage : MonoBehaviour
{
    //variables interactable via the Unity GUI to designate a dialogbox (usually the image child of the canvas child of the npc/sign/whevs) As well as a yes and no button if necessary.

    public GameObject thoughtBox;
    public GameObject dialogueBox;
    public GameObject yesButton;
    public GameObject noButton;

    public Sprite dogPortraitCurious;
    public Sprite dogPortraitSad;
    public Sprite dogPortraitHappy;

    public Sprite portraitTransparency;
    private Sprite defaultPortraitImage;

    public GameObject dialoguePortraitBox;
    public GameObject thoughtPortraitBox;


    public bool InteractingWithDog;



    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("Player"))
        {
            InteractingWithDog = true;

            thoughtBox.SetActive(true);
            yesButton.SetActive(true);
            noButton.SetActive(true);
            defaultPortraitImage = thoughtPortraitBox.GetComponent<Image>().sprite;
            thoughtPortraitBox.SetActive(true);
            dialoguePortraitBox.SetActive(true);
            thoughtPortraitBox.GetComponent<Image>().sprite = dogPortraitCurious;
            thoughtBox.GetComponentInChildren<TextMeshProUGUI>().verticalAlignment = VerticalAlignmentOptions.Top;
            thoughtBox.GetComponentInChildren<TextMeshProUGUI>().horizontalAlignment = HorizontalAlignmentOptions.Justified;
            thoughtBox.GetComponentInChildren<TextMeshProUGUI>().SetText("Pet the dog?");
            yesButton.GetComponentInChildren<TextMeshProUGUI>().verticalAlignment = VerticalAlignmentOptions.Middle;
            yesButton.GetComponentInChildren<TextMeshProUGUI>().horizontalAlignment = HorizontalAlignmentOptions.Center;
            yesButton.GetComponentInChildren<TextMeshProUGUI>().SetText("She looks so happy.");
            noButton.GetComponentInChildren<TextMeshProUGUI>().verticalAlignment = VerticalAlignmentOptions.Middle;
            noButton.GetComponentInChildren<TextMeshProUGUI>().horizontalAlignment = HorizontalAlignmentOptions.Center;
            noButton.GetComponentInChildren<TextMeshProUGUI>().SetText("What if she bites?");

        }

    }

    private void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {

            InteractingWithDog = false;

            if (thoughtBox.activeSelf == true)
            {

                thoughtBox.GetComponentInChildren<TextMeshProUGUI>().SetText("");
                thoughtBox.SetActive(false);

                yesButton.GetComponentInChildren<TextMeshProUGUI>().SetText("");
                yesButton.SetActive(false);

                noButton.GetComponentInChildren<TextMeshProUGUI>().SetText("");
                noButton.SetActive(false);
            }
            if (dialogueBox.activeSelf == true)
            {

                dialogueBox.GetComponentInChildren<TextMeshProUGUI>().SetText("");
                dialogueBox.SetActive(false);

            }
            if (dialoguePortraitBox.activeSelf == true)
            {
                dialoguePortraitBox.GetComponent<Image>().sprite = portraitTransparency;
                dialoguePortraitBox.SetActive(false);
            }
            if (thoughtPortraitBox.activeSelf == true)
            {
                thoughtPortraitBox.GetComponent<Image>().sprite = defaultPortraitImage;
                thoughtPortraitBox.SetActive(false);
            }


        }

    }

    public void OnDogYesClick()
    {

        Debug.Log("OnDogYesClick fired, this log coming from the DogPopUpMessage script.");
        thoughtBox.GetComponentInChildren<TextMeshProUGUI>().SetText("");
        thoughtPortraitBox.GetComponent<Image>().sprite = defaultPortraitImage;
        yesButton.SetActive(false);
        noButton.SetActive(false);
        thoughtBox.SetActive(false);
        dialogueBox.SetActive(true);
        dialoguePortraitBox.GetComponent<Image>().sprite = dogPortraitHappy;
        yesButton.GetComponentInChildren<TextMeshProUGUI>().verticalAlignment = VerticalAlignmentOptions.Middle;
        yesButton.GetComponentInChildren<TextMeshProUGUI>().horizontalAlignment = HorizontalAlignmentOptions.Center;
        dialogueBox.GetComponentInChildren<TextMeshProUGUI>().SetText("BARK! BARK!");


    }

    public void OnDogNoClick()
    {

        Debug.Log("OnDogNoClick fired, this log coming from the DogPopUpMessage script.");
        thoughtBox.GetComponentInChildren<TextMeshProUGUI>().SetText("");
        thoughtPortraitBox.GetComponent<Image>().sprite = defaultPortraitImage;
        thoughtBox.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        dialogueBox.SetActive(true);
        dialoguePortraitBox.GetComponent<Image>().sprite = dogPortraitSad;
        noButton.GetComponentInChildren<TextMeshProUGUI>().verticalAlignment = VerticalAlignmentOptions.Middle;
        noButton.GetComponentInChildren<TextMeshProUGUI>().horizontalAlignment = HorizontalAlignmentOptions.Center;
        dialogueBox.GetComponentInChildren<TextMeshProUGUI>().SetText("*whine*");


    }
}

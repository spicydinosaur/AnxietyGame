using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTree : MonoBehaviour
{
    //variables interactable via the Unity GUI to designate a dialogbox (usually the image child of the canvas child of the npc/sign/whevs) As well as a yes and no button if necessary.
    public GameObject dialogBox;


    public float textDisplayTime;
    public float currentTextDisplayTime = 0f;

    public enum interactionStateEnum {interactionNotBegun, interactionStarted, interactionInterrupted, interactionEnded};
    public interactionStateEnum interactionStateValue = interactionStateEnum.interactionNotBegun;

    public string heroLeaving;
    public string heroReturning;

    public string[] dialogue;//these variables hold the dialogue the NPC has in an array (dialogue)
    public string[] dialogueChoices; //response options for the PC where applicable (dialogueChoices) as well as the next interaction that will occur. 
    //3 dialogue choices each time this is needed. Should we skip to the next three choices when another interaction is needed?
    public DialogueTree[] nextDialogueTree; //holds the next dialogue tree to be used when a selection is made.
    public int currentDialoguePoint;
    public int nextDialoguePoint;     //The next step in the conversation is tracked here (nextDialoguePoint).
    public int[] restartPoints; //where the conversation picks up if the PC returns at any time during the dialogue
    public int nextRestartPoint; //keeps track of the next restart point and refreshes everytime that point is exceeded.

    //put in dialogue tree components so that multiple NPCs can use the code. 


    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {

            if (currentTextDisplayTime < textDisplayTime && interactionStateValue == interactionStateEnum.interactionStarted)
            {
                currentTextDisplayTime += Time.deltaTime;
                if (currentTextDisplayTime >= textDisplayTime && dialogBox.activeSelf == true)
                {
                    dialogBox.GetComponentInChildren<TextMeshProUGUI>().SetText(dialogue[nextDialoguePoint]);
                    currentTextDisplayTime = 0f;
                    // note to self: check for dialogue ending
                    nextDialoguePoint++;
                    if (nextDialoguePoint == dialogue.Length)
                    {
                        interactionStateValue = interactionStateEnum.interactionEnded;
                        StartCoroutine(coroutineCloseDialogueBox());
                    }
                    else if (nextDialoguePoint > restartPoints[nextRestartPoint])
                    {
                        nextRestartPoint++;

                    }

                }


            }

        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("Player"))
        {
            //StopCoroutine(coroutineCloseDialogueBox());

            if (interactionStateValue == interactionStateEnum.interactionNotBegun)
            {
                dialogBox.SetActive(true);
                dialogBox.GetComponentInChildren<TextMeshProUGUI>().SetText(dialogue[0]);
                dialogBox.GetComponentInChildren<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopLeft;
                interactionStateValue = interactionStateEnum.interactionStarted;

            }

            else if (interactionStateValue == interactionStateEnum.interactionInterrupted)
            {
                StopCoroutine(coroutineCloseDialogueBox()); //test this
                dialogBox.SetActive(true);
                dialogBox.GetComponentInChildren<TextMeshProUGUI>().SetText(heroReturning);
                interactionStateValue = interactionStateEnum.interactionStarted;
            }

        }

    }

    void  OnTriggerExit2D (Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        { 
            if (interactionStateValue == interactionStateEnum.interactionStarted)
            {
                dialogBox.GetComponentInChildren<TextMeshProUGUI>().SetText(heroLeaving);
                interactionStateValue = interactionStateEnum.interactionInterrupted;
                currentTextDisplayTime = 0f;
            }
            StartCoroutine(coroutineCloseDialogueBox());

        }
           
    }

    IEnumerator coroutineCloseDialogueBox()
    {
        yield return new WaitForSeconds(textDisplayTime);
        dialogBox.SetActive(false);
        StopCoroutine(coroutineCloseDialogueBox());
    }
}


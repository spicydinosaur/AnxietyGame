using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBranch : MonoBehaviour
{
    //variables interactable via the Unity GUI to designate a dialogbox (usually the image child of the canvas child of the npc/sign/whevs) As well as a yes and no button if necessary.
    public GameObject dialogBox;
    /// <summary>
    /// Sets the "speed" at which the dialogue pane switches between text blocks.  ALSO DETERMINES the base speed which longer text blocks stay on the screen. 
    /// </summary>
    public float defaultTextDisplayTime = 3f;
    public float textDisplayTime;
    public float currentTextDisplayTime = 0f;

    public enum interactionStateEnum {NotBegun, Started, Interrupted, WaitForResponse, Ended};
    public interactionStateEnum interactionStateValue = interactionStateEnum.NotBegun;

    public string heroLeaving;
    public string heroReturning;

    public string[] dialogue;//these variables hold the dialogue the NPC has in an array (dialogue)
    public string[] dialogueChoices; //response options for the PC where applicable (dialogueChoices) as well as the next interaction that will occur. 
    //3 dialogue choices each time this is needed. Should we skip to the next three choices when another interaction is needed?
    public DialogueTree dialogueTree;
    public DialogueBranch[] nextDialogueBranch; //holds the next dialogue tree to be used when a selection is made.
    public int currentDialoguePoint;
    public int nextDialoguePoint;     //The next step in the conversation is tracked here (nextDialoguePoint).
    public int[] restartPoints; //where the conversation picks up if the PC returns at any time during the dialogue
    public int nextRestartPoint; //keeps track of the next restart point and refreshes everytime that point is exceeded.

    //put in dialogue tree components so that multiple NPCs can use the code. 

    public void Start()
    {
        dialogueTree = transform.parent.GetComponent<DialogueTree>();
        textDisplayTime = defaultTextDisplayTime;
    }

    public void ContinueInteraction()
    {
        if (currentTextDisplayTime < textDisplayTime && interactionStateValue == interactionStateEnum.Started)
        {
            currentTextDisplayTime += Time.deltaTime;
            if (currentTextDisplayTime >= textDisplayTime && dialogBox.activeSelf == true)
            {
                dialogBox.GetComponentInChildren<TextMeshProUGUI>().SetText(dialogue[nextDialoguePoint]);
                currentTextDisplayTime = 0f;
                textDisplayTime = dialogue[nextDialoguePoint].Length * 0.1f * defaultTextDisplayTime < defaultTextDisplayTime ? defaultTextDisplayTime : dialogue[nextDialoguePoint].Length * 0.05f * defaultTextDisplayTime;
                // note to self: check for dialogue ending
                nextDialoguePoint++;
                if (nextDialoguePoint == dialogue.Length)
                {
                    Debug.Log(this.name + " Branch reached the end. Close Dialogue Box coroutine activated.");
                    interactionStateValue = interactionStateEnum.Ended;
                    //CloseDialogueBox only happens if there's no choices at the end of the branch.
                    StartCoroutine(coroutineCloseDialogueBox());
                }
                else if (nextDialoguePoint > restartPoints[nextRestartPoint])
                {
                    nextRestartPoint++;

                }

            }

        }
    }

   

    public void StartInteraction()
    {

        StopCoroutine(coroutineCloseDialogueBox());

        if (interactionStateValue == interactionStateEnum.NotBegun)
        {
            dialogBox.SetActive(true);
            dialogBox.GetComponentInChildren<TextMeshProUGUI>().SetText(dialogue[0]);
            dialogBox.GetComponentInChildren<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopLeft;
            interactionStateValue = interactionStateEnum.Started;

        }

        else if (interactionStateValue == interactionStateEnum.Interrupted)
        {
            StopCoroutine(coroutineCloseDialogueBox());
            dialogBox.SetActive(true);
            dialogBox.GetComponentInChildren<TextMeshProUGUI>().SetText(heroReturning);
            interactionStateValue = interactionStateEnum.Started;
            nextDialoguePoint = restartPoints[nextRestartPoint];
        }
    }

   

    public void EndInteraction()
    {
        if (interactionStateValue == interactionStateEnum.Started || interactionStateValue == interactionStateEnum.WaitForResponse)
        {
            dialogBox.GetComponentInChildren<TextMeshProUGUI>().SetText(heroLeaving);
            interactionStateValue = interactionStateEnum.Interrupted;
            currentTextDisplayTime = 0f;
        }
        StartCoroutine(coroutineCloseDialogueBox());

    }


    IEnumerator coroutineCloseDialogueBox()
    {
        yield return new WaitForSeconds(textDisplayTime);
        if (interactionStateValue == interactionStateEnum.Interrupted || interactionStateValue == interactionStateEnum.Ended)
        {
            dialogBox.SetActive(false);
        }
        StopCoroutine(coroutineCloseDialogueBox());
    }
}


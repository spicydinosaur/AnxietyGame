using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class ChildSquealing : MonoBehaviour
{
    //Various variables editable from the Unity GUI

    //This is the TMPro box for this script. Drag and Drop in the Unity GUI
    public GameObject dialogBox;
    //beginning of the squeallening
    public string squeal = "e";
    //excited end to squealing
    public string squealEnd = "!!!";
    //frames to wait while player is colliding with npc before adding another "e"
    public int framesToWait = 3;
    //Tracks how many frames have currently passed and which one we are on
    public int currentFrame = 0;
    //how many times we want to run the squeallening script, ultimately leading to more e's each time. (we settled at 300)
    public int timesToRun = 0;


    //if the player collides with this NPC it sets the TMPro to true, allowing for the text to be visible. Text starts out as "e!!!"
    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("Player"))
        {
            dialogBox.SetActive(true);
            GetComponentInChildren<TextMeshProUGUI>().SetText(squeal+squealEnd);
        }

    }


    //triggers every frame if the player has not left and remains inside the trigger area
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            
            timesToRun++;
            currentFrame++;
            if (timesToRun >= 300)
            {
                //Do nothing, keep adding those numbers up.
                return;

            }
            else if (framesToWait == currentFrame)
            {
                //We've waited long enough, add more eeeee's!
                //this adds another e to the back of the incessant screaming. (squeal is the e's, squealend is the !!!
                squeal += "e";
                currentFrame = 0;
                GetComponentInChildren<TextMeshProUGUI>().SetText(squeal+squealEnd);

            }



        }

    }

    //This is some stuff for when the player steps out of the collision area, mostly done to keep everything in order. (though it isn't necessary, both enter and exit have this.)
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            //Since this is the player leaving, reset everything
            squeal = "e";
            GetComponentInChildren<TextMeshProUGUI>().SetText(squeal+squealEnd);
            currentFrame = 0;
            dialogBox.SetActive(false);
            timesToRun = 0;
        }
    }
}

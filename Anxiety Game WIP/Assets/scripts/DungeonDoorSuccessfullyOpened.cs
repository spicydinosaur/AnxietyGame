using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoorSuccessfullyOpened : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite dungeonDoorOpened;
    public Sprite dungeonDoorClosed;
    public AudioSource dungeonDoorOpeningSound;


    public void GateOpeningOrClosing()
    {
        dungeonDoorOpeningSound.Play(0);
    }

    public void StopSound()
    {
        dungeonDoorOpeningSound.Stop();
    }

    public void PathClearedToRoomThree()
    {
        //dungeonDoorOpeningSound.Stop();
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = dungeonDoorOpened;
        GameManager.tutorialRuinsThreeDoorOpened = true;

    }

    public void PathBlockedForRoomThree()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = dungeonDoorClosed;
    }

}

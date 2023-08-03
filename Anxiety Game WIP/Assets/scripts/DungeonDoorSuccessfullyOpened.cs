using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoorSuccessfullyOpened : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite dungeonDoorOpened;
    public void PathClearedToRoomThree()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Animator>().SetBool("PathCleared", true);
        gameObject.GetComponent<SpriteRenderer>().sprite = dungeonDoorOpened;    }

}

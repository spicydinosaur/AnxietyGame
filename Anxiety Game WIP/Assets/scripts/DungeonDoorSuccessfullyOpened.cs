using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoorSuccessfullyOpened : MonoBehaviour
{
    // Start is called before the first frame update

    public void PathClearedToRoomThree()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Animator>().SetBool("PathCleared", true);
    }

}

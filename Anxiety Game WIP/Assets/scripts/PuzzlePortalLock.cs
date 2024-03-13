using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePortalLock : PuzzleLock
{

    public GameObject portalCollider;

    public override void CheckLocks()
    {
        base.CheckLocks();
        if (lockCheck == PuzzleLock.LockCheck.unlocked)
        {
            GetComponent<Animator>().SetBool("DoorOpening", true);
            GetComponent<SpriteRenderer>().enabled = true;
            portalCollider.SetActive(true);

            CompleteLock();

        }
    }
}

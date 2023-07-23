using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoorLock : PuzzleLock
{
    // Start is called before the first frame update
    public override void CheckLocks()
    {
        base.CheckLocks();
        if (lockCheck == LockCheck.unlocked)
        {
            GetComponent<Animator>().SetBool("DoorOpening", true);
        }
    } 
}


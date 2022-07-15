using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLock : MonoBehaviour 
{

    
    public PuzzleKey[] puzzleKeys;
    public enum LockCheck {locked, unlocked};
    public LockCheck lockCheck;



    
    public virtual void CheckLocks()
    {
        foreach (PuzzleKey puzzleKey in puzzleKeys)
        {
            if (lockCheck == LockCheck.unlocked)
            {
                continue;
            }
            else if (lockCheck == LockCheck.locked)
            {
                lockCheck = LockCheck.locked;
                break;
            }
            else
            {
                Debug.Log("value for the lock check " + this.name + " has a value of " + lockCheck);
            }
        }
    }
    
}

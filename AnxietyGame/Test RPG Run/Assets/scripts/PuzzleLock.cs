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
        Debug.Log("lockCheck - PuzzleLock script");
        foreach (PuzzleKey puzzleKey in puzzleKeys)
        {

            lockCheck = LockCheck.unlocked;

            if (puzzleKey.lockCheck == PuzzleKey.LockCheck.unlocked)
            {
                Debug.Log("lockCheck == LockCheck.unlocked - PuzzleLock script");
                continue;
            }
            else if (puzzleKey.lockCheck == PuzzleKey.LockCheck.locked)
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

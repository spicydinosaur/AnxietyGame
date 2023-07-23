using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLock : MonoBehaviour 
{

    
    public PuzzleKey[] puzzleKeys;
    public enum LockCheck {locked, unlocked, complete};
    public LockCheck lockCheck;


    public virtual void CompleteLock()
    {
        lockCheck = LockCheck.complete;
        foreach (PuzzleKey puzzleKey in puzzleKeys)
        {
            puzzleKey.lockCheck = PuzzleKey.LockCheck.complete;
        }
    }

    public virtual void CheckLocks()
    {
        if (lockCheck != LockCheck.complete)
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
}

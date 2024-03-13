using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleKey : MonoBehaviour 
{
    
     public enum LockCheck { locked, unlocked, complete };
    
    
     public LockCheck lockCheck;
     public PuzzleLock puzzleLock;

    public void CompleteKey()
    {
        lockCheck = LockCheck.complete;
    }


    public virtual void ChangeLockState(LockCheck lockState)
     {
        if (lockCheck != PuzzleKey.LockCheck.complete)
        {
            lockCheck = lockState;
            puzzleLock.CheckLocks();
        }
     }
    
}

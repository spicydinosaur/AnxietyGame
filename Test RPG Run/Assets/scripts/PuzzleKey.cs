using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleKey : MonoBehaviour 
{
    
     public enum LockCheck { locked, unlocked };
    
    
     public LockCheck lockCheck;
     public PuzzleLock puzzleLock;

    
     public virtual void ChangeLockState(LockCheck lockState)
     {
         lockCheck = lockState;
         puzzleLock.CheckLocks();
     }
    
}

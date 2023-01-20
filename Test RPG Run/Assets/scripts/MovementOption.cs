using Unity;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Connected to the script AdvancedPatrolScript

[Serializable]
public class MovementOption 
{
    public enum OptionType { Stop, Wait, MoveTo, LookAt, FollowTarget, ChaseTarget, RunFromTarget }
    public OptionType movementOption;
    public Vector2 movementPosition;
    public float duration;
    public GameObject target;

    public virtual void Activate()
    {

    }
}



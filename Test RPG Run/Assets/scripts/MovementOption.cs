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
    public enum OptionType { Stop, Wait, MoveTo, LookAt, FollowTarget, RunFromTarget, MeleeAttack }
    public OptionType movementOption;
    public Vector2 movementPosition;
    public float duration;
    public float distanceFromTarget;
    public GameObject target;
    //an individual interrupt should set the looping on its end.
}



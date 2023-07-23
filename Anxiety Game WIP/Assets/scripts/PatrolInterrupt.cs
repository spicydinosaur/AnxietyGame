using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolInterrupt : MonoBehaviour
{
    public AdvancedPatrolScript advancedPatrolScript;

    public string targetTag = "Player";

    [SerializeField]
    public MovementOption[] preinterrupt;
    [SerializeField]
    public MovementOption[] interrupt;
    [SerializeField]
    public MovementOption[] postinterrupt;

    public void OnEnable()
    {
        if (advancedPatrolScript == null)
        {
            advancedPatrolScript = GetComponentInParent<AdvancedPatrolScript>();
        }

    }

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(targetTag))
        {
            //how do we get the game to individually decide whether an interrupt is looped or not on advancedPatrolScript?
            advancedPatrolScript.Interrupt(preinterrupt, interrupt, postinterrupt);

        }
    }

    public virtual void OnTriggerExit2D(Collider2D collider)

    {
        if (collider.gameObject.CompareTag(targetTag))
        {
            advancedPatrolScript.StopInterrupt();

        }
    }
}

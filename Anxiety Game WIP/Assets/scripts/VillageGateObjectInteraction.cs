using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillageGateObjectInteraction : ObjectInteraction
{
    public Sprite gateClosed;
    public Sprite gateOpen;

    public GameObject canInteract;
    public GameObject cantInteract;

    public bool playerTouchingObject;

    public Animator animator;

    public GameObject gateColliderClosed;
    public GameObject gateColliderOpen;

    public Vector3 interactHereClosedLocation;
    public Vector3 interactHereOpenLocation;


//This script has a partner script called VillageGateColliders. That script enables and disables colliders depending on whether the gate is opened or closed.
    void Start()
    {
        playerTouchingObject = false;
        animator = GetComponent<Animator>();
        player = hero.GetComponent<Player>();
        if (animator.GetBool("closed"))
        {
            canInteract.transform.localPosition = interactHereClosedLocation;
            cantInteract.transform.localPosition = interactHereClosedLocation;
            GetComponent<SpriteRenderer>().sprite = gateClosed;
            gateColliderClosed.SetActive(true);
            gateColliderOpen.SetActive(false);
        }
        else
        {
            canInteract.transform.localPosition = interactHereOpenLocation;
            cantInteract.transform.localPosition = interactHereOpenLocation;
            GetComponent<SpriteRenderer>().sprite = gateOpen;
            gateColliderClosed.SetActive(false);
            gateColliderOpen.SetActive(true);
        }

    }


    public override void InteractWithObject()
    {
        if (playerTouchingObject)
        {
            if (GameManager.tutorialHasRuinsKey && !animator.GetBool("moving"))
            {
                if (animator.GetBool("closed"))
                {
                    StartOpenGate();
                }
                else
                {
                    StartCloseGate();
                }
            }
        }
    }

    public void StartOpenGate()
    {
        animator.SetBool("moving", true);

    }

    public void GateOpened()
    {
        animator.SetBool("moving", false);
        animator.SetBool("closed", false);

        gateColliderClosed.SetActive(false);
        gateColliderOpen.SetActive(true);

        canInteract.transform.localPosition = interactHereOpenLocation;
        cantInteract.transform.localPosition = interactHereOpenLocation;
        GetComponent<SpriteRenderer>().sprite = gateOpen;
    }

    public void StartCloseGate()
    {
        animator.SetBool("moving", true);
        
    }

    public void GateClosed()
    {
        animator.SetBool("moving", false);
        animator.SetBool("closed", true);

        gateColliderClosed.SetActive(true);
        gateColliderOpen.SetActive(false);

        canInteract.transform.localPosition = interactHereClosedLocation;
        cantInteract.transform.localPosition = interactHereClosedLocation;
        GetComponent<SpriteRenderer>().sprite = gateClosed;
    }

}


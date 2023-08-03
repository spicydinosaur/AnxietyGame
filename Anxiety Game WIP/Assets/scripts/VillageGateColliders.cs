using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageGateColliders : MonoBehaviour
{

    public VillageGateObjectInteraction objectInteraction;

    public void Start()
    {
        if (objectInteraction == null) 
        { 
            objectInteraction = gameObject.GetComponentInParent<VillageGateObjectInteraction>(); 
        }
    }


    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("Player"))
        {
            if (objectInteraction.hasKey)
            {
                objectInteraction.canInteract.SetActive(true);
                objectInteraction.player.objectInteraction = gameObject.GetComponentInParent<ObjectInteraction>();

            }
            else
            {

                objectInteraction.cantInteract.SetActive(true);

            }

            objectInteraction.playerTouchingObject = true;

        }
    }


    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (objectInteraction.canInteract.activeSelf == true)
            {
                objectInteraction.canInteract.SetActive(false);
            }
            else if (objectInteraction.cantInteract.activeSelf == true)
            {
                objectInteraction.cantInteract.SetActive(false);
            }

            objectInteraction.playerTouchingObject = false;
            objectInteraction.player.objectInteraction = null;
        }
    }
}

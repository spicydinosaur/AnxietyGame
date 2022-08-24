using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractionMiniColliders : MonoBehaviour
{

    public ObjectInteraction objectInteraction;

    public enum ColliderDirection { north, south, east, west};

    public ColliderDirection colliderDirection;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (colliderDirection == ColliderDirection.north)
            {
                objectInteraction.colliderDirection = ObjectInteraction.ColliderDirection.north;
            }
            else if (colliderDirection == ColliderDirection.south)
            {
                objectInteraction.colliderDirection = ObjectInteraction.ColliderDirection.south;
            }
            else if (colliderDirection == ColliderDirection.east)
            {
                objectInteraction.colliderDirection = ObjectInteraction.ColliderDirection.east;
            }
            else if (colliderDirection == ColliderDirection.west)
            {
                objectInteraction.colliderDirection = ObjectInteraction.ColliderDirection.west;
            }
            objectInteraction.playerTouchingObject = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //this is redundant with ObjectInteraction but hey, you never know if it will be needed and it doesn't hurt anything.
            objectInteraction.colliderDirection = ObjectInteraction.ColliderDirection.inactive;
            objectInteraction.playerTouchingObject = false;
        }
    }

}

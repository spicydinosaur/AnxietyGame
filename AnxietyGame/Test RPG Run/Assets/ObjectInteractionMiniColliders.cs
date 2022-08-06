using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractionMiniColliders : MonoBehaviour
{
    public string colliderDirection;

    public bool northColliderTriggered;
    public bool southColliderTriggered;
    public bool eastColliderTriggered;
    public bool westColliderTriggered;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (colliderDirection == "North")
            {
                northColliderTriggered = true;
            }
            else if (colliderDirection == "South")
            {
                southColliderTriggered = true;
            }
            else if (colliderDirection == "East")
            {
                eastColliderTriggered = true;
            }
            else if (colliderDirection == "West")
            {
                westColliderTriggered = true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (colliderDirection == "North")
            {
                northColliderTriggered = false;
            }
            else if (colliderDirection == "South")
            {
                southColliderTriggered = false;
            }
            else if (colliderDirection == "East")
            {
                eastColliderTriggered = false;
            }
            else if (colliderDirection == "West")
            {
                westColliderTriggered = false;
            }
        }
    }

}

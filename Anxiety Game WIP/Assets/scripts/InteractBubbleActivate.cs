using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBubbleActivate : MonoBehaviour
{

    public GameObject interactHere;

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            interactHere.SetActive(true);


        }
    }


    private void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            interactHere.SetActive(false);
        }
    }
}

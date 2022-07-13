using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignMessage : MonoBehaviour
{

    //this takes the canvas' image child (a pixelated looking scroll in this case,) and designates it for quick referencing further down.
    public GameObject dialogBox;

    void OnTriggerEnter2D(Collider2D collider)
    {
        //only do this if the player is the one it collides with
        if (collider.gameObject.CompareTag("Player"))
        {
            //show the image
            dialogBox.SetActive(true);

        }
        //Debug.Log("This is working");
    }

    void OnTriggerExit2D(Collider2D collider)
    {        
        //only do this if the player is the one exiting collision
        if (collider.gameObject.CompareTag("Player"))
        {
            //hide the image
            dialogBox.SetActive(false);

        }
    }

}
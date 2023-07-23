using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class activateMenu : MonoBehaviour
{
    //public PlayerControls playerControls;
    public Player player;


    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {

            player.SwitchMapUIInput();
            Debug.Log("current action map should be UI but who knows!");

        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            player.SwitchMapPlayerActions();
            Debug.Log("current action map should be playeractions but who knows!");
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAway : MonoBehaviour
{

    public Player player; 
    private PlayerControls playerControls;

    // Start is called before the first frame update


    public void PortalHome()
    {
        if (player.tutorialComplete != true)
        {
            player.GetComponent<Transform>().position = new Vector3(-67.69f, 95.38f, 0f);
        }
        else if (player.breakdown != true)
        { 
            player.GetComponent<Transform>().position = new Vector3(12.77f, 1.84f, 0f);
        }
        else
        {
            player.GetComponent<Transform>().position = new Vector3(-62.56f, 95.41f, 0f);
        }
        playerControls.Enable();
        GetComponent<Animator>().SetBool("heroIsInDanger", false);
    }

}

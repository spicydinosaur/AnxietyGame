using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAway : MonoBehaviour
{

    public GameObject hero;
    public GameManager gameManager;
    public Player player;

    // Start is called before the first frame update
    public void OnEnable()
    {
        transform.position = hero.transform.position;
        hero.SetActive(false);

    }

    public void TeleportedSuccessfully()
    {
        hero.SetActive(true);
        if (gameManager.breakdownComplete)
        {
                        
            Vector3 pos = new Vector3();
            hero.transform.position = pos;    

        }
        else if (gameManager.tutorialComplete)
        {
            Vector3 pos = new Vector3();
            hero.transform.position = pos;
        }
        else
        {
            Vector3 pos = new Vector3();
            hero.transform.position = pos;
        }

        player.PlayerHealth(player.maxHealth);
        player.PlayerMana(player.maxMana);
    }

}

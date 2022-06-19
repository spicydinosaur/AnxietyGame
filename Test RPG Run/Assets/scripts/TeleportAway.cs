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
                        
            Vector3 pos = new Vector3(-62.65f, 95.4f, 0f);
            hero.transform.position = pos;    

        }
        else if (gameManager.tutorialComplete)
        {
            Vector3 pos = new Vector3(18.44f, 64.18f, 0f);
            hero.transform.position = pos;
        }
        else
        {
            Vector3 pos = new Vector3(-67.8f, 95.4f, 0f);
            hero.transform.position = pos;
        }

        player.PlayerHealth(player.maxHealth);
        player.PlayerMana(player.maxMana);
    }

}

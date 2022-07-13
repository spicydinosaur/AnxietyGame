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
        player.lookDirection.Set(0f,-.5f);
        if (gameManager.breakdownComplete)
        {
                        
            Vector3 pos = new Vector3(-64.92f, 95.15f, 0f);
            hero.transform.position = pos;    

        }
        else if (gameManager.tutorialComplete)
        {
            Vector3 pos = new Vector3(8.76f, 3.86f, 0f);
            hero.transform.position = pos;
        }
        else
        {
            Vector3 pos = new Vector3(-68.46f, 95.21f, 0f);
            hero.transform.position = pos;
        }

        player.PlayerHealth(player.maxHealth);
        player.PlayerMana(player.maxMana);
    }

}

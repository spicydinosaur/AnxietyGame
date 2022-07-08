using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TeleportAway : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerControls playerControls;

    public GameObject hero;
    public Player player;

    public Light2D mainCamLighting;
    public GameObject nightmareLightingObject;
    public Light2D nightmareLighting;

    public List<GameObject> listOfGhosts = new List<GameObject>(6);


    // Start is called before the first frame update
    public void OnEnable()
    {
        playerControls = gameManager.playerControls;
        transform.position = hero.transform.position;
        hero.SetActive(false);

    }

    public void TeleportedSuccessfully()
    {
        hero.SetActive(true);
        player.lookDirection.Set(0f,-.5f);
        if (gameManager.breakdownComplete)
        {
                        
<<<<<<< Updated upstream:Test RPG Run/Assets/scripts/TeleportAway.cs
            Vector3 pos = new Vector3(-62.65f, 95.4f, 0f);
=======
            Vector3 pos = new Vector3(-64.92f, 95.15f, 0f);
>>>>>>> Stashed changes:AnxietyGame/Test RPG Run/Assets/scripts/TeleportAway.cs
            hero.transform.position = pos;    

        }
        else if (gameManager.tutorialComplete)
        {
<<<<<<< Updated upstream:Test RPG Run/Assets/scripts/TeleportAway.cs
            Vector3 pos = new Vector3(18.44f, 64.18f, 0f);
=======
            Vector3 pos = new Vector3(8.76f, 3.86f, 0f);
>>>>>>> Stashed changes:AnxietyGame/Test RPG Run/Assets/scripts/TeleportAway.cs
            hero.transform.position = pos;
        }
        else
        {
<<<<<<< Updated upstream:Test RPG Run/Assets/scripts/TeleportAway.cs
            Vector3 pos = new Vector3(-67.8f, 95.4f, 0f);
=======
            Vector3 pos = new Vector3(-68.46f, 95.21f, 0f);
>>>>>>> Stashed changes:AnxietyGame/Test RPG Run/Assets/scripts/TeleportAway.cs
            hero.transform.position = pos;
        }

        player.PlayerHealth(player.maxHealth);
        player.PlayerMana(player.maxMana);
        playerControls.Disable();

    }

}

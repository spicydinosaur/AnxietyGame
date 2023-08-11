using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportAway : MonoBehaviour
{

    public GameObject hero;
    public GameManager gameManager;
    public Player player;

    // Start is called before the first frame update
    public void DeathCutSceneEnable()
    {
        //transform.position = hero.transform.position;
        //hero.SetActive(false);

        SceneManager.LoadScene("DeathCutscene");


    }



}

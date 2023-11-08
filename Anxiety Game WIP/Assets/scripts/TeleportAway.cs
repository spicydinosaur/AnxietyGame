using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class TeleportAway : MonoBehaviour
{
    
    public void DeathCutSceneEnable()
    {
        //transform.position = hero.transform.position;
        //hero.SetActive(false);
        SceneManager.LoadScene("DeathCutscene");
    }


}

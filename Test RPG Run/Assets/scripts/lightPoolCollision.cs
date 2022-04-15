using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine.Rendering.Universal;

public class lightPoolCollision : MonoBehaviour
{


    public BrightSpellTemplate bright;
    public Light2D light2D;
    


    public void onTriggerExit2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("Player"))
        {

            Debug.Log(collider.name + "has left the ring of light and it should now deactivate!");

            bright.hasPoolBeenDisabled = true;
            light2D.intensity = 0f;
            gameObject.SetActive(false);



        }

    }

    public void Awake()
    {
        light2D.intensity = .75f;
    }
}

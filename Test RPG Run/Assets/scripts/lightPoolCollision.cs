using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class lightPoolCollision : MonoBehaviour
{


    public bool hasPoolBeenDisabled;


    public void onTriggerExit2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("Player"))
        {

            Debug.Log(collider.name + "has left the ring of light and it should now deactivate!");

            hasPoolBeenDisabled = true;
            gameObject.GetComponent<Light>().intensity = 0f;
            gameObject.SetActive(false);



        }

    }

    public void Awake()
    {
        GetComponent<Light>().intensity = .75f;
    }
}

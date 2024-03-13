using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TransitionLightingHUDChanges : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera mainCam;

    public float lightIntensity;
    public GameObject hero;
    public Canvas bossHUDElements;
    public TutorialBossPixie pixieScript;

    public void Start()
    {
        mainCam = Camera.main;
        hero = GameObject.Find("Hero");
    }

    public void changeLighting()
    {
        //mainCam.GetComponent<Light2D>().intensity = lightIntensity;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (bossHUDElements != null)
            {
                bossHUDElements.enabled = false;
            }
        }
        //if the player "dies" the pixie goes back to stage one and requires the intro timeline, as well. If they leave the room, the pixie quickly starts regenerating.
    }
}

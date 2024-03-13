using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LPLightTotem : LightPillars
{
    // Start is called before the first frame update

    public GameObject pixieBoss;

    public TutorialPixieBossTimeline pixieTimeline;

    public float playerLightIntensity;

    public GameObject hero;

    public Camera mainCam;

    /*public void LightBallChange()
    public override void HitByBright()
    {

        objectToLight.GetComponent<Light2D>().enabled = true;
        pixieTimeline.DirectorPlayed(pixieTimeline.directorIntro);
        
        //gameObject.GetComponent<Animator>().SetBool("pillarActivated", true);

    }

    {
        if (objectToLight)
        {
            objectToLight.SetActive(false);
            //gameObject.GetComponentInParent<GameObject>().SetActive(false);
            //timeline should start here, though we can probably make this entire function into part of the timeline
            //pixie rises up above the pillar, glowing in the darkness. there's an audible high pitched laugh and she blinks several times before disappearing. 
            //she spawns at the back center of the room and the fight commences.
        }
        else
        {
            objectToLight.SetActive(true);
        }
    }*/

    public void Start()
    {
        hero = GameObject.Find("Hero");
        mainCam = Camera.main;
    }

    public void PixieFightBegin()
    {
        pixieBoss.GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
    }



    public void PanCameraDone()
    {
        GetComponentInChildren<Image>().fillAmount = 0;
    }
}

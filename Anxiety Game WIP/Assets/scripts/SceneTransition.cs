using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Audio;

public class SceneTransition : MonoBehaviour
{

    public GameObject destination;
    public Transform destinationTransform;
    public GameObject collidingObject;
    public Vector2 lookDirectionOnEntry;
    public Player playerScript;
    //public UIFade uiFade;

    /*public AudioMixer audioMixer;

    public string exposedParameter;

    public float durationFadeOut;
    public float targetVolumeFadeOut;
    */

    public virtual void Awake()
    {
        //uiFade = GameManager.gameManagerObject.GetComponent<UIFade>();
        destinationTransform = destination.GetComponent<Transform>();
    }

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        

        if (collider.gameObject.CompareTag("Player") && collider.GetComponent<Player>().transitioningToScene == false)
           {

                //Debug.Log("Scene transition attempted. the variable collidingObject value is " + collider.gameObject.name + ". collision.gameObject value is " + collider.gameObject);
                collidingObject = collider.gameObject;
                playerScript = collidingObject.GetComponent<Player>();
                Debug.Log("transporting should commence!");

                playerScript.transitioningToScene = true;

                playerScript.lookDirection.Set(lookDirectionOnEntry.x, lookDirectionOnEntry.y);
                GameManager.gameManagerObject.GetComponent<UIFade>().callingTransition = gameObject.GetComponent<SceneTransition>();
                GameManager.gameManagerObject.GetComponent<UIFade>().fadingIn = false;
                GameManager.gameManagerObject.GetComponent<UIFade>().fadingOut = true;
                //StartCoroutine(FadeMixerGroup.StartFade(audioMixer, exposedParameter, durationFadeOut, targetVolumeFadeOut));
                Debug.Log("transition occured from: " + name + "?");
            //wait for UIFade to say it is done then move target!!!
        }

    }



    public virtual void fadeOutFinished()
    {
        collidingObject.transform.position = destinationTransform.position;
        Debug.Log("fade out finished!");
        GameManager.gameManagerObject.GetComponent<UIFade>().fadingOut = false;
        GameManager.gameManagerObject.GetComponent<UIFade>().fadingIn = true;
        GameManager.gameManagerObject.GetComponent<UIFade>().lerpTime = 0f;
        Debug.Log("transition occured to: " + destination);

    }


    public virtual void fadeInFinished()
    {
        Debug.Log("fade in finished!");
        GameManager.gameManagerObject.GetComponent<UIFade>().fadingOut = false;
        GameManager.gameManagerObject.GetComponent<UIFade>().fadingIn = false;
        GameManager.gameManagerObject.GetComponent<UIFade>().lerpTime = 0f;
        playerScript.transitioningToScene = false;

    }

    

}

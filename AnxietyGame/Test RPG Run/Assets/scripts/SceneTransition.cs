using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SceneTransition : MonoBehaviour
{

    public GameObject destination;
    public Transform destinationTransform;
    public GameObject collidingObject;
    public Vector2 lookDirectionOnEntry;
    public Player playerScript;
    public UIFade uiFade;


    public virtual void Awake()
    {
        //uiFade = GameManager.gameManagerObject.GetComponent<UIFade>();
        destinationTransform = destination.GetComponent<Transform>();
    }

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        


        if (collider.gameObject.CompareTag("Player") && collider.GetComponent<Player>().transitioningToScene == false)
           {

                Debug.Log("Scene transition attempted. the variable collidingObject value is " + collider.gameObject.name + ". collision.gameObject value is " + collider.gameObject);
                collidingObject = collider.gameObject;
                playerScript = collidingObject.GetComponent<Player>();
                Debug.Log("transporting should commence!");

                playerScript.transitioningToScene = true;

                playerScript.lookDirection.Set(lookDirectionOnEntry.x, lookDirectionOnEntry.y);
                GameManager.gameManagerObject.GetComponent<UIFade>().callingTransition = gameObject.GetComponent<SceneTransition>();
                GameManager.gameManagerObject.GetComponent<UIFade>().fadingIn = false;
                GameManager.gameManagerObject.GetComponent<UIFade>().fadingOut = true;
                Debug.Log("transition occured from: " + name + "?");
            //wait for UIFade to say it is done then move target!!!
        }

    }



    public virtual void fadeOutFinished()
    {
        collidingObject.transform.position = destinationTransform.position;
        Debug.Log("fade out finished!");
        uiFade.fadingOut = false;
        uiFade.fadingIn = true;
        uiFade.lerpTime = 0f;
        Debug.Log("transition occured to: " + destination);

    }


    public virtual void fadeInFinished()
    {
        Debug.Log("fade in finished!");
        uiFade.fadingOut = false;
        uiFade.fadingIn = false;
        uiFade.lerpTime = 0f;
        playerScript.transitioningToScene = false;

    }

    

}

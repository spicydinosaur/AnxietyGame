using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SceneTransition : MonoBehaviour
{

    public SceneTransition destination;
    public Collider2D collidingObject;


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Player>().transitioningToScene == false)
           {

                collision.gameObject.GetComponent<Player>().transitioningToScene = true;
                GameManager.gameManagerObject.GetComponent<UIFade>().callingTransition = destination;
                GameManager.gameManagerObject.GetComponent<UIFade>().fadingIn = false;
                GameManager.gameManagerObject.GetComponent<UIFade>().fadingOut = true;
                    //Debug.Log("transition occured from: " + name + "?");
                    //wait for UIFade to say it is done then move target!!!
            }

    }



    public virtual void fadeOutFinished()
    {
        collidingObject.gameObject.transform.position = GameManager.gameManagerObject.GetComponent<UIFade>().callingTransition.transform.position;
        GameManager.gameManagerObject.GetComponent<UIFade>().fadingOut = false;
        GameManager.gameManagerObject.GetComponent<UIFade>().fadingIn = true;
        GameManager.gameManagerObject.GetComponent<UIFade>().lerpTime = 0f;

        //Debug.Log("transition occured to: " + destination);
               
    }


    public virtual void fadeInFinished()
    {

        GameManager.gameManagerObject.GetComponent<UIFade>().fadingOut = false;
        GameManager.gameManagerObject.GetComponent<UIFade>().fadingIn = false;
        GameManager.gameManagerObject.GetComponent<UIFade>().lerpTime = 0f;
        collidingObject.gameObject.GetComponent<Player>().transitioningToScene = false;

    }

    

}

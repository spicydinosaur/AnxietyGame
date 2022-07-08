using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SceneTransition : MonoBehaviour
{

<<<<<<< Updated upstream:Test RPG Run/Assets/scripts/SceneTransition.cs
    public SceneTransition destination;
    public Collider2D collidingObject;
=======
    public GameObject destination;
    private GameObject collidingObject;
    public Vector2 lookDirectionOnEntry;
>>>>>>> Stashed changes:AnxietyGame/Test RPG Run/Assets/scripts/SceneTransition.cs


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Player>().transitioningToScene == false)
           {
<<<<<<< Updated upstream:Test RPG Run/Assets/scripts/SceneTransition.cs

                collision.gameObject.GetComponent<Player>().transitioningToScene = true;
                GameManager.gameManagerObject.GetComponent<UIFade>().callingTransition = destination;
=======
                collidingObject = collision.gameObject;
                collidingObject.GetComponent<Player>().lookDirection.Set(lookDirectionOnEntry.x, lookDirectionOnEntry.y);
                collidingObject.GetComponent<Player>().transitioningToScene = true;
                GameManager.gameManagerObject.GetComponent<UIFade>().callingTransition = destination.GetComponent<SceneTransition>();
>>>>>>> Stashed changes:AnxietyGame/Test RPG Run/Assets/scripts/SceneTransition.cs
                GameManager.gameManagerObject.GetComponent<UIFade>().fadingIn = false;
                GameManager.gameManagerObject.GetComponent<UIFade>().fadingOut = true;
                    //Debug.Log("transition occured from: " + name + "?");
                    //wait for UIFade to say it is done then move target!!!
            }

    }



    public virtual void fadeOutFinished()
    {
        collidingObject.transform.position = new Vector3(destination.transform.position.x, destination.transform.position.y, 0f);
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
        collidingObject.GetComponent<Player>().transitioningToScene = false;

    }

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SceneTransition : MonoBehaviour
{

    public SceneTransition destination;
    public Collider2D collidingObject;
    public CinemachineTransposer vcam;
    public CinemachineVirtualCamera camObject;


    public void Start()
    {
        vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();

    }

    public void OnTriggerEnter2D(Collider2D collider)
    {



        if (collidingObject.gameObject.GetComponent<Player>().transitioningToScene == false)
        {


            collidingObject.gameObject.GetComponent<Player>().transitioningToScene = true;
            GameManager.gameManagerObject.GetComponent<UIFade>().callingTransition = destination;
            GameManager.gameManagerObject.GetComponent<UIFade>().fadingIn = false;
            GameManager.gameManagerObject.GetComponent<UIFade>().fadingOut = true;
            Debug.Log("transition occured from: " + name + "?");
            //wait for UIFade to say it is done then move target!!!


        }

    }

    /*public void OnTriggerExit2D(Collider2D collider)
    {
        vcam.m_XDamping = 0;
        vcam.m_YDamping = 0;
    }*/

    public void fadeOutFinished()
    {
        collidingObject.gameObject.transform.position = GameManager.gameManagerObject.GetComponent<UIFade>().callingTransition.transform.position;
        GameManager.gameManagerObject.GetComponent<UIFade>().fadingOut = false;
        GameManager.gameManagerObject.GetComponent<UIFade>().fadingIn = true;
        GameManager.gameManagerObject.GetComponent<UIFade>().lerpTime = 0f;

        Debug.Log("transition occured to: " + destination);
               
    }


    public void fadeInFinished()
    {

        GameManager.gameManagerObject.GetComponent<UIFade>().fadingOut = false;
        GameManager.gameManagerObject.GetComponent<UIFade>().fadingIn = false;
        GameManager.gameManagerObject.GetComponent<UIFade>().lerpTime = 0f;
        collidingObject.gameObject.GetComponent<Player>().transitioningToScene = false;

    }

}

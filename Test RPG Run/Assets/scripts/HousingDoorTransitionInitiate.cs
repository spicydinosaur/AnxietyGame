using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HousingDoorTransitionInitiate : SceneTransition

{

    public Animator animator;
    public SceneTransition virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("doorOpening", false);
    }


    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (animator.GetBool("doorOpening") == false && collision.gameObject.GetComponent<Player>().transitioningToScene == false)
        {
            animator.SetBool("doorOpening", true);
            collision.gameObject.GetComponent<Player>().transitioningToScene = true;
            GameManager.gameManagerObject.GetComponent<UIFade>().callingTransition = destination;
            GameManager.gameManagerObject.GetComponent<UIFade>().fadingIn = false;
            GameManager.gameManagerObject.GetComponent<UIFade>().fadingOut = true;
            cameraSwitch.changeCamera(this);
        }

        else 
        {

            animator.SetBool("doorOpening", true);

        }

    }

}




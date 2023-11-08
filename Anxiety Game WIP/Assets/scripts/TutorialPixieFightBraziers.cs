using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TutorialPixieFightBraziers : MonoBehaviour
{

    public GameObject hero;
    public Light2D fireLight;
    public Animator animator;

    // Start is called before the first frame update
    public void Start()
    {
        hero = GameObject.Find("Hero");
        animator = GetComponent<Animator>();
        fireLight = GetComponentInChildren<Light2D>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("empty brazier") && fireLight.isActiveAndEnabled)
        {
            fireLight.enabled = false;
        }
    }

    public void AnimationBegins()
    {
        fireLight.enabled = true;
    }
}

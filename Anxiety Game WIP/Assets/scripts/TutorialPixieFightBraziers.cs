using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TutorialPixieFightBraziers : MonoBehaviour
{

    public GameObject hero;
    public Light2D fireLight;
    public Animator animator;
    public AudioClip extinguish;
    public AudioSource brazierAudio;
    public AudioClip litAudio;

    // Start is called before the first frame update
    public void Start()
    {
        hero = GameObject.Find("Hero");
        animator = GetComponent<Animator>();
        brazierAudio = GetComponent<AudioSource>();
        fireLight = GetComponentInChildren<Light2D>();
        fireLight.enabled = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("empty brazier") && fireLight.isActiveAndEnabled)
        {
            fireLight.enabled = false;
            GetComponent<AudioSource>().PlayOneShot(extinguish);
        }
    }

    public void AnimationBegins()
    {
        if (!fireLight.isActiveAndEnabled)
        {
            fireLight.enabled = true;
            brazierAudio.PlayOneShot(litAudio);
        }
    }
}

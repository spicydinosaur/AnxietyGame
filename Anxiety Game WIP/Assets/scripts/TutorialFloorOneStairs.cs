using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFloorOneStairs : MonoBehaviour
{
    public AudioSource stairsAudio;
    public Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
        stairsAudio = GetComponent<AudioSource>();
        if (GameManager.tutorialRuinsStairsRevealed)
        {
            animator.SetTrigger("isOpened");
        }
    }

    public void StairsOpening()
    {
        stairsAudio.Play(0);
    }

    public void StairsNowOpen()
    {
        stairsAudio.Stop();
        GameManager.tutorialRuinsStairsRevealed = true;
    }
}

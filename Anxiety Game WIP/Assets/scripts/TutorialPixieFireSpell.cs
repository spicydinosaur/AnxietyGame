using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPixieFireSpell : MonoBehaviour
{
    public GameObject hero;
    public Player player;
    public AudioSource audioSource;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.Find("Hero");
        player = hero.GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void SpellStarting()
    {
        audioSource.Play(0);
    }

    public void SpellEnding()
    {
        animator.SetTrigger("spellFinished");
    }
    // Update is called once per frame

}

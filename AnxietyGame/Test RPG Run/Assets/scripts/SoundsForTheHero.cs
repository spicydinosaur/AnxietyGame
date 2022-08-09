using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsForTheHero : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource audioSource;
    public AudioClip footsteps;


    public void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void SoundForWalking()
    {
        audioSource.PlayOneShot(footsteps, .5f);

    }
}

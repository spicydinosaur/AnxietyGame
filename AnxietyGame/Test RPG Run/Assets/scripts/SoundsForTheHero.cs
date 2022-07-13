using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsForTheHero : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource footstepsSource;
    public void SoundForWalking()
    {
        footstepsSource.Play();

    }
}

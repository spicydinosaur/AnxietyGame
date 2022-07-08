using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterController cc;
    public AudioSource footstepsSource;
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cc.isGrounded == true && cc.velocity.magnitude > 2 && footstepsSource.isPlaying == false)
        {
            footstepsSource.Play();
        }
    }
}

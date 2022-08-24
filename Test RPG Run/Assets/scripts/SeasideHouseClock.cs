using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SeasideHouseClock : MonoBehaviour
{
    public AudioSource clockTick;
    public AudioSource clockChime;

    public float chimeTimer;


    //this is for the seaside village house downstairs only



    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            if (chimeTimer <= 0)
            {
                clockTick.Stop();
                clockChime.Play();
                chimeTimer = 300;
            }
            else
            {
                clockTick.Play();
            }
        }
    }

    public void Update()
    {
        if (chimeTimer > 0)
        {
            chimeTimer -= Time.deltaTime;

        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            if (!clockChime.isPlaying && !clockTick.isPlaying)
            {
                clockTick.Play();
            }
            else if (chimeTimer<= 0)
            {
                clockTick.Stop();
                clockChime.Play();
                chimeTimer = 300;
            }

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            clockChime.Stop();
            clockTick.Stop();

        }

    }
}



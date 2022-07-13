using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraConfinerOnOffTriggers : MonoBehaviour
{

    public CinemachineVirtualCamera vCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if (collision.gameObject.CompareTag("Player"))
        {
            vCam.gameObject.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            vCam.gameObject.SetActive(false);
        }

    }

}

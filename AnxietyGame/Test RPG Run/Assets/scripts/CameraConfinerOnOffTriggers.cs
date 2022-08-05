 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraConfinerOnOffTriggers : MonoBehaviour
{

    public CinemachineVirtualCamera vCam;



    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
    
        if (collision.gameObject.CompareTag("Player"))
        {
            vCam.gameObject.SetActive(true);
            
        }

    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            vCam.gameObject.SetActive(false);
        }

    }

}

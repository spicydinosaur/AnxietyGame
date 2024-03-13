using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfiner : MonoBehaviour
{

    public CinemachineVirtualCamera vCam;

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            vCam.gameObject.SetActive(true);

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {


            vCam.gameObject.SetActive(false);


        }

    }
}

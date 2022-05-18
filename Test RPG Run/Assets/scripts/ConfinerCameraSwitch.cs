using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ConfinerCameraSwitch : MonoBehaviour
{

    public CinemachineVirtualCamera vcam;

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        vcam.gameObject.SetActive(true);   
    }

    // Update is called once per frame
    void OnTriggerExit2D(Collider2D collider)
    {
        vcam.gameObject.SetActive(false);
    }
}

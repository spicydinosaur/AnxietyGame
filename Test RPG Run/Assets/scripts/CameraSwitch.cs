using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    public CinemachineTransposer vcam;
    public CinemachineVirtualCamera[] camerasTransList;
    //public SceneTransition sceneDestination;


    public void Start()
    {
        foreach (CinemachineVirtualCamera cameraTransElement in camerasTransList)
        {
            if (cameraTransElement.isActiveAndEnabled)
            {
                vcam = cameraTransElement.GetCinemachineComponent<CinemachineTransposer>();
                return;
            }

        }
    }

    public void changeCamera(SceneTransition sceneDestination)
    {

        Debug.Log("changeCamera is firing! " + sceneDestination.cameraOnLocal.name + " should be true and " + sceneDestination.cameraOffLocal.name + " should be false.");
        sceneDestination.cameraOnLocal.SetActive(true);
        sceneDestination.cameraOffLocal.SetActive(false);
        foreach (CinemachineVirtualCamera cameraTransElement in camerasTransList)
        {
           if (cameraTransElement.isActiveAndEnabled)
           {
                vcam = cameraTransElement.GetCinemachineComponent<CinemachineTransposer>();
                return;
           }

        }
    }
}

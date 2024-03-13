using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PixieFightPanCamAcceleration : MonoBehaviour
{
    public Camera cameraMain = Camera.main;
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineBrain cameraBrain;
    public Transform tPlayer;



    public void OnEnable()
    {
        CinemachineTrackedDolly trackedDolly = virtualCamera.GetComponent<CinemachineTrackedDolly>();
        CinemachineSmoothPath smoothPath = virtualCamera.GetComponent<CinemachineSmoothPath>();

        virtualCamera.LookAt = tPlayer;

        for (int i = 0; i < smoothPath.m_Waypoints.Length; i++)
        {
            smoothPath.m_Waypoints[i] = new CinemachineSmoothPath.Waypoint();
            smoothPath.m_Waypoints[i].position = smoothPath.m_Waypoints[i].position;
        }

        trackedDolly.m_Path = smoothPath;
        trackedDolly.m_PathPosition = 0;
    }

}

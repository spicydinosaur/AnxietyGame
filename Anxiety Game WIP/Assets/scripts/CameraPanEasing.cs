using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class CameraPanEasing : MonoBehaviour
{
    public Transform startPoint; // Set the start point in the Inspector
    public Transform endPoint; // Set the end point in the Inspector
    
    public float duration = 5f; // Duration of movement
    public float easeSpeed = 1f;
    private float elapsedTime = 0f;
    public float waitTime;

    public void Awake()
    {
        if (waitTime <= 0f)
        {
            waitTime = 0.01f;
        }

        CamMove();
    }


    public IEnumerator CamMove()
    {
        elapsedTime += Time.deltaTime;

        float t = Mathf.Clamp01(elapsedTime / duration); // Normalize time

        // Use an easing function (e.g., EaseInOut) to interpolate between start and end points
        float easedT = EaseInOut(t);
        // Move the camera smoothly from start to end
        transform.position = Vector3.Lerp(new Vector3(startPoint.position.x, startPoint.position.y, -10f), new Vector3(endPoint.position.x, endPoint.position.y, -10f), easedT);
        Debug.Log("camera panning coroutine fired.");
        if (t >= 1f)
        {
            // Movement completed
            // Optionally reset values or stop movement
            elapsedTime = 0f;
            Debug.Log("if (t >= 1f) selected.");
            yield break;
        }
        else
        {
            Debug.Log("else selected.");
            yield return new WaitForSeconds(waitTime);
        }
       
    }

    // Example of EaseInOut easing function
    float EaseInOut(float t)
    {
        Debug.Log("EaseInOut() function fired!");
        //return t * t * t * (t * (6f * t - 15f) + 10f);
        return easeSpeed * elapsedTime;
    }


}

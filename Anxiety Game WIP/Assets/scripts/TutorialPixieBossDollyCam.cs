using UnityEngine;

public class TutorialPixieBossDollyCam : MonoBehaviour
{
    public Transform startPoint; // Set the start point in the Inspector
    public Transform endPoint; // Set the end point in the Inspector
    public float duration = 5f; // Duration of movement

    private float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        float t = Mathf.Clamp01(elapsedTime / duration); // Normalize time

        // Use an easing function (e.g., EaseInOut) to interpolate between start and end points
        float easedT = EaseInOut(t);

        // Move the camera smoothly from start to end
        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, easedT);

        if (t >= 1f)
        {
            // Movement completed
            // Optionally reset values or stop movement
            elapsedTime = 0f;
        }
    }

    // Example of EaseInOut easing function
    float EaseInOut(float t)
    {
        return t * t * t * (t * (6f * t - 15f) + 10f);
    }
}


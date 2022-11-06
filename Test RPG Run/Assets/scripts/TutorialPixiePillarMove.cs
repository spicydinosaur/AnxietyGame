using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPixiePillarMove : MonoBehaviour
{
    public float elapsedTime;
    public float totalMoveTime;
    public Vector2 finalLocation;
    public AudioSource pillarGrindSound;
    public TutorialBossFairy bossFairy;
   



    public IEnumerator objectMove(Vector2 locationToMove, float totalMoveTime, float waitForSecondsCoroutine)
    {

        Debug.Log("objectMove coroutine activated");
        while (elapsedTime <= totalMoveTime)
        {
            if (pillarGrindSound.isPlaying == false)
            {

                pillarGrindSound.Play();

            }
            if (elapsedTime >= totalMoveTime)
            {
                StopCoroutine("objectMove");
                elapsedTime = totalMoveTime;
                transform.position = finalLocation;
                Debug.Log("coroutine stopped");
                elapsedTime = 0f;
                pillarGrindSound.Stop();
            }

            transform.position = (Vector2)transform.position + (locationToMove * waitForSecondsCoroutine/totalMoveTime);
            elapsedTime += waitForSecondsCoroutine;

            yield return new WaitForSeconds(waitForSecondsCoroutine);



        }
    }
}

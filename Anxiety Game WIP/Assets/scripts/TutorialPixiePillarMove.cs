using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPixiePillarMove : MonoBehaviour
{
    public float elapsedTime;
    public Vector2 startLocation;
    [SerializeField]
    private Vector2 totalMovement;
    public AudioSource pillarGrindSound;
    public TutorialBossPixie bossPixie;
   

/*
    public void Awake()
    {
        StartCoroutine(objectMove(finalLocation, .5f, .02f));
    }
    
  */  
    
    public IEnumerator objectMove(Vector2 locationToMove, float totalMoveTime, float waitForSecondsCoroutine)
    {

        Debug.Log("objectMove coroutine activated");
        totalMovement = locationToMove - (Vector2)transform.position;
        while (elapsedTime <= totalMoveTime)
        {
            if (pillarGrindSound.isPlaying == false)
            {

                pillarGrindSound.Play(0);

            }
            transform.position = (Vector2)transform.position + (totalMovement * waitForSecondsCoroutine/totalMoveTime);
            elapsedTime += waitForSecondsCoroutine;

            yield return new WaitForSeconds(waitForSecondsCoroutine);

        }

        if (elapsedTime >= totalMoveTime)
        {
            elapsedTime = totalMoveTime;
            transform.position = startLocation;
            Debug.Log("coroutine stopped");
            elapsedTime = 0f;
            pillarGrindSound.Stop();
            StopCoroutine(objectMove(startLocation, .5f, .02f));
        }
    }
}

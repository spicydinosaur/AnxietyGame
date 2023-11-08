using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SleepRecoveryVoices : MonoBehaviour
{
    public List<GameObject> barkObjects;
    public List<Vector2> barkPositions;
    public SleepRecoveryEyes deathScript;
    public float waitTime = 1f;
    public int eyeListRandomChoice;
    public float yValueShift = 1f;
    public List<Vector2> tempVectorPositionList;
    public int currentMaxBarkObjectInList = 5;
    public int chimeTime = 8;
    public int loops = 0;


    public IEnumerator BarkCycle() 
    {
        foreach (GameObject eyes in deathScript.eyesThatBlink)
        {
            barkPositions.Add(eyes.transform.position);
        }
        tempVectorPositionList = new List<Vector2>(barkPositions);
        loops = 0;
        //for (int i = 0; i < barkObjects.Count; i++)
        for (int i = 0; i < currentMaxBarkObjectInList; i++)
        {
            loops++;
            Debug.Log("i = " + i + ". loops = " + loops);
            if (i >= barkObjects.Count)
            {
                GameManager.Instance.ClockChimeForRecoveryScene();
                StartCoroutine(WakeUp());
                break;
            }
            barkObjects[i].SetActive(false);
            if (tempVectorPositionList.Count <= 0)
            {


                Debug.Log("if statement fired, i =" + i);
                tempVectorPositionList = new List<Vector2>(barkPositions);
                currentMaxBarkObjectInList++;

            }
            //barkObjects[i].transform.position = barkPositions[(int)Mathf.Floor(UnityEngine.Random.Range(0, barkPositions.Count))];
            eyeListRandomChoice = UnityEngine.Random.Range(0, tempVectorPositionList.Count);
            barkObjects[i].transform.position = new Vector2(tempVectorPositionList[eyeListRandomChoice].x, tempVectorPositionList[eyeListRandomChoice].y + yValueShift);
            Debug.Log("eyelistrandomchoice = " + eyeListRandomChoice);
            tempVectorPositionList.Remove(tempVectorPositionList[eyeListRandomChoice]);
            //barkObjects[i].SetActive(true);
            
            yield return new WaitForSeconds(waitTime/(1+loops*0.1f));
            //modify waitTime in script to speed up the recovery sequence.
            barkObjects[i].SetActive(true);
            if (i == currentMaxBarkObjectInList - 1) //|| tempVectorPositionList.Count > 0)
            {
                i = -1;
                Debug.Log("if code in for loop fired, SleepRecoveryVoices");
            }
                
        }


    }

    public IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(chimeTime);
        GameManager.Instance.TeleportedSuccessfully();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        //if (yValueShift == 0) { yValueShift = 1f; }
        //if (waitTime == 0) { waitTime = 1f; }
        //if (currentMaxBarkObjectInList == 0) { currentMaxBarkObjectInList = 5; }
        foreach (GameObject darkthought in barkObjects)
        {
            darkthought.GetComponent<StandardBarkUI>().duration = waitTime/ (1 + loops * 0.1f);
        }
        StartCoroutine(BarkCycle());
    }

    // Update is called once per frame
    void OnDisable()
    {
        StopCoroutine(BarkCycle());
    }
}

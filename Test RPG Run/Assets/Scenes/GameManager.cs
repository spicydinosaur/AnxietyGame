using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : PersistentSingleton<GameManager>


{
    public static GameObject gameManagerObject;
    public bool tutorialComplete = false; //temporary permanent state until we have a way to save and keep it as a peristent variable
    public bool breakdownComplete = false;//same as above

    public List<GameObject> LightsOnPuzzleObjects = new List<GameObject>();




    public void OnEnable()
    {
        gameManagerObject = gameObject;


    }


}

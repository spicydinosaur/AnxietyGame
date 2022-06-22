using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>


{
    public static GameObject gameManagerObject;
    public PlayerControls playerControls;
    public bool tutorialComplete = false; //temporary permanent state until we have a way to save and keep it as a peristent variable
    public bool breakdownComplete = false;//same as above


    protected override void Awake()
    {

        base.Awake();
        playerControls = new PlayerControls();
        gameManagerObject = gameObject;

    }


    private void OnEnable()
    {

        playerControls.Enable();

    }

    private void OnDisable()
    {

        playerControls.Disable();

    }
}

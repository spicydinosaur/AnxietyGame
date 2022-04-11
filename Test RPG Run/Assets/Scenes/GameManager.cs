using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>


{
    public static GameObject gameManagerObject;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerObject = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChase : MonoBehaviour

{
    public GameObject currentChaseObject;



    // Start is called before the first frame update

    // Update is called once per frame

    public void StartChase(GameObject chaseObject)
    {
        currentChaseObject = chaseObject;

    }

    public void StopChase()
    {
        currentChaseObject = null;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChase : MonoBehaviour

{
    public GameObject currentChaseObject;
    public NPCController controller;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<NPCController>();
    }

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


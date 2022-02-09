using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveViewCone : MonoBehaviour
{
    public NPCController controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        controller = GetComponentInParent<NPCController>();
    }

    // Update is called once per frame
    void Update()
    {
        //is it looking left?
        if (controller.lookDirection.x > .5f && controller.lookDirection.y > -.69f &&  controller.lookDirection.y < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler (0,0,90);
        }
        //is it looking right?
        else if (controller.lookDirection.x < -0.5f && controller.lookDirection.y > -.69f && controller.lookDirection.y < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270);
        }
        //is it looking backward?
        else if (controller.lookDirection.y > .5f && controller.lookDirection.x > -.69f && controller.lookDirection.x < 0.69f)
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180);
        }
        else //we are looking forward
        {
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
        }
    }

}

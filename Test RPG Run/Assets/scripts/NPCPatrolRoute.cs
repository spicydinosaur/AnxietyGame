using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPatrolRoute : MonoBehaviour 

{
   
    public Vector2[] PatrolCoord;
    //public Vector2[] currentTransform;
    public int NextPatrolElement = -1;
    public bool patrollingBool;


    public Vector2 GetNextCoord()
    {
        //Debug.Log("calls of coord");


        if (patrollingBool == true)
        {
            NextPatrolElement++;
            if (NextPatrolElement == PatrolCoord.Length && gameObject )
            {

                NextPatrolElement = -1;
                return PatrolCoord[PatrolCoord.Length - 1];

            }

            else
            {

                return PatrolCoord[NextPatrolElement];

            }

        }
        else
        {

            
            return GetComponent<Transform>().position;
            
        }
       
    }
}

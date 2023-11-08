using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternalTransition : MonoBehaviour
{
    public GameObject objectToMove;
    public Vector3 destinationTransform;

        public void InternalSceneMove()
        {
            objectToMove.transform.position = destinationTransform;
        }


}

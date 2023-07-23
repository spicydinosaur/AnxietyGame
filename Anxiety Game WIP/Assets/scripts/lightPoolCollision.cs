using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class lightPoolCollision : MonoBehaviour
{




    public void OnTriggerExit2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("Player"))
        {
             gameObject.GetComponentInParent<Light2D>().enabled = false;
        }

    }

}

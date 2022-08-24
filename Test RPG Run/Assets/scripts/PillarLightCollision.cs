using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PillarLightCollision : MonoBehaviour
{
    // Start is called before the first frame update
    //public Light2D light2D;


    public void OnTriggerExit2D(Collider2D collider)
    {

        Debug.Log("There has been an OnTriggerExit2D collision! (PillarLightCollision)");

        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log(collider.name + "has left the ring of light and it should now deactivate! (PillarLightCollision)");
            
            if (GetComponentInParent<Light2D>().isActiveAndEnabled && PuzzleKey.LockCheck.complete != GetComponentInParent<PuzzleKey>().lockCheck)
            {
                GetComponentInParent<Light2D>().enabled = false;
                if (GetComponentInParent<PuzzleKey>().isActiveAndEnabled)
                {
                    GetComponentInParent<PuzzleKey>().ChangeLockState(PuzzleKey.LockCheck.locked);
                }

            }

        }

    }
}

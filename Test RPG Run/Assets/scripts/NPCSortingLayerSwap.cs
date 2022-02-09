using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSortingLayerSwap : MonoBehaviour
{

    public static MasterCollisionList mcl;
    public int mclPosition;
    public int totalCollisions = 0;


    private void Awake()
    {

        mcl = MasterCollisionList._instance;

    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("NPC"))
        {

            if (totalCollisions == 0) mclPosition = MasterCollisionList._instance.AddNewCollision(GetComponent<Transform>());
            totalCollisions++;
  
        }

        else if (collider.gameObject.CompareTag("Player"))
        {

            GetComponentInParent<SpriteRenderer>().sortingLayerName = "Hero";
            GetComponentInParent<SpriteRenderer>().sortingOrder = (int)(GetComponentInParent<SpriteRenderer>().bounds.min.y * -100);
            collider.GetComponentInParent<SpriteRenderer>().sortingOrder = (int)(collider.GetComponentInParent<SpriteRenderer>().bounds.min.y * -100);

        }
        /*
        else if (collider.gameObject.CompareTag("NPC"))
        {

            GetComponentInParent<SpriteRenderer>().sortingOrder = (int)(GetComponentInParent<SpriteRenderer>().bounds.min.y * -100);
            
        }
        */

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        
        if (collider.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<SpriteRenderer>().sortingLayerName = "NPCs";

        }
        
        else if (collider.gameObject.CompareTag("NPC"))
        {

            totalCollisions--;
            if (totalCollisions == 0)
            {
                MasterCollisionList._instance.RemoveCollision(mclPosition);
                mclPosition = -1;
            }
        }

    }

    public void RecalculatePosition()
    {

        GetComponentInParent<SpriteRenderer>().sortingOrder = (int)(GetComponentInParent<SpriteRenderer>().bounds.min.y * -100);

    }
}

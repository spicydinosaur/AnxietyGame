using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSortingLayerSwap : MonoBehaviour
{

    public static MasterCollisionList mcl;
    public int mclPosition;
    public int totalCollisions = 0;

    private string normalSortingLayer;
    private string[] colliderCarriersSortingLayer = new string[5];



    private void Start()
    {
        colliderCarriersSortingLayer = new string[] { "behind accessories", "NPCs", "Enemy", "Hero", "foreground accessories" };
        GetComponentInParent<SpriteRenderer>().sortingLayerName = normalSortingLayer;
    }

    private void Awake()
    {

        mcl = MasterCollisionList._instance;

    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("NPC"))
        {

            GetComponentInParent<SpriteRenderer>().sortingLayerName = "NPCs";
            if (totalCollisions == 0) mclPosition = MasterCollisionList._instance.AddNewCollision(GetComponent<Transform>());
            totalCollisions++;
  
        }

        else if (collider.gameObject.CompareTag("Enemy"))
        {

            GetComponentInParent<SpriteRenderer>().sortingLayerName = "Enemy";
            if (totalCollisions == 0) mclPosition = MasterCollisionList._instance.AddNewCollision(GetComponent<Transform>());
            totalCollisions++;

        }

        else if (collider.gameObject.CompareTag("Player"))
        {

            GetComponentInParent<SpriteRenderer>().sortingLayerName = "Hero";
            GetComponentInParent<SpriteRenderer>().sortingOrder = (int)(GetComponentInParent<SpriteRenderer>().bounds.min.y * -100);
            collider.GetComponentInParent<SpriteRenderer>().sortingOrder = (int)(collider.GetComponentInParent<SpriteRenderer>().bounds.min.y * -100);

        }

        else if (collider.gameObject.CompareTag("Scenery"))
        {
            if (GetComponentInParent<SpriteRenderer>().sortingLayerName == "behind accessories")
            {
                GetComponentInParent<SpriteRenderer>().sortingLayerName = "behind accessories";
            }
            else if (GetComponentInParent<SpriteRenderer>().sortingLayerName == "foreground accessories")
            {
                GetComponentInParent<SpriteRenderer>().sortingLayerName = "foreground accessories";
            }
            else
            {
                return;
            }


            if (totalCollisions == 0) mclPosition = MasterCollisionList._instance.AddNewCollision(GetComponent<Transform>());
            totalCollisions++;

        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        
        if (collider.gameObject.CompareTag("Player"))
        {

            GetComponentInParent<SpriteRenderer>().sortingLayerName = normalSortingLayer;

        }
        
        else if (collider.gameObject.CompareTag("NPC") || collider.gameObject.CompareTag("Scenery") || collider.gameObject.CompareTag("Enemy"))
        {
            GetComponentInParent<SpriteRenderer>().sortingLayerName = normalSortingLayer;
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

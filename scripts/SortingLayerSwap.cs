 using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SortingLayerSwap : MonoBehaviour
{

    public SpriteRenderer[] childSortingLayer;

    //Player enters collision, put sign/doghouseroof/whevs in front of player in sorting layer name
    //NPC enters collision, put the actual NPC behind by changing -its- sorting layer name
    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("Player"))
        {
            //GetComponentInParent<SpriteRenderer>().sortingLayerName = "Roofs";
            foreach (SpriteRenderer sprites in childSortingLayer)
                sprites.sortingLayerName = "Roofs";
        }

        else if (collider.gameObject.CompareTag("NPC"))
        {
            //collider.GetComponent<SpriteRenderer>().sortingLayerName = "behind accessories";
            //Debug.Log("are these colliding y/n circle one");
            foreach (SpriteRenderer sprites in childSortingLayer)
                sprites.sortingLayerName = "behind accessories";

        }


    }

    void OnTriggerExit2D(Collider2D collider)
    {
        //Player leaves collision, put sign/doghouseroof/whevs in behind player in sorting layer name (back to original spot)
        //NPC leaves collision, put the actual NPC at its appropriate sorting layer name
        if (collider.gameObject.CompareTag("Player"))
        {
            //GetComponentInParent<SpriteRenderer>().sortingLayerName = "foreground accessories";
            foreach (SpriteRenderer sprites in childSortingLayer)
                sprites.sortingLayerName = "foreground accessories";

        }

        else if (collider.gameObject.CompareTag("NPC"))
        {
            //collider.GetComponent<SpriteRenderer>().sortingLayerName = "NPCs";
            foreach (SpriteRenderer sprites in childSortingLayer)
                sprites.sortingLayerName = "NPCs";

        }

    }
}

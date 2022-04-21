 using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SortingLayerSwap : MonoBehaviour
{

    public SpriteRenderer[] childSortingLayer;
    public GameObject hero;
    public GameObject colliderScenery;
    public string defaultSortingLayer;


    //needs tweaking and testing. DON'T IGNORE ME!!!
   // private void Start()
   // {
        //how else can this be done, I can't pull up multiple sorting layer names this way, but using gameobject checks the sortinglayerswap gameobject. 
        //I wanted to do this through code but may have to use unity's editor. Hypothetically, this will grab the last sorting layer name and won't contain anything else.
        //This works, but it is clunky. I'd like to improve on it.

     //   foreach (SpriteRenderer sprites in childSortingLayer)
     //       defaultSortingLayer = sprites.GetComponent<SortingLayer>().ToString();
   // }

    //Player enters collision, put sign/doghouseroof/whevs in front of player in sorting layer name
    //NPC enters collision, put the actual NPC behind by changing -its- sorting layer name
    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("Player"))
        {
            if (hero.transform.position.y < colliderScenery.transform.position.y)
            {

                foreach (SpriteRenderer sprites in childSortingLayer)
                    sprites.sortingLayerName = "foreground accessories";
            }
            else
            {
                foreach (SpriteRenderer sprites in childSortingLayer)
                    sprites.sortingLayerName = "behind accessories";
            }

        }


    }

    void OnTriggerExit2D(Collider2D collider)
    {
        //Player leaves collision, put sign/doghouseroof/whevs in behind player in sorting layer name (back to original spot)
        //NPC leaves collision, put the actual NPC at its appropriate sorting layer name
        if (collider.gameObject.CompareTag("Player"))
        {
            if (defaultSortingLayer != null)
            {
                foreach (SpriteRenderer sprites in childSortingLayer)
                    sprites.sortingLayerName = defaultSortingLayer;
            }
            else
            {
                foreach (SpriteRenderer sprites in childSortingLayer)
                    sprites.sortingLayerName = "foreground accessories";
            }
        }



    }
}

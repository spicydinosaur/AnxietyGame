using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparency : MonoBehaviour
{

    public SpriteRenderer[] childTransparency;


    public void OnTriggerEnter2D (Collider2D collider)
    {
        Debug.Log("transparency scrip activated.");
     if (collider.gameObject.CompareTag ("Player"))
        {
            Debug.Log("transparency should happen now.");
            foreach (SpriteRenderer sprites in childTransparency)
                sprites.color = new Color(255f, 255f, 255f, .5f);



        }  
    }
    
    public void OnTriggerExit2D (Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("transparency should go away now.");
            foreach (SpriteRenderer sprites in childTransparency)
                sprites.color = new Color(255f, 255f, 255f, 1f);

        }
    }

    
    
}

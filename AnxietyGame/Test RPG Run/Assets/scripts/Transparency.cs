using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparency : MonoBehaviour
{

    public SpriteRenderer[] childTransparency;
    public Color defaultColor;


    public void OnTriggerEnter2D (Collider2D collider)
    {
    
     if (collider.gameObject.CompareTag ("Player"))
        {

            Debug.Log("transparency should happen now.");
            foreach (SpriteRenderer sprites in childTransparency)
                sprites.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, .5f);



        }  
    }
    
    public void OnTriggerExit2D (Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            foreach (SpriteRenderer sprites in childTransparency)
                sprites.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 1f);

        }
    }

    
    
}

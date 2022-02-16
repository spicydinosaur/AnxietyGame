using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewbieSpell : SpellTemplate

{ 




    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void attemptSpell()
    {
        base.attemptSpell();
    }

    public override void castSpell()
    {
        int layerMask = LayerMask.GetMask("Scenery", "Enemy");

        RaycastHit2D hit = Physics2D.Raycast(Hero.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - Hero.transform.position, rayCastDistance, layerMask);
        player.PlayerMana(manaCost);

        if (hit.collider != null)
        {

            spellHolder.currentCastDownTime = castDownTime;
            //spellBook.GetComponent<SpriteRenderer>().color = spellbookColor;
           //spellHolder.animPrefabInstantiate.SetBool("isCasting", true);
            if (spellPrefabInstantiate == null)
            {
               GameObject spellPrefabInstantiate = Instantiate(spellPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            }

             spellPrefabInstantiate.transform.position = hit.point;

            if (hit.collider.CompareTag("Enemy") == true)
            {

                hit.collider.GetComponent<NPCHealth>().damageNPCHealth(damageAmount);
                Debug.Log("hit enemy : " + hit.collider.name);

            }

            else if (hit.collider.CompareTag("Enemy") != true)
            {
                Debug.Log("hit Something : " + hit.collider.name);
                Debug.Log("hit tag  : " + hit.collider.tag);
            }

        }

        else
        {
            spellPrefabInstantiate.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 5);
        }

        spellPrefabInstantiate.GetComponent<Animator>().SetBool("isCasting", true);

    }
}

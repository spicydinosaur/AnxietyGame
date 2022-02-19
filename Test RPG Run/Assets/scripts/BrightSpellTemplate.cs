using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightSpellTemplate : SpellTemplate


{



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame


    public override void attemptSpell()
    {
        base.attemptSpell();
    }

    public override void castSpell()
    {
        int layerMask = LayerMask.GetMask("Scenery", "Enemy");

        RaycastHit2D hit = Physics2D.Raycast(hero.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - hero.transform.position, rayCastDistance, layerMask);



        if (hit.collider != null)
        {
            spellPrefabInstantiate.GetComponent<Animator>().SetBool("isCasting", true);
            spellPrefabInstantiate.GetComponent<Animator>().SetInteger("currentSpell", 1);
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
            fizzlePrefab = spellPrefab;
            fizzlePrefab = spellPrefabInstantiate;
            spellPrefabInstantiate.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 5);
            spellPrefabInstantiate.GetComponent<Animator>().SetBool("nothingHit", true);

        }


    }
}
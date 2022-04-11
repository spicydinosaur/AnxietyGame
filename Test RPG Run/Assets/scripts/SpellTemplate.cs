using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;



public class SpellTemplate : MonoBehaviour
{

    //what will this shit hit? Do we need this in template?
    public ContactFilter2D contactFilter;
    public float rayCastDistance;

    //Spells, can we work off of prefabs alone?
    //protected GameObject spellPrefab;

    public float damageAmount;
    public float manaCost;
    public float castDownTime;
    public float globalCastDownTime;
    public float currentCastDownTime;
    public int currentCastDowntimeRounded;
    public float fizzleSpellCastDownTime;
    public float fizzleSpellDownTimeMax;

    //public float fizzleGlobalCastDownTime;

    public GameObject hero;
    public Player player;

    public GameObject spellHolder;
    public SpellHolder spellHolderScript;

    public Vector2 mousePos;
    public Vector2 worldPos;
    public Vector2 direction;
    public Vector2 heroTransform;
    public Vector2 point;

    public Animator fizzleSpellAnim;
    public GameObject fizzleSpell;
    public Image spellIconMask;






    public virtual void Awake()
    {


        currentCastDownTime = 0f;



    }





    public virtual void Update()
    {
 
        if (currentCastDownTime > 0f)
        {
            currentCastDownTime -= Time.deltaTime;


        }
        else if (currentCastDownTime < 0f)
        {
            currentCastDownTime = 0f;

        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("isCasting", false);

        }




    }





    public virtual void castSpell()
    {

        
        int layerMask = LayerMask.GetMask("Scenery", "Enemy");

        heroTransform = hero.transform.position;
        direction = (worldPos - heroTransform).normalized;


        RaycastHit2D hit = Physics2D.Raycast(heroTransform, direction, rayCastDistance, layerMask);

        if (hit.collider != null)
        {

            gameObject.transform.position = hit.transform.position;
            gameObject.GetComponent<Animator>().SetBool("isCasting", true);
            currentCastDownTime = castDownTime;
            spellHolderScript.currentCastDownTime = currentCastDownTime;
            spellHolderScript.globalCastDownTime = globalCastDownTime;

            point = hit.point;

            if (hit.collider.CompareTag("Enemy") == true)
            {

                hit.collider.GetComponent<NPCHealth>().damageNPCHealth(damageAmount);
                Debug.Log("hit enemy : " + hit.collider.name + ". Spell should be " + spellHolderScript.currentSpell + ". Collision occurred at " + hit.point);

            }

            else
            {

                Debug.Log("hit Something : " + hit.collider.name + " spell should be " + spellHolderScript.currentSpell + ". Collision occurred at " + hit.point);
                Debug.Log("hit tag  : " + hit.collider.tag);
            }

            spellIconMask.fillAmount = 1f;

        }

        else 

        {

            point = heroTransform + direction * rayCastDistance;

            spellHolderScript.fizzleSpellAnim.SetBool("isCasting", true);
            spellHolderScript.fizzleSpell.transform.position = point;



            spellHolderScript.globalCastDownTime = globalCastDownTime;
            spellHolderScript.currentCastDownTime = castDownTime;
            currentCastDownTime = castDownTime;

            if (fizzleSpellCastDownTime == 0)
            {
                
                fizzleSpellCastDownTime = fizzleSpellDownTimeMax;

            }

            spellIconMask.fillAmount = 1f;

            Debug.Log("Nothing hit. Fizzlespell activating and moved to " + point );

        }




    }





    //Work on this later! Gamepad controls
    /*
    public virtual void castSpellGamepad()
    {
        int layerMask = LayerMask.GetMask("Scenery", "Enemy");

        RaycastHit2D hit = Physics2D.Raycast(hero.transform.position, spellCastGamepad.ReadValue<Vector2>(), rayCastDistance, layerMask);
        spellHolder.currentCastDownTime = castDownTime;

        if (hit.collider != null)
        {

            if (spellInstantiate == null)
            {
                spellInstantiate = Instantiate(spellPrefab, hit.point, Quaternion.identity);
                spellInstantiateTemplate = spellInstantiate.GetComponent<SpellTemplate>();
                spellHolder.spellInstantiate = spellInstantiate;
                spellHolder.spellInstantiateTemplate = spellInstantiate.GetComponent<SpellTemplate>();
            }
            else
            {
                spellInstantiate.transform.position = hit.point;
            }

            spellInstantiate.GetComponent<Animator>().SetBool("isCasting", true);
            currentCastDownTime = castDownTime;
            spellHolder.globalCastDownTime = globalCastDownTime;
            currentCastDownTime = castDownTime;

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

            fizzleSpell.SetBool("isCasting)", true);
            spellHolder.globalCastDownTime = globalCastDownTime;
            currentCastDownTime = castDownTime;

        }
    }
    }
    */



}




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

    public float damageAmount;
    public float manaCost;
    public float castDownTime;
    public float globalCastDownTime;
    public float currentCastDownTime;
    public int currentCastDowntimeRounded;
    public float fizzleSpellCastDownTime;
    public float fizzleSpellDownTimeMax;

    public GameObject hero;
    public Player player;

    public GameObject spellHolder;
    public SpellHolder spellHolderScript;

    public Vector2 mousePos;
    public Vector2 direction2D;
    public Vector3 heroTransform;
    public Vector2 point;
    public Vector3 mousePos3D;

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

        Vector2 heroTransform2D = new Vector2(heroTransform.x, heroTransform.y);
        Debug.Log("pre ScreenToWorldPoint location for heroTransform2D = " + heroTransform2D + ".");
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.farClipPlane));

        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        Debug.Log("pre ScreenToWorldPoint location for mousePos2D = " + mousePos2D + ".");

        Vector3 direction3D = (mousePos3D - heroTransform).normalized;
        direction2D = (mousePos2D - heroTransform2D).normalized;
        Debug.Log("direction has coords " + direction2D + ".");

        Debug.Log("heroTransform2D registered at " + heroTransform2D + ". mousePos registered at " + mousePos + ".");

        Debug.Log("rayCastDistance set at " + rayCastDistance + ".");


        RaycastHit2D hit = Physics2D.Raycast(heroTransform2D, direction2D, rayCastDistance, layerMask);

        if (hit.collider != null)
        {
            Vector3 hit3D = new Vector3(hit.point.x, hit.point.y, 0f);
            Debug.Log("pre camera hit3D value is " + hit3D + ", hit.point value is " + hit.point + ".");
            hit3D = Camera.main.ScreenToWorldPoint(hit3D);
            Debug.Log("post camera hit3D value is " + hit3D + ".");
            gameObject.transform.position = hit3D;
            gameObject.GetComponent<Animator>().SetBool("isCasting", true);


            point = new Vector3(hit.point.x, hit.point.y, 0f);

            if (hit.collider.CompareTag("Enemy") == true)
            {

                hit.collider.GetComponent<NPCHealth>().damageNPCHealth(damageAmount);
                Debug.Log("hit enemy : " + hit.collider.name + ". Spell should be " + spellHolderScript.currentSpell + ". Collision occurred at " + hit.point);

            }

            else if (hit.collider.CompareTag("Scenery") == true)
            {

                Debug.Log("hit Something : " + hit.collider.name + " spell should be " + spellHolderScript.currentSpell + ". Collision occurred at " + hit.point);
                Debug.Log("hit tag  : " + hit.collider.tag);
            }

            currentCastDownTime = castDownTime;
            spellHolderScript.currentCastDownTime = currentCastDownTime;
            spellHolderScript.globalCastDownTime = globalCastDownTime;

            spellIconMask.fillAmount = 1f;

        }

        else 

        {

            point = heroTransform + direction3D * rayCastDistance;


            spellHolderScript.fizzleSpellAnim.SetBool("isCasting", true);
            fizzleSpell.transform.position = point;



            spellHolderScript.globalCastDownTime = globalCastDownTime;
            spellHolderScript.currentCastDownTime = castDownTime;
            currentCastDownTime = castDownTime;

            if (fizzleSpellCastDownTime == 0)
            {
                
                fizzleSpellCastDownTime = fizzleSpellDownTimeMax;

            }

            spellIconMask.fillAmount = 1f;

            Debug.Log("Nothing hit. Fizzlespell (" + fizzleSpell.transform.position + ") activating and moved to " + point + ", point calculated with heroTransform2D + direction2D * rayCastDistance.");

        }




    }



    //Work on Gamepad controls!!




}




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

    //public float fizzleGlobalCastDownTime;

    public GameObject hero;
    public Player player;

    public Vector2 mousePos;
    public Vector2 worldPos;
    public Vector2 direction;
    public Vector2 heroTransform;
    public Vector3 point;

    public float mouseDistance;

    public Animator fizzleSpellAnim;
    public GameObject fizzleSpell;
    public Image spellIconMask;

    public AudioSource spellSound;

    public Camera mainCam;

    public GameObject hitObject;




    public virtual void Awake()
    {


        currentCastDownTime = 0f;
        spellSound = gameObject.GetComponent<AudioSource>();


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
        //rawDirection = new Vector3(mousePos.x, mousePos.y, 0f) - tempHeroTransformValues;
        heroTransform = hero.transform.position;
        direction = (mousePos - heroTransform).normalized;
        //Debug.Log("hero transform position = " + hero.transform.position);
        //Debug.Log("Camera.main.ScreenToWorldPoint(hero.transform.position) = " + Camera.main.ScreenToWorldPoint(hero.transform.position));
        //Debug.Log("heroTransform = " + heroTransform + ", direction = " + direction + ", mousePos = " + mousePos + ".");


        RaycastHit2D hit = Physics2D.Raycast(heroTransform, direction, rayCastDistance, layerMask);

        spellSound.Play();

       //Debug.Log("hit.point = " + hit.point + ".");

        if (hit.collider)
        {
            hitObject = hit.collider.gameObject;
            //gameObject.transform.position = hit.transform.position;
            gameObject.transform.position = hit.point;
            gameObject.GetComponent<Animator>().SetBool("isCasting", true);
            //Debug.Log("Spell now at " + gameObject.transform.position + " which should be the same as " + hit.transform.position + ".");

            if (hitObject.CompareTag("Enemy"))
            {

                hitObject.GetComponent<NPCHealth>().damageNPCHealth(damageAmount);

            }

            else if (hitObject.CompareTag("Scenery"))
            {


            }
            else
            {
                //Debug.Log("this is the statement appearing when a collider not on the layermask gets hit, it should never show up.");
                //Debug.Log("hit.name because something weird happened : " + hit.collider.name + ".");
            }

            currentCastDownTime = castDownTime;
            player.currentCastDownTime = currentCastDownTime;
            player.globalCastDownTime = globalCastDownTime;
            spellIconMask.fillAmount = 1f;
            //Debug.Log("hit Something : " + hit.collider.name + " spell should be " + player.currentSpell + ". Collision occurred at " + hit.transform.position);
            //Debug.Log("hit tag  : " + hit.collider.tag);
        

        }


        else

        {
            //Debug.Log("heroTransform (" + heroTransform + ") and mousePos (" + mousePos + ")");
            point = new Vector3(heroTransform.x + (direction.x * rayCastDistance), heroTransform.y + (direction.y * rayCastDistance), 0f);
            //Debug.Log("point now converted to heroTransform + direction * rayCastDistance and located at " + point + ".");

            player.fizzleSpell.transform.position = point;
            player.fizzleSpellAnim.SetBool("isCasting", true);


            player.globalCastDownTime = globalCastDownTime;
            player.currentCastDownTime = castDownTime;
            currentCastDownTime = castDownTime;

            spellIconMask.fillAmount = 1f;

           // Debug.Log("Nothing hit. Fizzlespell activating and moved to " + point);

        }




    }


    //Work on this later! Gamepad controls



}




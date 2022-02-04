using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHolder : MonoBehaviour

{

    public enum spellCastTypes { raycast, projectile, other };

    public GameObject Hero;
    public GameObject spellBook;
    public Animator animSpellBook;
    public AudioSource failSpell;
    public AudioClip failSpellClip;

    public float rayCastDistance;
    public float castDownTime;
    public float currentCastDownTime;
    private Color spellbookColor;

    //Spells
    public GameObject spellPrefab;
    public GameObject spellPrefabInstantiate;
    public Animator animPrefab;
    public Animator animPrefabInstantiate;
    public GameObject tracer;
    //public float damageAmount;

    public SpellTemplate[] equipSpell;
    public int selectedSpell;
    public float damageAmount;
    public float manaCost;

    public Player player;

    public ContactFilter2D contactFilter;

    //work needed on line 77 and here to allow for switching through spells!



    // Start is called before the first frame update
    void Start()
    {
        spellbookColor = spellBook.GetComponent<SpriteRenderer>().color;
        spellBook.GetComponent<SpriteRenderer>().color = Color.clear;
        currentCastDownTime = 0f;
        animSpellBook = spellBook.GetComponent<Animator>();
        spellPrefabInstantiate = Instantiate(spellPrefab, new Vector3(0, 0, -50), Quaternion.identity);
        animPrefabInstantiate = spellPrefabInstantiate.GetComponent<Animator>();
        selectedSpell = 0;

    }

    // Update is called once per frame
    void Update()
    {

        if (currentCastDownTime > 0)
        {
            currentCastDownTime -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0))
            {
                failSpell.PlayOneShot(failSpellClip, 1f);
            }
        }
        else if (currentCastDownTime <= 0f)
        {
            if (Input.GetMouseButtonDown(0))
            {

                spellBook.GetComponent<SpriteRenderer>().color = Color.clear;
                spellBook.GetComponent<Animator>().SetBool("isCasting", false);
                spellPrefab.GetComponent<Animator>().SetBool("isCasting", false);
                equipSpell[selectedSpell].attemptSpell();

            }


        }


        if ((Input.GetAxis("Mouse ScrollWheel") > 0f)) //scrollwheel goes forward
        {
            if (selectedSpell == equipSpell.Length - 1)
            {

                selectedSpell = 0;
                GetComponent<Animator>().SetInteger("currentSpell", 0);

            }
            else
            {
                selectedSpell++;
                GetComponent<Animator>().SetInteger("currentSpell", selectedSpell);

            }


        }
        
        else if ((Input.GetAxis("Mouse ScrollWheel") < 0f)) //scrollwheel goes backwards
        {

            if (selectedSpell > 0)
            {
                selectedSpell--;
                GetComponent<Animator>().SetInteger("currentSpell", selectedSpell);
                
            }

            else
            {
                selectedSpell = equipSpell.Length -1;
                GetComponent<Animator>().SetInteger("currentSpell", equipSpell.Length -1);
            }

        }
    }


}

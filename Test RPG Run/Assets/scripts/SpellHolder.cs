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

    public SpellTemplate spellPrefabTemplate;

    //These are for when the spell gets added to the players "spellbook" and are used to add details to three of the four relevant hashtables.
    public string addedSpellName;
    public GameObject addedSpellPrefab;
    public string addedSpellType;
    public SpriteRenderer addedSpellIcon;

    //these are the lists that hold the details for all the spells the character currently has.
    public string[] listOfSpellNames;
    public GameObject[] listOfSpellPrefabs;
    public string[] listOfSpellTypes;
    public SpriteRenderer[] listOfSpellIcons;

    public GameObject defaultSpell;
    public int selectedSpell;


    public Player player;

    public bool tutorialComplete;

    public ContactFilter2D contactFilter;

    //work needed on line 77 and here to allow for switching through spells!

    //WTF does instantiate do again? Oh right make something that is prefab real. Work on this! Something is continuously making new instantiates!


    // Start is called before the first frame update
    void Start()
    {

        var breatheSpellPrefab = Resources.Load<GameObject>("Assets/Prefabs/Spells/Breathe.prefab");
        var brightSpellPrefab = Resources.Load<GameObject>("Assets/Prefabs/Spells/Bright.prefab");
        var fireballSpellPrefab = Resources.Load<GameObject>("Assets/Prefabs/Spells/Fireball.prefab");
        var smiteSpellPrefab = Resources.Load<GameObject>("Assets/Prefabs/Spells/Smite.prefab");

        var breatheSpellIcon = Resources.Load<SpriteRenderer>("Assets/Prefabs/Spells/SpellIcons/BreatheIcon.prefab");
        var brightSpellIcon = Resources.Load<SpriteRenderer>("Assets/Prefabs/Spells/SpellIcons/BrightIcon.prefab");
        var fireballSpellIcon = Resources.Load<SpriteRenderer>("Assets/Prefabs/Spells/SpellIcons/FireballIcon.prefab");
        var smiteSpellIcon = Resources.Load<SpriteRenderer>("Assets/Prefabs/Spells/SpellIcons/SmiteIcon.prefab");

        if (tutorialComplete != true)
        {



            listOfSpellNames = new string[2] { "Breathe", "Bright" };
            listOfSpellPrefabs = new GameObject[2] { breatheSpellPrefab, brightSpellPrefab };
            listOfSpellTypes = new string[2] {"raycast", "raycast" };
            listOfSpellIcons = new SpriteRenderer[2] {breatheSpellIcon, brightSpellIcon};


        }

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
            if (spellBook.GetComponent<SpriteRenderer>().color != Color.clear)
            {
                spellBook.GetComponent<SpriteRenderer>().color = Color.clear;
                spellBook.GetComponent<Animator>().SetBool("isCasting", false);
                spellPrefab.GetComponent<Animator>().SetBool("isCasting", false);
            }

            if (Input.GetMouseButtonDown(0))
            {

                spellPrefabTemplate.attemptSpell();

            }


        }

        //Below needs to be fully implemented, allows cycling through spells with mouse wheel

        if ((Input.GetAxis("Mouse ScrollWheel") > 0f)) //scrollwheel goes forward
        {
            if (selectedSpell == listOfSpellNames.Length -1)
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
                selectedSpell = listOfSpellNames.Length - 1;
                GetComponent<Animator>().SetInteger("currentSpell", listOfSpellNames.Length - 1);
            }

        }
    }

    public virtual void addSpell()
    {

        var arrayLength = listOfSpellNames.Length - 1;
        listOfSpellNames.SetValue(addedSpellName, arrayLength);
        listOfSpellPrefabs.SetValue(addedSpellPrefab, arrayLength);
        listOfSpellTypes.SetValue(addedSpellType, arrayLength);
        listOfSpellIcons.SetValue(addedSpellIcon, arrayLength);

    }



}

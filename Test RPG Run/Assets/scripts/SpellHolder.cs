using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Serialization;
using UnityEngine.U2D;


public class SpellHolder : MonoBehaviour

{
       

    //public enum spellCastTypes { raycast, projectile, other };

    public GameObject hero;

    public AudioSource failSpell;
    public AudioClip failSpellClip;

    //Spells
    public GameObject currentSpell;
    public SpellTemplate currentSpellTemplate;
    
    //These are for when the spell gets added to the players "spellbook" and are used to add details to three of the four relevant hashtables.
    public GameObject addedSpell;
    public Sprite addedSpellIcon;

    public Image spellIconImage;
    public Image spellIconMask;
    public Sprite currentSpellIcon;

    //these are temporararily defined onawake until the player can save progress
    public Sprite[] listOfSpellIcons;
    public GameObject[] listOfSpells;

    public GameObject breatheSpell;
    public GameObject brightSpell;
    public GameObject flamesSpell;
    public GameObject smiteSpell;
    
    public int selectedSpell;
    public int selectedSpellMax;

    public GameObject fizzleSpell;
    public Animator fizzleSpellAnim;


    public float globalCastDownTime;
    public float currentCastDownTime;

    public Player player;

    private PlayerControls playerControls;
    private InputAction leftMouseClick;

    public GameObject dialogueBox;
    public GameObject thoughtBox;

    public int numberOfPools = 4;
    public List<GameObject> instantiatedPools = new List<GameObject>(4);
    public BrightSpellTemplate brightSpellTemplate;
    public GameObject poolToInstantiateFrom;


    public float spellSelectMouseScrollWheel;
    //public float mouseScrolling;


    //this is temporary until the player can save progress
    public bool tutorialComplete = false;



    public void Awake()
    {
        playerControls = new PlayerControls();
        leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
        InstantiatePools();

    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.PlayerActions.SpellSelectMouseScrollWheel.performed += x => spellSelectMouseScrollWheel = x.ReadValue<float>();
        //playerControls.PlayerActions.SpellCast.performed += PrepCastSpell;
        leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
        leftMouseClick.Enable();
        leftMouseClick.performed += ctx => PrepCastSpell();



    }

    public void Start()
    {

        var breatheSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/breatheicon.png");
        var brightSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/brighticon.png");
        var flamesSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/flamesicon.png");
        var smiteSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/smiteicon.png");



        listOfSpells = new GameObject[4];
        listOfSpells.SetValue(breatheSpell, 0);
        listOfSpells.SetValue(brightSpell, 1);
        listOfSpells.SetValue(flamesSpell, 2);
        listOfSpells.SetValue(smiteSpell, 3);

        currentSpell = breatheSpell;
        currentSpellTemplate = currentSpell.GetComponent<SpellTemplate>();



        selectedSpell = 0;

        //Temporary cap for spells until we can get a persistent variable in place to track how many spells the player has with saves and such.

        currentCastDownTime = 0f;
        globalCastDownTime = 0f;
        spellIconMask.fillAmount = 0f;
        //mouseScrolling = 0f;



    }





    public void InstantiatePools()
    {

        for (int i = 0; i < numberOfPools; i++)
        {

            GameObject instantiatedLightInProcessing = Instantiate(poolToInstantiateFrom, hero.transform.position, Quaternion.identity);
            instantiatedPools.Add(instantiatedLightInProcessing);
           
            Debug.Log("For loop on start is firing");

        }

    }


    private void PrepCastSpell()
    {
        if (thoughtBox.activeSelf == false && dialogueBox.activeSelf == false)
        { 
            if (selectedSpell == 0)
            {

                if (currentCastDownTime != 0)
                {
                    Debug.Log("mouse click registered for spell casting. Spell on cooldown, attempt failed.");
                    failSpell.PlayOneShot(failSpellClip, 1f);


                }
                else if (currentCastDownTime == 0f)
                {

                    Debug.Log("mouse click registered for spell casting. Spell available to cast, no cooldown in place. " + currentSpellTemplate.name + " is the current instantiated template.");
                    currentSpellTemplate.castSpell();

                }
            }
            else
            {
                if (currentCastDownTime != 0)
                {
                    Debug.Log("mouse click registered for spell casting. Spell on cooldown, attempt failed.");
                    failSpell.PlayOneShot(failSpellClip, 1f);


                }
                else if (currentCastDownTime == 0f)
                {

                    currentSpellTemplate.mousePos = Mouse.current.position.ReadValue();
                    currentSpellTemplate.worldPos = Camera.main.ScreenToWorldPoint(currentSpellTemplate.mousePos);
                    Debug.Log("mouse click registered for spell casting. Spell available to cast, no cooldown in place. Mouse position registered at: " + currentSpellTemplate.worldPos);
                    currentSpellTemplate.castSpell();

                }
            }

        }
    }



    public void Update()
    {

        //if (mouseScrolling == 0f)
        //{

            if (spellSelectMouseScrollWheel > 0) //scroll wheel gets moved up, moving through the spell list in a positive direction.
            {


                if (selectedSpell >= selectedSpellMax)
                {
                    selectedSpell = 0;

                }
                else
                {
                    selectedSpell++;
                }

                currentSpell = listOfSpells[selectedSpell];
                currentSpellTemplate = currentSpell.GetComponent<SpellTemplate>();
                Debug.Log("spell instantiate gameobject is " + currentSpell);
                Debug.Log("spell instantiate template script is " + currentSpellTemplate);
                Debug.Log("SpellPrefab selection list position is " + listOfSpells[selectedSpell]);

                if (globalCastDownTime > currentSpellTemplate.currentCastDownTime && selectedSpell != 0)
                {
                    currentSpellTemplate.currentCastDownTime = globalCastDownTime;
                }

                else
                {
                    //0 is breathe so not bothering to put in anything wrt global cast cooldowns
                    currentCastDownTime = currentSpellTemplate.currentCastDownTime;
                }



                Debug.Log("mouse scroll wheel up. Spellholder.selectedSpell = " + selectedSpell);
                spellIconImage.GetComponent<Image>().sprite = listOfSpellIcons[selectedSpell];


            }
            else if (spellSelectMouseScrollWheel < 0) //scroll wheel gets moved down, moving through the spell list in a negative direction.
            {


                if (selectedSpell <= 0)
                {
                    selectedSpell = selectedSpellMax;

                }
                else
                {
                    selectedSpell--;
                }


                currentSpell = listOfSpells[selectedSpell];
                currentSpellTemplate = currentSpell.GetComponent<SpellTemplate>();
                Debug.Log("spell instantiate gameobject is " + currentSpell);
                Debug.Log("spell instantiate template script is " + currentSpellTemplate);
                Debug.Log("SpellPrefab selection list position is " + listOfSpells[selectedSpell]);


                if (globalCastDownTime > currentSpellTemplate.currentCastDownTime && selectedSpell != 0)
                {
                    currentCastDownTime = globalCastDownTime;
                    currentSpellTemplate.currentCastDownTime = globalCastDownTime;
                }
                else
                {
                    //0 is breathe so not bothering to put in anything wrt global cast cooldowns
                    currentCastDownTime = currentSpellTemplate.currentCastDownTime;
                }


                Debug.Log("mouse scroll wheel down Spellholder.selectedSpell = " + selectedSpell);
                spellIconImage.GetComponent<Image>().sprite = listOfSpellIcons[selectedSpell];
            }

        //}
        //else if (mouseScrolling < 0f)
        //{
         //   mouseScrolling = 0f;
        //}
        //else if (mouseScrolling > 0f)
        //{
          //  mouseScrolling -= Time.deltaTime;
        //}

        if (currentCastDownTime > 0f)
        {
            currentCastDownTime -= Time.deltaTime;


        }
        else if (currentCastDownTime < 0f)
        {
            currentCastDownTime = 0f;

        }

        if (globalCastDownTime > 0f)
        {
            globalCastDownTime -= Time.deltaTime;
        }
        else if (globalCastDownTime < 0f)
        {
            globalCastDownTime = 0f;
        }

        if (currentSpellTemplate.fizzleSpellCastDownTime > 0)
        {
            currentSpellTemplate.fizzleSpellCastDownTime -= Time.deltaTime;
        }
        else if (currentSpellTemplate.fizzleSpellCastDownTime < 0)
        {
            currentSpellTemplate.fizzleSpellCastDownTime = 0;
        }
        else
        {
            fizzleSpellAnim.SetBool("isCasting", false);
        }

        spellIconMask.fillAmount = Mathf.Clamp01(currentSpellTemplate.currentCastDownTime / currentSpellTemplate.castDownTime);

        /* more work on this later!
           
        else if (playerControls.PlayerActions.SpellCastGamepad.triggered)
        {
            if (currentCastDownTime != 0f)
            {
                spellHolder.failSpell.PlayOneShot(spellHolder.failSpellClip, 1f);
                return;
            }
            else
            {
                
                castSpellGamepad();

            }
        }*/
    }





    public void addSpell(GameObject addedSpell, Sprite addedSpellIcon)
    {
        /*var breatheSpellPrefab = Resources.Load<GameObject>("Assets/Prefabs/Spells/Breathe.prefab");
        var brightSpellPrefab = Resources.Load<GameObject>("Assets/Prefabs/Spells/Bright.prefab");
        var flamesSpellPrefab = Resources.Load<GameObject>("Assets/Prefabs/Spells/Flames.prefab");
        var smiteSpellPrefab = Resources.Load<GameObject>("Assets/Prefabs/Spells/Smite.prefab");*/

        var breatheSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/breatheicon.png");
        var brightSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/brighticon.png");
        var flamesSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/flamesicon.png");
        var smiteSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/smiteicon.png");

        listOfSpells.SetValue(addedSpell, listOfSpells.Length);
        listOfSpellIcons.SetValue(addedSpellIcon, listOfSpells.Length);

    }



    private void OnDisable()
    {
        //playerControls.PlayerActions.SpellCast.performed -= PrepCastSpell;
        playerControls.Disable();

    }


}

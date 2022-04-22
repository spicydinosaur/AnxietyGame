using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SpellHolder : MonoBehaviour

{


    //public enum spellCastTypes { raycast, projectile, other };

    public GameObject hero;

    public AudioSource failSpell;
    public AudioClip failSpellClip;

    //Spells
    public GameObject currentSpellPrefab;
    public GameObject currentSpellInstantiate;
    public SpellTemplate currentSpellInstantiateTemplate;

    //These are for when the spell gets added to the players "spellbook" and are used to add details to three of the four relevant hashtables.
    public GameObject addedSpellPrefab;
    public Sprite addedSpellIcon;

    public Image spellIconImage;
    public Image spellIconMask;
    public Sprite currentSpellIcon;

    public int selectedSpell;
    public int selectedSpellMax;
    public int totalSelectedSpellMax;

    //these are temporararily defined onawake until the player can save progress
    public List<GameObject> listOfSpellPrefabs = new List<GameObject>(4);
    public List<Sprite> listOfSpellIcons = new List<Sprite>(4);
    public List<GameObject> listOfSpellInstantiates = new List<GameObject>(4);
    public List<GameObject> instantiatedPools = new List<GameObject>(4);

    //breathe doesn't actually get instantiated as there will never be more than one instance;
    public GameObject breatheSpellInstantiate;

    public GameObject fizzleSpell;
    public Animator fizzleSpellAnim;


    public float globalCastDownTime;
    public float currentCastDownTime;

    public Player player;

    private PlayerControls playerControls;
    private InputAction leftMouseClick;

    public GameObject dialogueBox;
    public GameObject thoughtBox;

    public Vector3 mousePosition;


    public float spellSelectMouseScrollWheel;

    public int numberOfPools;
    public GameObject poolToInstantiateFrom;
    //public float mouseScrolling;
    public Camera mainCam;



    //this is temporary until the player can save progress
    public bool tutorialComplete = false;



    public void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.PlayerActions.SpellSelectMouseScrollWheel.performed += x => spellSelectMouseScrollWheel = x.ReadValue<float>();
        //playerControls.PlayerActions.SpellCast.performed += PrepCastSpell;
        leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
        leftMouseClick.performed += ctx => PrepCastSpell();
        leftMouseClick.Enable();
        //mousePosition = playerControls.PlayerActions.MousePosition.ReadValue<Vector2>();



    }


    public void Start()
    {

        var breatheSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/breatheicon.png");
        var brightSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/brighticon.png");
        var flamesSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/flamesicon.png");
        var smiteSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/smiteicon.png");

        for (int i = 0; i < selectedSpellMax; i++)
        {
            if (i!=0)
            {
                currentSpellInstantiate = Instantiate(listOfSpellPrefabs[i], hero.transform.position, Quaternion.identity);
                listOfSpellInstantiates.Add(currentSpellInstantiate);
            }
            else
            {
                listOfSpellInstantiates.Add(breatheSpellInstantiate);
            }
            
        }


        for (int i = 0; i < numberOfPools; i++)
        {

            GameObject instantiatedLightInProcessing = Instantiate(poolToInstantiateFrom, hero.transform.position, Quaternion.identity);
            instantiatedLightInProcessing.name = new string("LightPool" + i);
            instantiatedPools.Add(instantiatedLightInProcessing);

            Debug.Log("For loop on start is firing");

        }

        currentSpellInstantiate = listOfSpellInstantiates[selectedSpell];
        currentSpellInstantiateTemplate = currentSpellInstantiate.GetComponent<SpellTemplate>();
        currentSpellIcon = listOfSpellIcons[selectedSpell];


        //Temporary cap for spells until we can get a persistent variable in place to track how many spells the player has with saves and such.

        currentCastDownTime = 0f;
        globalCastDownTime = 0f;
        spellIconMask.fillAmount = 0f;

    }

    public void InstantiateSpells()
    {
        for (int i = 0; i < selectedSpellMax; i++)
        {
            if (i != 0)
            {
                currentSpellInstantiate = Instantiate(listOfSpellPrefabs[i], hero.transform.position, Quaternion.identity);

            }
        }
    }

    public void InstantiatePools()
    {

        for (int i = 0; i < numberOfPools; i++)
        {

            GameObject instantiatedLightInProcessing = Instantiate(poolToInstantiateFrom, hero.transform.position, Quaternion.identity);
            instantiatedLightInProcessing.name = new string("LightPool" + i);
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
                    //This spell has no range and requires no values other than being at 0 on currentCastDownTime.
                    Debug.Log("mouse click registered for spell casting. Spell available to cast, no cooldown in place. " + currentSpellInstantiateTemplate.name + " is the current instantiated template.");
                    currentSpellInstantiateTemplate.castSpell();

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
                    Debug.Log("pre camera conversion gives us playerControls.PlayerActions.MousePosition.ReadValue<Vector2>() at " + Mouse.current.position.ReadValue());
                    //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0f));
                    Debug.Log("Mouse click registered for spell casting. mousePosition registered at: " + mousePosition);
                    currentSpellInstantiateTemplate.mousePos = new Vector2(mousePosition.x, mousePosition.y);
                    Debug.Log("Spell available to cast, no cooldown in place. mousePos registered at: " + currentSpellInstantiateTemplate.mousePos);
                    Debug.Log("mouse click registered for spell casting. Spell available to cast, no cooldown in place. " + currentSpellInstantiateTemplate + " is the current instantiated template. The currentSpellInstantiate is " + currentSpellInstantiate);
                    currentSpellInstantiateTemplate.castSpell();

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

                currentSpellPrefab = listOfSpellPrefabs[selectedSpell];

                currentSpellInstantiate = listOfSpellInstantiates[selectedSpell];
                currentSpellInstantiateTemplate = currentSpellInstantiate.GetComponent<SpellTemplate>();
                currentSpellIcon = listOfSpellIcons[selectedSpell];
                Debug.Log("spell instantiate gameobject is " + currentSpellInstantiate);
                Debug.Log("spell instantiate template script is " + currentSpellInstantiateTemplate);
                Debug.Log("SpellPrefab selection list position is " + listOfSpellPrefabs[selectedSpell] + " and the prefab variable is showing " + currentSpellPrefab);

                if (globalCastDownTime > currentSpellInstantiateTemplate.currentCastDownTime && selectedSpell != 0)
                {
                    currentSpellInstantiateTemplate.currentCastDownTime = globalCastDownTime;
                }

                else
                {
                    //0 is breathe so not bothering to put in anything wrt global cast cooldowns
                    currentCastDownTime = currentSpellInstantiateTemplate.currentCastDownTime;
                }



                Debug.Log("mouse scroll wheel up. Spellholder.selectedSpell = " + selectedSpell);
                spellIconImage.GetComponent<Image>().sprite = currentSpellIcon;


            }
            else if (spellSelectMouseScrollWheel < 0) //scroll wheel gets moved down, moving through the spell list in a negative direction.
            {


                if (selectedSpell == 0)
                {
                    selectedSpell = selectedSpellMax - 1;

                }
                else if (selectedSpell > 0)
                {
                    selectedSpell--;
                }
                
                Debug.Log("selectedSpell = " + selectedSpell);
            
                currentSpellInstantiate = listOfSpellInstantiates[selectedSpell];
                currentSpellIcon = listOfSpellIcons[selectedSpell];
                currentSpellInstantiateTemplate = currentSpellInstantiate.GetComponent<SpellTemplate>();
                Debug.Log("spell instantiate gameobject is " + currentSpellInstantiate);
                Debug.Log("spell instantiate template script is " + currentSpellInstantiateTemplate);
                Debug.Log("SpellPrefab selection list position is " + listOfSpellPrefabs[selectedSpell] + " and the prefab variable is showing " + currentSpellPrefab);


                if (globalCastDownTime > currentSpellInstantiateTemplate.currentCastDownTime && selectedSpell != 0)
                {
                    currentCastDownTime = globalCastDownTime;
                    currentSpellInstantiateTemplate.currentCastDownTime = globalCastDownTime;
                }
                else
                {
                    //0 is breathe so not bothering to put in anything wrt global cast cooldowns
                    currentCastDownTime = currentSpellInstantiateTemplate.currentCastDownTime;
                }


                Debug.Log("mouse scroll wheel down Spellholder.selectedSpell = " + selectedSpell);
                spellIconImage.GetComponent<Image>().sprite = currentSpellIcon;
            }


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

        if (currentSpellInstantiateTemplate.fizzleCastDownTime > 0)
        {
            currentSpellInstantiateTemplate.fizzleCastDownTime -= Time.deltaTime;
        }
        else if (currentSpellInstantiateTemplate.fizzleCastDownTime < 0)
        {
            currentSpellInstantiateTemplate.fizzleCastDownTime = 0;
        }
        else
        {
            fizzleSpellAnim.SetBool("isCasting", false);
        }

        spellIconMask.fillAmount = Mathf.Clamp01(currentSpellInstantiateTemplate.currentCastDownTime / currentSpellInstantiateTemplate.castDownTime);

        //more work on gamepads later!

    }





    public void addSpell(GameObject addedSpellPrefab, Sprite addedSpellIcon)
    {

        var breatheSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/breatheicon.png");
        var brightSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/brighticon.png");
        var flamesSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/flamesicon.png");
        var smiteSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/smiteicon.png");

        listOfSpellPrefabs.Capacity = listOfSpellPrefabs.Capacity + 1;

        listOfSpellPrefabs.Add(addedSpellPrefab);
        listOfSpellIcons.Add(addedSpellIcon);

    }



    private void OnDisable()
    {

        playerControls.Disable();

    }


}

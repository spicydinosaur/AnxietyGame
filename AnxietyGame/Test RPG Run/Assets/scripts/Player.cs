using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player instance { get; private set; }
    //private PlayerControls playerControls;
    
    public float maxHealth;
    public float maxMana;


    public float currentHealth;
    public float currentMana;

    public float maxHealthBar;
    public float maxManaBar;

    public int currentCoins = 0;
    public int maxCoins = 999;
    public TextMeshProUGUI coinsText;

    //this is used in a few other scripts so don't remove it just because it doesn't get called!
    public bool transitioningToScene = false;

    public GameManager gameManager;

    public GameObject teleportSpell;

    public float playerSpeed;

    private Animator animator;

    public Rigidbody2D rigidBody;

    public Vector2 lookDirection = new Vector2(0, -1);

    public InputAction move;
    public PlayerControls playerControls;

    public float spellSelectMouseScrollWheel;
    public float movement;
    public Vector2 moveInput;

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

    public int selectedSpell;
    public int selectedSpellMax;
    public int totalSelectedSpellMax;

    //these are temporararily defined onawake until the player can save progress
    public List<GameObject> listOfSpells = new List<GameObject>(4);
    public List<Sprite> listOfSpellIcons = new List<Sprite>(4);
    public List<GameObject> instantiatedPools = new List<GameObject>(4);

    //breathe doesn't actually get instantiated as there will never be more than one instance;
    public GameObject breatheSpell;

    public GameObject fizzleSpell;
    public Animator fizzleSpellAnim;


    public float globalCastDownTime;
    public float currentCastDownTime;


    public GameObject dialogueBox;
    public GameObject thoughtBox;

    public Vector3 mousePosition;


    public int numberOfPools;
    public GameObject poolToInstantiateFrom;

    public Camera mainCam;

    public void OnEnable()
    {
        playerControls = new PlayerControls();
        playerControls.PlayerActions.Enable();
    }

    public void OnDisable()
    {

        playerControls.PlayerActions.Disable();

    }

    public void Start()
    {
        instance = this;
        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        move = playerControls.PlayerActions.Movement;
        playerControls.PlayerActions.SpellSelectMouseScrollWheel.performed += ctx1 => spellSelectMouseScrollWheel = ctx1.ReadValue<float>();
        playerControls.PlayerActions.SpellCast.performed += ctx2 => PrepCastSpell();
        //move.performed += ctx3 => movement = ctx3.ReadValue<float>();

        currentHealth = maxHealth;
        currentMana = maxMana;
        gameManager.GetComponent<UIHealthBar>().SetValueHealth(maxHealthBar / (currentHealth / maxHealth));
        gameManager.GetComponent<UIHealthBar>().SetValueMana(maxManaBar / (currentMana / maxMana));


        var breatheSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/breatheicon.png");
        var brightSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/brighticon.png");
        var flamesSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/flamesicon.png");
        var smiteSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/smiteicon.png");



        for (int i = 0; i < numberOfPools; i++)
        {

            GameObject instantiatedLightInProcessing = Instantiate(poolToInstantiateFrom, transform.position, Quaternion.identity);
            instantiatedLightInProcessing.name = new string("LightPool" + i);
            instantiatedPools.Add(instantiatedLightInProcessing);

            Debug.Log("lightpool instantiating For loop on start is firing");

        }

        selectedSpell = 0;
        currentSpell = breatheSpell;
        currentSpellTemplate = currentSpell.GetComponent<SpellTemplate>();
        currentSpellIcon = listOfSpellIcons[selectedSpell];


        //Temporary cap for spells until we can get a persistent variable in place to track how many spells the player has with saves and such.

        currentCastDownTime = 0f;
        globalCastDownTime = 0f;
        spellIconMask.fillAmount = 0f;

    }


    public void InstantiatePools()
    {

        for (int i = 0; i < numberOfPools; i++)
        {

            GameObject instantiatedLightInProcessing = Instantiate(poolToInstantiateFrom, transform.position, Quaternion.identity);
            instantiatedLightInProcessing.name = new string("LightPool" + i);
            instantiatedPools.Add(instantiatedLightInProcessing);

            Debug.Log("For loop on start is firing");

        }

    }


    public void FixedUpdate()
    {
    //below is the code for player movement.
        moveInput = move.ReadValue<Vector2>();
        rigidBody.velocity = new Vector2(moveInput.x * playerSpeed, moveInput.y * playerSpeed);

        if (!Mathf.Approximately(moveInput.x, 0.0f) || !Mathf.Approximately(moveInput.y, 0.0f))
        {
            lookDirection.Set(moveInput.x, moveInput.y);
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", moveInput.magnitude);

        if (spellSelectMouseScrollWheel > 0) //scroll wheel gets moved up, moving through the spell list in a positive direction.
        {
            Debug.Log("spellSelectMouseScrollWheel fired on Player script, registering as a positive integer!");
        }
        else if (spellSelectMouseScrollWheel < 0)
        {
            Debug.Log("spellSelectMouseScrollWheel fired on Player script, registering as a negative integer!");
        }


        //below is the code for the scroll wheel to cycle through spells

        if (spellSelectMouseScrollWheel > 0) //scroll wheel gets moved up, moving through the spell list in a positive direction.
        {
            spellSelectMouseScrollWheel = 0;

            if (selectedSpell >= selectedSpellMax - 1)
            {
                selectedSpell = 0;

            }
            else
            {
                selectedSpell++;
            }

            currentSpell = listOfSpells[selectedSpell];
            currentSpellTemplate = currentSpell.GetComponent<SpellTemplate>();
            currentSpellIcon = listOfSpellIcons[selectedSpell];
            Debug.Log("spell instantiate gameobject is " + currentSpell);
            Debug.Log("spell instantiate template script is " + currentSpellTemplate);
            Debug.Log("Spell selection list position is " + selectedSpell);

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

            currentSpell = listOfSpells[selectedSpell];
            currentSpellIcon = listOfSpellIcons[selectedSpell];
            currentSpellTemplate = currentSpell.GetComponent<SpellTemplate>();
            Debug.Log("spell instantiate gameobject is " + currentSpell);
            Debug.Log("spell instantiate template script is " + currentSpellTemplate);


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


        spellIconMask.fillAmount = Mathf.Clamp01(currentCastDownTime / currentSpellTemplate.castDownTime);
        spellSelectMouseScrollWheel = 0;
        //more work on gamepads later!

    }

    public void PrepCastSpell()
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
                    Debug.Log("pre camera conversion gives us playerControls.PlayerActions.MousePosition.ReadValue<Vector2>() at " + Mouse.current.position.ReadValue());
                    mousePosition = mainCam.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0f));
                    Debug.Log("Mouse click registered for spell casting. mousePosition registered at: " + mousePosition);
                    currentSpellTemplate.mousePos = new Vector2(mousePosition.x, mousePosition.y);
                    Debug.Log("Spell available to cast, no cooldown in place. mousePos registered at: " + currentSpellTemplate.mousePos);
                    Debug.Log("mouse click registered for spell casting. Spell available to cast, no cooldown in place. " + currentSpellTemplate + " is the current instantiated template. The currentSpellInstantiate is " + currentSpell);
                    currentSpellTemplate.castSpell();

                }

            }
        }

    }

    public void PlayerHealth(float amount)
    {

        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
        gameManager.GetComponent<UIHealthBar>().SetValueHealth(maxHealthBar / (currentHealth / maxHealth));


        if (currentHealth <= 0)
        {

            teleportSpell.SetActive(true);
            teleportSpell.transform.position = gameObject.transform.position;
            EventBroadcaster.onHeroDeath();
            gameObject.SetActive(false);
        

        }
        
    }

    public void PlayerMana(float amount)
    {

        currentMana = Mathf.Clamp(currentMana + amount, 0, maxMana);
        gameManager.GetComponent<UIHealthBar>().SetValueMana(maxManaBar / (currentMana / maxMana));

    }

    public void PlayerCoins(int amount)
    {

        currentCoins = Mathf.Clamp(currentCoins + amount, 0, maxCoins);
        coinsText.text = currentCoins.ToString();

    }

    public void addSpell(GameObject addedSpell, Sprite addedSpellIcon)
    {

        var breatheSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/breatheicon.png");
        var brightSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/brighticon.png");
        var flamesSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/flamesicon.png");
        var smiteSpellIcon = Resources.Load<Sprite>("Assets/Prefabs/Spells/SpellIcons/smiteicon.png");

        listOfSpells.Capacity = listOfSpells.Capacity + 1;

        listOfSpells.Add(addedSpell);
        listOfSpellIcons.Add(addedSpellIcon);

    }


}


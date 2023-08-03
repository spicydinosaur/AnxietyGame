using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Events;


public class Player : MonoBehaviour
{
    //This script resides on the Hero and handles any context interactions/movement/and basic HUD changes
    //The game uses the 2019 Input System for controls.
    //There should never be more than one of this script active at any time.

    public static Player instance { get; private set; }
    
    public UnityEvent spellCast;
    public UnityEvent interact;

    //Health is actually Mental Health, and Mana is actually Coping.
    //Mental Health is the ability to exist within the world,
    //while Coping is the ability to interact, affect, and change the world
    public float maxHealth;
    public float maxMana;

    public float currentHealth;
    public float currentMana;

    //the health and mana bars max out at a spot that does not fully fill the bar,
    //because the Hero is never at 100% Mental Health or 100% Coping.
    public float maxHealthBar;
    public float maxManaBar;

    public int currentCoins;
    public int maxCoins = 999;
    public TextMeshProUGUI coinsText;

    //this is used in a few other scripts so don't remove it just because it doesn't get called!
    public bool transitioningToScene = false;

    public GameManager gameManager;

    public GameObject teleportSpell;

    public float playerSpeed;

    private Animator animator;

    public Rigidbody2D rigidBody;

    //temporary value until saving is a thing.
    public Vector2 lookDirection = new Vector2(0, -1);

    public InputAction move;
    public PlayerControls playerControls { get; private set; }
    public ObjectInteraction objectInteraction;

    //The scrollwheel is used to flip through the spells the Hero currently possesses.
    //The change can be seen on the HUD where the circle shaped image is at the bottom right.
    public float spellSelectMouseScrollWheel;
    
    public float movement;
    public Vector2 moveInput;

    public AudioSource audioSource;
    public AudioSource failSpell;

    //Spells
    public GameObject currentSpell;
    public SpellTemplate currentSpellScript;
    public SpellTimer spellTimer;

    //These are for when the spell gets added to the players "spellbook" but increasing selectedSpellMax may just be a better way to handle it.
    //This is probably unecessary code.
    //public GameObject addedSpell;
    //public Sprite addedSpellIcon;

    public Image spellIconImage;
    public Sprite currentSpellIcon;

    public int selectedSpell;
    //below starts at 0 and coincides with the number of spells the Hero currently has available, which may not be the entire list.
    public int selectedSpellMax;

    //these are temporararily defined here until the player can save progress
    public List<GameObject> listOfSpells = new List<GameObject>(4);
    public List<Sprite> listOfSpellIcons = new List<Sprite>(4);

    public GameObject breatheSpell;

    //spells and interactions are double casting, (leading to the failSpell beep on every cast.) The below should stop that from happening.
    //disabling this with the intention of testing if this is still a problem.
    public int waitForSpellCheck;
    //public SpellTimer spellTimer;

    public Vector3 mousePosition;

    public Camera mainCam;




    public void Awake()
    {

        playerControls = new PlayerControls();
        //below is temporary until things start being stored in saved games.
        currentCoins = 0;

    }

    public void OnEnable()
    {

        playerControls.Enable();
        playerControls.PlayerActions.Enable();
        playerControls.UIControl.Disable();


    }

    public void OnDisable()
    {


        playerControls.PlayerActions.SpellCast.performed -= OnSpellCast;
        playerControls.PlayerActions.Interact.performed -= OnInteract;
        playerControls.Disable();


    }


    public void SwitchMapPlayerActions()
    {
        playerControls.UIControl.Disable();
        playerControls.PlayerActions.Enable();

    }

    public void SwitchMapUIInput()
    {
        playerControls.PlayerActions.Disable();
        playerControls.UIControl.Enable();

    }

    public void Start()
    {
        instance = this;

        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();

        move = playerControls.Movement.Movement;
        playerControls.PlayerActions.SpellSelectMouseScrollWheel.performed += ctx => spellSelectMouseScrollWheel = ctx.ReadValue<float>();
        playerControls.PlayerActions.SpellCast.performed += OnSpellCast;
        playerControls.PlayerActions.Interact.performed += OnInteract;
        //move.performed += ctx3 => movement = ctx3.ReadValue<float>();

        currentHealth = maxHealth;
        currentMana = maxMana;
        gameManager.GetComponent<UIHealthBar>().SetValueHealth(maxHealthBar / (currentHealth / maxHealth));
        gameManager.GetComponent<UIHealthBar>().SetValueMana(maxManaBar / (currentMana / maxMana));

        selectedSpell = 0;
        currentSpell = breatheSpell;
        currentSpellScript = currentSpell.GetComponent<SpellTemplate>();
        currentSpellIcon = listOfSpellIcons[selectedSpell];

        spellTimer.currentSpellScript = currentSpellScript;
        spellTimer.castDownTime = currentSpellScript.castDownTime;

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


        //below is the code for the scroll wheel to cycle through spells
        if (spellSelectMouseScrollWheel != 0)
        {
            if (spellSelectMouseScrollWheel > 0) //scroll wheel gets moved up, moving through the spell list in a positive direction.
            {


                if (selectedSpell >= selectedSpellMax)
                {
                    //0 is Breathe
                    selectedSpell = 0;

                }
                else
                {
                    selectedSpell++;
                }


                //Debug.Log("mouse scroll wheel up. Spellholder.selectedSpell = " + selectedSpell);


            }
            else if (spellSelectMouseScrollWheel < 0) //scroll wheel gets moved down, moving through the spell list in a negative direction.
            {


                if (selectedSpell == 0)
                {
                    selectedSpell = selectedSpellMax;

                }
                else if (selectedSpell > 0)
                {
                    selectedSpell--;
                }



            }

            currentSpell = listOfSpells[selectedSpell];
            currentSpellIcon = listOfSpellIcons[selectedSpell];
            currentSpellScript = currentSpell.GetComponent<SpellTemplate>();
            spellIconImage.sprite = currentSpellIcon;
            spellSelectMouseScrollWheel = 0f;

            spellTimer.currentSpellScript = currentSpellScript;
            spellTimer.castDownTime = currentSpellScript.castDownTime;

        }

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //Debug.Log("OnInteract fired from Player script.");

        if (objectInteraction != null)
        {

            //ObjectInteraction is done with the keyboard, currently the enter key, that allows for things such as pushing pillars along a track and other future interactions.
            //Debug.Log("PillarObjectInteraction is set to " + PillarObjectInteraction);
            objectInteraction.InteractWithObject();
            //Debug.Log("callback context is " + ctx);

        }


    }
    public void OnSpellCast(InputAction.CallbackContext context)
    {
            //Below stops the fact that the spell gets double cast, actually casting the spell then makinf the spell failure sound a split second later.
            /*if (waitForSpellCheck < 1)
            {
                waitForSpellCheck++;
                return;
            }
            else
            {
                waitForSpellCheck = 0;
            }*/

            if (selectedSpell == 0) //again that's Breathe, which has special rules.
            {

                if (currentSpellScript.currentCastDownTime > 0f)
                {
                //Debug.Log("mouse click registered for spell casting. Spell on cooldown, attempt failed.");
                failSpell.Play(0);
                }
                else 
                {
                    //This spell has no range and requires no values other than being at 0 on currentCastDownTime.
                    //Debug.Log("mouse click registered for spell casting. Spell available to cast, no cooldown in place. " + currentSpellScript.name + " is the current instantiated template.");
                    currentSpellScript.castSpell();
                    spellTimer.globalCastDownTime = currentSpellScript.globalCastDownTime;

                }
            }
            else
            {
                if (currentSpellScript.currentCastDownTime > 0f || spellTimer.globalCastDownTime > 0f || currentSpellScript.manaCost > currentMana)
                {
                    //Debug.Log("mouse click registered for spell casting. Spell on cooldown, attempt failed.");
                    failSpell.Play(0);

                }
                else 
                {
                    //Debug.Log("pre camera conversion gives us playerControls.PlayerActions.MousePosition.ReadValue<Vector2>() at " + Mouse.current.position.ReadValue());
                    mousePosition = mainCam.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0f));
                    //Debug.Log("Mouse click registered for spell casting. mousePosition registered at: " + mousePosition);
                    currentSpellScript.mousePos = new Vector2(mousePosition.x, mousePosition.y);
                    //Debug.Log("Spell available to cast, no cooldown in place. mousePos registered at: " + currentSpellScript.mousePos);
                    //Debug.Log("mouse click registered for spell casting. Spell available to cast, no cooldown in place. " + currentSpellScript + " is the current instantiated template. The currentSpellInstantiate is " + currentSpell);
                    currentSpellScript.castSpell();
                    spellTimer.globalCastDownTime = currentSpellScript.globalCastDownTime;
                }

            }

    }

    public void PlayerHealth(float amount)
    {

        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
        //wait should the below equation be reversed? I'm not sure.
        gameManager.GetComponent<UIHealthBar>().SetValueHealth(maxHealthBar / (currentHealth / maxHealth));


        if (currentHealth <= 0f)
        {

            teleportSpell.SetActive(true);

            //Let's permanently move the teleport spell gameobject over the Hero's spriterenderer. Then we can delete the below commented out scripting.
            //We also need a script for the teleportSpell with a function that moves the hero to the appropriate bed and disables the spell.
            //Put the function at the end of the teleportSpell animation.
            //teleportSpell.transform.position = gameObject.transform.position;
            EventBroadcaster.onHeroDeath();

        }

    }


    public void PlayerMana(float amount)
    {

        currentMana = Mathf.Clamp(currentMana + amount, 0, maxMana);
        gameManager.GetComponent<UIHealthBar>().SetValueMana(maxManaBar / (currentMana / maxMana));
        //if mana reaches 0, interactions with anything other than coping mechanisms and beds should be shut down.

    }

    public void PlayerCoins(int amount)
    {

        currentCoins = Mathf.Clamp(currentCoins + amount, 0, maxCoins);
        coinsText.text = currentCoins.ToString();

    }

    //we might need the following, but if all the spells are in the right order in their list, we should just be able to bump up the selectedSpellMax value by 1.
    /*public void addSpell(GameObject addedSpell, Sprite addedSpellIcon)
    {

        listOfSpells.Capacity = listOfSpells.Capacity + 1;

        listOfSpells.Add(addedSpell);
        listOfSpellIcons.Add(addedSpellIcon);

    }*/

}


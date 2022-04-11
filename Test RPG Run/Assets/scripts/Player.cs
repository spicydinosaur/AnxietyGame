using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;


public class Player : MonoBehaviour
{
    public static Player instance { get; private set; }
    private PlayerControls playerControls;

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

    public GameObject tombstone;

    public InputAction interact;

    


    private void Awake()
    {
        instance = this;
        playerControls = new PlayerControls();
        interact = playerControls.PlayerActions.Interact;
    }


    void Start()
    {

        currentHealth = maxHealth;
        currentMana = maxMana;
        UIHealthBar.instance.SetValueHealth(maxHealthBar / (currentHealth / maxHealth));
        UIHealthBar.instance.SetValueMana(maxManaBar / (currentMana / maxMana));

    }



    public void PlayerHealth(float amount)
    {

        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
        UIHealthBar.instance.SetValueHealth(maxHealthBar / (currentHealth / maxHealth));


        if (currentHealth <= 0)
        {

            tombstone.SetActive(true);
            tombstone.transform.position = GetComponent<Transform>().position;
            EventBroadcaster.onHeroDeath();
            gameObject.SetActive(false);
        

        }
        
    }

    public void PlayerMana(float amount)
    {

        currentMana = Mathf.Clamp(currentMana + amount, 0, maxMana);
        UIHealthBar.instance.SetValueMana(maxManaBar / (currentMana / maxMana));

    }

    public void PlayerCoins(int amount)
    {

        currentCoins = Mathf.Clamp(currentCoins + amount, 0, maxCoins);
        coinsText.text = currentCoins.ToString();

    }

    public void Interact(InputAction.CallbackContext context)
    {

    }

    private void OnEnable()
    {

        playerControls.Enable();

    }

    private void OnDisable()
    {

        playerControls.Disable();

    }



}


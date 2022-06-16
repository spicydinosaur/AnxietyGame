using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

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

    public GameManager gameManager;

    public GameObject teleportSpell;


    


    private void Awake()
    {
        instance = this;
        playerControls = gameManager.playerControls;

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

            teleportSpell.SetActive(true);
            teleportSpell.transform.position = gameObject.transform.position;
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







}


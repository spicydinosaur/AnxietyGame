using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player instance { get; private set; }

    public float speed = 3.0f;
    public float maxHealth;
    public float maxMana;


    //public float health { get { return currentHealth; } }
    public float currentHealth;
    //public Image healthBar;
    //public Sprite[] spriteArray;

    //public float mana { get { return currentMana; } }
    public float currentMana;

    public int currentCoins = 0;
    public int maxCoins = 999;
    public TextMeshProUGUI coinsText;

    public bool transitioningToScene = false;

    public GameObject tombstone;


    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Vector2 move;

    Animator animator;
    Vector2 lookDirection = new Vector2(0, -1);


    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        currentMana = maxMana;

    }

    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");


        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);




    }

    void FixedUpdate()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        move = new Vector2(horizontal, vertical);

        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }



    public void PlayerHealth(float amount)
    {

        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
        //healthBar.sprite = spriteArray[currentHealth];
        UIHealthBar.instance.SetValueHealth(currentHealth / maxHealth);

        Debug.Log(currentHealth + "/" + maxHealth);

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
        //healthBar.sprite = spriteArray[currentHealth];
        UIHealthBar.instance.SetValueMana(currentMana / maxMana);

        //Debug.Log(currentMana + "/" + maxMana);

    }

    public void PlayerCoins(int amount)
    {

        currentCoins = Mathf.Clamp(currentCoins + amount, 0, maxCoins);
        //Debug.Log(currentHealth + "/" + maxHealth);
        //healthBar.sprite = spriteArray[currentHealth];
        coinsText.text = currentCoins.ToString();

    }

}


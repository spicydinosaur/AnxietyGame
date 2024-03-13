using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public Image healthBarImage;
    public GameObject healthBar;

    public float damageBlinkTime;
    public float maxDamageBlinkTime;
    private SpriteRenderer spriteRenderer;
    public Color NPCColor;


    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetActive(true);
        spriteRenderer = GetComponent<SpriteRenderer>();
        NPCColor = spriteRenderer.color;
        if (damageBlinkTime == 0)
        {
            damageBlinkTime = .5f;
        }

    }

    private void Update()
    {

        if (damageBlinkTime <= 0)
        {
            spriteRenderer.color = NPCColor;
        }
        else
        {
            damageBlinkTime -= Time.deltaTime;
        }
    }

    public void ChangeNPCHealth(float amount)
    {
        if (currentHealth > 0)
        {

            //amount must be a negative or it will heal the enemy!
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            healthBarImage.fillAmount = Mathf.Clamp(currentHealth/maxHealth, 0, 1f);

            if (damageBlinkTime <= 0)
            {
                damageBlinkTime = maxDamageBlinkTime;
                spriteRenderer.color = Color.red;
            }

            if(currentHealth <= 0)
            {
                healthBar.SetActive(false);
                GetComponent<EnemyController>().OnDeath();

            }

        }
 
    }
   
}

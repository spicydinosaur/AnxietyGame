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
    public Color NPCColor;


    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetActive(true);
        NPCColor =  GetComponent<SpriteRenderer>().color;

    }

    private void Update()
    {

        if (damageBlinkTime <= 0)
        {
            GetComponent<SpriteRenderer>().color = NPCColor;
        }
        else
        {
            damageBlinkTime -= Time.deltaTime;
        }
    }

    public void damageNPCHealth(float dmgAmount)
    {
        if (currentHealth > 0)
        {

            currentHealth = Mathf.Clamp(currentHealth - dmgAmount, 0, maxHealth);
            healthBarImage.fillAmount = Mathf.Clamp(currentHealth/maxHealth, 0, 1f);

            //Debug.Log("hit enemy. current health: " + currentHealth);
            //Debug.Log("hit enemy. damage amount: " + dmgAmount);

            if (damageBlinkTime <= 0)
            {
                damageBlinkTime = maxDamageBlinkTime;
                GetComponent<SpriteRenderer>().color = Color.red;
            }

            if(currentHealth <= 0)
            {

                GetComponent<EnemyController>().OnDeath();

            }

        }
 
    }
   
}

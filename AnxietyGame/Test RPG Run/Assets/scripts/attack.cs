using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{

    public EnemyController controller;
    public Player player;
    public Animator animator;
    public float maxTimeInvincible;
    public bool isInvincible;
    public float invincibilityCountdownTimer;
    public float healthDamage;

    
    
    public void Awake()
    {

        controller = GetComponentInParent<EnemyController>();
        animator = GetComponentInParent<EnemyController>().animator;


    }

   public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {

            player = collider.gameObject.GetComponent<Player>();
            invincibilityCountdownTimer = 0f;
            controller.Attack(collider);

        }
    }
    


    public void OnTriggerStay2D(Collider2D collider)
    {
    

        
        if (collider.gameObject.activeSelf && collider.gameObject.CompareTag("Player"))
        {

            if (isInvincible)
            {
                controller.animator.SetBool("isAttacking", false);
                invincibilityCountdownTimer -= Time.deltaTime;
                if (invincibilityCountdownTimer <= 0f)
                {
                    isInvincible = false;
                    invincibilityCountdownTimer = maxTimeInvincible;
                    controller.Attack(collider);
                    player.PlayerHealth(-healthDamage);
                    isInvincible = true;
                }
            
                Debug.Log("Hero active?" + collider.gameObject.activeSelf);
            }
            else
            {
                if (!collider.gameObject.activeSelf) //will it even register the hero if they are disabled?

                {
                    controller.animator.SetBool("isAttacking", false);

                    Debug.Log("Hero active?" + collider.gameObject.activeSelf);
                    return;

                }



                //Debug.Log("Hero active?" + collider.gameObject.activeSelf);
            }

        }

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {

            controller.animator.SetBool("isAttacking", false);
            invincibilityCountdownTimer = 0f;
            Debug.Log("Hero active?" + collider.gameObject.activeSelf);
            if (collider.gameObject.activeSelf)
            {


                controller.currentMovementType = NPCController.movementType.ToOther;

            }
            else
            {

                controller.RevertState();

            }
        }
    }

}



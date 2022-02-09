using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{

    public EnemyController controller;
    public Player player;
    private Animator animator;
    public float maxTimeInvincible;
    public bool isInvincible;
    public float invincibilityCountdownTimer;
    public GameObject tombstone;
    
    
    public void Awake()
    {

        controller = GetComponentInParent<EnemyController>();
        animator = GetComponentInParent<EnemyController>().animator;


    }

   public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            controller.Attack(collider);
            player = collider.gameObject.GetComponent<Player>();
            

        }
    }
    


    public void OnTriggerStay2D(Collider2D collider)
    {
    
        Debug.Log("player?" + player + "No answer? hero disabled!");

        
        if (tombstone.activeSelf == false && collider.gameObject.CompareTag("Player"))
        {

            if (isInvincible)
            {
                controller.animator.SetBool("isAttacking", false);
                invincibilityCountdownTimer -= Time.deltaTime;
                if (invincibilityCountdownTimer <= 0f)
                {
                    isInvincible = false;
                }
            
                Debug.Log("Hero active?" + collider.gameObject.activeSelf);
            }
            else if (!isInvincible)
            {
                if (tombstone.activeSelf)

                {
                    controller.animator.SetBool("isAttacking", false);

                    Debug.Log("Hero active?" + collider.gameObject.activeSelf);
                    return;

                }

                invincibilityCountdownTimer = maxTimeInvincible;
                controller.animator.SetBool("isAttacking", true);
                player.PlayerHealth(-1);
                isInvincible = true;

                //Debug.Log("Hero active?" + collider.gameObject.activeSelf);
            }

        }

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            controller.currentMovementType = NPCController.movementType.ToOther;

        }
    }

}



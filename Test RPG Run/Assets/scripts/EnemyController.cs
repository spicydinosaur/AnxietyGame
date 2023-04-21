using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting.Generated.PropertyProviders;

public class EnemyController : NPCController
{

    //why are the cave bats refusing to face the correct way!?


    public DropTable dropTable;

   /* public float maxTimeInvincible;
    public bool isInvincible;
    public float invincibilityCountdownTimer;
   */
    public MoveViewCone moveViewCone;

    public float numberOfBlinks;
    public float currentBlink;

    public float textDisplayTime;
    public float currentTextDisplayTime;
    public TextMeshProUGUI textMeshProUGUI;
    public float secondsPerHalfBlink;







    //onenable should have stuff set up to let the slime know where the player is and head that way.
    override public void OnEnable()
    {
        base.OnEnable();
        dropTable = GetComponent<DropTable>();
        

    }

    // Update is called once per frame

    public override void OnDeath()
    {


        animator.SetBool("isAttacking", false);
        animator.SetBool("isDead", true);
        dropTable.rollOnTable();
        animator.SetFloat("Speed", 0f);
        isMoving = false;
        EventBroadcaster.HeroDeath.RemoveListener(HeroDied);
        currentTextDisplayTime = 0f;
        textMeshProUGUI.enabled= false;


    }


    public void DeathCloudDeactivate()
    {

        GetComponent<Animator>().SetBool("isDead", false);
        gameObject.SetActive(false);

    }

    /*
    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("NPC"))
        {
            //target = collider.gameObject;
            //invincibilityCountdownTimer = 0f;
            //Attack(target);

        }
    }

     

    public void OnCollisionStay2D(Collision2D collider)
    {



        if (collider.gameObject.activeSelf && collider.gameObject.CompareTag("Player"))
        {

            if (isInvincible)
            {
                animator.SetBool("isAttacking", false);
                invincibilityCountdownTimer -= Time.deltaTime;

                if (invincibilityCountdownTimer <= 0f)
                {
                    invincibilityCountdownTimer = maxTimeInvincible;
                    hero.GetComponent<Player>().PlayerHealth(-healthDamage);
                    isInvincible = false;
                }

                Debug.Log("Hero active?" + collider.gameObject.activeSelf);
            }
            else
            {
                if (!collider.gameObject.activeSelf) //will it even register the hero if they are disabled?

                {
                    animator.SetBool("isAttacking", false);

                    Debug.Log("Hero active?" + collider.gameObject.activeSelf);
                    return;

                }



                //Debug.Log("Hero active?" + collider.gameObject.activeSelf);
            }

        }

    }
   
    //make this an event: HeroDied
    public void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {

            animator.SetBool("isAttacking", false);
            invincibilityCountdownTimer = 0f;
            Debug.Log("Hero active?" + collider.gameObject.activeSelf);
            advancedPatrolScript.StopInterrupt();                 


        }
    }
    */
}

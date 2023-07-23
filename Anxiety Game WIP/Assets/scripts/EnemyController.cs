using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting.Generated.PropertyProviders;

public class EnemyController : NPCController
{

    public DropTable dropTable;

    public float numberOfBlinks;
    public float currentBlink;

    public float textDisplayTime;
    public float currentTextDisplayTime;
    public TextMeshProUGUI textMeshProUGUI;
    public float secondsPerHalfBlink;

    public float maxAggroDistance;




    override public void OnEnable()
    {
        base.OnEnable();
        dropTable = GetComponent<DropTable>();
        animator.SetBool("isDead", false);
        animator.SetBool("isAttacking", false);
        animator.SetFloat("Speed", 0f);
        if (maxAggroDistance <= 0f )
        {
            maxAggroDistance = 15f;
        }       

    }

    // Update is called once per frame

    public override void OnDeath()
    {


        animator.SetBool("isAttacking", false);
        animator.SetBool("isDead", true);
        animator.SetFloat("Speed", 0f);
        EventBroadcaster.HeroDeath.RemoveListener(HeroDied);
        currentTextDisplayTime = 0f;
        textMeshProUGUI.enabled= false;

        //The below line will have use later when things actually drop loot.
        //dropTable.rollOnTable();

    }


    public void DeathCloudDeactivate()
    {

        gameObject.SetActive(false);

    }


   
    //make this an event: HeroDied
    public void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (Vector2.Distance(transform.position, collider.transform.position) > maxAggroDistance)
            {

                animator.SetBool("isAttacking", false);
                Debug.Log("Hero active? " + collider.gameObject.activeSelf);
                advancedPatrolScript.StopInterrupt();

            }
        }
    }

}

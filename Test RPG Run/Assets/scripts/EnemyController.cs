using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyController : NPCController
{

    //why are the cave bats refusing to face the correct way!?


    public float numberOfBlinks;
    public float currentBlink;

    public float textDisplayTime;
    public float currentTextDisplayTime;


    public DropTable dropTable;

    public TextMeshProUGUI textMeshProUGUI;
    public float secondsPerHalfBlink;

    public NPCPatrolRoute route;



    //onenable should have stuff set up to let the slime know where the player is and head that way.
    override public void OnEnable()
    {
        base.OnEnable();
        dropTable = GetComponent<DropTable>();
        textMeshProUGUI.enabled = false;
        currentTextDisplayTime = 0f;
        numberOfBlinks = 4;
        currentBlink = 0;
    }

    // Update is called once per frame
    override public void FixedUpdate()
    {
        if (textDisplayTime != 0f)
        {

            currentTextDisplayTime += Time.deltaTime;
            /*if (textMeshProUGUI == false
                &&
                currentTextDisplayTime <= (currentBlink + .5f) * textDisplayTime / numberOfBlinks
                &&
                currentTextDisplayTime >= (currentBlink) * textDisplayTime / numberOfBlinks)
            {
                textMeshProUGUI.enabled = true;
            }
            else if (textMeshProUGUI.enabled == true
                &&
                currentTextDisplayTime >= (currentBlink + 0.5f) * textDisplayTime / numberOfBlinks
                &&
                currentTextDisplayTime <= (currentBlink + 1f) * textDisplayTime / numberOfBlinks)
            {
                textMeshProUGUI.enabled = false;
                currentBlink++;
            }*/

            if (currentTextDisplayTime >= textDisplayTime)
            {
                textDisplayTime = 0f;
                currentTextDisplayTime = 0f;
                currentBlink = 0;
                textMeshProUGUI.enabled = false;
                textMeshProUGUI.SetText("");
            }

        }

        base.FixedUpdate();

    }


    public override void OnDeath()
    {


        animator.SetBool("isAttacking", false);
        animator.SetBool("isDead", true);
        dropTable.rollOnTable();
        animator.SetFloat("Speed", 1f);
        isMoving = false;
        currentMovementType = movementType.Death;
        EventBroadcaster.HeroDeath.RemoveListener(HeroDied);


    }


    public void DeathCloudDeactivate()
    {

        GetComponent<Animator>().SetBool("isDead", false);
        GetComponent<Animator>().SetBool("isAttacking", false);
        gameObject.SetActive(false);

    }


    public void ChangeTargetViewState(bool viewState)
    {
        canSeeTarget = viewState;

        if (!isChasing)
        {
            currentBlink = 0;

            if (canSeeTarget)
            {
                PauseMovement(.8f);
                textMeshProUGUI.enabled = true;
                textMeshProUGUI.SetText("!");

            }
            else
            {
                PauseMovement(3f);
                textMeshProUGUI.enabled = true;
                textMeshProUGUI.SetText("?");
            }
        }


    }

    public void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {

            StartMove();
            animator.SetBool("isAttacking", false);

        }
    }

    public void Attack(Collider2D collider)
    {
        isMoving = false;
        animator.SetFloat("Speed", 3f);
        prevMovementType = currentMovementType;
        animator.SetBool("isAttacking", true);
        currentMovementType = movementType.Attacking;
        targetToChase = collider.gameObject;
    }

    override public void PauseMovement(float paramPauseTime)
    {

        base.PauseMovement(paramPauseTime);
        textDisplayTime = paramPauseTime;
        secondsPerHalfBlink = paramPauseTime/(numberOfBlinks*2);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : NPCController
{

    public int numberOfBlinks = 4;
    public int currentBlink = 0;

    public float textDisplayTime;
    public float currentTextDisplayTime = 0f;

    public Animator deathCloud;
    public GameObject objectDeathCloud;
    public float heartPickupChance;
    public DropTable dropTable;

    override public void Start()
    {
        base.Start();
        deathCloud = deathCloud.GetComponent<Animator>();
        dropTable = GetComponent<DropTable>();

    }

    // Update is called once per frame
    override public void FixedUpdate()
    {
        if (textDisplayTime != 0f)
        {

            currentTextDisplayTime += Time.deltaTime;
            if (dialogBox.activeSelf == false
                &&
                currentTextDisplayTime <= (currentBlink + .5f) * textDisplayTime / numberOfBlinks
                &&
                currentTextDisplayTime >= (currentBlink) * textDisplayTime / numberOfBlinks)
            {
                dialogBox.SetActive(true);
            }
            else if (dialogBox.activeSelf == true
                &&
                currentTextDisplayTime >= (currentBlink + 0.5f) * textDisplayTime / numberOfBlinks
                &&
                currentTextDisplayTime <= (currentBlink + 1f) * textDisplayTime / numberOfBlinks)
            {
                dialogBox.SetActive(false);
                currentBlink++;
            }

            if (currentTextDisplayTime >= textDisplayTime)
            {
                textDisplayTime = 0f;
                currentTextDisplayTime = 0f;
                currentBlink = 0;
                dialogBox.SetActive(false);
                dialogBox.GetComponent<TextMeshProUGUI>().SetText("");
            }

        }

        base.FixedUpdate();

    }

    public override void HeroDied()
    {

        Debug.Log("HeroDied triggered for: " + name);
        base.HeroDied();
        animator.SetBool("isAttacking", false);
        targetToChase = null;

    }

    public override void OnDeath()
    {


        animator.SetBool("isAttacking", false);
        objectDeathCloud.transform.position = GetComponent<Transform>().position;
        deathCloud.SetBool("isDead", true);
        dropTable.rollOnTable();
        base.OnDeath();


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
                dialogBox.SetActive(true);
                dialogBox.GetComponent<TextMeshProUGUI>().SetText("!");

            }
            else
            {
                PauseMovement(3f);
                dialogBox.SetActive(true);
                dialogBox.GetComponent<TextMeshProUGUI>().SetText("?");
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
        animator.SetFloat("Speed", 0f);
        prevMovementType = currentMovementType;
        animator.SetBool("isAttacking", true);
        currentMovementType = movementType.Attacking;
        targetToChase = collider.GetComponent<Transform>();
    }

    override public void PauseMovement(float paramPauseTime)
    {

        base.PauseMovement(paramPauseTime);
        textDisplayTime = paramPauseTime;


    }
}

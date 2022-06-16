using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class NPCController : MonoBehaviour
{
    public float speed = 1.0f;

    Rigidbody2D rigidbody2d;

    float horizontal;
    float vertical;

    public enum movementType {Stop, Patrol, ToOther, Pause, Attacking, Death};
    public movementType currentMovementType;
    public bool isChasing = false;
    public bool canSeeTarget = false;
    public Transform targetToChase;
    public Image dialogBox;
    public Animator animator;
    public Vector2 lookDirection = new Vector2(0, -1);

    public bool isMoving = false;
    public float pauseTime;
    public float delayTime = 0f;
    public movementType prevMovementType;
    public movementType startingMovementType;


    public Vector2 nextMove;
    Vector2 velocity;

    public GameManager gameManager;


    // Start is called before the first frame update
    public virtual void OnEnable()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentMovementType = startingMovementType;
        EventBroadcaster.HeroDeath.AddListener(HeroDied);

        if (currentMovementType != movementType.Stop)
        { SetNextLocation(GetComponent<NPCPatrolRoute>().GetNextCoord()); }

    }

    public virtual void OnDisable()
    {
        EventBroadcaster.HeroDeath.RemoveListener(HeroDied);
    }

    public virtual void FixedUpdate()
    {

 

        if (isMoving)
        {

            Vector2 position = rigidbody2d.position;
            horizontal = nextMove.x - position.x;
            vertical = nextMove.y - position.y;

            velocity = new Vector2(horizontal, vertical);

            velocity.Normalize();
            position.x = position.x + speed * velocity.x * Time.deltaTime;
            position.y = position.y + speed * velocity.y * Time.deltaTime;

            rigidbody2d.MovePosition(position);

            if (currentMovementType == movementType.Patrol)
            {

                if (Mathf.Abs(rigidbody2d.position.x - nextMove.x) < 0.1f && Mathf.Abs(rigidbody2d.position.y - nextMove.y) < 0.1f)
                {
                    StopMove();
                    SetNextLocation(GetComponent<NPCPatrolRoute>().GetNextCoord());
                }

            }

            else if (currentMovementType == movementType.ToOther)
            {

                SetNextLocation(new Vector2(targetToChase.position.x, targetToChase.position.y));

            }

            else if (currentMovementType == movementType.Stop)
            {

            }

        }
        else if (!isMoving)
        {
            if (currentMovementType == movementType.Attacking)

            {
                return;
            }
                
            else if (currentMovementType == movementType.Pause)
            {
                delayTime += Time.deltaTime;
                if (delayTime >= pauseTime)
                {
                    currentMovementType = prevMovementType;
                    delayTime = 0f;
                    isMoving = true;

                }

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)

        {

            Vector2 move = new Vector2(horizontal, vertical);

            if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            {
                lookDirection.Set(move.x, move.y);
                lookDirection.Normalize();
            }

            animator.SetFloat("Look X", lookDirection.x);
            animator.SetFloat("Look Y", lookDirection.y);
            animator.SetFloat("Speed", move.magnitude);

        }


    }


    public virtual void HeroDied()
    {
        currentMovementType = prevMovementType;
        delayTime = 0f;
        
    }

    public virtual void OnDeath()
    {

        isMoving = false;
        currentMovementType = movementType.Death;
        EventBroadcaster.HeroDeath.RemoveListener(HeroDied);
        gameObject.SetActive(false);

    }

    public void ForceNextPatrolRoute()
    {
        //not currently called by anything.
        SetNextLocation(GetComponent<NPCPatrolRoute>().GetNextCoord());
    }


    public virtual void PauseMovement(float paramPauseTime)
    {
        
        isMoving = false;
        pauseTime = paramPauseTime;
        prevMovementType = currentMovementType;
        currentMovementType = movementType.Pause;

    }


    public void ChangeState(movementType changeToState)
    {
        prevMovementType = currentMovementType;
        currentMovementType = changeToState;
        SetMovingState();

    }

    public void SetMovingState()
    {

        if (currentMovementType == movementType.Stop || currentMovementType == movementType.Pause)
        {
            isMoving = false;
            animator.SetFloat("Speed", 0);
        }
        else if (currentMovementType == movementType.Patrol || currentMovementType == movementType.ToOther)
        {
            isMoving = true;
            animator.SetFloat("Speed", 1);
        }
    }

    public void RevertState()
    {
        //Debug.Log("revert state triggered.");
        currentMovementType = prevMovementType;
        prevMovementType = movementType.Stop;
        SetMovingState();
    }

    public void SetNextLocation(Vector2 Next_Move)
    {

        nextMove = Next_Move;
        StartMove();

    }

    public void StartMove()
    {

        isMoving = true;
      
    }

    public void StopMove()
    {

        isMoving = false;
         
    }





}

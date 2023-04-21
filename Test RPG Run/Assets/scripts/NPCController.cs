using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class NPCController : MonoBehaviour
{
    public float speed;

    //public Rigidbody2D rigidbody2d;

    public bool isChasing;
    public bool canSeeTarget;
    //public Image dialogBox;
    public Animator animator;
    public Vector2 lookDirection;

    public bool isMoving;
    //public float delayTime = 0f;

    //public Vector2 nextMove;
    //public Vector2 velocity;

    public GameManager gameManager;

    public GameObject hero;
    public GameObject target;

    public float healthDamage;
    public float manaDamage;

    public AdvancedPatrolScript advancedPatrolScript;

    [SerializeField]
    public MovementOption[] preinterrupt;
    [SerializeField]
    public MovementOption[] interrupt;
    [SerializeField]
    public MovementOption[] postinterrupt;

    [SerializeField]
    public MovementOption[] player_preinterrupt;
    [SerializeField]
    public MovementOption[] player_interrupt;
    [SerializeField]
    public MovementOption[] player_postinterrupt;

    [SerializeField]
    public MovementOption[] attack_preinterrupt;
    [SerializeField]
    public MovementOption[] attack_interrupt;
    [SerializeField]
    public MovementOption[] attack_postinterrupt;



    // Start is called before the first frame update
    public virtual void OnEnable()
    {
        //rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        EventBroadcaster.HeroDeath.AddListener(HeroDied);
        advancedPatrolScript = GetComponent<AdvancedPatrolScript>();
        isChasing = false;
        canSeeTarget= false;
        isMoving=false;
        animator.SetFloat("Speed", 0f);
        advancedPatrolScript.positionInList = 0;
        
    }

    public virtual void OnDisable()
    {
        EventBroadcaster.HeroDeath.RemoveListener(HeroDied);
    }

    public void Start()
    {
        //rigidbody2d = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        //advancedPatrolScript = GetComponent<AdvancedPatrolScript>();
    }

    /*
    public virtual void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject == hero)
        {

            advancedPatrolScript.Interrupt(player_preinterrupt, player_interrupt, player_postinterrupt);

        }
    }

    public virtual void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject == hero)
        {
            advancedPatrolScript.StopInterrupt();

        }
    }

    */

    public virtual void HeroDied()
    {

        //delayTime = 0f;
        //event for herodied
        
    }

    public virtual void OnDeath()
    {
        isMoving = false;
        EventBroadcaster.HeroDeath.RemoveListener(HeroDied);
        gameObject.SetActive(false);

    }


}

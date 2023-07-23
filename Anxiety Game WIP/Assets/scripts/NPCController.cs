using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class NPCController : MonoBehaviour
{
    public float speed;

    public bool canSeeTarget;

    public Animator animator;
    public Vector2 lookDirection;

    public GameManager gameManager;

    public GameObject hero;
    public GameObject target;


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
        animator = GetComponent<Animator>();
        EventBroadcaster.HeroDeath.AddListener(HeroDied);
        advancedPatrolScript = GetComponent<AdvancedPatrolScript>();
        canSeeTarget= false;
        animator.SetFloat("Speed", 0f);
        
    }

    public virtual void OnDisable()
    {
        EventBroadcaster.HeroDeath.RemoveListener(HeroDied);
    }


    public virtual void HeroDied()
    {

        //event for herodied
        
    }

    public virtual void OnDeath()
    {

        EventBroadcaster.HeroDeath.RemoveListener(HeroDied);
        gameObject.SetActive(false);

    }


}

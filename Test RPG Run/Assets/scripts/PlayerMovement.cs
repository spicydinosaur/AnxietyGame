using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    private PlayerControls playerControls;

    [SerializeField]
    private float playerSpeed = 3f;

    private Animator animator;

    public Rigidbody2D rigidBody;

    Vector2 lookDirection = new Vector2(0, -1);

    private InputAction move;


    private void Awake()
    {

        animator = GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        playerControls = new PlayerControls();
        move = playerControls.PlayerActions.Movement;
    }


    private void OnEnable()
    {

        move.Enable();

    }

    private void OnDisable()
    {
        move.Disable();

    }




    private void FixedUpdate()
    {

        Vector2 input = move.ReadValue<Vector2>();
        rigidBody.velocity = new Vector2(input.x * playerSpeed, input.y * playerSpeed);

        if (!Mathf.Approximately(input.x, 0.0f) || !Mathf.Approximately(input.y, 0.0f))
        {
            lookDirection.Set(input.x, input.y);
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", input.magnitude);

    }


    
}

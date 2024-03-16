using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls input = null;
    private Vector2 moveVector = Vector2.zero;
    private bool isMoving = false;
    private float horizontalMovement = 0;
    private bool facingRight = true;

    private Rigidbody2D rb = null;
    public float moveSpeed = 10f;

    public float maxSpeed = 30;

    private float runSpeedMultiplier = 2f;
    private bool isRunning = false;

    public Animator animator;



    private void Awake() {
        input = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCanceled;
        input.Player.Run.started += OnRunStarted;
        input.Player.Run.canceled += OnRunCanceled;
    }

    private void OnDisable() {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCanceled;
        input.Player.Run.started -= OnRunStarted;
        input.Player.Run.canceled -= OnRunCanceled;

    }

    private void OnMovementPerformed(InputAction.CallbackContext value) {
        moveVector = value.ReadValue<Vector2>().normalized;   
        isMoving = true; 
    }

    private void OnMovementCanceled(InputAction.CallbackContext value) {
        moveVector = Vector2.zero;
        isMoving = false;
    }

        private void OnRunStarted(InputAction.CallbackContext value)
    {
        isRunning = true;
    }

    private void OnRunCanceled(InputAction.CallbackContext value)
    {
        isRunning = false;
    }

    private void FixedUpdate() {
        float currentMoveSpeed = isRunning ? moveSpeed * runSpeedMultiplier : moveSpeed;
        Vector2 force = moveVector * currentMoveSpeed;

        if (rb.velocity.magnitude < maxSpeed) // Vérifie la vitesse maximale
        {
            rb.AddForce(force, ForceMode2D.Force); // Ajoute la force au rigidbody
        }

        horizontalMovement = rb.velocity.x;

        if (horizontalMovement > 0 && !facingRight) {
            Flip();
        } else if (horizontalMovement < 0 && facingRight) {
            Flip();
        }

        
    }

    private void Update() {
        animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(rb.velocity.x), Mathf.Abs(rb.velocity.y)));
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isRunning", isRunning);
    }

    void Flip() {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}

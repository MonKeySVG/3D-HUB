using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;

    float runMultiplier = 3.0f;
    bool isMovementPressed;
    bool isRunPressed;
    float rotationFactorPerFrame = 30.0f;

    void Awake() {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunnig");

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;

        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
        
    }

    void onRun(InputAction.CallbackContext context) {
        isRunPressed = context.ReadValueAsButton();
    }

    void onMovementInput (InputAction.CallbackContext context) {
        currentMovementInput = context.ReadValue<Vector2>();
            currentMovement.x = currentMovementInput.x;
            currentMovement.z = currentMovementInput.y;

            currentRunMovement.x = currentMovementInput.x * runMultiplier;
            currentRunMovement.z = currentMovementInput.y * runMultiplier;

            isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void handleAnimation () {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if (isMovementPressed && !isWalking) {
            animator.SetBool("isWalking", true);
        }

        else if (!isMovementPressed && isWalking) {
            animator.SetBool("isWalking", false);
        }

        if (isMovementPressed && isRunPressed && !isRunning) {
            animator.SetBool(isRunningHash, true);
        } 

        else if ((!isMovementPressed || !isRunPressed) && isRunning) {
            animator.SetBool(isRunningHash, false);
        }
    }

    void handleRotation() {
        Vector3 positionToLookAt = new Vector3(currentMovement.x, 0.0f, currentMovement.z);

        if (isMovementPressed) {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void handleGravity()
    {
        // Apply proper gravity depending on if the character is grounded or not
        if (characterController.isGrounded)
        {
            float groundedGravity = -0.5f;
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity;
            currentRunMovement.y += gravity;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handleGravity();
        handleRotation();
        handleAnimation();

        if (isRunPressed) {
            characterController.Move(currentRunMovement * Time.deltaTime);
        } else {
            characterController.Move(currentMovement * Time.deltaTime);
        }
        
        
        
    }

    void FixedUpdate() {
        
    }

    void OnEnable() {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable() {
        playerInput.CharacterControls.Disable();
    }
}

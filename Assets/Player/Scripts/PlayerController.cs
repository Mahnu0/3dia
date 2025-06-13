using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Entity, ITargeteable
{
    public static PlayerController instance;

    [Header("Movement Settings")]
    [SerializeField] float speedWalk = 5f;
    [SerializeField] float speedRun = 5f;
    [SerializeField] float verticalSpeedOnGrounded = -5f;
    [SerializeField] float jumpVelocity = 5f;

    public enum OrientationMode
    {
        ToMoveDirection,
        ToCameraForward,
        ToTarget,
    };
    [Header("Orientation")]
    [SerializeField] OrientationMode orientationMode = OrientationMode.ToMoveDirection;
    [SerializeField] Transform orientationTarget;
    [SerializeField] float angularSpeed = 720f;


    [Header("Input Actions")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference run;

    CharacterController characterController;
    Camera mainCamera;

    Orientator orientator;

    float speed;

    protected override void Awake()
    {
        base.Awake();

        instance = this;

        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;

        speed = speedWalk;

        orientator = GetComponent<Orientator>();
        orientator.SetAngularSpeed(angularSpeed);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        move.action.Enable();
        jump.action.Enable();
        run.action.Enable();

        move.action.performed += OnMove;
        move.action.started += OnMove;
        move.action.canceled += OnMove;

        jump.action.performed += OnJump;

        run.action.started += OnRun;
        run.action.canceled += OnRun;
    }

    private void Update()
    {
        UpdateMovementOnPlane();
        UpdateVerticalMovement();
        UpdateOrientation();
        UpdateAnimation();
    }

    Vector3 lastNormalizedVelocity = Vector3.zero;
    private void UpdateMovementOnPlane()
    {
        Vector3 movement =
                mainCamera.transform.right * rawMove.x +
                mainCamera.transform.forward * rawMove.z;
        float oldMovementMagnitude = movement.magnitude;

        Vector3 movementProjectedOnPlane =
            Vector3.ProjectOnPlane(movement, Vector3.up);

        movementProjectedOnPlane = movementProjectedOnPlane.normalized * oldMovementMagnitude;

        characterController.Move(movementProjectedOnPlane * speed * Time.deltaTime);
        lastNormalizedVelocity = movementProjectedOnPlane;
    }

    float gravity = -9.8f;
    float verticalVelocity;
    void UpdateVerticalMovement()
    {
        verticalVelocity += gravity * Time.deltaTime;
        characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);
        lastNormalizedVelocity.y = verticalVelocity;

        if (characterController.isGrounded)
            { verticalVelocity = verticalSpeedOnGrounded; }

        if (mustJump)
        {
            mustJump = false;
            if (characterController.isGrounded)
                { verticalVelocity = jumpVelocity; }
        }

    }

    void UpdateOrientation()
    {
        Vector3 desiredDirection = Vector3.forward;
        switch (orientationMode)
        {
            case OrientationMode.ToMoveDirection:
                desiredDirection = lastNormalizedVelocity;
                break;
            case OrientationMode.ToCameraForward:
                desiredDirection = mainCamera.transform.forward;
                break;
            case OrientationMode.ToTarget:
                desiredDirection = orientationTarget.position - transform.position;
                break;
        }
        desiredDirection.y = 0f;

        orientator.OrientateTo(desiredDirection);
    }


    protected override void OnDisable()
    {
        base.OnDisable();

        move.action.Disable();
        jump.action.Disable();
        run.action.Disable();

        move.action.performed -= OnMove;
        move.action.started -= OnMove;
        move.action.canceled -= OnMove;

        jump.action.performed -= OnJump;

        run.action.started -= OnRun;
        run.action.canceled -= OnRun;

    }

    Vector3 rawMove = Vector3.zero;
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 rawInput = context.ReadValue<Vector2>();
        rawMove = new Vector3(rawInput.x, 0f, rawInput.y);
    }

    bool mustJump;
    private void OnJump(InputAction.CallbackContext context)
    {
        mustJump = true;
    }

    void OnRun(InputAction.CallbackContext context)
    {
        speed = run.action.IsPressed() ? speedRun : speedWalk;
    }

    protected override float GetCurrentVerticalSpeed()
    {
        return verticalVelocity;
    }
    protected override float GetJumpSpeed()
    {
        return jumpVelocity;
    }
    protected override bool IsRunning()
    {
        return speed == speedRun;
    }
    protected override bool IsGrounded()
    {
        return characterController.isGrounded;
    }

    protected override Vector3 GetLastNormalizedVelocity()
    {
        return lastNormalizedVelocity;
    }

    ITargeteable.Faction ITargeteable.GetFaction()
    {
        return ITargeteable.Faction.Player;
    }

    Transform ITargeteable.GetTransform()
    {
        return transform;
    }
}

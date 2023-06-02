using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Vector2 minBounds, maxBounds;
    
    private PlayerInputActions playerInputAction; // Input Style 5

    Vector2 movementInputVector;

    [SerializeField] private float paddingTop, paddingBottom, paddingLeft, paddingRight;

    private Shooter shooter;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();

        playerInputAction = new PlayerInputActions(); // Input Style 5
        playerInputAction.Player.Enable();

        playerInputAction.Player.Fire.performed += HandleFireInput;
        playerInputAction.Player.Fire.canceled += HandleFireInput;
    }

    private void OnDestroy()
    {
        playerInputAction.Player.Fire.performed -= HandleFireInput;
        playerInputAction.Player.Fire.canceled -= HandleFireInput;
    }

    private void InitBounds()
    {
        minBounds = Camera.main.ViewportToWorldPoint(Vector2.zero);
        maxBounds = Camera.main.ViewportToWorldPoint(Vector2.one);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitBounds();
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
        Move();
    }

    #region MOVEMENT
    private void GetMovementInput()
    {
        movementInputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector2 newPosition = new Vector2(
            transform.position.x + movementInputVector.x * movementSpeed * Time.deltaTime,
            transform.position.y + movementInputVector.y * movementSpeed * Time.deltaTime);

        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);

        transform.position = newPosition;
    }

    #endregion

    #region COMBAT

    private void HandleFireInput(InputAction.CallbackContext ctx)
    {
        Fire(ctx.ReadValueAsButton());
    }

    private void Fire(bool isFire)
    {
        // TODO: change fire between true / false
        shooter.isShooting = isFire;
    }

    #endregion

}

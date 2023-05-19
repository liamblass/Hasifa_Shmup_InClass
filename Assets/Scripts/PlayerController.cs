using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Vector2 minBounds, maxBounds;
    
    private PlayerInputActions playerInputAction; // Input Style 5

    Vector2 movementInputVector;

    private void Awake()
    {
        playerInputAction = new PlayerInputActions(); // Input Style 5
        playerInputAction.Player.Enable();
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
        Debug.Log(movementInputVector);
    }

    private void Move()
    {
        Vector2 newPosition = new Vector2(
            transform.position.x + movementInputVector.x * movementSpeed * Time.deltaTime,
            transform.position.y + movementInputVector.y * movementSpeed * Time.deltaTime);

        transform.position = newPosition;
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] 
    private int movementSpeed;

    private Rigidbody2D rig;

    private Vector3 movementDirection;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rig.transform.Translate(movementDirection * Time.deltaTime * movementSpeed);
    }

    public void OnMove(InputAction.CallbackContext input_value)
    {
        Vector2 movement = input_value.ReadValue<Vector2>();
        movementDirection = new Vector3(movement.x, movement.y, 0);
        movementDirection.Normalize();
    }


}

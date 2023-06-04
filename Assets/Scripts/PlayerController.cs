using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.NonSerialized] public float movementStrafeForce = 100f;
    [System.NonSerialized] public float movementJumpForce = 1000f;
    public Rigidbody rb;
    public Collider rbCollider;

    void Start()
    {
        
    }

    void Update()
    {
        //Strafing
        Vector3 movementVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movementVector += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVector -= transform.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementVector += transform.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementVector -= transform.right;
        }

        if (movementVector != Vector3.zero) {
            rb.AddForce(movementVector.normalized * movementStrafeForce);
        }

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * movementJumpForce);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, rbCollider.bounds.extents.y + 0.1f);
    }
}

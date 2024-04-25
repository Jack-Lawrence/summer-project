using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variables")]
    public float walkSpeed = 1f;
    public float walkMaxSpeed = 1f;

    public float runSpeed = 2f;
    public float runMaxSpeed = 2f;

    public float acceleration = 50.0f;
    public float deceleration = 100.0f;

    [Header("References")]
    private Rigidbody rb;

    public string currentState = "Idle";

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Lock the constraints for the mean time to track
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(x, 0, z);
        moveDirection.Normalize();

        float speed = moveDirection.magnitude > 0 ? (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed) : 0f;
        float maxSpeed = moveDirection.magnitude > 0 ? (Input.GetKey(KeyCode.LeftShift) ? runMaxSpeed : walkMaxSpeed) : 0f;

        rb.velocity += moveDirection * acceleration * speed * Time.deltaTime;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        if (moveDirection.magnitude > 0)
        {
            currentState = Input.GetKey(KeyCode.LeftShift) ? "Running" : "Walking";
        }
        else
        {
            currentState = "Idle";
            rb.velocity -= rb.velocity * deceleration * Time.deltaTime;
        }
    }
}

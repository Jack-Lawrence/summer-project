using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variables")]
    public float moveSpeed = 5.0f;
    public float friction = 5.0f;

    [Header("References")]
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Lock axis so it can't fall over
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(x, 0, z);
        moveDirection.Normalize();

        moveDirection = Quaternion.Euler(0, 0, 0) * moveDirection;

        rb.velocity = moveDirection * moveSpeed;
    }

    void FixedUpdate()
    {
        rb.velocity -= rb.velocity * friction * Time.fixedDeltaTime;
    }
}

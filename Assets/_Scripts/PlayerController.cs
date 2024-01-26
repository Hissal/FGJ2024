using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    float moveDir;

    [SerializeField] float movementSpeed;
    [SerializeField] float jumpStrength;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveDir = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, jumpStrength, 0),ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(moveDir * movementSpeed, 0, 0));
    }
}

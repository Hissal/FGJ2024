using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadUpForce : MonoBehaviour
{
    [SerializeField] float forceAmount = 180;
    Rigidbody rb;

    [SerializeField] float distToGround;
    [SerializeField] Collider col;
    [SerializeField] LayerMask groundLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (IsGrounded()) rb.AddForce(Vector3.up * forceAmount);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - distToGround, transform.position.z));
    }
}

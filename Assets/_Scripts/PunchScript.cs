using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScript : MonoBehaviour
{
    Rigidbody rb;
    float pForce;

    bool swinging = false;

    [SerializeField] Rigidbody armRb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SwingArm(Vector3 dir, float swingForce, float punchForceMultiplier)
    {
        pForce = punchForceMultiplier;
        StartCoroutine(SwingRoutine());

        IEnumerator SwingRoutine()
        {
            swinging = true;
            //rb.AddForce(dir * swingForce, ForceMode.VelocityChange);
            //rb.AddForce(Vector3.up * swingForce * 0.5f, ForceMode.VelocityChange);
            //armRb.AddForce(dir * swingForce, ForceMode.VelocityChange);

            for (int i = 0; i < 5; i++)
            {
                rb.AddForce(dir * swingForce, ForceMode.Impulse);
                rb.AddForce(Vector3.up * swingForce * 0.5f, ForceMode.Impulse);
                //armRb.AddForce(dir * swingForce, ForceMode.Impulse);
                yield return new WaitForFixedUpdate();
            } 
            yield return new WaitForSeconds(0.1f);
            swinging = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (swinging == false) return;

        IHittable hittable = collision.transform.root.GetComponentInChildren<IHittable>();
        if (hittable == null) return;
        if (hittable == transform.root.GetComponentInChildren<IHittable>()) return;
        
        hittable.Hit(rb.velocity, pForce);
    }
}

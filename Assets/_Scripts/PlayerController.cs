using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IHittable
{
    Rigidbody rb;

    [SerializeField] float movementSpeed;
    [SerializeField] float jumpStrength;
    [SerializeField] float punchForce;
    [SerializeField] float punchStunDuration;

    InputActionAsset inputAsset;
    InputActionMap player;
    InputAction move;

    [SerializeField] PunchScript punchScript;

    float moveDir = 0;

    Coroutine stunRoutine = null;
    bool stunned = false;

    float distToGround;


    private void Awake()
    {
        inputAsset = GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("PlayerControls");
    }

    void Start()
    {
        distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        if (stunned || !IsGrounded()) return;
        rb.AddForce(new Vector3(0, jumpStrength, 0), ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        if (stunned) return;

        Move();
    }

    private void Move()
    {
        moveDir = move.ReadValue<float>();

        if (moveDir < 0) transform.rotation = new Quaternion(0, 180, 0, 1);
        else if (moveDir > 0) transform.rotation = new Quaternion(0, 0, 0, 1);
        else rb.velocity = new Vector3(rb.velocity.x * 0.9f, rb.velocity.y, 0);

        if (Mathf.Abs(rb.velocity.x) >= movementSpeed) rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        else rb.AddForce(new Vector3(moveDir * movementSpeed * 2, 0, 0));
    }

    public void Hit(Vector3 dir, float force)
    {
        rb.velocity = rb.velocity * 0.5f;
        rb.AddForce(force * dir, ForceMode.Impulse);
        Stun(punchStunDuration);
        print("Hit");
    }

    void Punch(InputAction.CallbackContext ctx)
    {
        StartCoroutine(PunchRoutine());

        IEnumerator PunchRoutine()
        {
            punchScript.gameObject.SetActive(true);

            yield return new WaitForFixedUpdate();

            foreach (Transform t in punchScript.GetPunched())
            {
                IHittable hittable = t.GetComponent<IHittable>();
                if (hittable != null)
                {
                    print(hittable);
                    Vector3 dir = (t.position - transform.position).normalized;
                    hittable.Hit(dir, punchForce);
                }
            }

            yield return new WaitForFixedUpdate();

            punchScript.ClearPunched();
            punchScript.gameObject.SetActive(false);
            yield return null;
        }
    }

    public void Stun(float duration)
    {
        if (stunned) StopCoroutine(stunRoutine);
        stunRoutine = StartCoroutine(StunRoutine());

        IEnumerator StunRoutine()
        {
            stunned = true;
            yield return new WaitForSeconds(duration);
            stunned = false;
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();

        player.FindAction("Jump").started += Jump;
        player.FindAction("Punch").started += Punch;
        move = player.FindAction("MoveAxis");
        player.Enable();
    }

    private void OnDisable()
    {
        player.FindAction("Jump").started -= Jump;
        player.FindAction("Punch").started -= Punch;
        player.Disable();
    }
}

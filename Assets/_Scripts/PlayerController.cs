using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IHittable
{
    Rigidbody rb;

    [SerializeField] float movementSpeed = 15f;
    [SerializeField] float jumpStrength = 100f;
    [SerializeField] float punchForce = 5f;
    [SerializeField] float punchForceMultiplier = 15f;
    [SerializeField] float punchStunDuration = 0.15f;
    [SerializeField] float punchCooldown = 0.25f;
    [SerializeField] float rotationSpeed = 0.6f;
    [SerializeField] float maxVelocity = 100;

    InputActionAsset inputAsset;
    InputActionMap player;
    InputAction move;

    float moveDir = 0;

    Coroutine stunRoutine = null;
    bool stunned = false;
    bool canPunch = true;

    bool doubleJump = true;

    [SerializeField] float distToGround;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] List<PunchScript> punchScripts;

    [SerializeField] GameObject hat;

    private void Awake()
    {
        inputAsset = GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("PlayerControls");
    }

    void Start()
    {
        SkinnedMeshRenderer rend = hat.GetComponent<SkinnedMeshRenderer>();
        //rend.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        rend.material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        if (stunned) return;

        if (IsGrounded())
        {
            rb.AddForce(new Vector3(0, jumpStrength, 0), ForceMode.Impulse);
        }
        else if (doubleJump)
        {
            doubleJump = false;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(new Vector3(0, jumpStrength, 0), ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (stunned) 
        {
            rb.velocity = new Vector3(rb.velocity.x * 0.95f, rb.velocity.y, 0);
            return;
        }
        
        Move();
    }

    void Update()
    {
        if (rb.velocity.magnitude >= maxVelocity) rb.velocity = rb.velocity.normalized * maxVelocity;

        if (IsGrounded())
        {
            doubleJump = true;
        }
    }

    private void Move()
    {
        moveDir = move.ReadValue<float>();

        if (moveDir == 0) rb.velocity = new Vector3(rb.velocity.x * 0.9f, rb.velocity.y, 0);
        else if (IsGrounded())
        {
            Vector3 targetDirection = (Vector3.right * moveDir).normalized;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed, 0.0f);
            //transform.rotation = Quaternion.LookRotation(newDirection);

            rb.MoveRotation(Quaternion.LookRotation(newDirection));
        }

        if (Mathf.Abs(rb.velocity.x) >= movementSpeed) rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        else rb.AddForce(new Vector3(moveDir * movementSpeed * 5, 0, 0));
    }

    public void Hit(Vector3 velocityOfHit, float forceMultiplier)
    {
        rb.velocity = rb.velocity * 0.5f;
        rb.AddForce(velocityOfHit * forceMultiplier, ForceMode.Impulse);
        Stun(punchStunDuration);
        print("Hit");
    }

    void Punch(InputAction.CallbackContext ctx)
    {
        if (stunned || !canPunch) return;

        StartCoroutine(PunchCooldown());

        foreach (PunchScript script in punchScripts)
        {
            script.SwingArm(transform.forward, punchForce, punchForceMultiplier);
            rb.AddForce(-transform.forward * punchForce * 2, ForceMode.VelocityChange);
            rb.AddForce(Vector3.up * -punchForce * 2, ForceMode.Impulse);
        }

        IEnumerator PunchCooldown()
        {
            canPunch = false;
            yield return new WaitForSeconds(punchCooldown);
            canPunch = true;
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
        return Physics.Raycast(transform.position, -Vector3.up, distToGround, groundLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - distToGround, transform.position.z));
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

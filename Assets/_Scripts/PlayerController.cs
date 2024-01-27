using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float movementSpeed;
    [SerializeField] float jumpStrength;

    InputActionAsset inputAsset;
    InputActionMap player;
    InputAction move;

    float moveDir = 0;


    private void Awake()
    {
        inputAsset = GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("PlayerControls");
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        rb.AddForce(new Vector3(0, jumpStrength, 0), ForceMode.Impulse);
        print("JUMP" + ctx);
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();

        player.FindAction("Jump").started += Jump;
        move = player.FindAction("MoveAxis");
        player.Enable();
    }

    private void OnDisable()
    {
        player.FindAction("Jump").started -= Jump;
        player.Disable();
    }

    private void FixedUpdate()
    {
        moveDir = move.ReadValue<float>();
        rb.AddForce(new Vector3(moveDir * movementSpeed, 0, 0));
    }
}

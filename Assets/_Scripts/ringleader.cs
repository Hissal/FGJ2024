using System.Collections.Generic;
using UnityEngine;
using System;

public class Ringleader : MonoBehaviour
{
    private Dictionary<string, Action> eventsSpecifics;
    public GameObject hoopPrefab;
    private System.Random random = new System.Random();

    GameManager gameManager;

    private readonly List<string> events = new List<string>
    {
        "ring_of_fire"
    };

    void Start()
    {
        gameManager = GameManager.Instance;

        eventsSpecifics = new Dictionary<string, Action>
        {
            { "ring_of_fire", RingOfFire }
        };
        RandomEvent();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RandomEvent();
        }
    }

    private List<GameObject> hoops;
    Transform ringOfFirePlayer = null;
    public void RingOfFire()
    {
        Debug.Log("Ring of Fire");
        // Get a random player
        ringOfFirePlayer = gameManager.PlayerList[random.Next(0, gameManager.PlayerList.Count)];
        Debug.Log(ringOfFirePlayer);
        print("PlayerAmount" + gameManager.PlayerList.Count);
        float distanceBetweenHoops = 15f; // Distance between each hoop
        Vector3 startPosition = transform.position; // Start position for the first hoop

        // Instantiate the hoops at the start position and 15 units to the right and left
        var hoop1 = Instantiate(hoopPrefab,
         startPosition, Quaternion.identity, transform);
        hoops = new List<GameObject> { hoop1 };

        // Child 1 is the hoop, 0 is chain
        var childScript = hoop1.transform.GetComponent<HoopAnchor>();
        if (childScript != null)
        {
            childScript.StorePlayer(ringOfFirePlayer);
        }
    }   

    private void RandomEvent()
    {
        var eventIndex = random.Next(events.Count);
        eventsSpecifics[events[eventIndex]]();
    }
    public void DetachHoops()
    {
        foreach (GameObject hoop in hoops)
        {
            // Get the Rigidbody component
            Rigidbody rb = hoop.GetComponent<Rigidbody>();

            // If the Rigidbody component exists
            if (rb != null)
            {
                // Apply an upward force
                rb.AddForce(Vector3.up * 10, ForceMode.Impulse);

                // Set isKinematic to false
                rb.isKinematic = false;

                // Disable gravity
                rb.useGravity = false;
            }

            // Create a queue to hold the GameObject and all its children
            Queue<Transform> queue = new Queue<Transform>();
            queue.Enqueue(hoop.transform);

            // While there are still GameObjects in the queue
            while (queue.Count > 0)
            {
                // Dequeue a GameObject
                Transform current = queue.Dequeue();

                // Get the Rigidbody component
                Rigidbody currentRb = current.GetComponent<Rigidbody>();

                // If the Rigidbody component exists
                if (currentRb != null)
                {
                    // Apply an upward force
                    currentRb.AddForce(Vector3.up * 2, ForceMode.Impulse);

                    // Set isKinematic to false
                    currentRb.isKinematic = false;

                    // Disable gravity
                    currentRb.useGravity = false;
                }

                // Enqueue all children of the current GameObject
                foreach (Transform child in current)
                {
                    queue.Enqueue(child);
                }
            }

            // Delete the hoop after 10 seconds
            Destroy(hoop, 10f);
        }
    }
}
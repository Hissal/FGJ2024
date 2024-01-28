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
        // Call RandomEvent immediately and then every 5 seconds
        StartCoroutine(InvokeRandomEvent());
    }

    private IEnumerator<object> InvokeRandomEvent()
    {
        while (true)
        {
            // Call RandomEvent
            RandomEvent();

            // Wait for a random amount of time between 3 and 8 seconds
            yield return new WaitForSeconds(UnityEngine.Random.Range(5f, 8f));
        }
    }

    void Update()
    {

    }

    private List<GameObject> hoops;
    public void RingOfFire()
    {
        if (hoops.Count > 4)
        {
            return;
        }
        Debug.Log("Ring of Fire");
        Vector3 startPosition = transform.position; // Start position for the first hoop
        // Instantiate the hoops at the start position and 15 units to the right and left
         var hoop1 = Instantiate(hoopPrefab,
         new Vector3(0, 10f, -21.2f), Quaternion.Euler(40, 0, 0), transform);
        hoops.Add(hoop1);

        // Child 1 is the hoop, 0 is chain
    }   

    private void RandomEvent()
    {
        var eventIndex = random.Next(events.Count);
        eventsSpecifics[events[eventIndex]]();
    }
    public void DetachHoops()
    {
    // foreach (GameObject hoop in hoops)
    // {
    //     // Get the Rigidbody component
    //     Rigidbody rb = hoop.GetComponent<Rigidbody>();

    //     // If the Rigidbody component exists
    //     if (rb != null)
    //     {
    //         // Enable gravity
    //         rb.useGravity = true;

    //         // Disable kinematic
    //         rb.isKinematic = false;
    //     }

    //     // Create a queue to hold the GameObject and all its children
    //     Queue<Transform> queue = new Queue<Transform>();
    //     queue.Enqueue(hoop.transform);

    //     // While there are still GameObjects in the queue
    //     while (queue.Count > 0)
    //     {
    //         // Dequeue a GameObject
    //         Transform current = queue.Dequeue();

    //         // Get the Rigidbody component
    //         Rigidbody currentRb = current.GetComponent<Rigidbody>();

    //         // If the Rigidbody component exists
    //         if (currentRb != null)
    //         {
    //             // Enable gravity
    //             currentRb.useGravity = true;

    //             // Disable kinematic
    //             currentRb.isKinematic = false;
    //         }

    //         // Enqueue all children of the current GameObject
    //         foreach (Transform child in current)
    //         {
    //             queue.Enqueue(child);
    //         }
    //     }
    // }
}
}
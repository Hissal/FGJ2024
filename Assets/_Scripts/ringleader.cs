using System.Collections.Generic;
using UnityEngine;
using System;

public class Ringleader : MonoBehaviour
{
    private Dictionary<string, Action> eventsSpecifics;
    public GameObject hoopPrefab;
    private System.Random random = new System.Random();
    public Coroutine rings;

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
        StartRings();
    }
    public void StartRings()
    {
        rings = StartCoroutine(InvokeRandomEvent());
    }
    public void StopRings()
    {
        StopCoroutine(rings);
        DetachHoops();
    }
    private IEnumerator<object> InvokeRandomEvent()
    {
        while (true)
        {
            // Call RandomEvent
            RandomEvent();

            // Wait for a random amount of time between 4 and 8 seconds
            yield return new WaitForSeconds(UnityEngine.Random.Range(4f, 8f));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StopRings();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartRings();
        }
    }

    public List<GameObject> hoops;
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
    }   

    private void RandomEvent()
    {
        var eventIndex = random.Next(events.Count);
        eventsSpecifics[events[eventIndex]]();
    }
    public void DetachHoops()
    {
        foreach (var hoop in hoops)
        {
            var hoopAnchor = hoop.GetComponentInParent<HoopAnchor>();
            if (hoopAnchor != null)
            {
                hoopAnchor.DetachHoop();
            }
        }
        hoops.Clear();
    }
}
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ringleader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager eventManager = new EventManager();
        eventManager.RandomEvent();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventManager eventManager = new EventManager();
            eventManager.RandomEvent();
        }
    }
}

public class EventManager
{
    private System.Random random = new System.Random();
    private readonly List<string> events = new List<string>
    {
        "ring_of_fire"
    };

    private readonly Dictionary<string, Action> eventsSpecifics;

    public EventManager()
    {
        eventsSpecifics = new Dictionary<string, Action>
        {
            { "ring_of_fire", () => RingOfFire() }
        };
    }

    public void RandomEvent()
    {
        var randomIndex = random.Next(events.Count);
        var randomEvent = events[randomIndex];
        eventsSpecifics[randomEvent]();
    }
    private void RingOfFire()
    {
        Debug.Log("Ring of Fire");
        List<Transform> PlayerList = GameManager.Instance.PlayerList;
        var playerIndex = random.Next(PlayerList.Count);

    }
}

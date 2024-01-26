using System.Collections.Generic;
using UnityEngine;
using System;

public class ringleader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager eventManager = new EventManager();
        eventManager.random_event();
    }

    void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EventManager eventManager = new EventManager();
                eventManager.random_event();
        }
    }
}
public class EventManager
{
    private List<string> events = new List<string>
    {
        "low_gravity"
    };

    private Dictionary<string, (Action<List<object>>, List<object>)> eventsSpecifics = new Dictionary<string, (Action<List<object>>, List<object>)>
    {
        { "low_gravity", (low_gravity, new List<object> { "parameter1", "parameter2" }) }
    };

    private static void low_gravity(List<object> parameters)
    {
        foreach (var param in parameters)
        {
            Debug.Log($"low_gravity event executed with parameter: {param}");
        }
    }

    public void execute_event(string eventName)
    {
        if (events.Contains(eventName))
        {
            eventsSpecifics[eventName].Item1(eventsSpecifics[eventName].Item2);
        }
    }

    public void random_event()
    {
        var random = new System.Random();
        var randomIndex = 0;
        var randomEvent = events[randomIndex];
        execute_event(randomEvent);
    }
}

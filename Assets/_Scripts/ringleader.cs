using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class ringleader : MonoBehaviour
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
                eventManager.random_event();
        }
    }
}
public class EventManager
{
    private List<string> events = new List<string>
    {
        "LowGravity"
    };

    private Dictionary<string, (Action<List<object>>, List<object>)> eventsSpecifics = new Dictionary<string, (Action<List<object>>, List<object>)>
    {
        { "LowGravity", (LowGravity, new List<object> { "parameter1", "parameter2" }) }
    };

    private static void LowGravity(List<object> parameters)
    {
        foreach (var param in parameters)
        {
            Debug.Log($"LowGravity event executed with parameter: {param}");
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
        eventsSpecifics[randomEvent].Item1(eventsSpecifics[randomEvent].Item2);
    }
}

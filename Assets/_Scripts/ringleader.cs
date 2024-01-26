using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ringleader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager eventManager = new EventManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class EventManager
{
    private List<(string)> events = new List<(string)>
    {
        ("LowGravity")
    };

    private Dictionary<string, List<object>> eventParameters = new Dictionary<string, List<object>>();
    {
        {"LowGravity", new List<object> {"parameters"} }
    }

    private static void LowGravity(param)
    {
        // Do something
    }
    public void ExecuteEvent(string eventName)
    {
        var (name, action) = events.Find(x => x.Item1 == eventName);
        action(eventParameters[eventName]);
    }

    public void RandomEvent()
    {
        var randomEvent = events[UnityEngine.Random.Range(0, events.Count)];
        randomEvent.Item2(eventParameters[randomEvent.Item1]);
    }
}
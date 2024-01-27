using System.Collections.Generic;
using UnityEngine;
using System;

public class Ringleader : MonoBehaviour
{
    private Dictionary<string, Action> eventsSpecifics;
    public GameObject hoopPrefab;
    private System.Random random = new System.Random();
    private readonly List<string> events = new List<string>
    {
        "ring_of_fire"
    };

    void Start()
    {
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
    private void RingOfFire()
    {
        Debug.Log("Ring of Fire");
        // List<Transform> PlayerList = GameManager.Instance.PlayerList;
        // var playerIndex = random.Next(PlayerList.Count);

        // // Instantiate the hoop and anchor at the player's position
        // var playerPosition = PlayerList[playerIndex].position;
        var hoop = Instantiate(hoopPrefab, transform.position, Quaternion.identity);
    }

    private void RandomEvent()
    {
        var eventIndex = random.Next(events.Count);
        eventsSpecifics[events[eventIndex]]();
    }
}
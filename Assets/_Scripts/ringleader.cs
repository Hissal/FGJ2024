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

    }
    private void RingOfFire()
    {
        Debug.Log("Ring of Fire");
        // List<Transform> PlayerList = GameManager.Instance.PlayerList;
        // var playerIndex = random.Next(PlayerList.Count);

        // // Instantiate the hoop and anchor at the player's position
        // var playerPosition = PlayerList[playerIndex].position

        float distanceBetweenHoops = 15f; // Distance between each hoop
        Vector3 startPosition = transform.position; // Start position for the first hoop

        // Instantiate the hoops at the start position and 15 units to the right and left
        Instantiate(hoopPrefab, startPosition, Quaternion.identity);
        Instantiate(hoopPrefab, startPosition + new Vector3(distanceBetweenHoops, 0, 0), Quaternion.identity);
        Instantiate(hoopPrefab, startPosition - new Vector3(distanceBetweenHoops, 0, 0), Quaternion.identity);
    }

    private void RandomEvent()
    {
        var eventIndex = random.Next(events.Count);
        eventsSpecifics[events[eventIndex]]();
    }
}
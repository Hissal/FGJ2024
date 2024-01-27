using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Transform> PlayerList { get; private set;} = new List<Transform>();
    public GameObject PlayerPrefab;

    void AddPlayer()
    {
        Transform newPlayer = Instantiate(PlayerPrefab, transform.position, Quaternion.identity).transform;
        PlayerList.Add(newPlayer);
    }

    void RemovePlayer(Transform playerToRemove)
    {
        PlayerList.Remove(playerToRemove);
        playerToRemove.gameObject.SetActive(false);
    }

    //TODO SINGLETON
    //PLAYER CONTROLLER ASSIGNING
}

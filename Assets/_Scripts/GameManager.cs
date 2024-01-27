using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Transform> PlayerList { get; private set;} = new List<Transform>();
    public GameObject PlayerPrefab;

    // Add a dictionary to keep track of players' scores
    private Dictionary<Transform, int> playerScores = new Dictionary<Transform, int>();
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }
    public void AddPlayer(Transform newPlayer)
    {
        PlayerList.Add(newPlayer);
        Debug.Log("Added player!");

        // Initialize the new player's score to 0
        playerScores[newPlayer] = 0;
    }

    void RemovePlayer(Transform playerToRemove)
    {
        PlayerList.Remove(playerToRemove);
        playerScores.Remove(playerToRemove);
        playerToRemove.gameObject.SetActive(false);
    }

    // Add a method to increase a player's score
    public void IncreasePlayerScore(Transform player, int amount)
    {
        if (!playerScores.ContainsKey(player))
            {
                // If the player doesn't exist in the dictionary, add them with a score of 0
                playerScores[player] = 0;
            }
        playerScores[player] += amount;
    }

    // Add a method to get a player's score
    public int GetPlayerScore(Transform player)
    {
        if (playerScores.ContainsKey(player))
        {
            return playerScores[player];
        }

        return 0;
    }

    //TODO SINGLETON
    //PLAYER CONTROLLER ASSIGNING
}
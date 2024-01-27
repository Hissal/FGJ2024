using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Transform> PlayerList { get; private set;} = new List<Transform>();
    public GameObject PlayerPrefab;

    // Add a dictionary to keep track of players' scores
    private Dictionary<Transform, int> playerScores = new Dictionary<Transform, int>();

    void AddPlayer()
    {
        Transform newPlayer = Instantiate(PlayerPrefab, transform.position, Quaternion.identity).transform;
        PlayerList.Add(newPlayer);

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
        if (playerScores.ContainsKey(player))
        {
            playerScores[player] += amount;
        }
        Debug.Log("Player " + player + " score: " + playerScores[player]);
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
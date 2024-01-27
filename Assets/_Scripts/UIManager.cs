using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ScoreUIManager : MonoBehaviour
{
    public GameObject scoreTextParent; // Assign this in the inspector, this should be the parent object for the score texts
    public GameObject playerScoreTextPrefab1; // Assign this in the inspector, this should be a prefab for the score text
    public GameObject playerScoreTextPrefab2; // Assign this in the inspector, this should be a prefab for the score text
    public GameObject playerScoreTextPrefab3; // Assign this in the inspector, this should be a prefab for the score text
    public GameObject playerScoreTextPrefab4; // Assign this in the inspector, this should be a prefab for the score text

    private List<GameObject> prefabs;

    private Dictionary<Transform, TextMeshProUGUI> playerScoreTexts = new Dictionary<Transform, TextMeshProUGUI>();

    void Awake()
    {
        prefabs = new List<GameObject>
        {
            playerScoreTextPrefab1,
            playerScoreTextPrefab2,
            playerScoreTextPrefab3,
            playerScoreTextPrefab4
        };
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreTexts();
    }

    void UpdateScoreTexts()
    {
        var i = 0;
        foreach (var player in GameManager.Instance.PlayerList)
        {
            if (!playerScoreTexts.ContainsKey(player))
            {
                // If the player doesn't exist in the dictionary, create a new score text for them
                GameObject newScoreTextObject = Instantiate(prefabs[i], scoreTextParent.transform);
                TextMeshProUGUI newScoreText = newScoreTextObject.GetComponent<TextMeshProUGUI>();
                playerScoreTexts[player] = newScoreText;
            }

            var (score, color) = GameManager.Instance.GetPlayerInfo(player);
            TextMeshProUGUI playerScoreText = playerScoreTexts[player];
            playerScoreText.text = $"{score}";
            playerScoreText.color = color;
            i++; // Increment i
        }
    }
}
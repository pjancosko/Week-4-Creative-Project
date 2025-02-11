using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int Score { get; private set; } = 0;
    public TMP_Text scoreText; // Use TextMeshPro for UI text

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        Score += points;
        Debug.Log("Score Updated: " + Score); // Debug log to confirm score change
        UpdateScoreUI();
    }

    public void DeductScore(int points)
    {
        Score -= points;
        Debug.Log("Score Updated: " + Score); // Debug log to confirm score change
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + Score;
        }
        else
        {
            Debug.LogError("⚠️ Score Text UI not assigned in ScoreManager!");
        }
    }
}

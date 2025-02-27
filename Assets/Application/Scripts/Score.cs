using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text scoreText; // Reference to the current score TextMeshPro UI element
    public TMP_Text highScoreText; // Reference to the high score TextMeshPro UI element

    private int currentScore = 0;
    private int highScore = 0;

    public static Score Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Load the high score when the game starts
        LoadHighScore();
        UpdateScoreUI();
        UpdateHighScoreUI();
    }

    // Method to add points to the current score
    public void AddScore(int points)
    {
        currentScore += points;

        // Check if the current score is higher than the high score
        if (currentScore > highScore)
        {
            highScore = currentScore;
            SaveHighScore(highScore); // Save the new high score
            UpdateHighScoreUI();
        }

        UpdateScoreUI();
    }

    // Method to reset the current score (e.g., when starting a new game)
    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreUI();
    }

    // Method to save the high score
    public void SaveHighScore(int score)
    {
        PlayerPrefs.SetInt("highscore", score);
        PlayerPrefs.Save(); // Ensure the data is saved immediately
    }

    // Method to load the high score
    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("highscore", 0); // Default to 0 if no high score is saved
    }

    // Method to update the current score UI
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }

    // Method to update the high score UI
    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }
}
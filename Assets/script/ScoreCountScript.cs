using TMPro;
using UnityEngine;
using System.IO;
using System;
using System.Collections;

public class ScoreCountScript : MonoBehaviour
{
    public static int ScoreValue = 0;
    private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;  // Hiển thị High Score
    [SerializeField] private TMP_Text finalScoreText; // Hiển thị Final Score (Your Score)
    private SurfaceEffector2D surfaceEffector; // Reference to SurfaceEffector2D from Map object

    private string savePath;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        if (scoreText == null)
        {
            Debug.LogError("ScoreText is missing a TMP_Text component!");
        }

        string gameFolder = Path.GetDirectoryName(Application.dataPath);
        savePath = Path.Combine(gameFolder, "highscore.json");

        // Find SurfaceEffector2D from the Map object
        GameObject mapObject = GameObject.Find("Map SpriteShape 1");
        if (mapObject != null)
        {
            surfaceEffector = mapObject.GetComponent<SurfaceEffector2D>();
        }
        else
        {
            Debug.LogError("Map object not found! Make sure it exists in the scene.");
        }

        LoadScores();
        StartCoroutine(IncrementScore());
    }

    void Update()
    {
        scoreText.text = "Score: " + ScoreValue;
    }

    private IEnumerator IncrementScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (surfaceEffector != null)
            {
                ScoreValue += Mathf.RoundToInt(surfaceEffector.speed);
            }
        }
    }

    // Khi game over, hiển thị Final Score
    public void GameOver()
    {
        SaveScores(ScoreValue);
        UpdateFinalScoreUI(ScoreValue);
    }

    // Khi ứng dụng đóng, lưu Final Score
    void OnApplicationQuit()
    {
        SaveScores(ScoreValue);
    }

    void SaveScores(int finalScore)
    {
        ScoreData data = new ScoreData
        {
            highScore = Mathf.Max(finalScore, GetHighScore()), // Cập nhật High Score nếu cần
            finalScore = finalScore // Lưu điểm cuối cùng khi tắt game
        };

        string json = JsonUtility.ToJson(data);

        try
        {
            File.WriteAllText(savePath, json);
            Debug.Log("Saved Scores - Final Score: " + finalScore + ", High Score: " + data.highScore);
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving scores: " + e.Message);
        }
    }

    void LoadScores()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            ScoreData data = JsonUtility.FromJson<ScoreData>(json);
            Debug.Log("Loaded Scores - Final Score: " + data.finalScore + ", High Score: " + data.highScore);

            UpdateHighScoreUI(data.highScore);
            UpdateFinalScoreUI(data.finalScore);
        }
        else
        {
            Debug.Log("No score file found, starting fresh.");
        }

        Debug.Log("Score file path: " + savePath);
    }

    int GetHighScore()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            ScoreData data = JsonUtility.FromJson<ScoreData>(json);
            return data.highScore;
        }
        return 0;
    }

    void UpdateHighScoreUI(int highScore)
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }
        else
        {
            Debug.LogWarning("HighScoreText is not assigned in Inspector!");
        }
    }

    void UpdateFinalScoreUI(int finalScore)
    {
        if (finalScoreText != null)
        {
            finalScoreText.text = "Final Score: " + finalScore;
        }
        else
        {
            Debug.LogWarning("FinalScoreText is not assigned in Inspector!");
        }
    }
}

[System.Serializable]
public struct ScoreData
{
    public int highScore;
    public int finalScore;
}

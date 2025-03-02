using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public void Restart()
    {
        OnGameOver(); // Lưu điểm trước khi restart
        SceneManager.LoadSceneAsync(1);
    }

    public void Home()
    {
        OnGameOver(); // Lưu điểm trước khi về màn hình chính
        SceneManager.LoadSceneAsync(0);
    }

    private void OnGameOver()
    {
        ScoreCountScript scoreScript = FindFirstObjectByType<ScoreCountScript>();
        if (scoreScript != null)
        {
            scoreScript.GameOver();
        }
        else
        {
            Debug.LogWarning("ScoreCountScript not found! Cannot save final score.");
        }
    }
}

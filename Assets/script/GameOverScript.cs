using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameOverUI; 
    void Start()
    {
        gameOverUI.SetActive(false); // Ẩn UI lúc đầu
    }

    public void ShowGameOver()
    {
        gameOverUI.SetActive(true);
    }
    public void Restart()
    {
       // Lưu điểm trước khi restart
        SceneManager.LoadSceneAsync(1);
    }

    public void Home()
    {
        // Lưu điểm trước khi về màn hình chính
        SceneManager.LoadSceneAsync(0);
    }

 
}

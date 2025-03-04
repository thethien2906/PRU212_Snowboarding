using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false); // Ẩn UI lúc đầu
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Dừng thời gian trong game
        isPaused = true;
        AudioManager.instance.AdjustBGMForPause(true);

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Tiếp tục thời gian trong game
        isPaused = false;
        AudioManager.instance.AdjustBGMForPause(false);

    }

    public void Home()
    {
        Time.timeScale = 1f; // Đảm bảo thời gian tiếp tục trước khi chuyển scene
        SceneManager.LoadSceneAsync(0);
        AudioManager.instance.AdjustBGMForPause(false);

    }
}
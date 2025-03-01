using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void Restart()
    {
		SceneManager.LoadSceneAsync(1);
	}

    // Update is called once per frame
    public void Home()
    {
		SceneManager.LoadSceneAsync(0);

	}
}

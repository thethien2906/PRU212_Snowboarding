using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCountScript : MonoBehaviour
{
    public static int ScoreValue = 0;
    private TMP_Text score;

    private TMP_Text finalScoreText;
    private TMP_Text HighScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = GetComponent<TMP_Text>();
        if (score == null)
        {
            Debug.LogError("ScoreText is missing a TMP_Text component!");
        }
    }


    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + ScoreValue.ToString();
		

	}
}

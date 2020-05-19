using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI highScoreText;
    

    void Start()
    {
        highScoreText.text = "High Score " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void UpdateScore()
    {
        GetComponent<TextMeshProUGUI>().text = score.ToString() + " Score";

        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "High Score " + score.ToString();
        }
        
    }
}

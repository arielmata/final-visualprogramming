using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public static bool GameIsOver = false;
    public GameObject gameOverBadMenuUI;
    public GameObject gameOverGoodMenuUI;
    public PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        // Don't want pause menu to show when game starts
        gameOverBadMenuUI.SetActive(false); 
        gameOverGoodMenuUI.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

        if (player.health >= 3)
        {
            GameOverBad();
        }
        else if (player.finishedGame)
        {
            GameOverGood();
        }
    }

    void GameOverBad()
    {
        Debug.Log("GAME OVER! YOU LOST!");
        gameOverBadMenuUI.SetActive(true);
        GameIsOver = true;
        Time.timeScale = 0f;
    }

    void GameOverGood()
    {
        Debug.Log("GAME OVER! CONGRATS!");
        gameOverGoodMenuUI.SetActive(true);
        GameIsOver = true;
        Time.timeScale = 0f;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu!");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}

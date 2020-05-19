using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public static bool GameIsOver = false;
    public GameObject gameOverMenuUI;
    public PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        gameOverMenuUI.SetActive(false); // Don't want pause menu to show when game starts
    }


    // Update is called once per frame
    void Update()
    {

        if (player.health >= 3)
        {
            GameOver();
        }
        else if (player.finishedGame)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("GAME OVER!");
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f; // freeze game
        GameIsOver = true;
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

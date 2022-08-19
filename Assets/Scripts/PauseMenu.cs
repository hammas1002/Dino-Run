using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject GameOverMenu;
    private GameManager gameManager;
    public GameObject ground;

    Transform playerStartingPosition;
    private Transform playerPosition;


    [Range(0, 2)]
    public float GameSpeed;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Time.timeScale = GameSpeed;
        playerPosition = FindObjectOfType<Movement_Player>().transform;
        playerStartingPosition = GameObject.Find("startingPosition").transform;
        ground = GameObject.Find("Ground01");
    }

    public void DisplayPauseMenu()
    {
        if (GameOverMenu.activeSelf) return;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void MainMenuGameOver()
    {
        Time.timeScale = GameSpeed;
        gameManager.GameOver();
        SceneManager.LoadScene("MainMenu");
    }
    public void MainMenuPause()
    {
        Time.timeScale = GameSpeed;
        SceneManager.LoadScene("MainMenu");
    }
    public void ResumeGame()
    {
        ground.SetActive(true);
        if (playerPosition.position.y<-4)
        {
            playerPosition.position = playerStartingPosition.position;
        }
   
        Debug.Log("Resuming Game");
        pauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
       
        
        Time.timeScale = GameSpeed;

        StartCoroutine(nameof(SettingUpSpeed));
    }

    public IEnumerator SettingUpSpeed()
    {
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = GameSpeed;

    }
    public void Retry()
    {
        Time.timeScale = GameSpeed;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void DisplayGameOverMenu()
    {
        
        GameOverMenu.SetActive(true);

        Time.timeScale = 0;
    }

    public void RewardAdClosed()
    {
        Debug.Log("RewardAdClosed");
        DisplayGameOverMenu();
    }


}

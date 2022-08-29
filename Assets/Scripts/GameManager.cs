using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text score;
    public TMP_Text coins;
    public TMP_Text Lives;
    private PlayfabManager playfabManager;
    [SerializeField]
    private int lives;
    public GameObject SlideButton;
    public GameObject WatchAddButton;

    public GameObject pauseMenu;
    public GameObject GameOverMenu;

    public float currentTime=0;
    public float startingTime;

    bool isHardcoreLevel;

    private void Awake()
    {
        playfabManager = FindObjectOfType<PlayfabManager>();
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            SlideButton.SetActive(false);
        }
    }
    int startScore = 00000;
    int currentScore;
    int currentCoins;
    
    private void Start()
    {
        currentScore = 0;
        currentCoins = 0;
        currentTime = startingTime;
        isHardcoreLevel = SceneManager.GetActiveScene().name == "hardcoreLevel" ? true : false;
        Lives.text = lives.ToString();
        if (isHardcoreLevel)
        {
            Time.timeScale = 1.7f;
        }
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        currentScore = startScore + (int)Time.timeSinceLevelLoad;
        score.text = currentScore.ToString("D5");
    }

    public void GameOver()
    {
        playfabManager.SendLeaderBoard(currentScore);
    }
    public void DisableWatchAdd()
    {
        WatchAddButton.SetActive(false);
    }
    public void Death()
    {
        lives = lives - 1;

        Lives.text =lives<0? "0": lives.ToString();
    }
    public int livesCount()
    {
        return lives;
    }

    public void CollectCoin()
    {
        currentCoins += 1;
        coins.text = currentCoins + "";
    }
}

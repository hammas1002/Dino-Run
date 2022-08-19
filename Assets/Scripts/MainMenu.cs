using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System;

public class MainMenu : MonoBehaviour
{

    [Header("Windows")]
    public GameObject nameWindow;
    public GameObject leaderBoardWindow;
    public GameObject aboutWindow;

    public GameObject rowPrefab;
    public Transform rowsParent;
    public TMP_InputField nameInput;
    public GameObject Buttons;
    public GameObject Back_Button;

    public Animator emailAnim;
    void Start()
    {
        Login();
    }

    public void OpenChannel()
    {
        Application.OpenURL("https://www.instagram.com/treetechgames/?igshid=YmMyMTA2M2Y");
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile=true
            }
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);

    }
    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Successfull Login/Account create");
        string name = null;
        if(result.InfoResultPayload.PlayerProfile!=null)
        name = result.InfoResultPayload.PlayerProfile.DisplayName;
        if (name==null)
        {
            nameWindow.SetActive(true);
            Buttons.SetActive(false);
        }
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account");
        Debug.Log(error.GenerateErrorReport());
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "DinoScore",
            StartPosition = 0,
            MaxResultsCount = 7

        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    private void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
            texts[0].text = (item.Position+1).ToString();
            texts[1].text = item.DisplayName.Contains("@") ?item.DisplayName.Split("@")[0]:"Anonymous";
            texts[2].text = item.StatValue.ToString();

            Debug.Log(item.PlayFabId + " : " + item.StatValue);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowLeaderBoard();
        }
    }
    public void StartLevel()
    {
        SceneManager.LoadScene("normalLevel");
    }

    public void StartHardcoreLevel()
    {
        SceneManager.LoadScene("hardcoreLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowLeaderBoard()
    {
        leaderBoardWindow.SetActive(true);
        Buttons.SetActive(false);
        Back_Button.SetActive(true);
        GetLeaderboard();
    }

    public void ShowContactInfo()
    {
        aboutWindow.SetActive(true);
        Back_Button.SetActive(true);
        Buttons.SetActive(false);
        leaderBoardWindow.SetActive(false);
    }

    public void SubmitNameButton()
    {
        if (!nameInput.text.Contains("@")) {
            emailAnim.SetTrigger("Email");
            return;
        }
            
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nameInput.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplaynameUpdate, OnError);
        nameWindow.SetActive(false);
    }

    public void BackButton()
    {
        Buttons.SetActive(true);
        leaderBoardWindow.SetActive(false);
        aboutWindow.SetActive(false);
        Back_Button.SetActive(false);

    }

    private void OnDisplaynameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Buttons.SetActive(true);
        
        Debug.Log("Updated Display Name");
    }


}

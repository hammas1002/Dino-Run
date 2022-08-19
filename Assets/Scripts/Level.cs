using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static string PreviousLevel { get; private set; }
    public GameObject BannerAd;
    private void OnDestroy()
    {
        PreviousLevel = gameObject.scene.name;
    }

    private void Start()
    {
        

        if (Level.PreviousLevel == null || Level.PreviousLevel == "MainMenu" || SceneManager.GetActiveScene().name!= "MainMenu") return;

        Debug.Log(Level.PreviousLevel);  // use this in any level to get the last level.
        BannerAd.SetActive(true);

        //changing yodo1 canvas scale mode to scaleWithScreenSize otherwise banner becomes too large
        GameObject.Find("Yodo1AdCanvas").GetComponentInChildren<CanvasScaler>().uiScaleMode=UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private GameManager gameManager;
    
    PauseMenu pauseMenu;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        pauseMenu = FindObjectOfType<PauseMenu>();
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.Death();
            pauseMenu.DisplayGameOverMenu();
            
            return;
        }
        Destroy(collision.gameObject);
    }
}

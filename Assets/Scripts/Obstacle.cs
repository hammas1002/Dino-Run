using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Obstacle : MonoBehaviour
{
    public float movementSpeed;
    Rigidbody2D rb;
    PauseMenu pauseMenu;
    GameManager gameManager;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pauseMenu = FindObjectOfType<PauseMenu>();
      
        gameManager = FindObjectOfType<GameManager>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(-movementSpeed*Time.fixedDeltaTime,0);
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Game over");
            pauseMenu.DisplayGameOverMenu();

            gameManager.Death();
            if (gameManager.livesCount() < 0)
            {
                gameManager.DisableWatchAdd();
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    private Transform trapClose, trapOpen;
    Rigidbody2D rb;
    public float movementSpeed;
   
    private GameObject ground;
    

    private void Start()
    {
        trapClose = transform.GetChild(0);
        trapOpen = transform.GetChild(1);
        rb = GetComponent<Rigidbody2D>();
        ground = GameObject.Find("Ground01");
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(-movementSpeed * Time.fixedDeltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            ground.SetActive(false);
            StartCoroutine(nameof(ActivateCollider));
            trapClose.gameObject.SetActive(false);
            trapOpen.gameObject.SetActive(true);
        }
    }
    IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(0.6f);
        //yield return new WaitForSecondsRealtime(0.4f);
        ground.SetActive(true);
        
        
    }
}

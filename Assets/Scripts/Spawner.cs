using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstacle;
    [SerializeField]
    float spawnTime= 3f;
    public float timeBetweenSpawns = 3f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), spawnTime, timeBetweenSpawns);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnObstacle()
    {
        int randomNum = Random.Range(0, obstacle.Length);
        Instantiate(obstacle[randomNum], transform.position, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyes;
    public Transform[] spawn;

    private float timer;
    private float newTimer;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SpawnEnemyes());
        timer = 3;
        newTimer = 3;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0) {
            SpawnEnemy();
            timer = newTimer;
        }
        
    }

    void SpawnEnemy() {
        int randomEnemy = Random.Range(0, enemyes.Length);
        int randomSpawn = Random.Range(0, spawn.Length);

        Instantiate(enemyes[randomEnemy], spawn[randomSpawn].position, Quaternion.identity);
    }
}

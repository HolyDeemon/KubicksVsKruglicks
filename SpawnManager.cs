using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public Vector3 spawnArea;
    public float spawnTimer;

    private float timer;

    [Header("Score Stuff")]
    public int score;
    public int maxscore;
    public int enemy1score;
    public int enemy2score;
    public int enemy3score;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            if (score <= maxscore)
            {
                Spawn();
                timer = spawnTimer;
                spawnTimer -= 0.1f;
            }
            
        }
    }

    private void Spawn()
    {
        Vector3 position = new Vector3(UnityEngine.Random.Range(-spawnArea.x, spawnArea.x), 0f, UnityEngine.Random.Range(-spawnArea.z, spawnArea.z));
        
        switch (UnityEngine.Random.Range(0, 3))
        {
            case 0:
                GameObject newEnemy1 = Instantiate(enemy1);
                newEnemy1.transform.position = position;
                score += enemy1score;
                break;
            case 1:
                GameObject newEnemy2 = Instantiate(enemy2);
                newEnemy2.transform.position = position;
                score += enemy2score;
                break;
            case 2:
                GameObject newEnemy3 = Instantiate(enemy3);
                newEnemy3.transform.position = position;
                score += enemy3score;
                break;
        }
            

        
    }
}

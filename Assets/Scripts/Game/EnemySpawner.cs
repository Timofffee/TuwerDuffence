using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Path))]
public class EnemySpawner : MonoBehaviour
{
    
    private float timer = 0f;
    private float startTimer = 0f;
    [Range(0, 10.0f)]
    public float startLatency = 1.0f;
    [Range(0.01f, 10.0f)]
    public float delay = 1.0f;
    [Range(1, 9999)]
    private int currentEnemy = 0;

    public GameObject[] enemyPrefab;

    [HideInInspector]
    public bool waveEnded = false;

    private void Spawn() {
        if (enemyPrefab[currentEnemy] == null)
        {
            return;
        }
        GameObject enemy = Instantiate(enemyPrefab[currentEnemy], GetComponent<Path>().points[0] + transform.position, transform.rotation, gameObject.transform);
        enemy.GetComponent<EnemyController>().SetPath(GetComponent<Path>().points);
        GameLogic.enemies.Add(enemy);
    }


    private void Start()
    {
        waveEnded = !(currentEnemy < enemyPrefab.Length);
    }


    private void Update()
    {
        startTimer += Time.deltaTime;
        if (enemyPrefab == null || waveEnded || startTimer < startLatency)
        {
            return;
        }
        if (timer >= delay)
        {
            Spawn();
            currentEnemy++;
            waveEnded = !(currentEnemy < enemyPrefab.Length);
            timer -= delay;
        }
        timer += Time.deltaTime;
    }
}

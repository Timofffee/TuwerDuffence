using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    static public int enemiesEnded = 0;
    static public int enemiesDied = 0;
    static public int towerCreated = 0;
    static public int score = 0;
    static public List<GameObject> enemies = new List<GameObject>();
    
    static private bool levelRunning = false;
    
    [HideInInspector]
    public EnemySpawner spawner;

    private float startTime = 0f;


    IEnumerator Lose()
    {
        levelRunning = false;
        PauseControl.gameIsPaused = true;
        yield return new WaitForSecondsRealtime(1.0f);
        if (GetComponent<LevelManager>().onLevel)
        {
            GetComponent<HUD>().ShowLosePanel();
        }
    }


    IEnumerator Win()
    {
        levelRunning = false;
        PauseControl.gameIsPaused = true;
        yield return new WaitForSecondsRealtime(1.0f);
        if (GetComponent<LevelManager>().onLevel)
        {
            GetComponent<HUD>().ShowWinPanel(Time.time - startTime, enemiesDied, towerCreated, score);
            GetComponent<LevelManager>().LevelComplited();
        }
    }


    public void StartLevel()
    {
        GetComponent<HUD>().objectUpgradeCounter.SetActive(true);
        ResetLevelStats();
        levelRunning = true;
        PauseControl.gameIsPaused = false;
    }


    private void ResetLevelStats()
    {
        enemiesEnded = 0;
        enemiesDied = 0;
        towerCreated = 0;
        startTime = Time.time;
        enemies = new List<GameObject>();
    }

    
    void Update()
    {
        if (!levelRunning)
        {
            return;
        }
        if (enemiesEnded > 0)
        {
            StartCoroutine(Lose());
            return;
        }
        enemies.RemoveAll(item => item == null);
        if (spawner.waveEnded && enemies.Count == 0)
        {
            StartCoroutine(Win());
            return;
        }
    }
}

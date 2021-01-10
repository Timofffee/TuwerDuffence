using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameLogic))]
public class LevelManager : MonoBehaviour
{
    private int currentLevelID = -1;
    private GameObject currentLevel;

    public GameObject[] levels;
    public int complitedLevels = 0;

    [HideInInspector]
    public bool onLevel = false;


    public bool LoadLevel(int level_id)
    {
        if (level_id >= levels.Length)
        {
            return false;
        }
        currentLevel = Instantiate(levels[level_id]);
        currentLevelID = level_id;
        GetComponent<GameLogic>().spawner = currentLevel.GetComponent<EnemySpawner>();
        GetComponent<GameLogic>().StartLevel();
        onLevel = true;
        return true;
    }
    

    public bool LoadNextLevel()
    {
        UnloadLevel();
        if (++currentLevelID >= levels.Length)
        {
            return false;
        }
        return LoadLevel(currentLevelID);
    }


    public bool ReloadLevel()
    {
        UnloadLevel();
        return LoadLevel(currentLevelID);
    }


    public bool UnloadLevel()
    {
        onLevel = false;
        if (currentLevel != null)
        {
            Destroy(currentLevel);
            return true;
        }
        return false;
    }


    public void LevelComplited()
    {
        if (currentLevelID < complitedLevels)
        {
            return;
        }
        complitedLevels++;
    }
}

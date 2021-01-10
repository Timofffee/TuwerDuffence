using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LevelManager))]
public class HUD : MonoBehaviour
{

    public Sprite iconVolumeEnabled;
    public Sprite iconVolumeDisabled;

    public GameObject buttonPlay;
    public GameObject buttonVolume;
    public GameObject buttonGoToMenu;
    public GameObject buttonHelp;

    public GameObject panelSelectLevel;
    public GameObject panelWin;
    public GameObject panelLose;
    public GameObject panelHelp;

    public GameObject objectLogo;
    public GameObject objectLevelsList;
    public GameObject objectUpgradeCounter;
    public GameObject prefabButtonSelectLevel;

    private LevelManager levelManager;

    public bool volumeEnabled = true;

    public void SelectLevel()
    {
        HideAllPanels();
        buttonGoToMenu.SetActive(true);
        panelSelectLevel.SetActive(true);
        objectLogo.SetActive(false);
        buttonPlay.SetActive(false);
    }


    public void ToggleVolume()
    {
        volumeEnabled = !volumeEnabled;
        buttonVolume.GetComponent<Image>().sprite = volumeEnabled ? iconVolumeEnabled : iconVolumeDisabled;
        UpdateVolume();
    }


    public void GoToMenu()
    {
        HideAllPanels();
        buttonGoToMenu.SetActive(false);
        objectLogo.SetActive(true);
        buttonPlay.SetActive(true);
        buttonHelp.SetActive(false);
        objectUpgradeCounter.SetActive(false);

        levelManager.UnloadLevel();
        PauseControl.gameIsPaused = false;
        RecalculateLevelList();
    }


    public void NextLevel()
    {
        HideAllPanels();
        if (!levelManager.LoadNextLevel())
        {
            GoToMenu();
            return;
        }
        objectUpgradeCounter.SetActive(true);
    }


    public void RetryCurrentLevel()
    {
        HideAllPanels();

        levelManager.ReloadLevel();

        objectUpgradeCounter.SetActive(true);
    }


    public void LoadLevel(int level_id)
    {
        HideAllPanels();
        buttonHelp.SetActive(true);

        levelManager.LoadLevel(level_id);

        objectUpgradeCounter.SetActive(true);
    }


    public void ToggleHelp()
    {
        if (panelWin.activeInHierarchy || panelLose.activeInHierarchy)
        {
            return;
        }
        bool active = !panelHelp.activeInHierarchy;
        panelHelp.SetActive(active);
        objectUpgradeCounter.SetActive(!active);
        // PauseControl.gameIsPaused = active;
    }


    public void ShowWinPanel(float time, int enemiesDied, int towerCreated, int score)
    {
        HideAllPanels();
        objectUpgradeCounter.SetActive(false);

        panelWin.GetComponent<WinPanel>().UpdateStats(time, enemiesDied, towerCreated, score);
        panelWin.SetActive(true);
    }


    public void ShowLosePanel()
    {
        HideAllPanels();
        objectUpgradeCounter.SetActive(false);

        panelLose.SetActive(true);
    }


    private void HideAllPanels()
    {
        panelSelectLevel.SetActive(false);
        panelWin.SetActive(false);
        panelLose.SetActive(false);
        panelHelp.SetActive(false);
    }


    private void UpdateVolume()
    {
        AudioListener.volume = volumeEnabled ? 1f : 0f;
    }


    private void GenerateLevelList()
    {
        if (levelManager.levels == null)
        {
            return;
        }
        for (int idx = 0; idx < levelManager.levels.Length; idx++)
        {
            GameObject button = Instantiate(prefabButtonSelectLevel, new Vector3(450*(idx%4), -450*(idx/4), 0), transform.rotation) as GameObject;
            button.transform.SetParent(objectLevelsList.transform, false);
            button.GetComponent<SelectLevelButton>().SetLevelInfo(idx+1, levelManager.complitedLevels>idx, levelManager.complitedLevels>=idx);
            int level_id = idx;
            button.GetComponent<Button>().onClick.AddListener(() => LoadLevel(level_id));
        }
    }

    private void RecalculateLevelList()
    {
        if (levelManager.levels == null)
        {
            return;
        }
        int children = objectLevelsList.transform.childCount;
        for (int idx = 0; idx < children; idx++)
        {
            if (idx < levelManager.levels.Length)
            {
                GameObject button = objectLevelsList.transform.GetChild(idx).gameObject;
                button.GetComponent<SelectLevelButton>().SetLevelInfo(idx+1, levelManager.complitedLevels>idx, levelManager.complitedLevels>=idx);
                button.GetComponent<Button>().onClick.RemoveAllListeners();
                int level_id = idx;
                button.GetComponent<Button>().onClick.AddListener(() => LoadLevel(level_id));
            }
            else
            {
                Destroy(objectLevelsList.transform.GetChild(idx).gameObject);
            }
        }

        if (children < levelManager.levels.Length)
        {
            for (int idx = children; idx < levelManager.levels.Length; idx++)
            {
                GameObject button = Instantiate(prefabButtonSelectLevel, new Vector3(450*(idx%4), -450*(idx/4), 0), transform.rotation) as GameObject;
                button.transform.SetParent(objectLevelsList.transform, false);
                button.GetComponent<SelectLevelButton>().SetLevelInfo(idx+1, levelManager.complitedLevels>idx, levelManager.complitedLevels>=idx);
                int level_id = idx;
                button.GetComponent<Button>().onClick.AddListener(() => LoadLevel(level_id));
            }
        }
    }


    private void Start()
    {
        levelManager = GetComponent<LevelManager>();
        GenerateLevelList();
    }
}

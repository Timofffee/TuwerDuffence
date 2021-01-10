using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    public Text timeText;
    public Text killedText;
    public Text scoreText;

    public void UpdateStats(float time, int enemiesKilled, int towerCreated, int score)
    {
        timeText.text = string.Format("{0:00}:{1:00}", (int)time/60, (int)time%60);
        killedText.text = enemiesKilled.ToString();
        scoreText.text = score.ToString();
    }
}

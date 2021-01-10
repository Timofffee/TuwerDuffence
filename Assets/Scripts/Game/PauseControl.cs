using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    static private bool stateChanged = false;
    static public bool gameIsPaused
    {
        get { return _gameIsPaused; }
        set {
            // if (!stateChanged)
            // {
                _gameIsPaused = value;
                stateChanged = true;
            // }
        }
    }
    static private bool _gameIsPaused;
    


    void Update()
    {
        if (stateChanged)
        {
            PauseGame();
        }
    }


    void PauseGame ()
    {
        if(gameIsPaused)
        {
            Time.timeScale = Mathf.Clamp(Time.timeScale - 0.01f, 0, 1.0f);
            if (Time.timeScale == 0)
            {
                stateChanged = false;
            }
        }
        else 
        {
            Time.timeScale = Mathf.Clamp(Time.timeScale + 0.01f, 0, 1.0f);
            if (Time.timeScale == 1)
            {
                stateChanged = false;
            }
        }
        AudioSource source = GetComponent<AudioSource>();
        if (source != null)
        {
            source.pitch = 0.5f + Time.timeScale/2;
            source.volume = 0.5f + Time.timeScale/2;
        }
    }
}
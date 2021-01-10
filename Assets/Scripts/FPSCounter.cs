using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {
 
     public float timer, refresh, avgFramerate;
     private Text m_Text;
 
     private void Start()
     {
         m_Text = GetComponent<Text>();
     }
 
 
     private void Update()
     {
         float timelapse = Time.unscaledDeltaTime;
         timer = timer <= 0 ? refresh : timer -= timelapse;
 
         if(timer <= 0) avgFramerate = (int) (1f / timelapse);
         m_Text.text = avgFramerate.ToString();
     } 
 }
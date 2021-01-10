using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelButton : MonoBehaviour
{
    public Text text;
    public GameObject objectComplited;


    public void SetLevelInfo(int id, bool complited, bool opened)
    {
        text.text = id.ToString();
        objectComplited.SetActive(complited);
        GetComponent<Button>().interactable = opened;
    }
}

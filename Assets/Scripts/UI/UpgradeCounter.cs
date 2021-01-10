using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCounter : MonoBehaviour
{
    
    static private bool countChanged = false;
    static public int count
    {
        get { return _count; }
        set {
            _count = value;
            countChanged = true;
        }
    }
    static private int _count = -1;

    public Text counter;


    private void Update()
    {
        if (countChanged)
        {
            counter.text = count.ToString();
            countChanged = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private bool pressed = false; 

    [Range(-1, 10000)]
    public int maxUpgrades = 4;


    private bool CastBlock()
    {        
        if (Input.GetMouseButton(0) && !pressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 10))
            {
                if (hit.collider.gameObject.GetComponent<TowerBlock>() != null)
                {
                    hit.collider.gameObject.GetComponent<TowerBlock>().UpgradeTower();
                } 
                else
                {
                    hit.collider.gameObject.transform.parent.gameObject.GetComponent<TowerBlock>().UpgradeTower();
                } 
                pressed = Input.GetMouseButton(0);
                return true;
            }
        }
        pressed = Input.GetMouseButton(0);
        return false;
    }

    
    private void Update() 
    {
        if (maxUpgrades > 0)
        {
            if (CastBlock())
            {
                maxUpgrades--;
                UpgradeCounter.count = maxUpgrades;
            }
        }
    }


    private void Start()
    {
        UpgradeCounter.count = maxUpgrades;
    }
}

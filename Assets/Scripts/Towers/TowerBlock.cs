using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBlock : MonoBehaviour
{
    public GameObject[] towers;

    private int current_tower_id = -1;
    private GameObject current_tower = null;
    
    public void UpgradeTower()
    {
        if (towers.Length - 1 > current_tower_id)
        {
            current_tower_id++;
            if (current_tower != null)
            {
                Destroy(current_tower);
            }

            current_tower = Instantiate(towers[current_tower_id], new Vector3(0, 0.5f, 0) + transform.position, transform.rotation, gameObject.transform);
        }
        
    }
}

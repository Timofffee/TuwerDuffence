using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BulletController : MonoBehaviour
{
    [Range(1, 1000)]
    public int damage = 1;

    [HideInInspector]
    public GameObject target;
    
    public void SetTarget(GameObject obj)
    {
        if (obj.GetComponent<EnemyController>() != null)
        {
            target = obj;
        }
    }
    
    // public abstract void SetTarget(GameObject obj);
}

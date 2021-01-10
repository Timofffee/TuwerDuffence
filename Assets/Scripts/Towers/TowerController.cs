using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    private List<GameObject> targets = new List<GameObject>();

    
    [Range(0.01f, 10.0f)]
    public float latency = 2.0f;
    private float timer;

    public GameObject bulletPrefab;
    public Transform spawnPos;

    [Range(0, 10000)]
    public int additiveScore = 10;


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy"){
            targets.Add(col.gameObject);
        }
    }


    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy"){
            targets.Remove(col.gameObject);
        }
    }


    private void Shoot(GameObject obj) {
        GameObject bullet = Instantiate(bulletPrefab, spawnPos.position, transform.rotation, gameObject.transform);
        bullet.GetComponent<BulletController>().SetTarget(obj);
    }


    private void Update()
    {
        if (targets.Count > 0)
        {
            if (timer >= latency)
            {
                targets.RemoveAll(item => item == null);
                if (targets.Count > 0)
                {
                    Shoot(targets[0]);
                    timer = 0;
                }
            }
        }
        timer += Time.deltaTime;
    }


    private void Start()
    {
        timer = latency/2.0f;
        GameLogic.towerCreated++;
        GameLogic.score += additiveScore;
    }
}

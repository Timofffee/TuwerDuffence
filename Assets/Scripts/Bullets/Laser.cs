using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Laser : BulletController {

    public AudioClip shootSound;

    private LineRenderer lineRenderer;
    private AudioSource source;
    private float lowPitchRange = 0.9f;
    private float highPitchRange = 1.1f;

    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
        
        source = GetComponent<AudioSource>();
        source.pitch = Random.Range(lowPitchRange, highPitchRange);
        source.PlayOneShot(shootSound);
        
        StartCoroutine(Shoot());
    }
    

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.2f);
        if (target != null)
        {
            target.GetComponent<EnemyController>().Damage(damage);
        }
        Destroy(gameObject);
    }


    void Update () {
        if (target == null)
        {
            lineRenderer.enabled = false;
            return;
        }
        lineRenderer.SetPosition(1, target.transform.position - transform.position + new Vector3(0f, 0.1f, 0f));
    }
}

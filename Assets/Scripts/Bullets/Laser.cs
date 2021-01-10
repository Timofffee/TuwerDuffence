using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// [RequireComponent(typeof(LineRenderer))]
// [RequireComponent(typeof(AudioSource))]
public class Laser : BulletController {

    public AudioClip shootSound;

    private LineRenderer lr;
    private AudioSource source;
    private float lowPitchRange = .9F;
    private float highPitchRange = 1.1F;

    void Start () {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, new Vector3());
        lr.SetPosition(1, new Vector3());
        
        source = GetComponent<AudioSource>();
        source.pitch = Random.Range (lowPitchRange,highPitchRange);
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
            lr.enabled = false;
            return;
        }
        lr.SetPosition(1, target.transform.position - transform.position + new Vector3(0f, 0.1f, 0f));
    }
}

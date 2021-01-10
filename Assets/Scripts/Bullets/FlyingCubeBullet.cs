using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCubeBullet : BulletController
{
    private Vector3 startPos;
    private Vector3 currentPos;
    private Vector3 lastTargetPos;

    [Range(0.01f, 100.0f)]
    public float speed = 4.0f;

    private float minDistance = 0.1f;

    public AnimationCurve heightCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [Range(0, 10.0f)]
    public float maxHeight = 1.0f;
    

    private void Start()
    {
        startPos = transform.position;
        currentPos = startPos;
    }


    private void Update()
    {
        if (target != null)
        {
            float distTarget = Vector3.Distance(target.transform.position, transform.position);
            if (distTarget < minDistance)
            {
                target.GetComponent<EnemyController>().Damage(damage);
                Destroy(gameObject);
                return;
            }
            float dist = Vector3.Distance(target.transform.position, startPos);
            Vector3 dir = Vector3.Normalize(target.transform.position - transform.position);
            
            currentPos += dir*speed*Time.deltaTime;
            lastTargetPos = target.transform.position;
            transform.position = currentPos + new Vector3(0, heightCurve.Evaluate(1 - distTarget/dist) * maxHeight, 0);
        }
        else if (lastTargetPos != null) 
        {
            float distTarget = Vector3.Distance(lastTargetPos, transform.position);
            if (distTarget < minDistance)
            {
                Destroy(gameObject);
                return;
            }
            float dist = Vector3.Distance(lastTargetPos, startPos);
            Vector3 dir = Vector3.Normalize(lastTargetPos - transform.position);
            currentPos += dir*speed*Time.deltaTime;
            transform.position = currentPos + new Vector3(0, heightCurve.Evaluate(1 - distTarget/dist) * maxHeight, 0);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public AnimationCurve moveCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [Range(0, 100.0f)]
    public float animTime = 1.0f;
    [Range(1,1000)]
    public int health = 4;

    private float moveTime = 0;
    private Vector3 oldPos;
    private Vector3 nextPos;

    private Vector3[] path;
    private int currentPoint = -1;

    [Range(0, 10000)]
    public int killScore = 50;


    public void SetPath(Vector3[] p) 
    {
        path = p;
        currentPoint = 0;
    }


    private void NextPos()
    {
        if (path == null || path.Length == 0) 
        {
            return;
        }
        currentPoint++;
        if (currentPoint < path.Length)
        {
            oldPos = path[currentPoint-1];
            nextPos = path[currentPoint];
            transform.LookAt(nextPos, Vector3.up);
        }
        else
        {
            GameLogic.enemiesEnded++;
            Destroy(gameObject);
        }
        
    }


    public void Damage(int val)
    {
        // Бывает, что противник имеет 0 hp, но ещё не уничтожен
        if (health <= 0)
        {
            return;
        }
        health -= val;
        if (health <= 0)
        {
            GameLogic.enemiesDied++;
            GameLogic.score += killScore;
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        NextPos();
    }


    private void Update()
    {
        transform.position = Vector3.Lerp(oldPos, nextPos, moveCurve.Evaluate(moveTime / animTime));
        moveTime += Time.deltaTime;
        if (moveTime > animTime)
        {
            moveTime -= animTime;
            NextPos();
        } 
    }
}

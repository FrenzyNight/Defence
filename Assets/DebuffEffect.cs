using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffEffect : MonoBehaviour
{
    
    GameObject enemy;

    void Start()
    {
        enemy = transform.parent.gameObject;
        StartEffect();
    }

    void StartEffect()
    {
        if(enemy.tag == "enemy")
        {
            enemy.GetComponent<EnemyMove>().speed *= 0.85f;
        }

        Invoke("EndEffect", 2f);
    }

    void EndEffect()
    {
        if(enemy.tag == "enemy")
        {
            enemy.GetComponent<EnemyMove>().speed /= 0.85f;
        }
        Destroy(gameObject);
    }
}

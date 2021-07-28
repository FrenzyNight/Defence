using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff2Effect : MonoBehaviour
{

    GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.parent.gameObject;
        StartEffect();
    }

    void StartEffect()
    {
        if(enemy.tag == "enemy")
        {
            enemy.GetComponent<EnemyMove>().speed *= 0.8f;
        }
    }

    public void EndEffect()
    {
        
        enemy.GetComponent<EnemyMove>().speed /= 0.8f;
        
        Destroy(gameObject);
    }

  
}

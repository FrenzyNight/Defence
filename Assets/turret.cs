using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class turret : MonoBehaviour
{
    GameObject target;
    WaveManager wm;
    public float turretDamage;
    public float turretNuckback;
    // Start is called before the first frame update
    void Start()
    {
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            CancelInvoke("Fire");
            if(wm.isWave)
            {
                target = FindNearestEnemy();
                InvokeRepeating("Fire",0.5f, 1f);
            }
        }
    }

    private GameObject FindNearestEnemy()
    {
        var objects = GameObject.FindGameObjectsWithTag("enemy").ToList();

        var targetenemy = objects.OrderBy(obj => 
        {
            return Vector2.Distance(transform.position, obj.transform.position);
        })
        .FirstOrDefault();

        if(targetenemy == null)
        {
            targetenemy = GameObject.FindWithTag("boss");
        }

        return targetenemy;
    }

    void Fire()
    {
        if(!wm.isStop && wm.isWave)
        {
            if(target.tag == "enemy")
            {
                target.GetComponent<EnemyMove>().enemyNowHp -= turretDamage;
                target.GetComponent<Transform>().Translate(Vector2.right * turretNuckback);
            }
            else if(target.tag == "boss")
            {
                target.GetComponent<Boss>().BossNowHP -= turretDamage;
            }
        }
    }
}

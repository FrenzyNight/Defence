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
    public float FPS;

    public AudioClip audioFire;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();
    
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioFire;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            CancelInvoke("Fire");
            if(wm.isWave)
            {
                float firstDelay = Random.Range(0.1f,0.55f);
                target = FindNearestEnemy();
                InvokeRepeating("Fire",firstDelay, 1/FPS);
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
            if(target!= null)
            {
                if(target.tag == "enemy")
                {
                    audioSource.Play();
                    target.GetComponent<EnemyMove>().enemyNowHp -= turretDamage;
                    target.GetComponent<Transform>().Translate(Vector2.right * turretNuckback);
                }
                else if(target.tag == "boss")
                {
                    audioSource.Play();
                    target.GetComponent<Boss>().BossNowHP -= turretDamage;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Debuffer_type1 : MonoBehaviour
{
    public GameObject debuffbulet;
    public GameObject debuffeffect;

    public float Damage;
    public float FPS;
    public float bulletSpeed;

    public GameObject target;
    WaveManager wm;
    Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        tr = GetComponent<Transform>();

        Golden();
        StartCoroutine(Fire());
    }

    void Golden()
    {
        if(GetComponent<MercenaryInfo>().step == 2)
            FPS *= 2;
    }


    private GameObject FindRandomEnemy()
    {
        var objects = GameObject.FindGameObjectsWithTag("enemy").ToList();
        
        GameObject targetenemy = null;

        if(objects.Count > 0)
        {
            int rndInx = Random.Range(0, objects.Count);
            targetenemy = objects[rndInx];
        }

        if(targetenemy == null)
        {
            targetenemy = GameObject.FindWithTag("boss");
        }

        return targetenemy;
    }

    IEnumerator Fire()
    {
        
        while(true)
        {
            yield return new WaitForSeconds(1/FPS);
            if(!wm.isStop && wm.isWave)
            {
                target = FindRandomEnemy();
                if(target!= null)
                {
                    Instantiate(debuffbulet, tr.position, Quaternion.identity, gameObject.transform);
                }
            }
        }
    }
}

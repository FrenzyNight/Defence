using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuffer_type2 : MonoBehaviour
{
    public GameObject debuffeffect2;
    WaveManager wm;

    public Vector2 limitMin;
    public Vector2 limitMax;

    public float useTime;
    public float cooltime;

    // Start is called before the first frame update
    void Start()
    {
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();

        StartCoroutine(Fire());
    }

    
    IEnumerator Fire()
    {
        while(true)
        {
            yield return new WaitForSeconds(cooltime);
            if(!wm.isStop && wm.isWave)
            {
                float r = Random.Range(limitMin.y, limitMax.y);
                Vector2 creatingPoint = new Vector2(limitMin.x, r);

                Instantiate(debuffeffect2, creatingPoint, Quaternion.identity);
                yield return new WaitForSeconds(useTime);
                
            }
            //yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(limitMin, limitMax);
    }
}

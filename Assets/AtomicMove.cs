using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AtomicMove : MonoBehaviour
{
    public GameObject effectPrefab;
    WaveManager wm;
    //Transform target;
    Transform tr;
    Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        target = GameObject.FindWithTag("target").GetComponent<Transform>().position;

        gameObject.transform.DOMove(new Vector2(target.x, target.y-0.01f), 1.5f).SetEase(Ease.InQuad);;
    }

    // Update is called once per frame
    void Update()
    {
       if(tr.position.y <= target.y)
       {
           Instantiate(effectPrefab ,target, Quaternion.identity);
           gameObject.SetActive(false);
           Destroy(gameObject, 0.1f);
       }

    }
}

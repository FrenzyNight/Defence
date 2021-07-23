using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffBulletMove : MonoBehaviour
{
    private Vector2 point;
    private float angle;
    
    Transform tr;
    Transform effectTr;
    WaveManager wm;
    GameObject debuffer;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        debuffer = transform.parent.gameObject;

        point = debuffer.GetComponent<Debuffer_type1>().target.GetComponent<Transform>().position;
        point.x -= tr.transform.position.x;
        point.y -= tr.transform.position.y;
        angle = Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg - 90f;
        tr.eulerAngles = new Vector3(0,0,angle);
    }

    // Update is called once per frame
    void Update()
    {
         if(!wm.isStop)
            tr.Translate(point.normalized * debuffer.GetComponent<Debuffer_type1>().bulletSpeed * Time.deltaTime,0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            effectTr = collision.gameObject.GetComponent<Transform>();
            collision.gameObject.GetComponent<EnemyMove>().enemyNowHp -= debuffer.GetComponent<Debuffer_type1>().Damage;
            Instantiate(debuffer.GetComponent<Debuffer_type1>().debuffeffect, tr.position, Quaternion.identity, collision.gameObject.transform);
            Destroy(gameObject);
        }
        else if(collision.tag == "boss")
        {
            collision.gameObject.GetComponent<Boss>().BossNowHP -= debuffer.GetComponent<Debuffer_type1>().Damage;
            Destroy(gameObject);
        }
    }
}

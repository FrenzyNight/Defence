using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_bullet : MonoBehaviour
{

    private Vector2 point;
    private float angle;
    
    Transform tr;
    WaveManager wm;

    public float damage;
    public float bulletspeed;
    public float bulletnuckback;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        //turret = GameObject.FindWithTag("Player").GetComponent<Player_char>();

        point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.x -= tr.transform.position.x;
        point.y -= tr.transform.position.y;
        angle = Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg - 90f;
        tr.eulerAngles = new Vector3(0,0,angle);
    }

    // Update is called once per frame
    void Update()
    {
        if(!wm.isStop)
            tr.Translate(point.normalized * bulletspeed * Time.deltaTime,0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            collision.gameObject.GetComponent<EnemyMove>().enemyNowHp -= damage;
            collision.gameObject.GetComponent<Transform>().Translate(Vector2.right * bulletnuckback);
            
            Destroy(gameObject);
        }
        else if(collision.tag == "blaster")
        {
            collision.gameObject.GetComponent<Blaster>().HP -= damage;
            Destroy(gameObject);
        }
        else if(collision.tag == "rock")
        {
            collision.gameObject.GetComponent<RockMove>().HP -= damage;
            Destroy(gameObject);
        }
        else if(collision.tag == "boss")
        {
            collision.gameObject.GetComponent<Boss>().BossNowHP -= damage;
            Destroy(gameObject);
        }
    }
}

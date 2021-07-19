using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private Vector2 point;
    private float angle;
    
    Transform tr;
    WaveManager wm;
    Player_char player;
    

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();

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
            tr.Translate(point.normalized * player.bulletspeed * Time.deltaTime,0);
        //transform.position = Vector2.MoveTowards(transform.position, point, Time.deltaTime * DataManager.Instance.bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            collision.gameObject.GetComponent<EnemyMove>().enemyNowHp -= player.damage;
            collision.gameObject.GetComponent<Transform>().Translate(Vector2.right * player.bulletnuckback);
            
            Destroy(gameObject);
        }
        else if(collision.tag == "blaster")
        {
            collision.gameObject.GetComponent<Blaster>().HP -= player.damage;
            Destroy(gameObject);
        }
        else if(collision.tag == "rock")
        {
            collision.gameObject.GetComponent<RockMove>().HP -= player.damage;
            Destroy(gameObject);
        }
        else if(collision.tag == "bomb")
        {
            collision.gameObject.GetComponent<BombMove>().bombHP -= player.damage;
            Destroy(gameObject);
        }
        else if(collision.tag == "boss")
        {
            collision.gameObject.GetComponent<Boss>().BossNowHP -= player.damage;
            Destroy(gameObject);
        }
    }

}

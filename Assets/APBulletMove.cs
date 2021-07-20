using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class APBulletMove : MonoBehaviour
{
    private Vector2 point;
    private float angle;
    
    Transform tr;
    Transform effectTr;
    WaveManager wm;
    Player_char player;

    public int piercing;
    public int pierCnt;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();

        point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.x -= tr.transform.position.x;
        point.y -= tr.transform.position.y;
        angle = Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg;
        tr.eulerAngles = new Vector3(0,0,angle);
    }

    // Update is called once per frame
    void Update()
    {
        if(!wm.isStop)
            tr.Translate(point.normalized * player.bulletspeed * Time.deltaTime,0);
        
        if(pierCnt >= piercing)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            effectTr = collision.gameObject.GetComponent<Transform>();
            collision.gameObject.GetComponent<EnemyMove>().enemyNowHp -= (player.damage * 1.1f);
            //collision.gameObject.GetComponent<Transform>().Translate(Vector2.right * player.bulletnuckback);
            collision.gameObject.transform.DOMove( new Vector2(effectTr.position.x+(player.bulletnuckback * 0.9f), effectTr.position.y), 0.05f);
            collision.gameObject.transform.DOShakePosition(0.05f, 0.1f , 1, 180f);
            pierCnt++;
            
        }
        else if(collision.tag == "blaster")
        {
            collision.gameObject.GetComponent<Blaster>().HP -= (player.damage * 1.1f);
            pierCnt++;
        }
        else if(collision.tag == "rock")
        {
            collision.gameObject.GetComponent<RockMove>().HP -= (player.damage * 1.1f);
            pierCnt++;
        }
        else if(collision.tag == "bomb")
        {
            collision.gameObject.GetComponent<BombMove>().bombHP -= (player.damage * 1.1f);
            pierCnt++;
        }
        else if(collision.tag == "boss")
        {
            collision.gameObject.GetComponent<Boss>().BossNowHP -= (player.damage * 1.1f);
            pierCnt++;
        }
    }
}

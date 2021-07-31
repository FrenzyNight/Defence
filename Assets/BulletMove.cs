using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletMove : MonoBehaviour
{
    private Vector2 point;
    private float angle;
    
    Transform tr;
    Transform effectTr;
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
            effectTr = collision.gameObject.GetComponent<Transform>();
            collision.gameObject.transform.DOMove( new Vector2(effectTr.position.x+player.bulletnuckback, effectTr.position.y), 0.05f);
            
            if(Random.Range(0,101) <= player.criticalrate) // 크리티컬 발생
            {   
                collision.gameObject.GetComponent<EnemyMove>().enemyNowHp -= player.FINAL_DMG*1.5f;
                collision.gameObject.transform.DOShakePosition(0.05f, 0.5f , 1, 180f);
            }
            else
            {
                collision.gameObject.GetComponent<EnemyMove>().enemyNowHp -= player.FINAL_DMG;
                collision.gameObject.transform.DOShakePosition(0.05f, 0.1f , 1, 180f);
            }
        
            Destroy(gameObject);
        }
        else if(collision.tag == "blaster")
        {
            if(Random.Range(0,101) <= player.criticalrate) // 크리티컬 발생
                collision.gameObject.GetComponent<Blaster>().HP -= player.FINAL_DMG *1.5f;
            else
                collision.gameObject.GetComponent<Blaster>().HP -= player.FINAL_DMG;
            Destroy(gameObject);
        }
        else if(collision.tag == "rock")
        {
            if(Random.Range(0,101) <= player.criticalrate)
                collision.gameObject.GetComponent<RockMove>().HP -= player.FINAL_DMG * 1.5f;
            else
                collision.gameObject.GetComponent<RockMove>().HP -= player.FINAL_DMG;
            Destroy(gameObject);
        }
        else if(collision.tag == "bomb")
        {
            if(Random.Range(0,101) <= player.criticalrate)
                collision.gameObject.GetComponent<BombMove>().bombHP -= player.FINAL_DMG * 1.5f;
            else
                collision.gameObject.GetComponent<BombMove>().bombHP -= player.FINAL_DMG;

            Destroy(gameObject);
        }
        else if(collision.tag == "boss")
        {
            if(Random.Range(0,101) <= player.criticalrate)
                collision.gameObject.GetComponent<Boss>().BossNowHP -= player.FINAL_DMG * 1.5f;
            else
                collision.gameObject.GetComponent<Boss>().BossNowHP -= player.FINAL_DMG;

            Destroy(gameObject);
        }
    }

}

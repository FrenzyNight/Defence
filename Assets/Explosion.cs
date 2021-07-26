using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float damageRat; // 데미지 배율

    public float gravity; // 빨아들이는 힘
    public float effectTime; // 지속시간
    Player_char player;
    Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();
        Destroy(gameObject, effectTime);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            if(Random.Range(0,101) <= player.criticalrate) // 크리티컬 발생
            {
                collision.gameObject.GetComponent<EnemyMove>().enemyNowHp -= (player.damage*1.5f * damageRat);
            }
            else
            {
                collision.gameObject.GetComponent<EnemyMove>().enemyNowHp -= player.damage* damageRat;
            }
        }
        else if(collision.tag == "blaster")
        {
            if(Random.Range(0,101) <= player.criticalrate) // 크리티컬 발생
                collision.gameObject.GetComponent<Blaster>().HP -= player.damage *1.5f* damageRat;
            else
                collision.gameObject.GetComponent<Blaster>().HP -= player.damage* damageRat;
        }
        else if(collision.tag == "rock")
        {
            if(Random.Range(0,101) <= player.criticalrate)
                collision.gameObject.GetComponent<RockMove>().HP -= player.damage * 1.5f * damageRat;
            else
                collision.gameObject.GetComponent<RockMove>().HP -= player.damage * damageRat;
        }
        else if(collision.tag == "bomb")
        {
            if(Random.Range(0,101) <= player.criticalrate)
                collision.gameObject.GetComponent<BombMove>().bombHP -= player.damage * 1.5f * damageRat;
            else
                collision.gameObject.GetComponent<BombMove>().bombHP -= player.damage * damageRat;
        }
        else if(collision.tag == "boss")
        {
            if(Random.Range(0,101) <= player.criticalrate)
                collision.gameObject.GetComponent<Boss>().BossNowHP -= player.damage * 1.5f * damageRat;
            else
                collision.gameObject.GetComponent<Boss>().BossNowHP -= player.damage * damageRat;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            Vector2 v = tr.position - collision.gameObject.GetComponent<Transform>().position;
            collision.gameObject.GetComponent<Transform>().Translate(v.normalized *gravity* Time.deltaTime);
        }
    }
}

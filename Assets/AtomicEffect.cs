using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AtomicEffect : MonoBehaviour
{
    public float damageRat; // 데미지 배율

    public float effectTime; // 지속시간
    Player_char player;
    Transform tr;
    SpriteRenderer sr;


    void Start()
    {
        tr = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();
        sr = GetComponent<SpriteRenderer>();
        sr.DOColor(new Color(sr.color.r, sr.color.g, sr.color.b, 0), effectTime);
        Destroy(gameObject, effectTime+0.05f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            if(Random.Range(0,101) <= player.criticalrate) // 크리티컬 발생
            {
                collision.gameObject.GetComponent<EnemyMove>().enemyNowHp -= (player.FINAL_DMG * 1.5f * damageRat/100);
            }
            else
            {
                collision.gameObject.GetComponent<EnemyMove>().enemyNowHp -= player.FINAL_DMG * damageRat/100;
            }
        }
        else if(collision.tag == "blaster")
        {
            if(Random.Range(0,101) <= player.criticalrate) // 크리티컬 발생
                collision.gameObject.GetComponent<Blaster>().HP -= player.FINAL_DMG * 1.5f * damageRat/100;
            else
                collision.gameObject.GetComponent<Blaster>().HP -= player.FINAL_DMG * damageRat/100;
        }
        else if(collision.tag == "rock")
        {
            if(Random.Range(0,101) <= player.criticalrate)
                collision.gameObject.GetComponent<RockMove>().HP -= player.FINAL_DMG * 1.5f * damageRat/100;
            else
                collision.gameObject.GetComponent<RockMove>().HP -= player.FINAL_DMG *  damageRat/100;
        }
        else if(collision.tag == "bomb")
        {
            if(Random.Range(0,101) <= player.criticalrate)
                collision.gameObject.GetComponent<BombMove>().bombHP -= player.FINAL_DMG * 1.5f * damageRat/100;
            else
                collision.gameObject.GetComponent<BombMove>().bombHP -= player.FINAL_DMG * damageRat/100;
        }
        else if(collision.tag == "boss")
        {
            if(Random.Range(0,101) <= player.criticalrate)
                collision.gameObject.GetComponent<Boss>().BossNowHP -= player.FINAL_DMG * 1.5f * damageRat/100;
            else
                collision.gameObject.GetComponent<Boss>().BossNowHP -= player.FINAL_DMG * damageRat/100;
        }
    }
}

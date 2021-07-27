using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeeleAttack : MonoBehaviour
{
    Player_char player;
    EnemyMove EM;
    public float enemyDamage;

    public float attackspeed;

    // Start is called before the first frame update
    void Start()
    {
        EM = GetComponent<EnemyMove>();
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            EM.isMove = false;
            InvokeRepeating("EnemyAttack",1.5f, attackspeed);
        }       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            EM.isMove = true;
            CancelInvoke("EnemyAttack");
        }
    }

    void EnemyAttack()
    {
        CinemachineShake.Instance.ShakeCamera(0.5f, 0.1f);
        player.nowHp -= enemyDamage;
        player.ultimateGauge += 1;
    }
}

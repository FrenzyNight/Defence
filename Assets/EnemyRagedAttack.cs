using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRagedAttack : MonoBehaviour
{

    public bool isAttack;
    Vector2 range;
    Player_char player;
    Transform tr;
    Transform playerTr;
    EnemyMove EM;

    public float enemyDamage;
    public float attackrange;

    public float attackspeed;
    // Start is called before the first frame update
    void Start()
    {
        EM = GetComponent<EnemyMove>();
        tr = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        canAttack();
    }

    void canAttack()
    {
        range = tr.position - playerTr.position;

        if(range.sqrMagnitude <= attackrange*attackrange && !isAttack)
        {
            EM.isMove = false;
            isAttack = true;
            InvokeRepeating("EnemyAttack",1.5f, attackspeed);
        }
        else if(range.sqrMagnitude > attackrange*attackrange && isAttack)
        {
            EM.isMove = true;
            isAttack= false;
            CancelInvoke("EnemyAttack");
        }
    }

    void EnemyAttack()
    {
        CinemachineShake.Instance.ShakeCamera(0.5f,0.1f);
        player.nowHp -= enemyDamage;
        player.ultimateGauge += 1;
    }

}

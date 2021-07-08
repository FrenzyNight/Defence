using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    Transform tr;
    public float speed;
    public float EnemyHp;
    public float enemyNowHp;

    public float enemyDamage;

    public float attackspeed;

    
    public GameObject HpImg;
    private GameObject HP;

    private Image Bar;

    Player_char player;

    public bool isMove;
    public bool isAttack;


    //private Transform HpTrans;

    // Start is called before the first frame update
    void Start()
    { 
        EnemyHp = GameObject.Find("WaveManager").GetComponent<CreateEnemy>().monsterMaxHp;
        isMove = true;
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();
        tr = GetComponent<Transform>();
        enemyNowHp = EnemyHp;
        
        HP =  Instantiate(HpImg, tr.position, Quaternion.identity, GameObject.Find("Canvas_Enemy").transform);  
        
        Bar = HP.GetComponent<Image>();
        Bar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMove)
        {
            tr.Translate(Vector2.left * speed * Time.deltaTime);
            HP.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            HP.GetComponent<Transform>().Translate(new Vector2(10f,95f));
        }
        Bar.fillAmount = enemyNowHp / EnemyHp ;
        Death();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            isMove = false;
            
            InvokeRepeating("EnemyAttack",1.5f, attackspeed);
        }

       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            isMove = true;
            CancelInvoke("EnemyAttack");
        }
    }

    void EnemyAttack()
    {
        player.nowHp -= enemyDamage;
    }


    void Death()
    {
        if(enemyNowHp <= 0)
        {
            Destroy(Bar);
            Destroy(HP);
            Destroy(gameObject);
            DataManager.Instance.money += 2;
            player.nowExp += 5;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    Transform tr;
    private float speed;
    public float EnemyHp;

    
    public GameObject HpImg;
    private GameObject HP;

    private Image Bar;

    // Start is called before the first frame update
    void Start()
    { 
        tr = GetComponent<Transform>();
        EnemyHp = DataManager.Instance.enemyMaxHp;
        speed = DataManager.Instance.enemySpeed;
        HP =  Instantiate(HpImg, new Vector2(0,0), Quaternion.identity, GameObject.Find("Canvas_Enemy").transform);  
        Bar = HP.GetComponent<Image>();
        Bar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        tr.Translate(Vector2.left * speed * Time.deltaTime);
        HP.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        
        Bar.fillAmount = EnemyHp / DataManager.Instance.enemyMaxHp;
        Death();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "bullet")
        {
            EnemyHp -= DataManager.Instance.bulletDamge;
            
            Destroy(collision.gameObject);
        }

        if(collision.tag == "Player")
        {
            DataManager.Instance.NowHp -= DataManager.Instance.enemyDamge;
            Destroy(Bar);
            Destroy(HP);
            Destroy(gameObject);
        }
    }


    void Death()
    {
        if(EnemyHp <= 0)
        {
            Destroy(Bar);
            Destroy(HP);
            Destroy(gameObject);
            DataManager.Instance.money += 2;
        }
    }
}

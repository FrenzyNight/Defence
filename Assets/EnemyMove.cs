using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Transform tr;
    private float speed;
    public float EnemyHp;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        EnemyHp = DataManager.Instance.enemyMaxHp;
        speed = DataManager.Instance.enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        tr.Translate(Vector2.left * speed * Time.deltaTime);
        Death();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "bullet")
        {
            EnemyHp -= DataManager.Instance.bulletDamge;
            Destroy(collision.gameObject);
        }
    }

    void Death()
    {
        if(EnemyHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}

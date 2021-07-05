using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private Vector2 point;
    
    Transform tr;
    

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.x -= tr.transform.position.x;
        point.y -= tr.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(!DataManager.Instance.isStop)
            tr.Translate(point.normalized * DataManager.Instance.bulletSpeed * Time.deltaTime);
        //transform.position = Vector2.MoveTowards(transform.position, point, Time.deltaTime * DataManager.Instance.bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            collision.gameObject.GetComponent<EnemyMove>().EnemyHp -= DataManager.Instance.bulletDamge;
            collision.gameObject.GetComponent<Transform>().Translate(Vector2.right * DataManager.Instance.bulletNuckback);
            
            Destroy(gameObject);
        }
    }

}

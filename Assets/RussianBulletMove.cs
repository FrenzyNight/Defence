using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RussianBulletMove : MonoBehaviour
{

    Vector2 point;
    Transform tr;
    WaveManager wm;
    Player_char player;

    public float skillspeed;

    public float skilldamagerate;
    private float angle;
    private bool isFire;

    

    // Start is called before the first frame update
    void Start()
    {
        isFire = false;
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
        Rotation();
        Fire();
        if(Input.GetMouseButtonUp(0) && !isFire)
        {
            isFire = true;
        }
    }

    void Fire()
    {
        if(!wm.isStop && isFire)
            tr.Translate(point.normalized * skillspeed * Time.deltaTime, 0);
    }

    void Rotation()
    {
        if(!wm.isStop && !isFire)
        {
            if(Input.GetMouseButton(0))
            {
                if(EventSystem.current.IsPointerOverGameObject() == false)
                {
                    point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    point.x -= tr.transform.position.x;
                    point.y -= tr.transform.position.y;
                    angle = Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg;
                    tr.eulerAngles = new Vector3(0,0,angle);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            collision.gameObject.GetComponent<EnemyMove>().enemyNowHp -= player.damage * (skilldamagerate / 100);
            //collision.gameObject.GetComponent<Transform>().Translate(Vector2.right * player.bulletnuckback);
            
            //Destroy(gameObject);
        }
        else if(collision.tag == "blaster")
        {
            collision.gameObject.GetComponent<Blaster>().HP -= player.damage * (skilldamagerate / 100);
            
        }
        else if(collision.tag == "rock")
        {
            collision.gameObject.GetComponent<RockMove>().HP -= player.damage * (skilldamagerate / 100);
            
        }
        else if(collision.tag == "bomb")
        {
            collision.gameObject.GetComponent<BombMove>().bombHP -= player.damage * (skilldamagerate / 100);
            
        }
        else if(collision.tag == "boss")
        {
            collision.gameObject.GetComponent<Boss>().BossNowHP -= player.damage * (skilldamagerate / 100);
            
        }
    }
}

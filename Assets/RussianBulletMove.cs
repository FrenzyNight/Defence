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
            CinemachineShake.Instance.ShakeCamera(1f,0.5f);
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
                    CinemachineShake.Instance.ShakeCamera(0.3f,0.1f);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            if(Random.Range(0,101) <= player.criticalrate) // 크리티컬 발생
            {
                collision.gameObject.GetComponent<EnemyMove>().enemyNowHp -= (player.FINAL_DMG * 1.5f * (skilldamagerate / 100));
            }
            else
            {
                collision.gameObject.GetComponent<EnemyMove>().enemyNowHp -= player.FINAL_DMG * (skilldamagerate / 100);
            }
        }
        else if(collision.tag == "blaster")
        {
            if(Random.Range(0,101) <= player.criticalrate) // 크리티컬 발생
                collision.gameObject.GetComponent<Blaster>().HP -= player.FINAL_DMG * 1.5f * (skilldamagerate / 100);
            else
                collision.gameObject.GetComponent<Blaster>().HP -= player.FINAL_DMG * (skilldamagerate / 100);
        }
        else if(collision.tag == "rock")
        {
            if(Random.Range(0,101) <= player.criticalrate)
                collision.gameObject.GetComponent<RockMove>().HP -= player.FINAL_DMG * 1.5f * (skilldamagerate / 100);
            else
                collision.gameObject.GetComponent<RockMove>().HP -= player.FINAL_DMG *  (skilldamagerate / 100);
        }
        else if(collision.tag == "bomb")
        {
            if(Random.Range(0,101) <= player.criticalrate)
                collision.gameObject.GetComponent<BombMove>().bombHP -= player.FINAL_DMG * 1.5f * (skilldamagerate / 100);
            else
                collision.gameObject.GetComponent<BombMove>().bombHP -= player.FINAL_DMG * (skilldamagerate / 100);
        }
        else if(collision.tag == "boss")
        {
            if(Random.Range(0,101) <= player.criticalrate)
                collision.gameObject.GetComponent<Boss>().BossNowHP -= player.FINAL_DMG * 1.5f * (skilldamagerate / 100);
            else
                collision.gameObject.GetComponent<Boss>().BossNowHP -= player.FINAL_DMG * (skilldamagerate / 100);
        }
    }
}

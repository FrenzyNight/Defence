using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    Transform tr;
    WaveManager wm;
    Player_char player;
    Transform playerTr;
    Vector2 point;
    GameObject child;

    private float angle;

    public float damage;
    public float HP;
    public float laserSpeed;
    bool isFire;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        point = playerTr.position;
        point.x -= tr.transform.position.x;
        point.y -= tr.transform.position.y;
        angle = Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg;
        tr.eulerAngles = new Vector3(0,0,angle + 90f );

        child = transform.GetChild(0).gameObject;

      
        Invoke("Attack", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0)
        {
            CancelInvoke("Attac");
            Destroy(gameObject);
        }
        
        if(!wm.isStop && isFire)
        {
            child.transform.Translate(point.normalized * laserSpeed * Time.deltaTime,0);
        }
    }

    void Attack()
    {
        isFire = true;
        player.nowHp -= damage;
        Destroy(gameObject, 0.3f);
    }


}

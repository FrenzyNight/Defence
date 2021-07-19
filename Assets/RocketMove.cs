using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RocketMove : MonoBehaviour
{
    Vector2 point; //목표 지점
    float angle;
    Transform tr;
    WaveManager wm;
    Player_char player;
    Transform effectTr;

    public GameObject rocketeffect;

    public float rocketSpeed;
    public float nuckback;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();

        point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.x -= tr.transform.position.x;
        point.y -= tr.transform.position.y;
        angle = Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg ;
        tr.eulerAngles = new Vector3(0,0,angle);
    }

    // Update is called once per frame
    void Update()
    {
        if(!wm.isStop)
            tr.Translate(point.normalized * rocketSpeed * Time.deltaTime,0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {   
            effectTr = collision.gameObject.GetComponent<Transform>();
            collision.gameObject.transform.DOMove( new Vector2(effectTr.position.x+nuckback, effectTr.position.y), 0.3f);
            
            gameObject.SetActive(false);
            Invoke("CreateEffect", 0.2f);
            
            
        }
        else if(collision.tag == "blaster")
        {
            effectTr = collision.gameObject.GetComponent<Transform>();
            Instantiate(rocketeffect, effectTr.position, Quaternion.identity);
            Destroy(gameObject,0.1f);
        }
        else if(collision.tag == "rock")
        {
            effectTr = collision.gameObject.GetComponent<Transform>();
            Instantiate(rocketeffect, effectTr.position, Quaternion.identity);
            Destroy(gameObject,0.1f);
        }
        else if(collision.tag == "bomb")
        {
            effectTr = collision.gameObject.GetComponent<Transform>();
            Instantiate(rocketeffect, effectTr.position, Quaternion.identity);
            Destroy(gameObject,0.1f);
        }
        else if(collision.tag == "boss")
        {
            effectTr = collision.gameObject.GetComponent<Transform>();
            Instantiate(rocketeffect, effectTr.position, Quaternion.identity);
            Destroy(gameObject,0.1f);
        }
    }

    
    void CreateEffect()
    {
        Instantiate(rocketeffect, effectTr.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

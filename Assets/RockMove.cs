using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMove : MonoBehaviour
{
    Transform tr;
    Transform playerTr;
    Transform wallTr;
    WaveManager wm;
    Player_char player;

    public float gravity = 9.8f;
    public float Angle;
    private float correctAngle;
    public float rockSpeed;

    public float damage;
    public float HP;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        wallTr = GameObject.FindWithTag("Wall").GetComponent<Transform>();


        Vector2 v = playerTr.position - tr.position;
        v.x *= (-1);
        correctAngle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg ;
        
        StartCoroutine(Parabola());
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0 || !wm.isWave)
        {
            Destroy(gameObject);
        }

        
    }

    IEnumerator Parabola()
    {
        yield return new WaitForSeconds(0.08f);

        Angle = Random.Range(20f, 35.5f);

        //Angle += 270;

        float target_Distance = Vector2.Distance(playerTr.position, tr.position);

        float projectile_Velocity = target_Distance / (Mathf.Sin(2*Angle*Mathf.Deg2Rad) / gravity) ;

        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos((Angle + correctAngle) * Mathf.Deg2Rad);

        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin((Angle + correctAngle)  * Mathf.Deg2Rad);

        float flightDuration = target_Distance / Vx;

        float elapse_time = 0;

        Vx *= (-1);

        while(true)
        {
            if(!wm.isStop && wm.isWave)
            {
                tr.Translate(new Vector2((Vx - (gravity * Mathf.Sin(correctAngle * Mathf.Deg2Rad) * elapse_time)), (Vy - (gravity * Mathf.Cos(correctAngle * Mathf.Deg2Rad) * elapse_time )))* Time.deltaTime * rockSpeed);
                 
                elapse_time += Time.deltaTime * rockSpeed ;

                yield return null;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            player.nowHp -= damage;
            Destroy(gameObject, 0.1f);
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMove : MonoBehaviour
{
    Transform tr;
    WaveManager wm;
    Player_char player;
    public float bombDamage;
    public float bombSpeed;
    public float bombHP;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();

    }

    // Update is called once per frame
    void Update()
    {
        if(bombHP <= 0 || !wm.isWave)
        {
            Destroy(gameObject);
        }

        Moving();
    }

    void Moving()
    {
        if(!wm.isStop)
        {
            tr.Translate(Vector2.left * bombSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            CinemachineShake.Instance.ShakeCamera(0.7f,0.3f);
            player.nowHp -= bombDamage;
            Destroy(gameObject, 0.1f);
        }
    }
}

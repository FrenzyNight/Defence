using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject Blaster;
    public GameObject Rock;
    public GameObject Bomb;

    Transform tr;
    Transform playerTr;
    Player_char player;
    WaveManager wm;

    Vector2 spontr;
    
    private float randY;
    private float plusminus;
    
    private int rnd;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();

        StartCoroutine(AttackStart());
    }


    IEnumerator AttackStart()
    {
        while(true)
        {
            //3~9개 블라스터 소환
            rnd = Random.Range(3,10);
            for(int i=0;i<rnd;i++)
            {
                if(wm.isWave && !wm.isStop)
                {
                    yield return new WaitForSeconds(0.5f);
                    randY = Random.Range(0.5f,5f);
                    plusminus = Random.Range(0,2);
                    if(plusminus == 0)
                    {
                        randY *= (-1);
                    }
                    spontr = new Vector2(tr.position.x - 3f, tr.position.y + randY);
                    Instantiate(Blaster, spontr, Quaternion.identity);
                }
            }
            yield return new WaitForSeconds(2f);
            //2초간 후딜레이

            // 3~9개의 투석 투척
            rnd = Random.Range(3,10);
            for(int i=0;i<rnd;i++)
            {
                if(wm.isWave && !wm.isStop)
                {
                    yield return new WaitForSeconds(0.6f);
                    randY = Random.Range(0.5f,4f);
                    plusminus = Random.Range(0,2);
                    if(plusminus == 0)
                    {
                        randY *= (-1);
                    }
                    spontr = new Vector2(tr.position.x, tr.position.y + randY);
                    Instantiate(Rock, spontr, Quaternion.identity);
                }
            }
            yield return new WaitForSeconds(2f);
            //후딜레이 2초

            
            yield return new WaitForSeconds(0.5f);
        
            //spontr = new Vector2(tr.position.x, tr.position.y + randY);
            for(int i = 0;i<5;i++)
            {
                Instantiate(Bomb, new Vector2(tr.position.x, tr.position.y + 4f - (2f * i)), Quaternion.identity);
            }
            yield return new WaitForSeconds(2f);
            //후딜레이 2초
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffer_type3 : MonoBehaviour
{
    public GameObject effect;
    GameObject effectimg;
    Player_char player;
    Transform playerTr;
    WaveManager wm;

    public float cooltime;
    public float keeptime; //유지시간
    public float buff_value; // 비율
    float firstDelay; //랜덤 선딜
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
    
        Golden();
        StartCoroutine(Buff());
    }

    void Golden()
    {
        if(GetComponent<MercenaryInfo>().step == 2)
            cooltime /= 2;
    }

    IEnumerator Buff()
    {
        while(true)
        {
            if(!wm.isStop && wm.isWave)
            {
                firstDelay = Random.Range(0.2f,1.5f);
                yield return new WaitForSeconds(firstDelay);

                
                effectimg = Instantiate(effect, new Vector2(playerTr.position.x, playerTr.position.y + 2f), Quaternion.identity);
                player.damage *= (1 + buff_value/100);
                yield return new WaitForSeconds(keeptime);
                
                Destroy(effectimg);
                player.damage /= (1 + buff_value/100);
                
                yield return new WaitForSeconds(cooltime);
            }

            yield return null;
        }
    }
}

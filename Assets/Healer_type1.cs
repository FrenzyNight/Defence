using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer_type1 : MonoBehaviour
{
    public GameObject effect;
    Player_char player;
    Transform playerTr;
    WaveManager wm;

    public float cooltime;
    public float delay;
    public float cycle;
    public float healvalue;
    float firstDelay;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        StartCoroutine(Healing());
    }

    // Update is called once per frame

    IEnumerator Healing()
    {
        while(true)
        {
            if(!wm.isStop && wm.isWave)
            {
                firstDelay = Random.Range(0.2f,1.5f);
                yield return new WaitForSeconds(firstDelay);

                for(int i=0;i<cycle;i++)
                {
                    Instantiate(effect, playerTr.position, Quaternion.identity);
                    player.nowHp += healvalue;
                    yield return new WaitForSeconds(delay);
                }
                
                yield return new WaitForSeconds(cooltime);
            }

            yield return null;
        }
    }


}

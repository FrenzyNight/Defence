using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float BossMaxHP;
    public float BossNowHP;
    public GameObject HpImg;
    private GameObject HP;
    Player_char player;
    Transform tr;
    WaveManager wm;
    private Image Bar;
    
    // Start is called before the first frame update
    void Start()
    {
        BossNowHP = BossMaxHP;
        tr = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();
        HP =  Instantiate(HpImg, tr.position, Quaternion.identity, GameObject.Find("Canvas_Enemy").transform);  
        Bar = HP.GetComponent<Image>();
        Bar.fillAmount = 1;
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        HP.transform.position = Camera.main.WorldToScreenPoint(transform. position);
        //HP.GetComponent<Transform>().Translate(new Vector2(10f,95f));
        Bar.fillAmount = BossNowHP / BossMaxHP;
        Death();
    }

    void Death()
    {
        if(BossNowHP <= 0)
        {
            Destroy(Bar);
            Destroy(HP);
            Destroy(gameObject);
            //gameObject.SetActive(false);
            DataManager.Instance.money += 500;
            player.nowExp += 100;
            player.ultimateGauge += 100;
            wm.Coin += 400;
        }
    }
}

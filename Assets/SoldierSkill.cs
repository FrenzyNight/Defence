using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SoldierSkill : MonoBehaviour
{
    public GameObject rocketPrefab;
    public GameObject APBulletPrefab;
    public GameObject NormalBulletPrefab;

    public GameObject ApEffect;

    GameObject apeffectimg;
    Transform tr;
    Vector2 mousePosition;

    public Button skillBtn1, skillBtn2, skillBtn3;

    //public bool isSkill;

    Player_char player;
    Transform playerTr;

    public float sk1ct, sk2ct, sk3ct; // skill 1 cool time
    public bool sk1ready, sk2ready, sk3ready;
    
    WaveManager wm;

    
    // Start is called before the first frame update
    void Start()
    {
        

        tr = GetComponent<Transform>();
        player = GetComponent<Player_char>();
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        playerTr = GetComponent<Transform>();
        
        SetButton();
    }

    // Update is called once per frame
    void Update()
    {
        if(sk1ready)
        {
            StartCoroutine(Skill1trigger());
        }
        else if(sk2ready)
        {
            StartCoroutine(Skill2trigger());
        }
        else if(sk3ready)
        {
            Skill3trigger();
        }
    }

    public void SetButton()
    {
        skillBtn1 = GameObject.Find("Canvas_skill").transform.GetChild(0).transform.Find("Button_skill1").GetComponent<Button>();
        skillBtn2 = GameObject.Find("Canvas_skill").transform.GetChild(0).transform.Find("Button_skill2").GetComponent<Button>();
        skillBtn3 = GameObject.Find("Canvas_skill").transform.GetChild(0).transform.Find("Button_skill3").GetComponent<Button>();

        skillBtn1.onClick.AddListener(Skill1);
        skillBtn2.onClick.AddListener(Skill2);
        skillBtn3.onClick.AddListener(Skill3);
    }

    public void Skill1() // 로켓 런처
    {
        if(!player.isSkill)
        {
            skillBtn1.interactable = false;
            sk1ready = true;
            player.isSkill = true;
        }
    }

    IEnumerator Skill1trigger()
    {
        if(Input.GetMouseButtonDown(0) && wm.isWave)
        {
            if(EventSystem.current.IsPointerOverGameObject() == false)
            {
                sk1ready = false;

                Instantiate(rocketPrefab, tr.position , Quaternion.identity);

                yield return new WaitForSeconds(0.3f); //후딜
                player.isSkill = false;
                
                yield return new WaitForSeconds(sk1ct);
                skillBtn1.interactable = true;
            }
        }
    }

    public void Skill2() // 철갑탄 : 기본공격의 탄환을 5초(임시)간 데미지1.1배 넉백 0.9배의 AP탄으로 변경한다. 탄환은 한명의 적을 관통한다
    {
        if(!player.isSkill)
        {
            skillBtn2.interactable = false;
            sk2ready = true;
            player.isSkill = true;
        }
    }

    IEnumerator Skill2trigger()
    {
        
        
        sk2ready = false;

        yield return new WaitForSeconds(0.4f); //선딜

        apeffectimg = Instantiate(ApEffect, new Vector2(playerTr.position.x, playerTr.position.y + 2f), Quaternion.identity);

        player.bulletPrefab = APBulletPrefab;
        player.isSkill = false;
        yield return new WaitForSeconds(5f); // 지속시간
        
        Destroy(apeffectimg);

        player.isSkill = true;
        yield return new WaitForSeconds(0.4f); // 후딜
        player.bulletPrefab = NormalBulletPrefab;
        player.isSkill = false;


        yield return new WaitForSeconds(sk2ct);
        skillBtn2.interactable = true;
        
        
    }


    public void SKill2CoolDown()
    {
        StartCoroutine(Skill2Delay());
    }
    IEnumerator Skill2Delay()
    {
        yield return new WaitForSeconds(sk2ct);
        skillBtn2.interactable = true;
    }

    public void Skill3() // 러시안룰렛 : 6가지 랜덤한 효과(이펙트)를 가진 강력한 총할중 하나 발사
    {   
        if(!player.isSkill)
        {
            skillBtn3.interactable = false;
            sk3ready = true;
            player.isSkill = true;
        }

    }

    void Skill3trigger()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject() == false)
            {
                sk3ready = false;
               
            }
        }
    }

    public void SKill3CoolDown()
    {
        StartCoroutine(Skill3Delay());
    }
    IEnumerator Skill3Delay()
    {
        yield return new WaitForSeconds(sk3ct);
        skillBtn3.interactable = true;
    }
}

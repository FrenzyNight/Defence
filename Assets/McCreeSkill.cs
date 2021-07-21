using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class McCreeSkill : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject Target;
    public GameObject sk3tg;
    public GameObject sk3bullet;
    Transform tr;
    public Vector2 mousePosition;

    public Button skillBtn1, skillBtn2, skillBtn3;

    Player_char player;

    public float sk1ct, sk2ct, sk3ct; // skill 1 cool time
    public bool sk1ready, sk2ready, sk3ready;
    
    WaveManager wm;

    public AudioClip audioFire;

    AudioSource audioSource;
    
    
    Image skill3img;


    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioFire;

        tr = GetComponent<Transform>();
        player = GetComponent<Player_char>();
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        
        SetButton();
    }

    // Update is called once per frame
    void Update()
    {
        if(sk1ready)
        {
            Skill1trigger();
        }
        else if(sk2ready)
        {
            Skill2trigger();
        }
        else if(sk3ready)
        {
            Skill3trigger();
        }

        KeyMaping();

        
        skill3img.fillAmount = player.ultimateGauge / 100;
        Skill3Check();
    }

    void KeyMaping()
    {
        if(Input.GetKeyDown(KeyCode.Q) && skillBtn1.interactable == true && !sk1ready)
        {
            Skill1();
        }
        if(Input.GetKeyDown(KeyCode.W) && skillBtn2.interactable == true && !sk1ready)
        {
            Skill2();
        }
        if(Input.GetKeyDown(KeyCode.E) && skillBtn3.interactable == true && !sk1ready)
        {
            Skill3();
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

        skill3img = skillBtn3.GetComponent<Image>();
        skill3img.fillAmount = 0;
        skillBtn3.interactable = false;
    }

    public void Skill1() // 패닝 리볼버 : 빠른속도로 3발의 총알을 연사
    {
        if(!player.isSkill)
        {
            skillBtn1.interactable = false;
            sk1ready = true;
            player.isSkill = true;
        }
    }

    void Skill1trigger()
    {
        if(Input.GetMouseButtonDown(0) && wm.isWave)
        {
            if(EventSystem.current.IsPointerOverGameObject() == false)
            {
                sk1ready = false;

                StartCoroutine(Create3Bullet());
                
                player.isSkill = false;
                StartCoroutine(Skill1Delay());
                
            }
        }
    }

    IEnumerator Skill1Delay()
    {
        yield return new WaitForSeconds(sk1ct);
        skillBtn1.interactable = true;
    }

    IEnumerator Create3Bullet() 
    {
        for(int i=0;i<3;i++)
        {
            audioSource.Play();
            Instantiate(bulletPrefab, tr.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);

        }
    }

    public void Skill2() // 중력류탄 : 적들을 끌어모으는 수류탄을 던짐
    {
        if(!player.isSkill)
        {
            skillBtn2.interactable = false;
            sk2ready = true;
            player.isSkill = true;
        }
    }

    void Skill2trigger()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject() == false)
            {
                sk2ready = false;
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(Target, mousePosition , Quaternion.identity);
            }
        }
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

    void Skill3Check()
    {
        if(player.ultimateGauge >= 100)
        {
            player.ultimateGauge = 100;
            skillBtn3.interactable = true;
        }
    }

    public void Skill3() // 러시안룰렛 : 6가지 랜덤한 효과(이펙트)를 가진 강력한 총할중 하나 발사
    {   
        if(!player.isSkill)
        {
            skillBtn3.interactable = false;
            sk3ready = true;
            player.isSkill = true;
            player.ultimateGauge -= 100;
        }

    }

    void Skill3trigger()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject() == false)
            {
                sk3ready = false;
                Instantiate(sk3tg, tr.position , Quaternion.identity);
                Instantiate(sk3bullet, tr.position , Quaternion.identity);
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
        //skillBtn3.interactable = true;
    }
}

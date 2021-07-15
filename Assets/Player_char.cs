using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Player_char : MonoBehaviour
{
    public GameObject bulletPrefab; //총알 오브젝트
    public Vector2 mousePosition; //마우스 위치
    
    public AudioClip audioFire;

    AudioSource audioSource;
    
    WaveManager wm;
    public int level; //레벨
    public float growrate; //성장률
    public float damage; //데미지

    public float FPS; // 초당 발사수 Fire Per Seconds

    public float bulletspeed; //탄속

    public float bulletnuckback; //넉백치

    Transform tr; //캐릭터 위치

    public bool isDelay; //공격 후딜레이
    public bool isSkill; //스킬시전여부체크

    public float nowExp; // 현재 경험치
    public float maxExp; // 최대 경험치

    public float nowHp; // 현재 체력
    public float maxHp; // 최대 체력

    public float growHp;
    public float growDamage;

    public int characterIndex;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioFire;
        growrate += DataManager.Instance.growRateUpgrade[characterIndex];

        tr = GetComponent<Transform>();
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();

        nowHp = maxHp;
        growHp = maxHp * (growrate)/100;
        growDamage = damage * (growrate)/100;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        LevelUp();
        Died();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1/FPS);
        isDelay = false;
    }

    void Fire()
    {
        if(Input.GetMouseButton(0) && wm.isWave)
        {
            if(EventSystem.current.IsPointerOverGameObject() == false)
            {
                if(!isDelay && !wm.isStop && !isSkill)
                {
                    
                    isDelay = true;
                    audioSource.Play();
                    //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Instantiate(bulletPrefab, tr.position, Quaternion.identity);
            
                    StartCoroutine(Delay());
                }
            }
        }
    }


    void LevelUp()
    {
        if(nowExp >= maxExp)
        {
            //레벨업
            level+=1;
            nowExp -= maxExp;
            maxExp *= 1.05f;

            maxHp += growHp;
            damage += growDamage;
            nowHp = maxHp;
        }
    }

    void Died()
    {
        if(nowHp <= 0)
        {
            //You Die
            SceneManager.LoadScene("GameOver");
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    public GameObject BossPrefab;
    public GameObject[] prefabEnemy;
    public GameObject StagePanel;
    public GameObject SkillPanel;
    public Vector2 limitMin;
    public Vector2 limitMax;

    private bool isStart;
    private bool isCreate;
    private int count;
    public int mobcount;

    public float sponInterval;
    public float monsterMaxHp;
    private int enemyIndex;

    WaveManager wm;

    public Vector2 BossSponPoint;


    // Start is called before the first frame update
    void Start()
    {
        isStart = false;
        isCreate = false;

        wm = GetComponent<WaveManager>();
    }

    void Update()
    {
        if(wm.isWave&& isStart && !isCreate && GameObject.FindWithTag("enemy") == null && GameObject.FindWithTag("boss")==null ) // 웨이브 종료(클리어)시
        {
            wm.wave += 1;
            wm.isWave = false;
            isStart = false;
            StagePanel.SetActive(true);
            SkillPanel.SetActive(false);


        }
    }

    public void StartWave()
    {
        wm.isWave = true;
        isCreate = true;

        if(wm.wave % 3 == 0)
        {
            monsterMaxHp += 10f;
            sponInterval -= 0.05f;
        }

        count = 0;
        mobcount = 10 + (wm.wave * 3);
        if(wm.wave % 5 == 0 )// || wm.wave == 1) //boss전
        {
            CreateBoss();
        }
        else
        {
            InvokeRepeating("Create",1f, 0.1f + sponInterval);
        }
    }

    void Create()
    {
        if(!wm.isStop)
        {
            count++;

            float r = Random.Range(limitMin.y, limitMax.y);
            Vector2 creatingPoint = new Vector2(limitMin.x, r);
            enemyIndex = Random.Range(0,2);
            Instantiate(prefabEnemy[enemyIndex], creatingPoint, Quaternion.identity);  
            
            isStart = true;
            if(count >= mobcount)
            {
                isCreate = false;
                CancelInvoke("Create");
            }
        }
    }

    void CreateBoss()
    {
        isStart = true;
        Instantiate(BossPrefab, BossSponPoint, Quaternion.identity);
        isCreate = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(limitMin, limitMax);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(BossSponPoint, 0.2f);
    }

}

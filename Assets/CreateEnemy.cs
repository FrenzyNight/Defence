using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    public GameObject prefabEnemy;
    public GameObject StagePanel;
    public Vector2 limitMin;
    public Vector2 limitMax;

    private bool isStart;
    private bool isCreate;
    private int count;
    public int mobcount;


    // Start is called before the first frame update
    void Start()
    {
        isStart = false;
        isCreate = false;
    }

    void Update()
    {
        if(DataManager.Instance.isWave&& isStart && !isCreate && GameObject.FindWithTag("enemy") == null) // 웨이브 종료(클리어)시
        {
            DataManager.Instance.Wave += 1;
            DataManager.Instance.isWave = false;
            isStart = false;
            StagePanel.SetActive(true);
        }
    }

    public void StartWave()
    {
        DataManager.Instance.isWave = true;
        isCreate = true;

        if(DataManager.Instance.Wave % 2 == 0)
        {
            DataManager.Instance.enemyDamge *= 1.2f;
        }
        
        if(DataManager.Instance.Wave % 3 == 0)
        {
            DataManager.Instance.enemyMaxHp *= 1.2f;
        }

        if(DataManager.Instance.Wave % 5 == 0)
        {
            DataManager.Instance.enemyInterval *= 0.8f;
        }

        
        count = 0;
        mobcount = 5 + (DataManager.Instance.Wave * 3);
        InvokeRepeating("Create",1f,DataManager.Instance.enemyInterval);
    }

    void Create()
    {
        
        count++;

        float r = Random.Range(limitMin.y, limitMax.y);
        Vector2 creatingPoint = new Vector2(limitMin.x, r);

        Instantiate(prefabEnemy, creatingPoint, Quaternion.identity);  
         
        isStart = true;
        if(count >= mobcount)
        {
            isCreate = false;
            CancelInvoke("Create");
            //DataManager.Instance.isWave = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(limitMin, limitMax);
    }
}

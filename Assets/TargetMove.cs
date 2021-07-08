using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TargetMove : MonoBehaviour
{
    public GameObject granadePrefab;
    Player_char player;
    private Vector2 point;
   
    private bool isLock = false;

    Transform tr;
    Transform playertr;

    McCreeSkill mc;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();
        
        playertr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        mc = GameObject.FindWithTag("Player").GetComponent<McCreeSkill>();


        point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.x -= tr.transform.position.x;
        point.y -= tr.transform.position.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        TargetMoving();
        LockOn();
    }

    void TargetMoving()
    {
        if(Input.GetMouseButton(0) && !isLock)
        {
            if(EventSystem.current.IsPointerOverGameObject() == false)
            {
                point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //point.x -= tr.transform.position.x;
                //point.y -= tr.transform.position.y;
                tr.position = point;
            }
        }
    }

    void MakeGranade()
    {
        Instantiate(granadePrefab, playertr.position, Quaternion.identity);
    }

    
    void LockOn()
    {
        if(Input.GetMouseButtonUp(0) && !isLock) // 타겟 완료시
        {
            isLock = true;
            Invoke("MakeGranade", 0.1f);
            player.isSkill = false;
            
            mc.SKill2CoolDown();
            Destroy(gameObject, 0.2f);
            
        }

        
    }
}

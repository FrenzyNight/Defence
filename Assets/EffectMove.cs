using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EffectMove : MonoBehaviour
{
    public GameObject russianPrefab;
    Transform tr;
    Vector2 point;
    private float angle;
    bool isLock = false;
    McCreeSkill mc;
    Player_char player;


    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();
        
        mc = GameObject.FindWithTag("Player").GetComponent<McCreeSkill>();

    }

    // Update is called once per frame
    void Update()
    {
        
        //tr.eulerAngles = new Vector3(0,0,angle);
        EffectRotation();
        LockOn();
    
    }

    void EffectRotation()
    {
        if(Input.GetMouseButton(0) && !isLock)
        {
            if(EventSystem.current.IsPointerOverGameObject() == false)
            {
                point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                point.x -= tr.transform.position.x;
                point.y -= tr.transform.position.y;
                angle = Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg;
                tr.eulerAngles = new Vector3(0,0,angle);
            }
        }
    }

    void LockOn()
    {
        if(Input.GetMouseButtonUp(0) && !isLock) // 타겟 완료시
        {
            isLock = true;
            Invoke("MakeRussian", 0.1f);
            player.isSkill = false;
            
            mc.SKill3CoolDown();
            Destroy(gameObject, 0.3f);
            
        }
    }

    void MakeRussian()
    {
        Instantiate(russianPrefab, playertr.position, Quaternion.identity);
    }
}

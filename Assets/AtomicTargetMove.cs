using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AtomicTargetMove : MonoBehaviour
{
    public GameObject atomicPrefab;
    Player_char player;
    Vector2 point;
    Transform tr;
    bool isLock = false;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();
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

    void LockOn()
    {
        if(Input.GetMouseButtonUp(0) && !isLock) // 타겟 완료시
        {
            isLock = true;
            Instantiate(atomicPrefab,new Vector2(tr.position.x, tr.position.y + 20f), Quaternion.identity);
            player.isSkill = false;
            
            Destroy(gameObject, 0.5f);
            
        }
    }
}

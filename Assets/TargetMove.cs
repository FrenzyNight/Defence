using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetMove : MonoBehaviour
{
    public GameObject granadePrefab;
    private Vector2 point;

    private bool des = false;
    private bool isLock = false;

    Transform tr;
    Transform castle;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        castle = GameObject.Find("Castle").GetComponent<Transform>();
        point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.x -= tr.transform.position.x;
        point.y -= tr.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && !isLock) // 타겟 완료시
        {
            isLock = true;
            StartCoroutine(Delay());
        }
        TargetMoving();
        if(des)
        {
            Targeting();
        }
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

    IEnumerator Delay()
    {
        Instantiate(granadePrefab, castle.position, Quaternion.identity);
        yield return new WaitForSeconds(4f);
        des = true;
    }

    void Targeting()
    {
        
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TargetMove : MonoBehaviour
{
    public GameObject granadePrefab;
    public GameObject Player;
    private Vector2 point;
    public Button SkillButton;

    private GameObject SkillManager;

    private bool des = false;
    private bool isLock = false;

    Transform tr;
    Transform castle;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        SkillManager = GameObject.Find("SkillManager");
        castle = GameObject.Find("Castle").GetComponent<Transform>();
        point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.x -= tr.transform.position.x;
        point.y -= tr.transform.position.y;
        Player = GameObject.Find("Castle");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && !isLock) // 타겟 완료시
        {
            isLock = true;
            StartCoroutine(Delay());
            Player.GetComponent<Castle>().isDelay = false;
            SkillManager.GetComponent<SkillSet>().StartCooltime();

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
            //if(EventSystem.current.IsPointerOverGameObject() == false)
            //{
                point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                tr.position = point;
            //}
        }
    }

    IEnumerator Delay()
    {
        Instantiate(granadePrefab, castle.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        des = true;
    }

    

    void Targeting()
    {

        Destroy(gameObject);
    }
}

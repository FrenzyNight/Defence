using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSet : MonoBehaviour
{
    public GameObject Target;
    public GameObject Player;
    //public GameObject SkillButton1;

    public Vector2 mousePosition;

    public bool isReady = false;
    public bool isCool = false;

    //public bool iscooltime = false;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Castle");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isReady)
        {
            MakeTarget();
        }

        if(!isCool && DataManager.Instance.isWave)
        {
            GameObject.Find("Button_skill1").GetComponent<Button>().interactable = true;
        }
    }

    public void ReadyToMakeTarget()
    {
        isReady = true;
        isCool = true;
        Player.GetComponent<Castle>().isDelay = true;
    }

    void MakeTarget()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject() == false)
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(Target, mousePosition , Quaternion.identity);
                isReady = false;
                
            }
        }
    }

    public void StartCooltime()
    {
        StartCoroutine(Cooltime());
    }

    IEnumerator Cooltime()
    {
        yield return new WaitForSeconds(DataManager.Instance.skill1cooltime);
        //GameObject.Find("Button_skill1").GetComponent<Button>().interactable = true;
        //SkillButton1.GetComponent<Button>().interactable = true;
        isCool = false;
    }
    
}

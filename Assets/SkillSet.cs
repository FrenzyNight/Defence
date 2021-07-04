using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSet : MonoBehaviour
{
    public GameObject Target;

    public Vector2 mousePosition;

    public bool isReady = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isReady)
        {
            MakeTarget();
        }
    }

    public void ReadyToMakeTarget()
    {
        isReady = true;
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

    
}

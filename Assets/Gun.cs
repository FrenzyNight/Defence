using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour
{

    public Vector2 mousePosition;

    Transform tr;
    public GameObject prefabBullet;

    public bool isDelay = false;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(Input.GetMouseButton(0) && DataManager.Instance.isWave)
        {
            if(EventSystem.current.IsPointerOverGameObject() == false)
            {
                if(!isDelay && !DataManager.Instance.isStop)
                {
                    
                    isDelay = true;
                    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Instantiate(prefabBullet, tr.position, Quaternion.identity);
            
                    StartCoroutine(Delay());
                }
            }
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.05f + 1/DataManager.Instance.bulletDelay);
        isDelay = false;
    }
}

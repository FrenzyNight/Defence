using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private Vector2 point;
    
    Transform tr;
    

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.x -= tr.transform.position.x;
        point.y -= tr.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        tr.Translate(point.normalized * DataManager.Instance.bulletSpeed * Time.deltaTime);
        //transform.position = Vector2.MoveTowards(transform.position, point, Time.deltaTime * DataManager.Instance.bulletSpeed);
    }

    

}

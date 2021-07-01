using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 터치한 곳을 월드 좌표로 받아옴
            Ray2D ray = new Ray2D(wp, Vector2.zero); //레이 기준 설정

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction); // 발사한 레이에 대한 반환값

            if(hit.collider != null) //콜라이더면
            {
                if(hit.collider.tag == "enemy")
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}

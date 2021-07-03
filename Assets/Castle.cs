using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Castle : MonoBehaviour
{
    
    public Vector2 mousePosition;

    Transform tr;
    public GameObject prefabBullet;

    private bool isDelay = false;

    // Start is called before the first frame update
    void Start()
    {
        
        tr = GetComponent<Transform>();

        

    }

    // Update is called once per frame
    void Update()
    {
        if(DataManager.Instance.NowHp <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
       Fire();
    }

    void CreateBullet()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(prefabBullet, tr.position, Quaternion.identity);
        
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.05f + 1/DataManager.Instance.bulletDelay);
        isDelay = false;
    }

    void Fire()
    {
        if(Input.GetMouseButton(0) && DataManager.Instance.isWave)
        {
            if(EventSystem.current.IsPointerOverGameObject() == false)
            {
                if(!isDelay)
                {
                    isDelay = true;
                    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Instantiate(prefabBullet, tr.position, Quaternion.identity);
            
                    StartCoroutine(Delay());
                }
            }
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            DataManager.Instance.NowHp -= DataManager.Instance.enemyDamge;
            //GuageBar.fillAmount -= Damge / MaxHp; //게이지를 데미지만큼 감소
            Destroy(collision.gameObject);
        }
    }
    */

}

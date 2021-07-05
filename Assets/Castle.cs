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

    public bool isDelay = false;

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

        if(DataManager.Instance.NowExp >= DataManager.Instance.MaxExp)
        {
            LevelUP();
        }
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

    void LevelUP()
    {
        DataManager.Instance.MaxHp *= 1.2f;
        DataManager.Instance.NowHp = DataManager.Instance.MaxHp;

        DataManager.Instance.bulletDamge *= 1.15f;
        DataManager.Instance.bulletDelay *= 1.05f;

        DataManager.Instance.Level += 1;
        DataManager.Instance.NowExp -= DataManager.Instance.MaxExp;
        DataManager.Instance.MaxExp *= 1.5f;
    }

}

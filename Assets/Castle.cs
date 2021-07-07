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

    public GameObject LevelUpPanel;


    public GameObject[] ButtonPrefabs;
    private int RandomIndex;

    private GameObject button1;


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
        if(DataManager.Instance.Level < DataManager.Instance.MaxLevel)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().GameStopMenu();
            LevelUpPanel.SetActive(true);
            LevelUpPanel.transform.SetAsLastSibling();

            RandomIndex = Random.Range(0,5);
            button1 = Instantiate(ButtonPrefabs[RandomIndex], LevelUpPanel.GetComponent<Transform>().position, Quaternion.identity, GameObject.Find("Panel_levelUP").transform);  
            button1.GetComponent<Transform>().Translate(new Vector2(0,150f));

            RandomIndex = Random.Range(0,5);
            button1 = Instantiate(ButtonPrefabs[RandomIndex], LevelUpPanel.GetComponent<Transform>().position, Quaternion.identity, GameObject.Find("Panel_levelUP").transform);  
            button1.GetComponent<Transform>().Translate(new Vector2(0,-150f));

            RandomIndex = Random.Range(0,5);
            button1 = Instantiate(ButtonPrefabs[RandomIndex], LevelUpPanel.GetComponent<Transform>().position, Quaternion.identity, GameObject.Find("Panel_levelUP").transform);  
            //button1.GetComponent<Transform>().Translate(new Vector2(0,200f));

            DataManager.Instance.Level += 1;
            DataManager.Instance.NowExp -= DataManager.Instance.MaxExp;
            DataManager.Instance.MaxExp *= 1.2f;
        }
    }

}

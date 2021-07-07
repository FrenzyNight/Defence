using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpEffects : MonoBehaviour
{
    private Button btn;

    public GameObject GunPrefab;

    
 
    // Start is called before the first frame update
    void Awake()
    {
        btn = GetComponent<Button>();
        if(gameObject.CompareTag("Btn_damgeup"))
        {
            btn.onClick.AddListener(DamgeUp);
        }
        else if(gameObject.CompareTag("Btn_fullhp"))
        {
            btn.onClick.AddListener(FullHealth);
        }
        else if(gameObject.CompareTag("Btn_hpup"))
        {
            btn.onClick.AddListener(MaxHpUp);
        }
        else if(gameObject.CompareTag("Btn_shotspeedup"))
        {
            btn.onClick.AddListener(ShotSpeedUP);
        }
        else if(gameObject.CompareTag("Btn_addgun"))
        {
            btn.onClick.AddListener(AddGun);
        }

    }

    
    void OffPanel()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().BackToGame();
        GameObject.Find("Panel_levelUP").SetActive(false);
    }

    public void MaxHpUp()
    {
        DataManager.Instance.MaxHp += 50;
        DataManager.Instance.NowHp += 50;


        OffPanel();
    }

    public void DamgeUp()
    {
        DataManager.Instance.bulletDamge += 1;
        DataManager.Instance.NowHp += 30;
        if(DataManager.Instance.NowHp > DataManager.Instance.MaxHp)
        {
            DataManager.Instance.NowHp = DataManager.Instance.MaxHp;
        }

        OffPanel();
    }

    public void ShotSpeedUP()
    {
        DataManager.Instance.bulletDelay += 1;
        DataManager.Instance.NowHp += 30;
        if(DataManager.Instance.NowHp > DataManager.Instance.MaxHp)
        {
            DataManager.Instance.NowHp = DataManager.Instance.MaxHp;
        }

        OffPanel();
    }

    public void FullHealth()
    {
        DataManager.Instance.NowHp = DataManager.Instance.MaxHp;
    
        OffPanel();
    }

    void AddGun()
    {
        Transform tr;
        tr = GameObject.Find("Castle").GetComponent<Transform>();
        GameObject gun = Instantiate(GunPrefab, tr.position, Quaternion.identity);
        float x, y;
        x = Random.Range(-2f, 3f);
        y = Random.Range(-2f, 3f);
        gun.GetComponent<Transform>().Translate(new Vector2(x, y));

        DataManager.Instance.NowHp += 30;
        if(DataManager.Instance.NowHp > DataManager.Instance.MaxHp)
        {
            DataManager.Instance.NowHp = DataManager.Instance.MaxHp;
        }

        OffPanel();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpEffects : MonoBehaviour
{
    private Button btn;
 
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
        DataManager.Instance.NowHp += 50;

        OffPanel();
    }

    public void ShotSpeedUP()
    {
        DataManager.Instance.bulletDelay += 1;
        DataManager.Instance.NowHp += 50;

        OffPanel();
    }

    public void FullHealth()
    {
        DataManager.Instance.NowHp = DataManager.Instance.MaxHp;
    
        OffPanel();
    }
}

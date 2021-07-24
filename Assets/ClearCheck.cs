using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClearCheck : MonoBehaviour
{
    public GameObject ClearPanel;
    public GameObject RebornPanel;
    public Text clearTitle;
    public Text getExpAndMoney;
    WaveManager wm;

    private int DeathCount;
    Player_char player;

    // Start is called before the first frame update
    void Start()
    {
        wm = gameObject.GetComponent<WaveManager>();
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clear()
    {
        ClearPanel.SetActive(true);
        wm.GameStopMenu();
        clearTitle.text = "Misson Clear!";
        getExpAndMoney.text = "획득 경험치 & money: " + (wm.wave*10).ToString() + "\n";
        
        DataManager.Instance.money += wm.wave*10;
        DataManager.Instance.exp += wm.wave*10;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Failed()
    {

        if(DeathCount != 0)
        {
            ClearPanel.SetActive(true);
            wm.GameStopMenu();
            clearTitle.text = "Misson Failed!";
            getExpAndMoney.text = "획득 경험치 & money: " + (wm.wave*10).ToString() + "\n";
            
            DataManager.Instance.money += wm.wave*10;
            DataManager.Instance.exp += wm.wave*10;
        }
        else if(DeathCount == 0)
        {
            RebornPanel.SetActive(true);
            wm.GameStopMenu();
            DeathCount+=1;
        }
    }

    public void Reborn()
    {
        player.nowHp = player.maxHp;
        wm.BackToGame();
        player.isDead = false;
    }
}

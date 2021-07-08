using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
   
    public bool isWave;
    public bool isStop;

    public int wave;
    

    public Image HpBar;
    public Image ExpBar;
    
    public Text HpText;
    public Text moneyText;
    public Text waveText;
    public Text expText;
    public Text LvText;

    Player_char player;
    WaveManager wm;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();
        

        HpBar.fillAmount = 1;
        ExpBar.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        HpBar.fillAmount = player.nowHp / player.maxHp;

        HpText.text = "HP : " + player.nowHp.ToString("0.#") + " / " + player.maxHp.ToString("0.#");
        moneyText.text = "Money : " + DataManager.Instance.money.ToString();
        waveText.text = "Wave : " + wave.ToString();

        LvText.text = "Lv. " + player.level.ToString();
        expText.text = "Exp : " + player.nowExp.ToString("0.#") + " / " + player.maxExp.ToString("0.#");
        ExpBar.fillAmount = player.nowExp / player.maxExp;
    }

    public void GameStopMenu()
    {
        isStop = true;
        Time.timeScale = 0;
    }

    public void BackToGame()
    {
        isStop = false;
        Time.timeScale = 1;
    }
}

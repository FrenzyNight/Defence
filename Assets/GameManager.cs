using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text HpText;
    public Image GuageBar;
    public Image ExpBar;

    public Text scoreText;
    public Text waveText;
    public Text expText;
    public Text LvText;

    // Start is called before the first frame update
    void Start()
    {
        GuageBar = GameObject.Find("HpGuage").GetComponent<Image>(); //이미지 컴포넌트 받아옴
        GuageBar.fillAmount = 1; //시작시 체력게이지 최대
        ExpBar = GameObject.Find("ExpBar").GetComponent<Image>();
        ExpBar.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GuageBar.fillAmount = DataManager.Instance.NowHp / DataManager.Instance.MaxHp;
        ExpBar.fillAmount = DataManager.Instance.NowExp / DataManager.Instance.MaxExp;
        

        HpText.text = "HP : " + DataManager.Instance.NowHp.ToString("0.#") + " / " + DataManager.Instance.MaxHp.ToString("0.#");
        scoreText.text = "Money : " + DataManager.Instance.money.ToString();
        waveText.text = "Wave : " + DataManager.Instance.Wave.ToString();
        LvText.text = "Lv. " + DataManager.Instance.Level.ToString();
        expText.text = "Exp : " + DataManager.Instance.NowExp.ToString("0.#") + " / " + DataManager.Instance.MaxExp.ToString("0.#");
    }

    public void GameStopMenu()
    {
        DataManager.Instance.isStop = true;
        Time.timeScale = 0;
    }

    public void BackToGame()
    {
        DataManager.Instance.isStop = false;
        Time.timeScale = 1;
    }
}

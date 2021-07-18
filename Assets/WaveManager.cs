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

    public Vector2 PlayerSponPoint;
    public GameObject PlayerCharPrefab;
    public GameObject[] mercenary; //용병단
    
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(PlayerCharPrefab, PlayerSponPoint, Quaternion.identity);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(PlayerSponPoint, 0.2f);
    }

    public void CallMercenary()
    {
        int merNum = mercenary.Length;
        int merInd = Random.Range(0,merNum);

        float rndX = Random.Range(0.5f, 1.5f);
        float rndY = Random.Range(1f, 3.5f);

        int rndXpm = Random.Range(0,2);
        int rndYpm = Random.Range(0,2);

        if(rndXpm == 0)
            rndX *= (-1f);
        

        if(rndYpm == 0)
            rndY *= (-1f);

        Instantiate(mercenary[merInd], PlayerSponPoint + new Vector2(rndX,rndY), Quaternion.identity);
    }
}

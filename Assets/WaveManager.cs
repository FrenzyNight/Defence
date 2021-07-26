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
    public Text CostText;

    Player_char player;
    WaveManager wm;

    public Vector2 PlayerSponPoint;
    GameObject PlayerCharPrefab;

    //용병단 뽑기

    public int Coin;
    public int Cost;

    
    // Start is called before the first frame update

    void Awake()
    {
        PlayerCharPrefab = DataManager.Instance.Characters[DataManager.Instance.selectedChar];
        Instantiate(PlayerCharPrefab, PlayerSponPoint, Quaternion.identity);
    }
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
        moneyText.text = "Coin : " + Coin.ToString();
        waveText.text = "Wave : " + wave.ToString();

        LvText.text = "Lv. " + player.level.ToString();
        expText.text = "Exp : " + player.nowExp.ToString("0.#") + " / " + player.maxExp.ToString("0.#");
        ExpBar.fillAmount = player.nowExp / player.maxExp;
        CostText.text = "Cost : " + Cost.ToString();

        
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
   
}

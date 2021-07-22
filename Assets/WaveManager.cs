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
    public Button CallBtn;
    public GameObject[] mercenary; //용병단
    public int resetNum;
    public Text resetNumText;

    public int Coin;
    public int Cost;

    public Button MerBtn1, MerBtn2, MerBtn3; 
    int idx1, idx2, idx3;
    
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
        CallBtn.interactable = false;

        ButtonMapping();
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
        resetNumText.text = "ResetNum : " + resetNum.ToString();

        CanCallCheck();
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

    void CanCallCheck()
    {
        if(Coin >= Cost)
        {
            CallBtn.interactable = true;
        }
    }

    public void SponMercenary(int idx)
    {
        if(Coin >= Cost)
        {
            float rndX = Random.Range(0.5f, 1.5f);
            float rndY = Random.Range(1f, 3.5f);

            int rndXpm = Random.Range(0,2);
            int rndYpm = Random.Range(0,2);

            if(rndXpm == 0)
                rndX *= (-1f);
            

            if(rndYpm == 0)
                rndY *= (-1f);

            Instantiate(mercenary[idx], PlayerSponPoint + new Vector2(rndX,rndY), Quaternion.identity);
            CallBtn.interactable = false;

            Coin -= Cost;
            Cost += 25;
        }
    }

    void ButtonMapping()
    {
        MerBtn1.onClick.AddListener(() => SponMercenary(idx1));
        MerBtn2.onClick.AddListener(() => SponMercenary(idx2));
        MerBtn3.onClick.AddListener(() => SponMercenary(idx3));
    }

    public void RandomMer()
    {
        int merNum = mercenary.Length;
        idx1 = Random.Range(0,merNum);
        idx2 = Random.Range(0,merNum);
        idx3 = Random.Range(0,merNum);

        MerBtn1.GetComponent<Image>().sprite = mercenary[idx1].GetComponent<SpriteRenderer>().sprite;
        MerBtn2.GetComponent<Image>().sprite = mercenary[idx2].GetComponent<SpriteRenderer>().sprite;
        MerBtn3.GetComponent<Image>().sprite = mercenary[idx3].GetComponent<SpriteRenderer>().sprite;
    }

    public void ResetMer()
    {
        if(resetNum > 0)
        {
            int merNum = mercenary.Length;

            idx1 = Random.Range(0,merNum);
            idx2 = Random.Range(0,merNum);
            idx3 = Random.Range(0,merNum);

            MerBtn1.GetComponent<Image>().sprite = mercenary[idx1].GetComponent<SpriteRenderer>().sprite;
            MerBtn2.GetComponent<Image>().sprite = mercenary[idx2].GetComponent<SpriteRenderer>().sprite;
            MerBtn3.GetComponent<Image>().sprite = mercenary[idx3].GetComponent<SpriteRenderer>().sprite;

            resetNum--;
            if(resetNum < 0)
                resetNum = 0;
        }
    }


    public void AdandplusResetNum()
    {
        resetNum++;
    }
}

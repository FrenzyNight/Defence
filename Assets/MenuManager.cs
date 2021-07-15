using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Text moneyText;
    public Text UpgradeText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money : " + DataManager.Instance.money.ToString();
        UpgradeText.text = "현재 성장률 : " + (4+DataManager.Instance.growRateUpgrade[0]).ToString() + "\n";
        UpgradeText.text += "업그레이드 비용 : " + DataManager.Instance.upgradecost[0].ToString();
    }

    public void UpgradeButton()
    {
        if(DataManager.Instance.money >= DataManager.Instance.upgradecost[0])
        {
            DataManager.Instance.growRateUpgrade[0] += 0.5f;
            DataManager.Instance.money -= DataManager.Instance.upgradecost[0];
            DataManager.Instance.upgradecost[0] *= 2;
        }
    }

    public void StartWave()
    {
        SceneManager.LoadScene("MainScene");
    }
}

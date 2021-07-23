using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Text moneyText;
    public Text UpgradeText;
    public Text selectText2;
    public Image SelecteCharImg;

    public Text CharNameText;
    

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = DataManager.Instance.money.ToString("#,##0");
        UpgradeText.text = "현재 성장률 : " + (4+DataManager.Instance.growRateUpgrade[DataManager.Instance.selectedChar]).ToString() + "\n";
        UpgradeText.text += "업그레이드 비용 : " + DataManager.Instance.upgradecost[DataManager.Instance.selectedChar].ToString();
        
        selectText2.text = "Select Char : " + DataManager.Instance.selectedChar.ToString();
    
        SelecteCharImg.sprite = DataManager.Instance.Characters[DataManager.Instance.selectedChar].GetComponent<SpriteRenderer>().sprite;
        CharNameText.text = DataManager.Instance.Characters[DataManager.Instance.selectedChar].GetComponent<Player_char>().charname;
    }

    public void UpgradeButton()
    {
        if(DataManager.Instance.money >= DataManager.Instance.upgradecost[DataManager.Instance.selectedChar])
        {
            DataManager.Instance.growRateUpgrade[DataManager.Instance.selectedChar] += 0.5f;
            DataManager.Instance.money -= DataManager.Instance.upgradecost[DataManager.Instance.selectedChar];
            DataManager.Instance.upgradecost[DataManager.Instance.selectedChar] *= 2;
        }
    }

    public void StartWave()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SelectPlus()
    {
        DataManager.Instance.selectedChar++;
        if(DataManager.Instance.selectedChar >= DataManager.Instance.Characters.Length)
            DataManager.Instance.selectedChar = 0;
    }

    public void SelectMinus()
    {
        DataManager.Instance.selectedChar--;
        if(DataManager.Instance.selectedChar < 0)
            DataManager.Instance.selectedChar = DataManager.Instance.Characters.Length-1;
    }
}

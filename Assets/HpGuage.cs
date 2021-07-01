using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpGuage : MonoBehaviour
{
    public Text HpText;
    public Image GuageBar;
    // Start is called before the first frame update
    void Start()
    {
        GuageBar = GameObject.Find("HpGuage").GetComponent<Image>(); //이미지 컴포넌트 받아옴
        GuageBar.fillAmount = 1; //시작시 체력게이지 최대
    }

    // Update is called once per frame
    void Update()
    {
        GuageBar.fillAmount = DataManager.Instance.NowHp / DataManager.Instance.MaxHp;
        HpText.text = "HP : " + DataManager.Instance.NowHp.ToString() + " / " + DataManager.Instance.MaxHp.ToString();
    }
}
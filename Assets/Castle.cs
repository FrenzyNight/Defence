using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Castle : MonoBehaviour
{
    private float MaxHp;
    private float Damge;
    public Image GuageBar;

    // Start is called before the first frame update
    void Start()
    {
        MaxHp = 10; // 최대 체력
        Damge = 1; // 몬스터 공격력
        GuageBar = GameObject.Find("HpGuage").GetComponent<Image>(); //이미지 컴포넌트 받아옴
        GuageBar.fillAmount = 1; //시작시 체력게이지 최대

    }

    // Update is called once per frame
    void Update()
    {
        if(GuageBar.fillAmount <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GuageBar.fillAmount -= Damge / MaxHp; //게이지를 데미지만큼 감소
        Destroy(collision.gameObject);
    }
}

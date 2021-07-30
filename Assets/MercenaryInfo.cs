using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercenaryInfo : MonoBehaviour
{
    public int step;
    public string MerName;

    public int locationNum;

    void Start()
    {
        stepcheck();
    }

    public void stepcheck() //2단계 (황금) 임시표시
    {
        if(step == 2)
            GetComponent<SpriteRenderer>().color = new Color(255,215,0,255);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MerManager : MonoBehaviour
{
    public Vector2[] MerSponPoint;
    [SerializeField] List<int> sponlist = new List<int>() {0,1,2,3,4,5,6,7,8,9,10,11,12,13};
    public int mercount;
    //용병단 뽑기
    public Button CallBtn;
    public GameObject[] mercenary; //용병단
    public int resetNum;
    public Text resetNumText;

    List<GameObject> merlist = new List<GameObject>();

    WaveManager wm;

    public Button MerBtn1, MerBtn2, MerBtn3; 
    int idx1, idx2, idx3;

    
    void Start()
    {
        SetMercenary();
        wm = GetComponent<WaveManager>();
        CallBtn.interactable = false;
        
        ButtonMapping();
    }

    // Update is called once per frame
    void Update()
    {
        CanCallCheck();
        resetNumText.text = "ResetNum : " + resetNum.ToString();
    }

    void SetMercenary()
    {
        GameObject[] RandomMer = (GameObject[])DataManager.Instance.MerList[DataManager.Instance.selectInventoryIndex].MerInventory.Clone();

        int idx;
        for(int i=0;i<5;i++)
        {
            idx = Random.Range(0,8-i);

            mercenary[i] = RandomMer[idx];

            RandomMer[idx] = RandomMer[7-i];

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for(int i = 0;i<MerSponPoint.Length;i++)
        {
            Gizmos.DrawSphere(MerSponPoint[i], 0.2f);
        }
        
    }

    void CanCallCheck()
    {
        if(wm.Coin >= wm.Cost && mercount != MerSponPoint.Length)
        {
            CallBtn.interactable = true;
        }
    }

    public void SponMercenary(int idx)
    {
        if(wm.Coin >= wm.Cost)
        {
            int rndidx = Random.Range(0,sponlist.Count); // 랜덤 스폰 위치

            int count = 0;
            foreach(var mer in merlist)
            {
                //소환되어 있는 1단계 용병 수 체크
                if(mer.GetComponent<MercenaryInfo>().MerName.Equals(mercenary[idx].GetComponent<MercenaryInfo>().MerName) && mer.GetComponent<MercenaryInfo>().step == 1)
                    count++;
            }

            //기존에 2마리 미만시 1단계 소환
            if(count!=2)
            {
                GameObject mer = Instantiate(mercenary[idx], MerSponPoint[sponlist[rndidx]], Quaternion.identity);
                CallBtn.interactable = false;

                mer.GetComponent<MercenaryInfo>().locationNum = sponlist[rndidx];

                merlist.Add(mer);
                sponlist.RemoveAt(rndidx);

                mercount+=1;
            }
            //기존에 2마리 있을시 기존 2명 삭제 후 2단계(황금) 용병 소환
            else if(count == 2)
            {
                foreach(var merc in merlist.ToList())
                {
                    if(merc.GetComponent<MercenaryInfo>().MerName.Equals(mercenary[idx].GetComponent<MercenaryInfo>().MerName) && merc.GetComponent<MercenaryInfo>().step == 1)
                    {
                        sponlist.Add(merc.GetComponent<MercenaryInfo>().locationNum);
                        merlist.Remove(merc);
                        Destroy(merc);
                        mercount-=1;
                    }
                }
                rndidx = Random.Range(0,sponlist.Count);

                GameObject mer = Instantiate(mercenary[idx], MerSponPoint[sponlist[rndidx]], Quaternion.identity);
                CallBtn.interactable = false;

                mer.GetComponent<MercenaryInfo>().step = 2;
                mer.GetComponent<MercenaryInfo>().locationNum = sponlist[rndidx];

                mer.GetComponent<MercenaryInfo>().stepcheck();

                merlist.Add(mer);
                sponlist.RemoveAt(rndidx);

                mercount+=1;
            }
            
            

            wm.Coin -= wm.Cost;
            wm.Cost += 25;
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

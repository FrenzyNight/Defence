using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerInventory : MonoBehaviour
{
    public static MerInventory Instance{get; private set;}

    public Button[] inventory;
    public GameObject[] selectedMer;
    public GameObject merc;

    public Button selectButton;
    public Image Infoimg;

    public int selectIndex;

    public GameObject info;

    public Button saveButton;
    public GameObject MerPanel;

    void Awake()
    {
        Instance = this;
        selectButton.onClick.AddListener(()=>SelectMercenary(merc));
        selectedMer = (GameObject[])DataManager.Instance.MerInventory.Clone();
    }

    // Update is called once per frame
    void Update()
    {
        SetInventory();
        CanSave();
    }

    void SetInventory()
    {
        for(int i=0;i<8;i++)
        {
            if(selectedMer[i] != null)
            {
                inventory[i].GetComponent<Image>().sprite = selectedMer[i].GetComponent<SpriteRenderer>().sprite;
            }
        }
    }

    public void ClickInventory(int idx)
    {
        selectIndex = idx;

        //open panel

        if(selectedMer[idx]!= null)
        {
            //용병 정보 표시
            info.SetActive(true);

            Infoimg.sprite = selectedMer[idx].GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void ClickMercenary(GameObject mer)
    {
        //용병 정보 표시
        info.SetActive(true);

        Infoimg.sprite = mer.GetComponent<SpriteRenderer>().sprite;

        merc = mer;
        
    }


    public void SelectMercenary(GameObject mer)
    {
        if(selectIndex != -1)
        {
            if(selectedMer[selectIndex] != mer && Array.IndexOf(selectedMer, mer) == -1)
            {
                selectedMer[selectIndex] = mer;
                selectIndex = -1;
                info.SetActive(false);
            }
        }
    }

    void CanSave()
    {
        bool isFull = true;

        for(int i=0;i<8;i++)
        {
            if(selectedMer[i] == null)
            {
                isFull = false;
            }
        }

        if(isFull)
            saveButton.gameObject.SetActive(true);
    }

    public void SaveInventory()
    {
        DataManager.Instance.MerInventory = (GameObject[])selectedMer.Clone();
        MerPanel.SetActive(false);
    }
}

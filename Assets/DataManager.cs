using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class MerInventoryList
{
    public GameObject[] MerInventory;
}

public class DataManager : MonoBehaviour
{
   static DataManager instance;
   public static DataManager Instance
   {
       get
       {
           return instance;
       }
   }

   private void Awake()
   {
       if(instance == null)
       {
           DontDestroyOnLoad(gameObject);
           instance = this;
       }
       else
       {
           Destroy(gameObject);
       }
   }

   
    public int money;

    public int selectedChar;

    public GameObject[] Characters;
    public float[] growRateUpgrade;

    public int[] upgradecost;

    public int playerLevel;

    public float exp;

    public int selectInventoryIndex;

    public MerInventoryList[] MerList;
}




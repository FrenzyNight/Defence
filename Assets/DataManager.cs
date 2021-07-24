using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

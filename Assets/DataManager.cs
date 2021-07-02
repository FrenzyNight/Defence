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

   public float bulletSpeed;
   public float bulletDamge;
   public float bulletDelay;
   
   public float enemyMaxHp;
   public float enemySpeed;
   public float enemyInterval;
   public float enemyDamge;

   public float MaxHp;
   public float NowHp;

   public int Wave;

   public bool isWave;

   public int money;
}

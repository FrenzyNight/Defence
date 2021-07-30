using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Turret_assist : MonoBehaviour
{
    public GameObject Bullet;
    public float FPS;

    WaveManager wm;

    public bool isDelay; //공격 후딜레이

    Transform tr;

    public AudioClip audioFire;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        tr = GetComponent<Transform>();

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioFire;
        Golden();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Golden()
    {
        if(GetComponent<MercenaryInfo>().step == 2)
            FPS *= 2;
    }

    void Fire()
    {
        if(Input.GetMouseButton(0) && wm.isWave)
        {
            if(EventSystem.current.IsPointerOverGameObject() == false)
            {
                if(!isDelay && !wm.isStop)
                {
                    
                    isDelay = true;
                    audioSource.Play();
                    Instantiate(Bullet, tr.position, Quaternion.identity);
            
                    StartCoroutine(Delay());
                }
            }
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1/FPS);
        isDelay = false;
    }

}

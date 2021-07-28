using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff2_floor : MonoBehaviour
{
    public GameObject debuff2effect;

    List<GameObject> effectlist = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Invoke("EndEffect", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            if(collision.gameObject.transform.Find("Debuff2_Effect(Clone)") == null)
            {
                Transform effectTr = collision.gameObject.GetComponent<Transform>();
                effectlist.Add(Instantiate(debuff2effect, effectTr.position, Quaternion.identity, collision.gameObject.transform));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            if(collision.gameObject.transform.Find("Debuff2_Effect(Clone)") != null)
            {
                Destroy(collision.gameObject.transform.Find("Debuff2_Effect(Clone)").gameObject);
            }
        }
    }

    void EndEffect()
    {
        foreach(GameObject debuff in effectlist)
        {
            if(debuff != null)
                debuff.GetComponent<Debuff2Effect>().EndEffect();
        }

        Destroy(gameObject);
    }
}

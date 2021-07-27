using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerButton : MonoBehaviour
{
    public GameObject mercenary;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().sprite = mercenary.GetComponent<SpriteRenderer>().sprite;

        gameObject.GetComponent<Button>().onClick.AddListener(()=>MerInventory.Instance.ClickMercenary(mercenary));
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealEffectMove : MonoBehaviour
{
    Transform tr;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();

        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        tr.Translate(Vector2.up * Time.deltaTime);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a-0.01f);
    }
}

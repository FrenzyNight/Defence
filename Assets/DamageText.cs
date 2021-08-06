using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    Transform tr;
    public float textMoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();

        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        tr.Translate(Vector2.up * textMoveSpeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    public GameObject prefabEnemy;
    public Vector2 limitMin;
    public Vector2 limitMax;

    private float delay;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Create());
    }

    IEnumerator Create()
    {
        while(true)
        {
            count++;

            float r = Random.Range(limitMin.y, limitMax.y);
            Vector2 creatingPoint = new Vector2(limitMin.x, r);

            Instantiate(prefabEnemy, creatingPoint, Quaternion.identity);
            
            delay = DataManager.Instance.enemyInterval/(count)  + 0.1f;

            yield return new WaitForSeconds(delay);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(limitMin, limitMax);
    }
}

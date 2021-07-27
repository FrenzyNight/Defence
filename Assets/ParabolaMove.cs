using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaMove : MonoBehaviour
{
    public Transform Target;
    public float Angle = 35.0f;
    private float correctAngle;

    public float gravity = 9.8f;
    public float granadeSpeed;

    Transform Projectile;
    //private Transform myTransform;

    private Vector2 vec;
    public GameObject explosionPrefab;
    WaveManager wm;
    
    void Awake()
    {
        Target = GameObject.FindWithTag("target").GetComponent<Transform>();
        Projectile = GetComponent<Transform>();

        vec = Target.position;

        Vector2 v = Target.position - Projectile.position;
        wm = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        correctAngle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

        //Angle += correctAngle;
       
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Parabola());
    }


    IEnumerator Parabola()
    {
        yield return new WaitForSeconds(0.08f);

        //Projectile.position = myTransform.position;
        

        float target_Distance = Vector2.Distance(Projectile.position, vec);

        float projectile_Velocity = target_Distance / (Mathf.Sin(2*Angle*Mathf.Deg2Rad) / gravity) ;

        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos((Angle + correctAngle) * Mathf.Deg2Rad);

        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin((Angle + correctAngle)  * Mathf.Deg2Rad);

        float flightDuration = target_Distance / Vx;

        float elapse_time = 0;

        

        while(Projectile.position.x <= vec.x)
        {
            if(!wm.isStop)
            {
                Projectile.Translate(new Vector2((Vx + (gravity * Mathf.Sin(correctAngle * Mathf.Deg2Rad) * elapse_time)), (Vy - (gravity * Mathf.Cos(correctAngle * Mathf.Deg2Rad) * elapse_time)))* Time.deltaTime * granadeSpeed);
                 
                elapse_time += Time.deltaTime * granadeSpeed ;

                yield return null;
            }
            else
                yield return null;
        }

        yield return new WaitForSeconds(0.03f);
        CinemachineShake.Instance.ShakeCamera(0.3f, 0.15f);
        Instantiate(explosionPrefab, Projectile.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

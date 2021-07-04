using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaMove : MonoBehaviour
{
    public Transform Target;
    public float Angle = 45.0f;

    public float gravity = 9.8f;

    public Transform Projectile;
    private Transform myTransform;


    void Awake()
    {
        Target = GameObject.FindWithTag("target").GetComponent<Transform>();
        myTransform = GetComponent<Transform>();
       
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Parabola());
    }


    IEnumerator Parabola()
    {
        yield return new WaitForSeconds(0.1f);

        Projectile.position = myTransform.position;

        float target_Distance = Vector2.Distance(Projectile.position, Target.position);

        float projectile_Velocity = target_Distance / (Mathf.Sin(2*Angle*Mathf.Deg2Rad) / gravity);

        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(Angle * Mathf.Deg2Rad);

        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(Angle * Mathf.Deg2Rad);


        float flightDuration = target_Distance / Vx;

        Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);

        float elapse_time = 0;

        while(elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx*Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}

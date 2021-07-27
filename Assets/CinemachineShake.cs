using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance {get; private set; }

    private CinemachineVirtualCamera cinVC;
    private float shakeTimer;
    void Awake()
    {
        Instance = this;
        cinVC = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinBMCP =
            cinVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinBMCP.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0f)
            {
                //time over
                CinemachineBasicMultiChannelPerlin cinBMCP =
                    cinVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinBMCP.m_AmplitudeGain = 0f;
            }

        }
    }
}

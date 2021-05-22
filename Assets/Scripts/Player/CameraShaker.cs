using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
    float shakeStartIntensity = 3;
    float shakeTimeTotal = 0;
    float shakeTime = 1;

    CinemachineVirtualCamera virtualCamera;
    CinemachineBasicMultiChannelPerlin cameraPerlin;
    public static CameraShaker instance;

    void Awake()
    {
        instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cameraPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        UpdateCameraShake();
    }

    void UpdateCameraShake()
    {
        if (shakeTime <= shakeTimeTotal)
        {
            // Update shake
            shakeTime = Mathf.Clamp(shakeTime + Time.deltaTime, 0, shakeTimeTotal);
            cameraPerlin.m_AmplitudeGain = Mathf.Lerp(shakeStartIntensity, 0.0f, shakeTime / shakeTimeTotal);
        }
        else
        {
            // Stop shake
            cameraPerlin.m_AmplitudeGain = 0;
        }
    }

    public void ShakeCamera(float shakeDuration = 0.1f)
    {
        shakeTimeTotal = shakeDuration;
        shakeTime = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Screenshake : MonoBehaviour
{
    public static Screenshake instance = null;

    [SerializeField] private CinemachineCameras cam;
    [SerializeField] private float durationOfScreenShake = .1f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        cam.basicMultiChannelPerlin.m_AmplitudeGain = 0;
        cam.basicMultiChannelPerlin.m_FrequencyGain = 0;
    }

    public void StartScreenShake()
    {
        StopAllCoroutines();
        StartCoroutine(TimedScreenShake());
    }

    private IEnumerator TimedScreenShake()
    {
        ChangeAmplitudeAndFrequency(10, 1);
        yield return new WaitForSeconds(durationOfScreenShake);
        ChangeAmplitudeAndFrequency(0, 0);
    }

    private void ChangeAmplitudeAndFrequency(float amplitude, float frequency)
    {
        cam.basicMultiChannelPerlin.m_AmplitudeGain = amplitude;
        cam.basicMultiChannelPerlin.m_FrequencyGain = frequency;
    }
}

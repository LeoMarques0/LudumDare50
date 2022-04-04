using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CinemachineCameras : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;
    [HideInInspector]
    public CinemachineBasicMultiChannelPerlin basicMultiChannelPerlin;

    private void Awake()
    {
        if(vCam == null)
            vCam = GetComponent<CinemachineVirtualCamera>();

        basicMultiChannelPerlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
}

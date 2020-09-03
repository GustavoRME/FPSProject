using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerComponent : MonoBehaviour
{    
    [SerializeField] private Camera _mainCamera = null;
    [SerializeField] private Camera _weaponCamera = null;

    [SerializeField] private CinemachineVirtualCamera _mainVirtualCamera = null;

    private CinemachineBasicMultiChannelPerlin _noise;

    private void Awake() 
    {
        _noise = _mainVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _noise.m_AmplitudeGain = 0.0f;
        _noise.m_FrequencyGain = 0.0f;
    } 
        
    public void ShakeCamera(float frequencyGain, float amplitudeGain)
    {
        _noise.m_FrequencyGain = frequencyGain;
        _noise.m_AmplitudeGain = amplitudeGain;

        StartCoroutine("Noise");
    }
    public Camera GetMainCamera() => _mainCamera;
    public Camera GetWeaponCamera() => _weaponCamera;    

    private IEnumerator Noise()
    {        
        yield return new WaitForSeconds(0.2f);

        _noise.m_FrequencyGain = 0.0f;
        _noise.m_AmplitudeGain = 0.0f;
    }
}

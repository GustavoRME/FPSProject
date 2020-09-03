using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepComponent : MonoBehaviour
{
    [SerializeField] private AudioSource _source = null;
    [SerializeField] private AudioClip[] _footStepClips = null;

    [Header("Pitch")]
    [SerializeField] private float _minPitch = 0.8f;
    [SerializeField] private float _maxPitch = 1.2f;

    public void PlayFootstep()
    {
        if(_source && _footStepClips.Length > 0)
        {
            _source.pitch = Random.Range(_minPitch, _maxPitch);
            _source.PlayOneShot(_footStepClips[Random.Range(0, _footStepClips.Length)]);            
        }
    }
}

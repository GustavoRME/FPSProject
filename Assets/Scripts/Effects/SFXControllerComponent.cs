using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXControllerComponent : MonoBehaviour
{
    [SerializeField] private AudioSource _sfxSource = null;

    [SerializeField] private int _poolSize = 10;

    private void Awake() => PoolSystem.InitiliazePool(_sfxSource, null, _poolSize);        
    
    public void PlaySFXSource(AudioClip clip, Vector3 hitPosition, Vector3 normal)
    {
        if(clip)
        {
            var source = PoolSystem.GetInstance<AudioSource>(_sfxSource);

            float pitch = Random.Range(0.8f, 1.2f);

            source.gameObject.transform.position = hitPosition;
            source.gameObject.transform.forward = normal;

            source.gameObject.SetActive(true);
            source.pitch = pitch;
            source.PlayOneShot(clip);
        }
    }
}

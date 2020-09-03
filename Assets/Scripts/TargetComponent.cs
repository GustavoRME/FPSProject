using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetComponent : MonoBehaviour
{
    public int life = 3;    
    public bool useParticlesOnDeath = true;        
    
    public ParticleSystem dieParticle = null;
    public AudioClip[] hitClips = null;
    public UnityEvent onDie = null;
    
    private AudioSource _source;
    private VFXControllerComponent _vfxController;
    private SFXControllerComponent _sFXController;

    private void Awake()
    {
        _source = GetComponentInChildren<AudioSource>();
        
        _vfxController = FindObjectOfType<VFXControllerComponent>();
        _sFXController = FindObjectOfType<SFXControllerComponent>();
    }

    public void TakeHit(int damage)
    {
        life -= damage;        

        //Playsound
        if(_source)
        {
            AudioClip clip = hitClips[Random.Range(0, hitClips.Length)];       
            
            if(clip)
                _source.PlayOneShot(clip);
        }

        if(life <= 0)
        {           
            if(useParticlesOnDeath)
                _vfxController.SpawnDeathVFX(dieParticle, transform.position + transform.up, transform.forward);

            PlaySound();
            
            onDie?.Invoke();
            
            Destroy(gameObject);
        }
    }
    
    private void PlaySound()
    {
        if(hitClips.Length > 0 && _sFXController)
        {
            AudioClip clip = hitClips[Random.Range(0, hitClips.Length)];
            _sFXController.PlaySFXSource(clip, transform.position, transform.forward);
        }
    }
}

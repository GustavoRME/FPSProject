using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXControllerComponent : MonoBehaviour
{            
    [Header("VFX Prefabs")]
    [SerializeField] private ParticleSystem _hitPrefabVFX = null;
    [SerializeField] private ParticleSystem _explositionVFX = null;
    [SerializeField] private ParticleSystem _deathDefaultPrefabVFX = null;
    [SerializeField] private LineRenderer _bulletTrailVFX = null;
    
    [Space]
    [SerializeField] private int _poolSize = 10;   

    private void Awake()
    {
        PoolSystem.InitiliazePool(_hitPrefabVFX, null, _poolSize);
        PoolSystem.InitiliazePool(_explositionVFX, null, _poolSize);
        PoolSystem.InitiliazePool(_deathDefaultPrefabVFX, null, _poolSize);
        PoolSystem.InitiliazePool(_bulletTrailVFX, null, _poolSize);
    }

    public void SpawnImpactHitVFX(Vector3 hitPosition, Vector3 normal)
    {
        var particle = PoolSystem.GetInstance<ParticleSystem>(_hitPrefabVFX);

        SpawnVFX(particle, hitPosition, normal);
    }
    
    public void SpawnExplositionVFX(Vector3 position, Vector3 rotation)
    {
        var particle = PoolSystem.GetInstance<ParticleSystem>(_explositionVFX);

        SpawnVFX(particle, position, rotation);
    }
   
    /// <summary>
    ///
    /// </summary>
    /// <param name="prefab">Wheather passed how null, will put default death VFX</param>
    /// <param name="hitPosition">Position to place the vfx</param>
    /// <param name="normal">Normal that forward will point</param>
    public void SpawnDeathVFX(ParticleSystem prefab, Vector3 hitPosition, Vector3 normal)
    {        
        var particle = PoolSystem.GetInstance<ParticleSystem>(prefab);

        SpawnVFX(particle, hitPosition, normal);
    }
    
    public void SpawnBulletTrailVFX(Vector3 start, Vector3 end)
    {
        var bulletTrail = PoolSystem.GetInstance<LineRenderer>(_bulletTrailVFX);

        bulletTrail.SetPosition(0, start);
        bulletTrail.SetPosition(1, end);

        bulletTrail.gameObject.SetActive(true);        
    }  
    
    private void SpawnVFX(ParticleSystem particle, Vector3 hitPosition, Vector3 normal)
    {
        if(particle)
        {
            particle.transform.position = hitPosition;
            particle.transform.forward = normal;

            particle.gameObject.SetActive(true);
            particle.Play();
        }
    }
}

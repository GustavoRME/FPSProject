using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ProjectilComponent : MonoBehaviour
{
    [SerializeField] private float _explositionTime = 1.0f;
    [SerializeField] private float _explositionRange = 5.0f;

    private int _damage;

    private Rigidbody _rb;
    private Action<Vector3, Vector3> _vfxExplositionHandle;      //Used to handle the explosition VFX throught the weapon

    private void Awake() => _rb = GetComponent<Rigidbody>();
    private void OnEnable()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explositionRange);
    }
    
    //Launcher Projectil
    public void Launcher(Action<Vector3, Vector3> explosition, Vector3 startPosition, Quaternion startRotation, Vector3 startForward, float force, int damage)
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        transform.forward = startForward;

        _vfxExplositionHandle = explosition;
        _damage = damage;
        
        gameObject.SetActive(true);        

        _rb.AddForce(transform.forward * force);

        Invoke("Explode", _explositionTime);        
    }

    private void Explode()
    {
        Debug.Log("Exploded");

        RaycastSphere();

        //Spawn the explosition VFX through the weapon
        _vfxExplositionHandle?.Invoke(transform.position, transform.eulerAngles);
        
        gameObject.SetActive(false);
    }
    private void RaycastSphere() //Responsible for take all colliders inside the explosition and apply damage
    {
        var colliders = Physics.OverlapSphere(transform.position, _explositionRange, 9);

        foreach (Collider col in colliders)
        {
            if(col.TryGetComponent(out TargetComponent target))
                target.TakeHit(_damage);
        }
    }

}
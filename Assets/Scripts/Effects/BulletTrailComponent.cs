using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BulletTrailComponent : MonoBehaviour
{
    [SerializeField] private float _lifeTime = .3f;
    [SerializeField] private float _speed = 50.0f;

    private LineRenderer _renderer;
    private readonly Vector3[] _pos = new Vector3[2];
    private Vector3 _direction;

    private void Awake() => _renderer = GetComponent<LineRenderer>();
        
    private void OnEnable()
    {        
        _renderer.GetPositions(_pos);
        _direction = (_pos[1] - _pos[0]).normalized;

        Invoke("Disable", _lifeTime);        
    }
    private void Update()
    {
        float vel = _speed * Time.deltaTime;

        _pos[0] += _direction * vel;
        _pos[1] += _direction * vel;

        _renderer.SetPositions(_pos);        
    }

    private void Disable() => gameObject.SetActive(false);
}

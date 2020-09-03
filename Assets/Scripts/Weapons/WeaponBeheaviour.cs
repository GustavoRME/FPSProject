using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public abstract class WeaponBeheaviour : MonoBehaviour
{        
    [SerializeField] protected int _damage = 1;
    [SerializeField] protected float _fireSpeed = 1;
    [SerializeField] protected float _reloadSpeed = 1;

    [Header("Clip")]
    [SerializeField] protected int _startAmmo = 6;
    [SerializeField] protected int _clipSize = 6;

    [Space]
    [SerializeField] protected AmmoComponent _ammo = null;
    [SerializeField] protected Transform _muzzleEnd = null;
    
    [Header("Animations")]
    [SerializeField] protected WeaponAnimatorComponent _anim = null;
    [SerializeField] protected AnimationClip _fireAnimation = null;
    [SerializeField] protected AnimationClip _reloadAnimation = null;

    [Header("Events")]
    [Tooltip("Called only just reloaded. The first int is the clip ammo and the seconds is the ammo remaining")]
    [SerializeField] protected UnityEvent<int, int> _OnUseAmmo = null;
    [Tooltip("Called only just fired. The int parameter is the clip ammo remaining")]
    [SerializeField] protected UnityEvent<int> _OnUseClipAmmo = null;

    private Animator _animatorHandle;

    private bool _isTaked;
        
    public int ClipAmmo { get; protected set; }
    public virtual int Ammo => _ammo.Bullets;    
    
    protected virtual void Awake()
    {
        _animatorHandle = GetComponent<Animator>();
        ClipAmmo = _startAmmo;
        gameObject.SetActive(_isTaked);       
    }    
    public virtual void Take()
    {
        if(!_isTaked)
        {
            _isTaked = true;
            gameObject.SetActive(true);
            _anim.TakeAnimation(_animatorHandle, _fireAnimation, _reloadAnimation, _fireSpeed, _reloadSpeed);
            _OnUseClipAmmo?.Invoke(ClipAmmo);
        }
    }
    public virtual void Retain()
    {
        if(_isTaked)
        {
            _isTaked = false;
            gameObject.SetActive(false);
        }
    }
    public virtual void Fire()
    {
        _anim.FireAnimation();        
        _OnUseClipAmmo?.Invoke(ClipAmmo);
    }
    public virtual void Reload()
    {
        _anim.ReloadAnimation();
        _OnUseAmmo?.Invoke(ClipAmmo, Ammo);
        _OnUseClipAmmo?.Invoke(ClipAmmo);
    }         
    protected Vector3 GetMuzzleEndPosition(Camera mainCamera, Camera weaponCamera)
    {
        Vector3 muzzle = weaponCamera.WorldToScreenPoint(_muzzleEnd.position);
        return mainCamera.ScreenToWorldPoint(muzzle);
    }
    protected bool CanFire() => ClipAmmo > 0;
    protected bool CanReloadBullets() => ClipAmmo < _clipSize && _ammo.Bullets > 0;
    protected bool CanReloadPills() => ClipAmmo < _clipSize && _ammo.Pills > 0;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class WeaponComponent : WeaponBeheaviour
{   
    [Serializable]
    private class AdvancedSettings
    {
        private enum SprayType
        {
            Single,
            Automatic
        }

        [SerializeField] private SprayType spray = SprayType.Single;
        
        public float spreadAngle = 0.0f;

        [Header("Camera Noise")]
        public float frequencyGain = 0.0f;
        public float amplitudeGain = 0.0f;

        public bool IsSingle() => spray == SprayType.Single;
        public bool IsAutomatic() => spray == SprayType.Automatic;
    }

    [SerializeField] private AdvancedSettings _advancedSettings = null;

    [Header("Audios")]
    [SerializeField] private AudioClip _bulletImpactClip = null;            
    
    //Controllers
    private CameraControllerComponent _cameraController;
    private VFXControllerComponent _vfxController;
    private SFXControllerComponent _sfxController;

    protected override void Awake()
    {       
        base.Awake();        
        
        _cameraController = FindObjectOfType<CameraControllerComponent>();
        _vfxController = FindObjectOfType<VFXControllerComponent>();
        _sfxController = FindObjectOfType<SFXControllerComponent>();

        enabled = false;
    }
    private void Update()
    {
        if (CanFire() && _anim.CanReloadOrFire())
        {
            ClipAmmo--;
            
            RaycastFire();
            _cameraController.ShakeCamera(_advancedSettings.frequencyGain, _advancedSettings.amplitudeGain);

            base.Fire();
        }
        else if (!CanFire())
            Reload();
    }

    public override void Fire()
    {
        if(_advancedSettings.IsSingle())
        {
            if (CanFire() && _anim.CanReloadOrFire())
            {
                ClipAmmo--;
                
                RaycastFire();
                _cameraController.ShakeCamera(_advancedSettings.frequencyGain, _advancedSettings.amplitudeGain);

                base.Fire();
            }
            else if (!CanFire())
                Reload();
        }
        else if(_advancedSettings.IsAutomatic())
        {
            enabled = !enabled;
        }

    }
    public override void Reload()
    {
        if(CanReloadBullets() && _anim.CanReloadOrFire())
        {
            int bullets = Mathf.Clamp(_clipSize - ClipAmmo, 0, _ammo.Bullets);

            ClipAmmo += bullets;

            _ammo.DecreaseBullets(bullets);

            base.Reload();
        }
    }

    private void RaycastFire()
    {
        float spreadRatio = _advancedSettings.spreadAngle / Camera.main.fieldOfView;
        
        Vector3 spread = spreadRatio * Random.insideUnitCircle;

        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * .5f + spread);

        if(Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue))
        {
            //Spawn Bullet Impact
            _vfxController.SpawnImpactHitVFX(hitInfo.point, hitInfo.normal);

            //Play impact sound
            _sfxController.PlaySFXSource(_bulletImpactClip, hitInfo.point, hitInfo.normal);
            
            //Check if hit enemy
            if (hitInfo.collider.gameObject.layer == 10)
                hitInfo.collider.GetComponent<TargetComponent>().TakeHit(_damage);                
        }

        //Spawn Bullet Trail
        _vfxController.SpawnBulletTrailVFX(GetMuzzleEndPosition(_cameraController.GetMainCamera(), _cameraController.GetWeaponCamera()), hitInfo.point);
    }        
}

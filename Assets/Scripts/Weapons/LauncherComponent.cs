using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherComponent : WeaponBeheaviour
{
    [Header("Projectil")]
    [SerializeField] private ProjectilComponent _projectilPrefab = null;
    [SerializeField] private float _throwForce = 200f;
   
    private VFXControllerComponent _vfxController;
    private CameraControllerComponent _cameraController;
    private WeaponHolderComponent _weaponHolderController;

    //Used to pass to projectil the forward from player
    private Transform _playerForward;

    public override int Ammo => _ammo.Pills;

    protected override void Awake()
    {
        //Initiliaze projectil pool
        PoolSystem.InitiliazePool(_projectilPrefab, null, 1);

        //Get controller components from scene
        _vfxController = FindObjectOfType<VFXControllerComponent>();
        _cameraController = FindObjectOfType<CameraControllerComponent>();
        _weaponHolderController = FindObjectOfType<WeaponHolderComponent>();

        //Get the player transform from the root of this object
        _playerForward = transform.root;

        //Stop update method
        enabled = false;

        base.Awake();        
    }
    private void LateUpdate()
    {
        //Check if can reload. It's mean we don't doing the reload or firing animation.
        if(_anim.CanReloadOrFire())
        {
            if(CanReloadPills())
            {
                //Wheather have pills to reload, do it.
                Reload();

                print("Reloaded");
            }
            else
            {
                //Return to previous weapon if don't have more pills to take from ammo.
                _weaponHolderController.NumberKeySwitch(1);

                print("Switched");
            }
            
            //Stop update
            enabled = false;
        }
    }

    public override void Fire()
    {
        if (CanFire() && _anim.CanReloadOrFire())
        {
            var projectil = PoolSystem.GetInstance<ProjectilComponent>(_projectilPrefab);
            
            Vector3 startPosition = GetMuzzleEndPosition(_cameraController.GetMainCamera(), _cameraController.GetWeaponCamera());
            
            projectil.Launcher(ExplositionVFX, startPosition, _muzzleEnd.rotation, _playerForward.forward, _throwForce, _damage);

            ClipAmmo--;

            base.Fire();
        }

        //Start call update method
        enabled = true;
    }
    public override void Take()
    {
        //Only take if can fire
        if(CanFire())
            base.Take();
    }
    public override void Reload()
    {        
        //Take pill from ammo
        int pills = Mathf.Clamp(_clipSize - ClipAmmo, 1, _clipSize);

        ClipAmmo += pills;

        //Decrease from ammo
        _ammo.DecreaseThrows(pills);

        //Play animation
        _anim.ReloadAnimation();

        //Event
        _OnUseAmmo?.Invoke(ClipAmmo, _ammo.Pills);
    }

    //Spawn throught weapon the vfx explosition given position and rotation from projectil
    private void ExplositionVFX(Vector3 position, Vector3 rotation) => _vfxController.SpawnExplositionVFX(position, rotation);    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponHolderComponent : MonoBehaviour
{
    [Tooltip("Put a number with represent the index on weapon array")]
    [SerializeField] private int _weaponToStart = 0;

    [Space]
    [SerializeField] private WeaponBeheaviour[] _weapons = null;

    [Space]
    [SerializeField] private UnityEvent<string, int , int> _OnChangeWeapon = null;

    private WeaponBeheaviour _currentWeapon = null;    

    private int _currentWeaponIndex;

    private void Start()
    {        
        _currentWeaponIndex = _weaponToStart;
        
        _currentWeapon = _weapons[_currentWeaponIndex];
        _currentWeapon.Take();

        _OnChangeWeapon?.Invoke(_currentWeapon.name, _currentWeapon.ClipAmmo, _currentWeapon.Ammo);        
    }

    public void ShootWithCurrentWeapon() => _currentWeapon.Fire();
    public void ReloadWithCurrentWeapon() => _currentWeapon.Reload();
    public void ScrollMouseSwitch(float scrollY)
    {
        //When used scroll we can get only two values: 120: this means his scrolling forward // -120: this means his scrolling downward                         
        int key = Mathf.FloorToInt(scrollY) > 0 ? -1 : 1;       
        int nextWeapon = Mathf.Clamp(_currentWeaponIndex + key, 0, _weapons.Length - 1);

        SwitchWeapon(nextWeapon);
    }
    public void NumberKeySwitch(float keyNumb)
    {
        if(keyNumb != _currentWeaponIndex)
            SwitchWeapon(Mathf.Clamp(Mathf.FloorToInt(keyNumb), 0, _weapons.Length - 1));
    }    

    private void SwitchWeapon(int nextWeapon)
    {
        //Retain the current Weapon
        _currentWeapon.Retain();

        //Take the next Weapon
        _currentWeaponIndex = nextWeapon;
        _currentWeapon = _weapons[_currentWeaponIndex];

        _currentWeapon.Take();

        _OnChangeWeapon?.Invoke(_currentWeapon.name, _currentWeapon.ClipAmmo, _currentWeapon.Ammo);
    }
}

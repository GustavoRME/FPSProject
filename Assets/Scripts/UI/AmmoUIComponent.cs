using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoUIComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _weaponText = null;
    [SerializeField] private TextMeshProUGUI _ammoText = null;

    private int _clipAmmo;
    private int _ammo;

    //Called by events
    public void OnSwitchWeapon(string name, int clipAmmo, int ammo)
    {
        _clipAmmo = clipAmmo;
        _ammo = ammo;

        WriteNameUI(name);
        WriteAmmoUI();
    }
    public void OnUseAmmo(int clipAmmo, int ammo)
    {
        _clipAmmo = clipAmmo;
        _ammo = ammo;
        WriteAmmoUI();        
    }
    public void OnUseClipAmmo(int clipAmmo)
    {
        _clipAmmo = clipAmmo;
        WriteAmmoUI();        
    }

    private void WriteNameUI(string name) => _weaponText.text = name;
    private void WriteAmmoUI() => _ammoText.text = _clipAmmo + "/" + _ammo;
    
}
